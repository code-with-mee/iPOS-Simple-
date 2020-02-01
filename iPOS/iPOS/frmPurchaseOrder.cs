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
    public partial class frmPurchaseOrder : Form
    {
        public Form1 form1;
        public PurchaseOrder purchaseOrder;
        public frmPurchaseOrder()
        {
            InitializeComponent();
        }

        private void frmPurchaseOrder_Load(object sender, EventArgs e)
        {
            int Id = form1.data.purchaseOrders.Count + 1;
            txtId.Text = Id.ToString();
            foreach(Employee employee in form1.data.employees)
            {
                cmbEmployeeName.Items.Add(employee.Name);
            }
            foreach(Product product in form1.data.products)
            {
                cmbProductName.Items.Add(product.Name);
            }

            foreach(PurchaseOrder purchaseOrder in form1.data.purchaseOrders)
            {
                cmbHistoryId.Items.Add(purchaseOrder.Id);
            }

            purchaseOrder = new PurchaseOrder();
            purchaseOrder.Id = Id;
            purchaseOrder.Products = new List<PurchaseDetail>();
        }

        private void cmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product product = form1.data.products.Where(p => p.Name == cmbProductName.Text).FirstOrDefault();

            txtCostPrice.Text = product.CostPrice.ToString();
            txtSellingPrice.Text = product.SellingPrice.ToString();
            lblUnit.Text = product.Unit;

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            cmbProductName.Text = "";
            txtQuantity.Text = "";
            txtCostPrice.Text = "";
            txtSellingPrice.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PurchaseDetail purchaseDetail = new PurchaseDetail();
            Product product = form1.data.products.Where(p => p.Name == cmbProductName.Text).FirstOrDefault();
            purchaseDetail.ProductId = product.Id;
            purchaseDetail.Quantity = int.Parse(txtQuantity.Text);
            purchaseDetail.CostPrice = float.Parse(txtCostPrice.Text);
            purchaseDetail.SellingPrice = float.Parse(txtSellingPrice.Text);
            purchaseOrder.Products.Add(purchaseDetail);

            dgv.Rows.Add(cmbProductName.Text, purchaseDetail.Quantity,product.Unit, purchaseDetail.CostPrice, purchaseDetail.SellingPrice);

            cmbProductName.Text = "";
            txtQuantity.Text = "";
            txtCostPrice.Text = "";
            txtSellingPrice.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            form1.data.purchaseOrders.Add(purchaseOrder);

            foreach(PurchaseDetail purchaseDetail in purchaseOrder.Products)
            {
                Product product = form1.data.products.Where(p => p.Id == purchaseDetail.ProductId).FirstOrDefault();
                product.CostPrice = purchaseDetail.CostPrice;
                product.SellingPrice = purchaseDetail.SellingPrice;

                Stock stock = form1.data.stocks.Where(s => s.ProductId == purchaseDetail.ProductId).FirstOrDefault();
                if(stock != null)
                {
                    stock.ProductInStock += purchaseDetail.Quantity;
                }
                else
                {
                    stock = new Stock();

                    stock.Id = form1.data.stocks.Count + 1;
                    stock.ProductId = purchaseDetail.ProductId;
                    stock.ProductInStock = purchaseDetail.Quantity;

                    form1.data.stocks.Add(stock);
                }
            }
            form1.SaveData();

            form1.RefreshGridData();
        }

        private void btnPurchaseOrderNew_Click(object sender, EventArgs e)
        {
            cmbEmployeeName.Text = "";
            cmbProductName.Text = "";
            txtQuantity.Text = "";
            txtCostPrice.Text = "";
            txtSellingPrice.Text = "";
            dgv.Rows.Clear();
        }

        private void cmbHistoryId_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            List<PurchaseDetail> purchaseDetails = form1.data.purchaseOrders[cmbHistoryId.SelectedIndex].Products;
            foreach(PurchaseDetail purchaseDetail in purchaseDetails)
            {
                Product product = form1.data.products.Where(p => p.Id == purchaseDetail.ProductId).FirstOrDefault();
                dgv.Rows.Add(product.Name, purchaseDetail.Quantity,product.Unit, purchaseDetail.CostPrice, purchaseDetail.SellingPrice);

            }
        }
    }
}
