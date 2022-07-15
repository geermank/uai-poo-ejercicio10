using System;

namespace KindergardenLib
{
    public class Person
    {
        protected string firstName;
        protected string lastName;
        protected string file;

        public Person(string firstName,
                      string lastName,
                      string file)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.file = file;
        }

        public string File
        {
            get { return file; }
        }

        public string FirstName
        {
            get { return firstName; }
        }
        public string LastName
        {
            get { return lastName; }
        }
    }
}