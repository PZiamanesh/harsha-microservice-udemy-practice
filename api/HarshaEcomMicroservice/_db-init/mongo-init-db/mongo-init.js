var orders = [
    {
        _id: UUID("4d9b6010-48e6-4a0e-bd4e-461c85c32c1d"),
        OrderID: UUID("4d9b6010-48e6-4a0e-bd4e-461c85c32c1d"),
        UserID: UUID("c32f8b42-60e6-4c02-90a7-9143ab37189f"),
        OrderDate: ISODate("2050-10-20T08:00:00Z"),
        TotalBill: NumberDecimal("2799.98"),
        OrderItems: [
            {
                _id: UUID('fd89d1d7-6922-4f7b-96c3-aac3a35df7fd'),
                ProductID: UUID("1a9df78b-3f46-4c3d-9f2a-1b9f69292a77"),
                UnitPrice: NumberDecimal("1299.99"),
                Quantity: 1,
                TotalPrice: NumberDecimal("1299.99")
            },
            {
                _id: UUID('fd89d1d7-6922-4f7b-96c3-aac3a35df7fd'),
                ProductID: UUID("2c8e8e7c-97a3-4b11-9a1b-4dbe681cfe17"),
                UnitPrice: NumberDecimal("1499.99"),
                Quantity: 1,
                TotalPrice: NumberDecimal("1499.99")
            }
        ]
    },
    {
        _id: UUID("62c2fb9c-b36e-497e-b0b7-f07c6c3c22b2"),
        OrderID: UUID("62c2fb9c-b36e-497e-b0b7-f07c6c3c22b2"),
        UserID: UUID("8ff22c7d-18c7-4ef0-a0ac-988ecb2ac7f5"),
        OrderDate: ISODate("2050-10-21T09:00:00Z"),
        TotalBill: NumberDecimal("969.95"),
        OrderItems: [
            {
                _id: UUID('fd89d1d7-6922-4f7b-96c3-aac3a35df7fd'),
                ProductID: UUID("3f3e8b3a-4a50-4cd0-8d8e-1e178ae2cfc1"),
                UnitPrice: NumberDecimal("249.99"),
                Quantity: 1,
                TotalPrice: NumberDecimal("249.99")
            },
            {
                _id: UUID('fd89d1d7-6922-4f7b-96c3-aac3a35df7fd'),
                ProductID: UUID("4c9b6f71-6c5d-485f-8db2-58011a236b63"),
                UnitPrice: NumberDecimal("179.99"),
                Quantity: 4,
                TotalPrice: NumberDecimal("719.96")
            }
        ]
    },
    {
        _id: UUID("e3f6d6b7-bc84-48e3-8d22-961e1e084f0e"),
        OrderID: UUID("e3f6d6b7-bc84-48e3-8d22-961e1e084f0e"),
        UserID: UUID("8ff22c7d-18c7-4ef0-a0ac-988ecb2ac7f5"),
        OrderDate: ISODate("2050-10-22T10:00:00Z"),
        TotalBill: NumberDecimal("5499.88"),
        OrderItems: [
            {
                _id: UUID('fd89d1d7-6922-4f7b-96c3-aac3a35df7fd'),
                ProductID: UUID("3f3e8b3a-4a50-4cd0-8d8e-1e178ae2cfc1"),
                UnitPrice: NumberDecimal("249.99"),
                Quantity: 10,
                TotalPrice: NumberDecimal("2499.90")
            },
            {
                _id: UUID('fd89d1d7-6922-4f7b-96c3-aac3a35df7fd'),
                ProductID: UUID("2c8e8e7c-97a3-4b11-9a1b-4dbe681cfe17"),
                UnitPrice: NumberDecimal("1499.99"),
                Quantity: 2,
                TotalPrice: NumberDecimal("2999.98")
            }
        ]
    }
];

console.log(orders);

//Switch to your database
var db = db.getSiblingDB("HarshaEcomOrderMgmtDB");

db.orders.insertMany(orders);
