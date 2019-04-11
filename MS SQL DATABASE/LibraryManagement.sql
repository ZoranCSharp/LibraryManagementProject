CREATE TABLE [Member] (
[MemberID] int NOT NULL IDENTITY(1,1),
[FullName] nvarchar(50) NOT NULL,
[Email] nvarchar(50) NOT NULL,
[Phone] nvarchar(50) NOT NULL,
[MembershipTypeID] int,
PRIMARY KEY ([MemberID]) 
)
GO
CREATE INDEX [MembershipTypeFK] ON [Member] ([MembershipTypeID] ASC)
GO

CREATE TABLE [MembershipType] (
[MembershipTypeID] int NOT NULL IDENTITY(1,1),
[Name] nvarchar(50) NOT NULL,
[Price] decimal(8,2) NOT NULL,
PRIMARY KEY ([MembershipTypeID]) 
)
GO
CREATE TABLE [Publisher] (
[PublisherID] int NOT NULL IDENTITY(1,1),
[Name] nvarchar(50) NOT NULL,
PRIMARY KEY ([PublisherID]) 
)
GO
CREATE TABLE [Book] (
[BookID] int NOT NULL IDENTITY(1,1),
[Title] nvarchar(50) NOT NULL,
[Quantity] int NOT NULL,
[PublisherID] int,
[GenreID] int,
[LibraryID] int,
PRIMARY KEY ([BookID]) 
)
GO
CREATE INDEX [PublisherFK] ON [Book] ([PublisherID] ASC)
GO
CREATE INDEX [GenreFK] ON [Book] ([GenreID] ASC)
GO
CREATE INDEX [LibraryFK] ON [Book] ([LibraryID] ASC)
GO

CREATE TABLE [Genre] (
[GenreID] int NOT NULL IDENTITY(1,1),
[Name] nvarchar(50) NOT NULL,
PRIMARY KEY ([GenreID]) 
)
GO
CREATE TABLE [Role] (
[RoleID] int NOT NULL IDENTITY(1,1),
[Name] nvarchar(50) NOT NULL,
PRIMARY KEY ([RoleID]) 
)
GO
CREATE TABLE [Employee] (
[EmployeeID] int NOT NULL IDENTITY(1,1),
[FullName] nvarchar(50) NOT NULL,
[Email] nvarchar(50) NOT NULL,
[RoleID] int,
[LibraryID] int,
PRIMARY KEY ([EmployeeID]) 
)
GO
CREATE INDEX [RoleFK] ON [Employee] ([RoleID] ASC)
GO
CREATE INDEX [LibraryFK2] ON [Employee] ([LibraryID] ASC)
GO

CREATE TABLE [Issued] (
[IssueID] int NOT NULL,
[IssuedDate] date NOT NULL,
[ReturnDate] date NOT NULL,
[BookID] int,
[EmployeeID] int,
[MemberID] int,
PRIMARY KEY ([IssueID]) 
)
GO
CREATE INDEX [BookFK] ON [Issued] ([BookID] ASC)
GO
CREATE INDEX [EmployeeFK] ON [Issued] ([EmployeeID] ASC)
GO
CREATE INDEX [MemberFK] ON [Issued] ([MemberID] ASC)
GO

CREATE TABLE [Library] (
[LibraryID] int NOT NULL IDENTITY(1,1),
[Name] nvarchar(50) NOT NULL,
[City] nvarchar(50) NOT NULL,
[State] nvarchar(50) NOT NULL,
[Zip] int NOT NULL,
PRIMARY KEY ([LibraryID]) 
)
GO

ALTER TABLE [Book] ADD CONSTRAINT [PublisherFK] FOREIGN KEY ([PublisherID]) REFERENCES [Publisher] ([PublisherID]) ON DELETE SET NULL
GO
ALTER TABLE [Book] ADD CONSTRAINT [GenreFK] FOREIGN KEY ([GenreID]) REFERENCES [Genre] ([GenreID]) ON DELETE SET NULL
GO
ALTER TABLE [Book] ADD CONSTRAINT [LibraryFK] FOREIGN KEY ([LibraryID]) REFERENCES [Library] ([LibraryID]) ON DELETE SET NULL
GO
ALTER TABLE [Employee] ADD CONSTRAINT [RoleFK] FOREIGN KEY ([RoleID]) REFERENCES [Role] ([RoleID]) ON DELETE SET NULL
GO
ALTER TABLE [Employee] ADD CONSTRAINT [LibraryFK2] FOREIGN KEY ([LibraryID]) REFERENCES [Library] ([LibraryID]) ON DELETE SET NULL
GO
ALTER TABLE [Member] ADD CONSTRAINT [MembershipTypeFK] FOREIGN KEY ([MembershipTypeID]) REFERENCES [MembershipType] ([MembershipTypeID]) ON DELETE SET NULL
GO
ALTER TABLE [Issued] ADD CONSTRAINT [BookFK] FOREIGN KEY ([BookID]) REFERENCES [Book] ([BookID]) ON DELETE SET NULL
GO
ALTER TABLE [Issued] ADD CONSTRAINT [EmployeeFK] FOREIGN KEY ([EmployeeID]) REFERENCES [Employee] ([EmployeeID]) ON DELETE SET NULL
GO
ALTER TABLE [Issued] ADD CONSTRAINT [MemberFK] FOREIGN KEY ([MemberID]) REFERENCES [Member] ([MemberID]) ON DELETE SET NULL
GO

