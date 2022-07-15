using System.Collections.Generic;

namespace KindergardenLib
{
    public class Course
    {
        private readonly string name;
        private readonly int minAgeInMonths;
        private readonly int maxAgeInMonths;
        private readonly int maxNumberOfStudents;
        private readonly int maxNumberOfTeachers;

        private float monthlyPrice;

        private readonly List<Teacher> assignedTeachers;
        private readonly List<Student> enrolledStudents;

        public Course(string name, int minAgeInMonths, int maxAgeInMonths, int maxNumberOfStudents,
            int maxNumberOfTeachers, float monthlyPrice)
        {
            this.name = name;
            this.minAgeInMonths = minAgeInMonths;
            this.maxAgeInMonths = maxAgeInMonths;
            this.maxNumberOfStudents = maxNumberOfStudents;
            this.maxNumberOfTeachers = maxNumberOfTeachers;
            this.monthlyPrice = monthlyPrice;
            assignedTeachers = new List<Teacher>();
            enrolledStudents = new List<Student>();
        }

        public string Name { get { return name; } }
        public int MinAgeInMonths { get { return minAgeInMonths; } }
        public int MaxAgeInMonths { get { return maxAgeInMonths; } }
        public float MonthlyPrice
        {
            get { return monthlyPrice; }
            set
            {
                if (value > 0)
                {
                    monthlyPrice = value;
                }
            }
        }
        public List<Teacher> Teachers { get { return assignedTeachers; } }
        public List<Student> Students { get { return enrolledStudents; } }

        public bool AssignTeacher(Teacher teacher)
        {
            if (teacher == null || assignedTeachers.Count == maxNumberOfTeachers || TeacherAlreadyAssigned(teacher))
            {
                return false;
            }
            assignedTeachers.Add(teacher);
            return true;
        }

        public OperationResult<Course> EnrollStudent(Student student)
        {
            OperationResult<Course> result = null;
            if (student == null)
            {
                result = new OperationResult<Course>(false, "Elegí el estudiante que deseas inscribir");
            }
            else if (enrolledStudents.Count == maxNumberOfStudents)
            {
                result = new OperationResult<Course>(false, "El curso alcanzó el cupo máximo de alumnos");
            }
            else if (StudentAlreadyEnrolled(student))
            {
                result = new OperationResult<Course>(false, "El alumno ya se inscribió en este curso");
            }
            else if (StudentAgeNotSuitable(student))
            {
                result = new OperationResult<Course>(false, "La edad del alumno no es adecuada para este curso");
            }

            if (result != null)
            {
                return result;
            }

            enrolledStudents.Add(student);

            return new OperationResult<Course>(true, this);
        }

        public void UnsuscribeStudent(Student student)
        {
            if (enrolledStudents.Contains(student))
            {
                enrolledStudents.Remove(student);
            }
        }

        private bool TeacherAlreadyAssigned(Teacher teacher)
        {
            return assignedTeachers.Contains(teacher);
        }

        private bool StudentAlreadyEnrolled(Student student)
        {
            return enrolledStudents.Contains(student);
        }

        private bool StudentAgeNotSuitable(Student student)
        {
            int studentAgeInMonths = student.Age * 12;
            return studentAgeInMonths < minAgeInMonths || studentAgeInMonths > maxAgeInMonths;
        }

        public override string ToString()
        {
            return name;
        }
    }
}