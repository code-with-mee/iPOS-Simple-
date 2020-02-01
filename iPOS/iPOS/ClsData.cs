using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPOS
{
    class ClsData
    {

    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float CostPrice { get; set; }
        public float SellingPrice { get; set; }
        public int ReorderPoint { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
    }

    public class PurchaseOrder
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public List<PurchaseDetail> Products { get; set; }
    }

    public class PurchaseDetail
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float CostPrice { get; set; }
        public float SellingPrice { get; set; }
    }
    public class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductInStock { get; set; }
    }

    public class Sale
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public List<SaleDetail> SaleDetails { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public Sale()
        {
            SaleDetails = new List<SaleDetail>();
        }
    }
    public class SaleDetail
    {
        public int ProductId { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
