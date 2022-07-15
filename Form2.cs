using KindergardenLib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ejercicio10_Final
{
    public partial class Form2 : Form
    {
        private List<Student> students;

        public Form2()
        {
            InitializeComponent();
            students = new List<Student>();
        }

        public List<Student> Students
        {
            get { return students; }
            set { 
                students.Clear();
                students.AddRange(value);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            students.ForEach(s => listBox1.Items.Add(s));
        }
    }
}
