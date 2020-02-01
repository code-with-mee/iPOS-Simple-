using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iPOS
{
    public partial class frmEmployee : Form
    {
        public Form1 form1;
        public frmEmployee()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtName.Text == "")
            {
                MessageBox.Show("Please input Name ");
            }else if(txtPhoneNumber.Text == "")
            {
                MessageBox.Show("Please input phone number");
            }
            else
            {
                Employee employee = new Employee();
                employee.Id = form1.data.employees.Count + 1;
                employee.Name = txtName.Text;
                employee.PhoneNumber = txtPhoneNumber.Text;
                form1.data.employees.Add(employee);
                form1.SaveData();

                txtName.Text = "";
                txtPhoneNumber.Text = "";

                dgv.Rows.Add(employee.Id, employee.Name, employee.PhoneNumber);
            }    
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            //dgv.DataSource = form1.data.employees;
            
            foreach(Employee employee in form1.data.employees)
            {
                dgv.Rows.Add(employee.Id, employee.Name, employee.PhoneNumber);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int index = dgv.CurrentRow.Index;
            dgv.Rows[index].Cells[1].Value = txtName.Text;
            dgv.Rows[index].Cells[2].Value = txtPhoneNumber.Text;
            form1.data.employees[index].Name = txtName.Text;
            form1.data.employees[index].PhoneNumber = txtPhoneNumber.Text;

            form1.SaveData();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgv.CurrentRow.Index;
            txtName.Text = dgv.Rows[index].Cells[1].Value.ToString();
            txtPhoneNumber.Text = dgv.Rows[index].Cells[2].Value.ToString();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPhoneNumber.Text = "";
        }
    }
}
