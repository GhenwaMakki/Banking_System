# Banking_System

docker containers: 
keycloak: docker run -d --name keycloak -p 8080:8080 -e KEYCLOAK_USER=admin -e KEYCLOAK_PASSWORD=admin quay.io/keycloak/keycloak:latest

--> http://localhost:8080

postgres:ocker run --rm  --name Postgres -e POSTGRES_PASSWORD=123456 -d -p 5432:5432 postgres:14.9

Database:

--USER DB

CREATE TABLE Branches (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE Users (
    Id SERIAL PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(50) NOT NULL,  -- Admin, Employee, Customer
    BranchId INT,
    FOREIGN KEY (BranchId) REFERENCES Branches(Id)
);

--Account DB

CREATE TABLE Accounts (
    Id SERIAL PRIMARY KEY,
    CustomerId INT NOT NULL,
    BranchId INT NOT NULL,
    AccountType VARCHAR(50) NOT NULL,
    Balance DECIMAL(18, 2) NOT NULL DEFAULT 0,
    FOREIGN KEY (CustomerId) REFERENCES Users(Id),
    FOREIGN KEY (BranchId) REFERENCES Branches(Id)
);

CREATE TABLE Users (
    Id SERIAL PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(50) NOT NULL,  -- Admin, Employee, Customer
    BranchId INT,
    FOREIGN KEY (BranchId) REFERENCES Branches(Id)
);

--Transaction DB

CREATE TABLE Accounts (
    Id BIGINT PRIMARY KEY, 
    AccountNumber VARCHAR(50) NOT NULL UNIQUE, 
    Balance DECIMAL(18, 2) NOT NULL DEFAULT 0 
);

CREATE TABLE RecurrentTransactions (
    Id BIGINT PRIMARY KEY,  
    AccountId BIGINT NOT NULL, 
    TransactionType VARCHAR(50) CHECK (TransactionType IN ('Withdrawal', 'Deposit')) NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    Interval VARCHAR(50) CHECK (Interval IN ('Daily', 'Weekly', 'Monthly')) NOT NULL,
    NextExecutionDate TIMESTAMP, 
    FOREIGN KEY (AccountId) REFERENCES Accounts(Id) ON DELETE CASCADE
);

CREATE TABLE Transactions (
    Id BIGINT PRIMARY KEY, 
    AccountId BIGINT NOT NULL, 
    TransactionType VARCHAR(50) CHECK (TransactionType IN ('Withdrawal', 'Deposit')) NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,  
    FOREIGN KEY (AccountId) REFERENCES Accounts(Id) ON DELETE CASCADE
);







