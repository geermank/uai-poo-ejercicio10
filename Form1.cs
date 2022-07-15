using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KindergardenLib;

namespace Ejercicio10_Final
{
    public partial class Form1 : Form
    {
        private Kindergarden kinder =
            new Kindergarden(new CoursesFactory(), new EnrollmentFactory());

        public Form1()
        {
            InitializeComponent();


            foreach (Course c in kinder.ActiveCourses)
            {
                listBox2.Items.Add(c);
            }

            foreach (IEnrollment enrollment in kinder.Enrollments)
            {
                listBox4.Items.Add(enrollment);
            }

            kinder.PaymentResult += OnKindergardenPaymentResult;

            kinder.CreateStudentResult += OnKindergardenCreateStudentResult;
            kinder.EnrollStudentResult += OnKindergardenEnrollStudentResult;
            kinder.DeleteStudentResult += OnKindergardenDeleteStudentResult;

            kinder.CreateTeacherResult += OnKinderCreateTeacherResult;
            kinder.EnrollTeacherResult += OnKinderEnrollTeacherResult;
        }

        private void OnKinderEnrollTeacherResult(OperationResult<Teacher> result)
        {
            string resultMessage;
            if (result.Success)
            {
                resultMessage = "Maestra asignada con éxito";
            }
            else
            {
                resultMessage = result.Message;
            }
            MessageBox.Show(resultMessage);
        }

        private void OnKinderCreateTeacherResult(OperationResult<Teacher> result)
        {
            string resultMessage;
            if (result.Success)
            {
                listBox5.Items.Add(result.Data);
                resultMessage = "Maestra registrada con éxito";
            }
            else
            {
                resultMessage = result.Message;
            }
            MessageBox.Show(resultMessage);
        }

        private void OnKindergardenDeleteStudentResult(OperationResult<Student> result)
        {
            string resultMessage;
            if (result.Success)
            {
                resultMessage = "Alumno dado de baja con éxito: " + result.Data.FirstName;
            }
            else
            {
                resultMessage = result.Message;
            }
            MessageBox.Show(resultMessage);
        }

        private void OnKindergardenEnrollStudentResult(OperationResult<Course> result)
        {
            string resultMessage;
            if (result.Success)
            {
                resultMessage = "Alumno asignado con éxito a " + result.Data.Name;
            }
            else
            {
                resultMessage = result.Message;
            }
            MessageBox.Show(resultMessage);
        }

        private void OnKindergardenCreateStudentResult(OperationResult<Student> result)
        {
            string resultMessage;
            if (result.Success)
            {
                resultMessage = "Alumno creado";
                listBox1.Items.Add(result.Data);
            }
            else
            {
                resultMessage = result.Message;
            }
            MessageBox.Show(resultMessage);
        }

        private void OnKindergardenPaymentResult(OperationResult<Payment> result)
        {
            if (result.Success)
            {
                MessageBox.Show("Pago generado con éxito!");
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private List<Student> GetSelectedInstituteStudents()
        {
            List<Student> selectedStudents = new List<Student>();

            foreach (Student s in listBox1.SelectedItems)
            {
                selectedStudents.Add(s);
            }

            return selectedStudents;
        }

        private IEnrollment GetSelectedEnrollment()
        {
            return listBox4.SelectedItem as IEnrollment;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length > 0)
            {
                kinder.LunchPrice = float.Parse(textBox4.Text);
                textBox4.Text = null;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Course selectedCourse = listBox2.SelectedItem as Course;
            Teacher selectedTeacher = listBox5.SelectedItem as Teacher;
            kinder.EnrollTeacher(selectedTeacher, selectedCourse);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Course course = (Course)listBox2.SelectedItem;
            if (course == null)
            {
                return;
            }

            listBox3.Items.Clear();

            foreach (Teacher teacher in course.Teachers)
            {
                listBox3.Items.Add(teacher);
            }
            foreach (Student student in course.Students)
            {
                listBox3.Items.Add(student);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Course selectedCourse = listBox2.SelectedItem as Course;
            Student selectedStudent = listBox1.SelectedItem as Student;
            kinder.EnrollStudent(selectedStudent, selectedCourse);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            object selectedItem = listBox3.SelectedItem;
            if (selectedItem is Student student)
            {
                kinder.DeleteStudentFromCourse(student);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Student student)
            {
                DateTime paymentDate = dateTimePicker2.Value;
                kinder.GeneratePayment(student, paymentDate);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El monto total de recaudacion es " + kinder.GetRecaudation().ToString());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("La ganancia del jardin es " + kinder.GetProfit().ToString());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Student student = (Student)listBox1.SelectedItem;
            Payment lastPayment = kinder.GetStudentsLastPayment(student);
            if (lastPayment == null)
            {
                return;
            }
            MessageBox.Show("Estudiante: " + student.LastName + " " +
                "Ultima fecha de pago: " + lastPayment.Date.ToString() + " " +
                "Monto pagado: " + lastPayment.Amount);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Course selectedCourse = (Course)listBox2.SelectedItem;
            if (selectedCourse != null && textBox3.Text.Length > 0)
            {
                kinder.ChangeCoursePrice(float.Parse(textBox3.Text), selectedCourse);
                textBox3.Text = null;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string firstName = textBox1.Text;
            string lastName = textBox2.Text;
            DateTime dob = dateTimePicker1.Value;
            List<Student> studentSiblings = GetSelectedInstituteStudents();
            IEnrollment selectedEnrollment = GetSelectedEnrollment();

            kinder.CreateStudent(firstName, lastName, dob, studentSiblings, selectedEnrollment);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string name = textBox6.Text;
            string surname = textBox5.Text;
            float salary = float.Parse(textBox7.Text);
            bool hasFinishedStudies = checkBox1.Checked;
            kinder.CreateTeacher(name, surname, salary, hasFinishedStudies);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2
            {
                Students = kinder.Students
            };
            form2.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("No olvides de cargar los precios de los cursos antes de empezar. Todos fueron inicializados en cero");
        }
    }
}
