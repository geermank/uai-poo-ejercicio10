using System;

namespace KindergardenLib
{
    public class Payment
    {
        private DateTime date;
        private float amount;
        private Student student;

        public Payment(DateTime date, float amount, Student student)
        {
            this.date = date;
            this.amount = amount;
            this.student = student;
        }

        public DateTime Date
        {
            get { return date; }
        }

        public float Amount
        {
            get { return amount; }
        }

        public Student Student
        {
            get { return student; }
        }
    }
}