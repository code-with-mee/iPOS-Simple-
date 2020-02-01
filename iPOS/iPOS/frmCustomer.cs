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
    public partial class frmCustomer : Form
    {
        public Form1 form1;
        public frmCustomer()
        {
            InitializeComponent();
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            foreach (Customer customer in form1.data.customers)
            {
                dgv.Rows.Add(customer.Id, customer.Name, customer.PhoneNumber);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPhoneNumber.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int index = dgv.CurrentRow.Index;
            dgv.Rows[index].Cells[1].Value = txtName.Text;
            dgv.Rows[index].Cells[2].Value = txtPhoneNumber.Text;
            form1.data.customers[index].Name = txtName.Text;
            form1.data.customers[index].PhoneNumber = txtPhoneNumber.Text;

            form1.SaveData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Please input Name ");
            }
            else if (txtPhoneNumber.Text == "")
            {
                MessageBox.Show("Please input phone number");
            }
            else
            {
                Customer customer = new Customer();
                customer.Id = form1.data.customers.Count + 1;
                customer.Name = txtName.Text;
                customer.PhoneNumber = txtPhoneNumber.Text;
                form1.data.customers.Add(customer);
                form1.SaveData();

                txtName.Text = "";
                txtPhoneNumber.Text = "";

                dgv.Rows.Add(customer.Id, customer.Name, customer.PhoneNumber);
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgv.CurrentRow.Index;
            txtName.Text = dgv.Rows[index].Cells[1].Value.ToString();
            txtPhoneNumber.Text = dgv.Rows[index].Cells[2].Value.ToString();

        }
    }
}
