using System;
using System.Collections.Generic;

namespace KindergardenLib
{
    public class Student : Person, IComparable<Student>
    {
        private List<Student> siblings;
        private IEnrollment enrollment;
        private DateTime dob;

        public Student(string firstName,
                       string lastName,
                       string file,
                       DateTime dob,
                       List<Student> siblings,
                       IEnrollment enrollment) : base(firstName, lastName, file)
        {
            this.siblings = siblings;
            this.enrollment = enrollment;
            this.dob = dob;
        }

        public DateTime Dob
        {
            get { return dob; }
        }

        public List<Student> Siblings
        {
            get { return siblings; }
        }

        public IEnrollment Enrollment
        {
            get { return enrollment; }
            set { enrollment = value; }
        }

        public bool HasSibligns
        {
            get
            {
                return siblings != null && siblings.Count > 0;
            }
        }

        public int Age
        {
            get
            {
                int years = DateTime.Now.Year - dob.Year;
                if (dob.Month > DateTime.Now.Month && dob.Day > DateTime.Now.Day)
                {
                    years--;
                }
                return years;
            }
        }

        public void AddSibling(Student student)
        {
            siblings.Add(student);
        }

        public float CalculatePaymentAmountFromCourse(Course course)
        {
            float totalPaymentAmount = enrollment.CalculatePrice(course.MonthlyPrice);
            if (HasSibligns)
            {
                totalPaymentAmount -= (totalPaymentAmount * 40) / 100;
            }
            return totalPaymentAmount;
        }

        public override string ToString()
        {
            return firstName + " " + lastName;
        }

        public override bool Equals(object obj)
        {
            return obj is Student && ((Student)obj).file == this.file;
        }

        public int CompareTo(Student other)
        {
            return lastName.CompareTo(other.lastName);
        }
    }
}