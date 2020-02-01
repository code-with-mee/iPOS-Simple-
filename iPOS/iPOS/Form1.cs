using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Newtonsoft.Json;

namespace iPOS
{
    public partial class Form1 : Form
    {
        public Data data;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadData();

            this.RefreshGridData();
        }

        public void RefreshGridData()
        {
            dgv.Rows.Clear();
            int i = 0;
            foreach (Product product in data.products)
            {
                int quantity = data.stocks.Where(s => s.ProductId == product.Id).FirstOrDefault().ProductInStock;

                dgv.Rows.Add(
                              product.Id,
                              product.Name,
                              product.CostPrice,
                              product.SellingPrice,
                              quantity,
                              product.Unit
                            );
                if (product.ReorderPoint >= quantity)
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Red;

                i++;
            }
            dgv.Refresh();
        }
        private string path = "./data/data.json";
        public void LoadData()
        {
            if(File.Exists(path))
            {
                string jsonData = File.ReadAllText(path);
                data = JsonConvert.DeserializeObject<Data>(jsonData);
            }
            else
            {
                data = new Data();
                string jsonData = JsonConvert.SerializeObject(data);
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(jsonData);
                }
                //File.WriteAllText(path, jsonData);
            }
        }
        public void SaveData()
        {
            string jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(path, jsonData);
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            frmEmployee frmEmployee = new frmEmployee();
            frmEmployee.form1 = this;
            frmEmployee.ShowDialog();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmProduct frmProduct = new frmProduct();
            frmProduct.form1 = this;
            frmProduct.ShowDialog();
        }

        private void btnPurchaseOrder_Click(object sender, EventArgs e)
        {
            frmPurchaseOrder frmPurchaseOrder = new frmPurchaseOrder();
            frmPurchaseOrder.form1 = this;
            frmPurchaseOrder.ShowDialog();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            frmStock frmStock = new frmStock();
            frmStock.form1 = this;
            frmStock.ShowDialog();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            frmSale frmSale = new frmSale();
            frmSale.form1 = this;
            frmSale.ShowDialog();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            frmCustomer frmCustomer = new frmCustomer();
            frmCustomer.form1 = this;
            frmCustomer.ShowDialog();
        }
    }

    public class Data
    {
        public List<Employee> employees;
        public List<Product> products;
        public List<PurchaseOrder> purchaseOrders;
        public List<Stock> stocks;
        public List<Sale> sales;
        public List<Customer> customers;

        public Data()
        {
            employees = new List<Employee>();
            products = new List<Product>();
            purchaseOrders = new List<PurchaseOrder>();
            stocks = new List<Stock>();
            sales = new List<Sale>();
            customers = new List<Customer>();
        }
    }
}
