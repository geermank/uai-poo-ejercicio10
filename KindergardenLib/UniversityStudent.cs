using System;

namespace KindergardenLib
{
    class UniversityTeacher : Teacher
    {
        public UniversityTeacher(string firstName, string lastName, string file, float salary)
            : base(firstName, lastName, file, salary)
        {
            // empty constructor
        }

        public override float CalculateSalary()
        {
            return salary;
        }
    }
}