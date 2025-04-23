/*******************************************************************************
   Drop database if it exists
********************************************************************************/
IF EXISTS (SELECT name
FROM master.dbo.sysdatabases
WHERE name = N'JAZA')
BEGIN
    ALTER DATABASE [JAZA] SET OFFLINE WITH ROLLBACK IMMEDIATE;
    ALTER DATABASE [JAZA] SET ONLINE;
    DROP DATABASE [JAZA];
END

GO

/*******************************************************************************
   Create database
********************************************************************************/
CREATE DATABASE [JAZA];
GO

USE [JAZA];
GO

/*******************************************************************************
   Create Tables
********************************************************************************/
CREATE TABLE [Users]
(
    UserID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FName NVARCHAR(50) NOT NULL,
    LName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(15),
    IsAdmin BIT NOT NULL DEFAULT 0,
    -- IsAdmin (0 = No, 1 = Yes)
    IsActive BIT NOT NULL DEFAULT 1
    -- IsActive (0 = Inactive, 1 = Active)
);
GO


CREATE TABLE [Property]
(
    PropertyID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Country NVARCHAR(50) NOT NULL,
    State NVARCHAR(50) NOT NULL,
    City NVARCHAR(50) NOT NULL,
    ZipCode NVARCHAR(10) NOT NULL,
    StreetAddress NVARCHAR(100) NOT NULL,
    StartingPrice DECIMAL(18, 2) NOT NULL,
    ListDate DATETIME NOT NULL DEFAULT GETDATE(),
    OwnerID UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (OwnerID) REFERENCES [Users](UserID)
);
GO

CREATE TABLE [favorites]
(
    UserID UNIQUEIDENTIFIER NOT NULL,
    PropertyID UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (UserID, PropertyID),
    FOREIGN KEY (UserID) REFERENCES [Users](UserID),
    FOREIGN KEY (PropertyID) REFERENCES [Property](PropertyID)
);

CREATE TABLE [Purchases]
(
    PurchaseID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserID UNIQUEIDENTIFIER NOT NULL,
    PropertyID UNIQUEIDENTIFIER NOT NULL,
    FinalPrice DECIMAL(18, 2) NOT NULL,
    PurchaseDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES [Users](UserID),
    FOREIGN KEY (PropertyId) REFERENCES [Property](PropertyID)
)
GO

CREATE TABLE [Credentials]
(
    UserID UNIQUEIDENTIFIER PRIMARY KEY,
    UserToken NVARCHAR(255) NOT NULL,
    FOREIGN KEY (UserID) REFERENCES [Users](UserID)
)
GO

CREATE Table [Offers]
(
    OfferID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserID UNIQUEIDENTIFIER NOT NULL,
    PropertyID UNIQUEIDENTIFIER NOT NULL,
    OfferPrice DECIMAL(18, 2) NOT NULL,
    OfferDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES [Users](UserID),
    FOREIGN KEY (PropertyID) REFERENCES [Property](PropertyID)
)
GO


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
