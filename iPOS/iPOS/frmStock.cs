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
    public partial class frmStock : Form
    {
        public Form1 form1;
        public frmStock()
        {
            InitializeComponent();
        }

        private void frmStock_Load(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            int i = 0;
            foreach (Product product in form1.data.products)
            {
                int quantity = form1.data.stocks.Where(s => s.ProductId == product.Id).FirstOrDefault().ProductInStock;

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
    }
}
