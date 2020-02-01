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
    public partial class frmSale : Form
    {
        public Form1 form1;
        public frmSale()
        {
            InitializeComponent();
        }

        public Sale sale;
        private void frmSale_Load(object sender, EventArgs e)
        {
            int Id = form1.data.sales.Count + 1;
            txtId.Text = Id.ToString();
            foreach (Employee employee in form1.data.employees)
            {
                cmbEmployeeName.Items.Add(employee.Name);
            }
            foreach(Customer customer in form1.data.customers)
            {
                cmbCustomer.Items.Add(customer.Name);
            }
            foreach (Product product in form1.data.products)
            {
                cmbProductName.Items.Add(product.Name);
            }

            List<int> tempHisSaleId = new List<int>();
            foreach (Sale sale in form1.data.sales)
            {
                tempHisSaleId.Add(sale.Id);
                
            }

            tempHisSaleId.Reverse();
            foreach(int id in tempHisSaleId)
            {
                cmbHistoryId.Items.Add(id);
            }


            sale = new Sale();
            sale.Id = Id;
        }

        private void cmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product product = form1.data.products.Where(p => p.Name == cmbProductName.Text).FirstOrDefault();

            txtSellingPrice.Text = product.SellingPrice.ToString();
            txtSellingPrice.Tag = product.CostPrice.ToString();
            lblUnit.Text = product.Unit;
            lblRemain.Text = form1.data.stocks.Where(s => s.ProductId == product.Id).FirstOrDefault().ProductInStock.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            SaleDetail tempSaleDetail = (SaleDetail)dgv.Rows[dgv.CurrentRow.Index].Tag;
            sale.SaleDetails.Remove(tempSaleDetail);
            dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            cmbProductName.Text = "";
            txtQuantity.Value = 0;
            txtSellingPrice.Text = "";
            txtTotalPrice.Text = "";
            lblRemain.Text = "";
            lblUnit.Text = "";
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if( !string.IsNullOrEmpty(txtQuantity.Text) && (! string.IsNullOrEmpty(txtSellingPrice.Text)))
            {
                int quantity = int.Parse(txtQuantity.Text);
                int rquantity = int.Parse(lblRemain.Text);
                if(quantity > rquantity)
                {
                    txtQuantity.Text = "0";
                    MessageBox.Show("Stock Item not enought");
                }
                else
                {
                    float totalPrice = int.Parse(txtQuantity.Text) * float.Parse(txtSellingPrice.Text);
                    txtTotalPrice.Text = totalPrice.ToString();
                }
                
            }
        }

        private decimal totalAmount = 0;
        private decimal totalAmountPaid = 0;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(cmbProductName.Text == "")
            {
                MessageBox.Show("Please select product name");
                return;
            }
            if(txtQuantity.Value == 0)
            {
                MessageBox.Show("Please enter quantity");
                return;
            }
            SaleDetail saleDetail = new SaleDetail();
            Product product = form1.data.products.Where(p => p.Name == cmbProductName.Text).FirstOrDefault();
            saleDetail.ProductId = product.Id;
            saleDetail.Quantity = int.Parse(txtQuantity.Text);
            saleDetail.CostPrice = decimal.Parse(txtSellingPrice.Tag.ToString());
            saleDetail.SellingPrice = decimal.Parse(txtSellingPrice.Text);
            saleDetail.TotalPrice = decimal.Parse(txtTotalPrice.Text);

            sale.SaleDetails.Add(saleDetail);

            dgv.Rows.Add(dgv.Rows.Count + 1,
                         cmbProductName.Text,
                         saleDetail.Quantity, 
                         product.Unit, 
                         saleDetail.SellingPrice,
                         saleDetail.TotalPrice
                         );

            dgv.Rows[dgv.Rows.Count - 1].Tag = saleDetail;

            cmbProductName.Text = "";
            txtQuantity.Value = 0;
            txtSellingPrice.Text = "";
            txtTotalPrice.Text = "";
            lblRemain.Text = "";
            lblUnit.Text = "";

            
            foreach(SaleDetail sd in sale.SaleDetails)
            {
                totalAmount += sd.TotalPrice;
            }
            totalAmountPaid = totalAmount;
            lblTotalAmount.Text = totalAmount.ToString();
            txtTotalAmount.Text = totalAmount.ToString();
            txtAmountPaid.Value = totalAmount;
            txtBalance.Text = (totalAmount - totalAmountPaid).ToString();
        }

        private void btnSaleNew_Click(object sender, EventArgs e)
        {
            this.Clear();
        }

        private void btnSaleSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbCustomer.Text))
                MessageBox.Show("Please select customer name");
            else if (string.IsNullOrEmpty(cmbEmployeeName.Text))
                MessageBox.Show("please select employee name");
            else if (dgv.Rows.Count == 0)
                MessageBox.Show("At least have one item to be sold");
            else if (string.IsNullOrEmpty(txtAmountPaid.Text))
                MessageBox.Show("Ämount paid can be zero or greater then zero");
            else
            {
                sale.CustomerId = form1.data.customers.Where(c => c.Name == cmbCustomer.Text).FirstOrDefault().Id;
                sale.EmployeeId = form1.data.employees.Where(emp => emp.Name == cmbEmployeeName.Text).FirstOrDefault().Id;
                sale.TotalAmount = totalAmount;
                if (!string.IsNullOrEmpty(txtAmountPaid.Text))
                    sale.TotalAmountPaid = decimal.Parse(txtAmountPaid.Text);
                form1.data.sales.Add(sale);

                foreach (SaleDetail saleDetail in sale.SaleDetails)
                {
                    Stock stock = form1.data.stocks.Where(s => s.ProductId == saleDetail.ProductId).FirstOrDefault();
                    stock.ProductInStock -= saleDetail.Quantity;
                }

                form1.SaveData();
                form1.RefreshGridData();
                this.Clear();
            }
        }

        public void Clear()
        {
            int Id = form1.data.sales.Count + 1;
            txtId.Text = Id.ToString();
            txtBalance.Text = "";
            cmbHistoryId.Text = "";
            cmbCustomer.Text = "";
            cmbEmployeeName.Text = "";
            cmbProductName.Text = "";
            txtQuantity.Value = 0;
            txtSellingPrice.Text = "";
            txtTotalPrice.Text = "";
            txtAmountPaid.Value = 0;
            lblRemain.Text = "";
            lblUnit.Text = "";
            dgv.Rows.Clear();

            btnNew.Enabled = true;
            btnAdd.Enabled = true;
            btnRemove.Enabled = true;

            btnSaleSave.Enabled = true;

            cmbCustomer.Enabled = true;
            cmbEmployeeName.Enabled = true;
            cmbProductName.Enabled = true;

        }

        private void cmbHistoryId_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            Sale tempSale = form1.data.sales[cmbHistoryId.SelectedIndex];
            foreach(SaleDetail saleDetail in tempSale.SaleDetails)
            {
                Product product = form1.data.products.Where(p => p.Id == saleDetail.ProductId).FirstOrDefault();

                dgv.Rows.Add(dgv.Rows.Count + 1,
                         product.Name,
                         saleDetail.Quantity,
                         product.Unit,
                         saleDetail.SellingPrice,
                         saleDetail.TotalPrice
                         );
            }
            cmbCustomer.Text = form1.data.customers.Where(c => c.Id == tempSale.CustomerId).FirstOrDefault().Name;
            cmbEmployeeName.Text = form1.data.employees.Where(emp => emp.Id == tempSale.EmployeeId).FirstOrDefault().Name;
            txtTotalAmount.Text = tempSale.TotalAmount.ToString();
            txtAmountPaid.Text = tempSale.TotalAmountPaid.ToString();
            lblTotalAmount.Text = tempSale.TotalAmount.ToString();

            txtBalance.Text = (tempSale.TotalAmount - tempSale.TotalAmountPaid).ToString();

            btnNew.Enabled = false;
            btnAdd.Enabled = false;
            btnRemove.Enabled = false;

            btnSaleSave.Enabled = false;

            cmbCustomer.Enabled = false;
            cmbEmployeeName.Enabled = false;
            cmbProductName.Enabled = false;
        }

        private void CmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            Sale tempSale = form1.data.sales[cmbHistoryId.SelectedIndex];
            if(!string.IsNullOrEmpty(txtAmountPaid.Text))
            {
                totalAmountPaid = decimal.Parse(txtAmountPaid.Text);
                tempSale.TotalAmountPaid = totalAmountPaid;
                txtBalance.Text = (tempSale.TotalAmount - tempSale.TotalAmountPaid).ToString();
                MessageBox.Show("Update Sale Completed");
            }
        }

        private void TxtQuantity_ValueChanged(object sender, EventArgs e)
        {
            int rquantity = int.Parse(lblRemain.Text);
            if (txtQuantity.Value > rquantity)
            {
                txtQuantity.Text = "0";
                MessageBox.Show("Stock Item not enought");
            }
            else
            {
                decimal totalPrice = txtQuantity.Value * decimal.Parse(txtSellingPrice.Text);
                txtTotalPrice.Text = totalPrice.ToString();
            }
        }

        private void TxtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void TxtQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQuantity.Text) && (!string.IsNullOrEmpty(txtSellingPrice.Text)))
            {
                int quantity = int.Parse(txtQuantity.Text);
                int rquantity = int.Parse(lblRemain.Text);
                if (quantity > rquantity)
                {
                    txtQuantity.Text = "0";
                    MessageBox.Show("Stock Item not enought");
                }
                else
                {
                    float totalPrice = int.Parse(txtQuantity.Text) * float.Parse(txtSellingPrice.Text);
                    txtTotalPrice.Text = totalPrice.ToString();
                }

            }
        }
    }
}
