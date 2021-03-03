CREATE TABLE [dbo].[Jobs]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [DueDate] DATE NOT NULL, 
    [OtherDetails] NVARCHAR(MAX) NOT NULL, 
    [Status] INT NOT NULL, 
    [EmployeeID] INT NOT NULL, 
    [PermitID] INT NOT NULL, 
    CONSTRAINT [EmployeeFK] FOREIGN KEY ([EmployeeID]) REFERENCES [Employee]([Id]), 
    CONSTRAINT [PermitFK] FOREIGN KEY ([PermitID]) REFERENCES [Permit]([Id])
)
