CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY,
    LastName NVARCHAR(128) NOT NULL,
    FirstName NVARCHAR(128) NOT NULL
);

CREATE TABLE Timesheet (
    Id INT PRIMARY KEY IDENTITY,
    Employee INT NOT NULL,
    Reason INT NOT NULL,
    StartDate DATE NOT NULL,
    Duration INT NOT NULL,
    Discounted BIT NOT NULL,
    Description NVARCHAR(1024),
    FOREIGN KEY (Employee) REFERENCES Employees(Id)
);
