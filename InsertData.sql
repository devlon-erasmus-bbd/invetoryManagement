-- Insert data into the Company table
INSERT INTO [Company] ([company_name], [company_description], [created_date]) VALUES ('ABC Corp', 'A manufacturing company', '2022-01-01');
INSERT INTO [Company] ([company_name], [company_description], [created_date]) VALUES ('ABC Company', 'This is a software company', '2023-04-17')
INSERT INTO [Company] ([company_name], [company_description], [created_date]) VALUES ('XYZ Inc', 'This is a manufacturing company', '2023-04-16')

INSERT INTO [Supplier] ([supplier_name], [supplier_contact_number], [created_date]) VALUES ('ABC Supplier', '1234567890', '2023-04-15')
INSERT INTO [Supplier] ([supplier_name], [supplier_contact_number], [created_date]) VALUES ('XYZ Supplier', '9876543210', '2023-04-14')

-- Insert data into the ItemCategory table
INSERT INTO [ItemCategory] ([item_category_name], [item_category_description]) VALUES ('Electronics', 'Electronic devices and components');
INSERT INTO [ItemCategory] ([item_category_name], [item_category_description]) VALUES ('Apparel', 'Clothing and accessories');
INSERT INTO [ItemCategory] ([item_category_name], [item_category_description]) VALUES ('Grocery', 'Items related to groceries')

-- Insert data into the Item table
INSERT INTO [Item] ([fk_company_id], [fk_supplier_id], [fk_item_category_id], [item_name], [item_description], [acquired_date], [cost_price], [sell_price], [quantity], [expiry_date]) VALUES (1, 1, 1, 'Smartphone', 'A high-end smartphone', '2022-01-15', 500, 800, 100, '2023-01-01');
INSERT INTO [Item] ([fk_company_id], [fk_supplier_id], [fk_item_category_id], [item_name], [item_description], [acquired_date], [cost_price], [sell_price], [quantity], [expiry_date]) VALUES (2, 2, 2, 'T-Shirt', 'A plain white t-shirt', '2022-02-15', 10, 20, 500, NULL);
INSERT INTO [Item] ([fk_company_id], [fk_supplier_id], [fk_item_category_id], [item_name], [item_description], [acquired_date], [cost_price], [sell_price], [quantity], [expiry_date]) VALUES (1, 1, 1, 'Laptop', 'A high-end laptop', '2023-04-13', 1200.00, 1500.00, 5, '2025-04-13')
INSERT INTO [Item] ([fk_company_id], [fk_supplier_id], [fk_item_category_id], [item_name], [item_description], [acquired_date], [cost_price], [sell_price], [quantity], [expiry_date]) VALUES (2, 2, 3, 'Rice', 'A 1kg pack of rice', '2023-04-12', 25.00, 30.00, 10, '2024-04-12')

INSERT INTO [Staff] ([staff_name]) VALUES ('John Doe')
INSERT INTO [Staff] ([staff_name]) VALUES ('Jane Smith')

-- Insert data into the Customer table
INSERT INTO [Customer] ([customer_name], [customer_contact_number]) VALUES ('Alex John', '555-1111');
INSERT INTO [Customer] ([customer_name], [customer_contact_number]) VALUES ('Joe Smith', '555-2222');
INSERT INTO [Customer] ([customer_name], [customer_contact_number]) VALUES ('Bob Johnson', '555-1234')
INSERT INTO [Customer] ([customer_name], [customer_contact_number]) VALUES ('Alice Brown', '555-5678')

INSERT INTO [Bill] ([fk_staff_id], [fk_customer_id], [order_date]) VALUES (1, 1, '2023-04-11')
INSERT INTO [Bill] ([fk_staff_id], [fk_customer_id], [order_date]) VALUES (2, 2, '2023-04-10')

-- Insert data into the Order table
INSERT INTO [Order] ([fk_item_id], [fk_bill_id], [quantity], [discount], [price_paid]) VALUES (1, 1, 2, 0.1, 1440);
INSERT INTO [Order] ([fk_item_id], [fk_bill_id], [quantity], [discount], [price_paid]) VALUES (2, 2, 5, 0.05, 95);
INSERT INTO [Order] ([fk_item_id], [fk_bill_id], [quantity], [discount], [price_paid]) VALUES (1, 1, 1, 0.00, 1500.00)
INSERT INTO [Order] ([fk_item_id], [fk_bill_id], [quantity], [discount], [price_paid]) VALUES (2, 2, 2, 0.50, 45.00)
