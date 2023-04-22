using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Braaten_CourseProject_Part2
{

    public partial class MainForm : Form
    {
        private const string FILENAME = "Employees.dat";
        public MainForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            InputForm frmInput = new InputForm();

            using (frmInput)
            {
                DialogResult result = frmInput.ShowDialog();

                if (result == DialogResult.Cancel)
                    return;

                string fName = frmInput.FirstNameTextBox.Text;
                string lName = frmInput.LastNameTextBox.Text;
                string ssn = frmInput.SSNTextBox.Text;
                string date = frmInput.HireDateTextBox.Text;
                DateTime hireDate = DateTime.Parse(date);

                string hIns = frmInput.hInsTB.Text;
                int lIns = int.Parse(frmInput.lInsTB.Text);
                int vDays = Int32.Parse(frmInput.vDaysTB.Text);

                Benefits b = new Benefits(hIns, lIns, vDays);

                Employee emp = null;
                if (frmInput.SalaryButton.Checked)
                {
                    double salary = double.Parse(frmInput.AnnualSalaryTextBox.Text);
                    emp = new Salary(fName, lName, ssn, hireDate, b, salary);
                }
                else if (frmInput.HourlyButton.Checked)
                {
                    double hourlyRate = double.Parse(frmInput.HourlyRateTextBox.Text);
                    double hoursWorked = double.Parse(frmInput.HoursWorkedTextBox.Text);
                    emp = new Hourly(fName, lName, ssn, hireDate, b, hourlyRate, hoursWorked);
                }
                else
                {
                    MessageBox.Show("Error. Choose a pay type");
                    return;
                }

                EmployeesListBox.Items.Add(emp);
                WriteEmpsToFile();
            }

        }

        private void WriteEmpsToFile()
        {
            List<Employee> empList = new List<Employee>();

            foreach(Employee emp in EmployeesListBox.Items)
            {
                empList.Add(emp);
            }

            FileStream fs = new FileStream(FILENAME, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(fs, empList);

            fs.Close();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            int itemNumber = EmployeesListBox.SelectedIndex;

            if (itemNumber > -1)
            {
                EmployeesListBox.Items.RemoveAt(itemNumber);
            }
            else
            {
                MessageBox.Show("Please select employee to remove.");
            }

            WriteEmpsToFile();
        }

        private void DisplayButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Displaying all employees...");
            ReadEmpsFromFile();
        }

        private void ReadEmpsFromFile()
        {
            if (File.Exists(FILENAME) && new FileInfo(FILENAME).Length > 0)
            {
                FileStream fs = new FileStream(FILENAME, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                List<Employee> list = (List<Employee>)formatter.Deserialize(fs);

                fs.Close();

                EmployeesListBox.Items.Clear();
                foreach(Employee emp in list)
                {
                    EmployeesListBox.Items.Add(emp);
                }
            }
        }

        private void PrintPaychecksButton_Click(object sender, EventArgs e)
        {
            foreach(Employee emp in EmployeesListBox.Items)
            {
                string line1 = "Pay To: " + emp.FirstName + " " + emp.LastName;
                string line2 = "Amount Of: " + emp.CalculatePay().ToString("C2");

                string output = "Paycheck:\n\n" + line1 + "\n" + line2;

                MessageBox.Show(output);
            }
        }

        private void EmployeesListBox_DoubleClick(object sender, EventArgs e)
        {
            Employee emp = (Employee)EmployeesListBox.SelectedItem;

            if (emp!= null )
            {
                InputForm updateForm = new InputForm();

                updateForm.Text = "Update Employee Information";
                updateForm.SubmitButton.Text = "Update";
                updateForm.StartPosition = FormStartPosition.CenterParent;
                updateForm.FirstNameTextBox.Text = emp.FirstName;
                updateForm.LastNameTextBox.Text = emp.LastName;
                updateForm.SSNTextBox.Text = emp.SSN;
                updateForm.HireDateTextBox.Text = emp.HireDate.ToShortDateString();
                updateForm.hInsTB.Text = emp.BenefitsEmp.HealthInsurance;
                updateForm.lInsTB.Text = emp.BenefitsEmp.LifeInsurance.ToString();
                updateForm.vDaysTB.Text = emp.BenefitsEmp.Vacation.ToString();

                if (emp is Salary)
                {
                    updateForm.AnnualSalaryLabel.Visible = true;
                    updateForm.AnnualSalaryTextBox.Visible = true;

                    updateForm.HourlyRateLabel.Visible = false;
                    updateForm.HourlyRateTextBox.Visible = false;
                    updateForm.HoursWorkedLabel.Visible = false;
                    updateForm.HoursWorkedTextBox.Visible = false;

                    updateForm.SalaryButton.Checked = true;

                    Salary sal = (Salary)emp;

                    updateForm.AnnualSalaryTextBox.Text = sal.AnnualSalary.ToString("F0");
                }
                else if (emp is Hourly)
                {
                    updateForm.AnnualSalaryLabel.Visible = false;
                    updateForm.AnnualSalaryTextBox.Visible = false;

                    updateForm.HourlyRateLabel.Visible = true;
                    updateForm.HourlyRateTextBox.Visible = true;
                    updateForm.HoursWorkedLabel.Visible = true;
                    updateForm.HoursWorkedTextBox.Visible = true;

                    updateForm.HourlyButton.Checked = true;

                    Hourly hour = (Hourly)emp;

                    updateForm.HourlyRateTextBox.Text = hour.HourlyRate.ToString("F2");
                    updateForm.HoursWorkedTextBox.Text = hour.HoursWorked.ToString("F1");
                }

                DialogResult result = updateForm.ShowDialog();

                if (result == DialogResult.Cancel)
                {
                    return;
                }

                int position = EmployeesListBox.SelectedIndex;
                EmployeesListBox.Items.RemoveAt(position);

                Employee newEmp = null;
                string fName = updateForm.FirstNameTextBox.Text;
                string lName = updateForm.LastNameTextBox.Text;
                string ssn = updateForm.SSNTextBox.Text;
                string date = updateForm.HireDateTextBox.Text;
                DateTime hireDate = DateTime.Parse(date);

                string hIns = updateForm.hInsTB.Text;
                int lIns = int.Parse(updateForm.lInsTB.Text);
                int vDays = Int32.Parse(updateForm.vDaysTB.Text);

                Benefits b = new Benefits(hIns, lIns, vDays);

                if (updateForm.SalaryButton.Checked)
                {
                    double salary = double.Parse(updateForm.AnnualSalaryTextBox.Text);
                    newEmp = new Salary(fName, lName, ssn, hireDate, b, salary);
                }
                else if (updateForm.HourlyButton.Checked)
                {
                    double hourlyRate = double.Parse(updateForm.HourlyRateTextBox.Text);
                    double hoursWorked = double.Parse(updateForm.HoursWorkedTextBox.Text);
                    newEmp = new Hourly(fName, lName, ssn, hireDate, b, hourlyRate, hoursWorked);
                }
                else
                {
                    MessageBox.Show("Error. Please check a pay type.");
                    return;
                }



                EmployeesListBox.Items.Add(newEmp);
            }
        }
    }
}
