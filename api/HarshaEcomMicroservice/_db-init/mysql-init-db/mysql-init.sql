-- Create the database if not defined in mysql docker image environment variable
CREATE DATABASE IF NOT EXISTS HarshaEcomProdMgmtDB;
USE HarshaEcomProdMgmtDB;

-- Drop the existing table if it exists (to recreate with new schema)
DROP TABLE IF EXISTS Products;

-- Create the products table with tinyint category
CREATE TABLE IF NOT EXISTS Products (
  ProductID char(36) NOT NULL,
  ProductName varchar(50) NOT NULL,
  Category tinyint DEFAULT 0,  -- 0 = Undefined, 1 = Electronics, 2 = HomeAppliances, 3 = Furniture, 4 = Accessories
  UnitPrice decimal(10,2) DEFAULT NULL,
  QuantityInStock int DEFAULT NULL,
  PRIMARY KEY (ProductID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Insert 12 sample rows into the products table with integer category values
INSERT INTO Products (ProductID, ProductName, Category, UnitPrice, QuantityInStock) VALUES
  ('1a9df78b-3f46-4c3d-9f2a-1b9f69292a77', 'Apple iPhone 15 Pro Max', 1, 1299.99, 50),           -- Electronics
  ('2c8e8e7c-97a3-4b11-9a1b-4dbe681cfe17', 'Samsung Foldable Smart Phone 2', 1, 1499.99, 100),    -- Electronics
  ('3f3e8b3a-4a50-4cd0-8d8e-1e178ae2cfc1', 'Ergonomic Office Chair', 3, 249.99, 25),              -- Furniture
  ('4c9b6f71-6c5d-485f-8db2-58011a236b63', 'Coffee Table with Storage', 3, 179.99, 30),            -- Furniture
  ('5d7e36bf-65c3-4a71-bf97-740d561d8b65', 'Samsung QLED 75 inch', 1, 1999.99, 20),               -- Electronics
  ('6a14f510-72c1-42c8-9a5a-8ef8f3f45a0d', 'Running Shoes', 4, 49.99, 75),                        -- Accessories (corrected from Furniture)
  ('7b39ef14-932b-4c84-9187-55b748d2b28f', 'Anti-Theft Laptop Backpack', 4, 59.99, 60),           -- Accessories
  ('8c5f6e73-68fc-49d9-99b4-aecc3706a4f4', 'LG OLED 65 inch', 1, 1499.99, 15),                    -- Electronics
  ('9e7e7085-6f4e-4921-8f15-c59f084080f9', 'Modern Dining Table', 3, 699.99, 10),                 -- Furniture
  ('10d7b110-ecdb-4921-85a4-58a5d1b32bf4', 'PlayStation 5', 1, 499.99, 40),                       -- Electronics
  ('11f2e86a-9d5d-42f9-b3c2-3e4d652e3df8', 'Executive Office Desk', 3, 299.99, 18),               -- Furniture
  ('12b369b7-9101-41b1-a653-6c6c9a4fe1e4', 'Breville Smart Blender', 2, 129.99, 50);              -- HomeAppliances