create database Member
use Member

CREATE TABLE Customer(
	ID int IDENTITY(1, 1) NOT NULL,
	FirstName nvarchar(100) NULL,
	LastName nvarchar(100) NULL,
	DateCreated DATETIME NOT NULL DEFAULT(GetUtcDate()),
	DateLastModified DATETIME NOT NULL DEFAULT(GetUtcDate())
	PRIMARY KEY(ID)
)

INSERT INTO Customer(FirstName, LastName) VALUES
('Jin', 'Xiao'),
('Tessa', 'Konkol'),
('Serkan', 'Nizam'),
('Jerry', 'Bony'),
('Jimmy', 'Ellis')

CREATE TABLE Courses(
	Name nvarchar(100) NOT NULL,
	[Description] ntext NULL,
	DateCreated DATETIME NOT NULL DEFAULT(GetUtcDate()),
	DateLastModified DATETIME NOT NULL DEFAULT(GetUtcDate())
	PRIMARY KEY(Name)
)