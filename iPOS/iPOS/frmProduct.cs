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
    public partial class frmProduct : Form
    {
        public Form1 form1;
        public frmProduct()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtCostPrice.Text = "";
            txtSellingPrice.Text = "";
            txtReorderPoint.Text = "";
            txtDescription.Text = "";
            txtUnit.Text = "";
        }

        private void txtCostPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgv.CurrentRow.Index;
            if(index < dgv.Rows.Count -1)
            {
                txtName.Text = dgv.Rows[index].Cells[1].Value.ToString();
                txtCostPrice.Text = dgv.Rows[index].Cells[2].Value.ToString();
                txtSellingPrice.Text = dgv.Rows[index].Cells[3].Value.ToString();
                txtReorderPoint.Text = dgv.Rows[index].Cells[4].Value.ToString();
                txtDescription.Text = dgv.Rows[index].Cells[5].Value.ToString();
                txtUnit.Text = dgv.Rows[index].Cells[6].Value.ToString();
            }
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int index = dgv.CurrentRow.Index;
            dgv.Rows[index].Cells[1].Value = txtName.Text;
            dgv.Rows[index].Cells[2].Value = txtCostPrice.Text;
            dgv.Rows[index].Cells[3].Value = txtSellingPrice.Text;
            dgv.Rows[index].Cells[4].Value = txtReorderPoint.Text;
            dgv.Rows[index].Cells[5].Value = txtDescription.Text;
            dgv.Rows[index].Cells[6].Value = txtUnit.Text;

            form1.data.products[index].Name = txtName.Text;
            form1.data.products[index].CostPrice = float.Parse(txtCostPrice.Text);
            form1.data.products[index].SellingPrice = float.Parse(txtSellingPrice.Text);
            form1.data.products[index].ReorderPoint = int.Parse(txtReorderPoint.Text);
            form1.data.products[index].Description = txtDescription.Text;
            form1.data.products[index].Unit = txtUnit.Text;

            form1.SaveData();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            foreach (Product product in form1.data.products)
            {
                dgv.Rows.Add(product.Id, product.Name, product.CostPrice, product.SellingPrice, product.ReorderPoint, product.Description,product.Unit);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Id = form1.data.products.Count + 1;
            product.Name = txtName.Text;
            product.CostPrice = float.Parse( txtCostPrice.Text);
            product.SellingPrice = float.Parse( txtSellingPrice.Text);
            product.ReorderPoint = int.Parse(txtReorderPoint.Text);
            product.Description = txtDescription.Text;
            product.Unit = txtUnit.Text;
            form1.data.products.Add(product);
            form1.SaveData();

            txtName.Text = "";
            txtCostPrice.Text = "";
            txtSellingPrice.Text = "";
            txtReorderPoint.Text = "";
            txtDescription.Text = "";
            txtUnit.Text = "";

            dgv.Rows.Add(product.Id, product.Name, product.CostPrice, product.SellingPrice, product.ReorderPoint, product.Description,product.Unit);

        }
    }
}
