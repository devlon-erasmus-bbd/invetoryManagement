INSERT INTO [Company] ([company_name], [company_description], [created_date])
VALUES ('ABC Company', 'A manufacturer of electronics', '2022-01-01'),
('XYZ Corp', 'A software development company', '2022-02-15'),
('Acme Corporation', 'A manufacturer of industrial equipment', '2021-06-01'),
('Global Foods', 'A food and beverage distributor', '2021-09-15');

INSERT INTO [Supplier] ([supplier_name], [supplier_contact_number], [created_date])
VALUES ('ABC Supplies', '555-1234', '2022-01-01'),
('XYZ Parts', '555-5678', '2022-02-15'),
('Acme Supplies', '555-1111', '2021-06-01'),
('Global Farms', '555-2222', '2021-09-15'),
('Electro Parts', '555-3333', '2022-01-01');

INSERT INTO [ItemCategory] ([item_category_name], [item_category_description])
VALUES ('Electronics', 'Electronic devices and components'),
('Food and Beverage', 'Various food and drink products for consumption'),
('Electrical Components', 'Parts and accessories for electrical systems and devices');

INSERT INTO [Item] ([fk_company_id], [fk_supplier_id], [fk_item_category_id], [item_name], [item_description], [acquired_date], [cost_price], [sell_price], [quantity], [expiry_date])
VALUES (1, 1, 1, 'Smartphone', 'A high-end smartphone with advanced features', '2022-01-10', 500.00, 700.00, 50, '2023-01-10'),
(1, 2, 1, 'Laptop', 'A powerful laptop for business and gaming', '2022-02-20', 1000.00, 1500.00, 25, '2024-02-20'),
(1, 4, 2, 'Coffee Beans', 'Freshly roasted coffee beans from South America', '2021-10-01', 5.00, 10.00, 100, '2022-10-01'),
(2, 5, 3, 'Circuit Board', 'A board containing electronic components', '2022-01-15', 10.00, 20.00, 500, NULL);

INSERT INTO [Staff] ([staff_name])
VALUES ('John Smith'),
('Jane Doe'),
('Bob Johnson'),
('Sarah Lee'),
('Tom Jones'),
('Alice Wang');

INSERT INTO [Customer] ([customer_name], [customer_contact_number])
VALUES ('Mike Davis', '555-1111'),
('Amy Lee', '555-2222'),
('David Kim', '555-3333'),
('Lisa Chen', '555-4444'),
('Robert Smith', '555-5555'),
('Emily Davis', '555-6666');

INSERT INTO [OrderTransaction] ([fk_staff_id], [fk_customer_id], [order_date])
VALUES (1, 2, '2022-04-01'),
(2, 1, '2022-05-15'),
(1, 3, '2022-02-01'),
(2, 2, '2022-03-15');

INSERT INTO [Orders] ([fk_item_id], [quantity], [discount], [price_paid])
VALUES (1, 2, 0.10, 1260.00),
(3, 1, 0.00, 300.00),
(2, 3, 0.15, 3825.00);