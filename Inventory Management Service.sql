CREATE DATABASE invetory_manager

GO

USE invetory_manager

GO

CREATE TABLE [Company] (
  [company_id] INT PRIMARY KEY IDENTITY(1, 1),
  [company_name] VARCHAR(20),
  [company_description] VARCHAR(250),
  [created_date] DATE
)
GO

CREATE TABLE [Supplier] (
  [supplier_id] INT PRIMARY KEY IDENTITY(1, 1),
  [supplier_name] VARCHAR(20),
  [supplier_contact_number] VARCHAR(20),
  [created_date] DATE
)
GO

CREATE TABLE [ItemCategory] (
  [item_category_id] INT PRIMARY KEY IDENTITY(1, 1),
  [item_category_name] VARCHAR(20),
  [item_category_description] VARCHAR(250)
)
GO

CREATE TABLE [Item] (
  [item_id] INT PRIMARY KEY IDENTITY(1, 1),
  [fk_company_id] INT,
  [fk_supplier_id] INT,
  [fk_item_category_id] INT,
  [item_name] VARCHAR(25),
  [item_description] VARCHAR(250),
  [acquired_date] DATE,
  [cost_price] DECIMAL(12, 2),
  [sell_price] DECIMAL(12, 2),
  [quantity] INT,
  [expiry_date] DATE
)
GO

CREATE TABLE [Staff] (
  [staff_id] INT PRIMARY KEY IDENTITY(1, 1),
  [staff_name] VARCHAR(25)
)
GO

CREATE TABLE [Customer] (
  [customer_id] INT PRIMARY KEY IDENTITY(1, 1),
  [customer_name] VARCHAR(25),
  [customer_contact_number] VARCHAR(25)
)
GO

CREATE TABLE [OrderTransaction] (
  [transaction_id] INT PRIMARY KEY IDENTITY(1, 1),
  [fk_staff_id] INT,
  [fk_customer_id] INT,
  [order_date] DATE
)
GO

CREATE TABLE [Orders] (
  [order_id] INT PRIMARY KEY IDENTITY(1, 1),
  [fk_item_id] INT,
  [quantity] INT,
  [discount] DECIMAL(12, 2),
  [price_paid] DECIMAL(12, 2)
)
GO

ALTER TABLE [Item] ADD FOREIGN KEY ([fk_company_id]) REFERENCES [Company] ([company_id])
GO

ALTER TABLE [Item] ADD FOREIGN KEY ([fk_supplier_id]) REFERENCES [Supplier] ([supplier_id])
GO

ALTER TABLE [Item] ADD FOREIGN KEY ([fk_item_category_id]) REFERENCES [ItemCategory] ([item_category_id])
GO

ALTER TABLE [Bill] ADD FOREIGN KEY ([fk_staff_id]) REFERENCES [Staff] ([staff_id])
GO

ALTER TABLE [Bill] ADD FOREIGN KEY ([fk_customer_id]) REFERENCES [Customer] ([customer_id])
GO

ALTER TABLE [Order] ADD FOREIGN KEY ([fk_item_id]) REFERENCES [Item] ([item_id])
GO

