using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KindergardenLib
{
    public class Teacher : Person
    {
        protected float salary;

        public Teacher(string firstName,
                       string lastName,
                       string file,
                       float salary) : base(firstName, lastName, file)
        {
            this.salary = salary;
        }

        public virtual float CalculateSalary()
        {
            return salary / 2;
        }

        public override bool Equals(object obj)
        {
            return obj is Teacher && (obj as Teacher).file == this.file;
        }

        public override string ToString()
        {
            return firstName + " " + lastName;
        }
    }
}