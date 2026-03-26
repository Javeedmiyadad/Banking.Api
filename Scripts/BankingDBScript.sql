CREATE DATABASE BankingDB;
GO
 
USE BankingDB;
GO
 
CREATE TABLE Accounts
(
    AccountId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(150) NOT NULL,
    Balance DECIMAL(18,2) NOT NULL DEFAULT 0,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE()
);
 
CREATE TABLE Transactions
(
    TransactionId INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    TransactionType NVARCHAR(50) NOT NULL,
    TransactionDate DATETIME2 NOT NULL DEFAULT GETDATE(),
 
    CONSTRAINT FK_Transactions_Accounts
        FOREIGN KEY (AccountId)
        REFERENCES Accounts(AccountId)
);
 
 
CREATE INDEX IX_Transactions_AccountId
ON Transactions(AccountId);
 
CREATE INDEX IX_Transactions_Date
ON Transactions(TransactionDate);
 
 
ALTER TABLE Transactions
ADD CONSTRAINT CK_TransactionType
CHECK (TransactionType IN ('Deposit','Withdraw'));