

/*******************************************************************************
   Populate Tables
********************************************************************************/

INSERT INTO [Users]
    (FName, LName, Email, PhoneNumber, IsAdmin, IsActive)
VALUES
    ('is', 'admin', 'admin@example.com', '123-456-7890', 1, 0),
    ('is', 'banned', 'banned@example.com', '123-456-7890', 0, 0),
    ('John', 'Doe', 'john.doe@example.com', '123-456-7890', 0, 1),
    ('Jane', 'Smith', 'jane.smith@example.com', '098-765-4321', 0, 1),
    ('Alice', 'Johnson', 'alice.johnson@example.com', '555-123-4567', 0, 1),
    ('Bob', 'Brown', 'bob.brown@example.com', '555-987-6543', 0, 1);

INSERT INTO [Property]
    (Country, State, City, ZipCode, StreetAddress, StartingPrice, ListDate, OwnerID)
VALUES
    ('USA', 'California', 'Los Angeles', '90001', '123 Main St', 500000.00, GETDATE(), (SELECT TOP 1
            UserID
        FROM Users)),
    ('USA', 'New York', 'New York', '10001', '456 Elm St', 750000.00, GETDATE(), (SELECT TOP 1
            UserID
        FROM Users )),
    ('USA', 'Texas', 'Houston', '77001', '789 Oak St', 300000.00, GETDATE(), (SELECT TOP 1
            UserID
        FROM Users)),
    ('USA', 'Florida', 'Miami', '33101', '321 Pine St', 400000.00, GETDATE(), (SELECT TOP 1
            UserID
        FROM Users));
