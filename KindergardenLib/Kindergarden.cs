using System;
using System.Collections.Generic;


namespace KindergardenLib
{
    public class Kindergarden
    {
        public event delPaymentResult PaymentResult;

        public event delCreateStudentResult CreateStudentResult;
        public event delEnrollStudentResult EnrollStudentResult;
        public event delDeleteStudentResult DeleteStudentResult;

        public event delCreateTeacher CreateTeacherResult;
        public event delEnrollTeacherResult EnrollTeacherResult;

        private readonly List<Course> activeCourses;
        private readonly List<Student> students;
        private readonly List<Teacher> teachers;
        private readonly List<Payment> payments;
        private readonly List<IEnrollment> enrollments;

        public Kindergarden(CoursesFactory coursesFactory, EnrollmentFactory enrollmentFactory)
        {
            activeCourses = coursesFactory.CreateCourses();
            enrollments = enrollmentFactory.CreateEnrollments(0f);
            students = new List<Student>();
            payments = new List<Payment>();
            teachers = new List<Teacher>();
        }

        public List<IEnrollment> Enrollments
        {
            get { return enrollments; }
        }

        public List<Course> ActiveCourses
        {
            get { return activeCourses; }
        }

        public List<Student> Students
        {
            get
            {
                students.Sort();
                return students;
            }
        }

        public float LunchPrice
        {
            get
            {
                IEnrollment enrollment = enrollments.Find(e => e is DoubleShiftWithLunchEnrollment);
                return (enrollment as DoubleShiftWithLunchEnrollment).LunchPrice;
            }
            set
            {
                IEnrollment enrollment = enrollments.Find(e => e is DoubleShiftWithLunchEnrollment);
                (enrollment as DoubleShiftWithLunchEnrollment).LunchPrice = value;
            }
        }

        public void ChangeCoursePrice(float newPrice, Course course)
        {
            if (course == null || newPrice <= 0)
            {
                return;
            }
            course.MonthlyPrice = newPrice;
        }

        public void EnrollStudent(Student student, Course course)
        {
            OperationResult<Course> enrollmentResult;
            if (GetStudentCourse(student) != null)
            {
                enrollmentResult = new OperationResult<Course>(false, "El alumno ya está cursando!");
            }
            else
            {
                enrollmentResult = course.EnrollStudent(student);
            }
            EnrollStudentResult(enrollmentResult);
        }

        public void EnrollTeacher(Teacher teacher, Course course)
        {
            OperationResult<Teacher> operationResult;
            if (teacher == null || course == null)
            {
                operationResult = new OperationResult<Teacher>(false, "Los datos ingresados no son correctos");
            }
            else if (course.AssignTeacher(teacher))
            {
                operationResult = new OperationResult<Teacher>(true, teacher);
            }
            else
            {
                operationResult = new OperationResult<Teacher>(false, "No se pudo asignar el maestro");
            }
            EnrollTeacherResult(operationResult);
        }

        public void CreateTeacher(string firstName,
                                  string lastName,
                                  float salary,
                                  bool hasFinishedStudies)
        {
            OperationResult<Teacher> operationResult;

            if (firstName == null || firstName.Length == 0 ||
                lastName == null || lastName.Length == 0 ||
                salary <= 0)
            {
                operationResult = new OperationResult<Teacher>(false, "Los datos ingresados no son válidos");
            }
            else
            {
                string file = Guid.NewGuid().ToString();

                Teacher newTeacher;
                if (hasFinishedStudies)
                {
                    newTeacher = new UniversityTeacher(firstName, lastName, file, salary);
                }
                else
                {
                    newTeacher = new Teacher(firstName, lastName, file, salary);
                }

                teachers.Add(newTeacher);

                operationResult = new OperationResult<Teacher>(true, newTeacher);
            }

            CreateTeacherResult(operationResult);
        }

        public void CreateStudent(string firstName,
                                  string lastName,
                                  DateTime dob,
                                  List<Student> siblings,
                                  IEnrollment enrollment)
        {
            OperationResult<Student> operationResult;

            if (firstName == null || firstName.Length == 0 ||
                lastName == null || lastName.Length == 0 ||
                dob > DateTime.Now || enrollment == null)
            {
                operationResult = new OperationResult<Student>(false, "Los datos ingresados no son correctos");
            }
            else
            {
                string studentFile = Guid.NewGuid().ToString();
                Student newStudent = new Student(firstName, lastName, studentFile, dob, siblings, enrollment);
                students.Add(newStudent);

                UpdateSiblingsOnPreviousStudents(newStudent, siblings);

                operationResult = new OperationResult<Student>(true, newStudent);
            }

            CreateStudentResult(operationResult);
        }

        public void GeneratePayment(Student student, DateTime date)
        {
            Course course = GetStudentCourse(student);
            if (course == null)
            {
                PaymentResult(new OperationResult<Payment>(false, "Este estudiante no está anotado en ningún curso"));
                return;
            }

            float paymentAmount = student.CalculatePaymentAmountFromCourse(course);

            Payment payment = new Payment(date, paymentAmount, student);
            if (StudentHasAlreadyPaidThisMoth(payment))
            {
                PaymentResult(new OperationResult<Payment>(false, "Este mes ya está pago!"));
                return;
            }

            payments.Add(payment);

            PaymentResult(new OperationResult<Payment>(true, payment));
        }

        private bool StudentHasAlreadyPaidThisMoth(Payment payment)
        {
            bool hasAlreadyPaidThisMonth = false;
            foreach (Payment p in payments)
            {
                if (p.Student.Equals(payment.Student) &&
                    p.Date.Month == payment.Date.Month &&
                    p.Date.Year == payment.Date.Year)
                {
                    hasAlreadyPaidThisMonth = true;
                    break;
                }
            }
            return hasAlreadyPaidThisMonth;
        }

        public void DeleteStudentFromCourse(Student student)
        {
            OperationResult<Student> operationResult;
            if (student == null)
            {
                operationResult = new OperationResult<Student>(false, "Debes elegir qué estudiante deseas eliminar");
            }
            else if (!students.Contains(student))
            {
                operationResult = new OperationResult<Student>(false, "El estudiante no está inscripto en el instituto");
            }
            else
            {
                Course course = GetStudentCourse(student);
                if (course != null)
                {
                    course.UnsuscribeStudent(student);
                    operationResult = new OperationResult<Student>(true, student);
                }
                else
                {
                    operationResult = new OperationResult<Student>(false, "El estudiante no está inscripto en ningún curso");
                }
            }
            DeleteStudentResult(operationResult);
        }

        public float GetRecaudation()
        {
            float recaudation = 0f;
            payments.ForEach(payment => recaudation += payment.Amount);
            return recaudation;
        }

        public float GetProfit()
        {
            float profit = GetRecaudation();
            foreach (Course c in activeCourses)
            {
                c.Teachers.ForEach(t => profit -= t.CalculateSalary());
            }
            return profit;
        }

        public Payment GetStudentsLastPayment(Student student)
        {
            return payments.FindLast(p => p.Student.Equals(student));
        }

        private Course GetStudentCourse(Student student)
        {
            return activeCourses.Find(c => c.Students.Contains(student));
        }

        private void UpdateSiblingsOnPreviousStudents(Student newStudent, List<Student> siblings)
        {
            siblings.ForEach(s => s.AddSibling(newStudent));
        }
    }
}