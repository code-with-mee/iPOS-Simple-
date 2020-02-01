# iPOS-Simple-
simple point of sale for small business 

Functionality belows

Employee
Customer
Product
PurchaseOrder
Stock
Sale

+ Employee
	- Id
	- Name
	- PhoneNumber

+ Customer 
	- Id
	- Name
	- PhoneNumber
	
+ Product
	- Id
	- Name
	- CostPrice
	- SellingPrice
	- ReorderPoint
	- Description
	- Unit
	
+ PurchaseOrder
	- Id
	- EmployeeId
	- List<PurchaseDetail> PurchaseDetail
	
+ PurchaseDetail 
	- ProductId
	- Quantity
	- CostPrice
	- SellingPrice
	
+ Stock
	- Id
	- ProductId
	- ProductInStock
	
+ Sale
	- Id
	- EmployeeId
	- CustomerId
	- List<SaleDetail> SaleDetail
	- TotalAmount
	- TotalAmountPaid
	
+ SaleDetail
	- ProductId
	- CostPrice
	- SellingPrice
	- Quantity
	- TotalPrice