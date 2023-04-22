using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Braaten_CourseProject_Part2
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
            this.Hide();
        }

        private void SalaryButton_CheckedChanged(object sender, EventArgs e)
        {
            AnnualSalaryLabel.Visible = true;
            AnnualSalaryTextBox.Visible = true;

            HourlyRateLabel.Visible = false;
            HourlyRateTextBox.Visible = false;
            HoursWorkedLabel.Visible = false;
            HoursWorkedTextBox.Visible = false;
        }

        private void HourlyButton_CheckedChanged(object sender, EventArgs e)
        {
            HourlyRateLabel.Visible = true;
            HourlyRateTextBox.Visible = true;
            HoursWorkedLabel.Visible = true;
            HoursWorkedTextBox.Visible = true;

            AnnualSalaryTextBox.Visible = false;
            AnnualSalaryLabel.Visible = false;
        }
    }
}
