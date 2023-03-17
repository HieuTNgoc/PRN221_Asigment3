Create database PostApp

-------------------------------------------------------------------------

create table AppUsers(
	UserID int primary key identity(1,1),
	FullName varchar(50),
	Addresss varchar(255),
	Email varchar(50),
	[Password] varchar(50),
)

create table PostCategories(
	CategoryID int primary key identity(1,1),
	CategoryName varchar(255),
	[Description] nvarchar(max),
)

create table Posts(
	PostID int primary key identity(1,1),
	CreatedDate datetime,
	UpdatedDate datetime,
	Title varchar(255),
	Content nvarchar(max),
	PublishStatus int,
	AuthorID int foreign key references AppUsers(UserID),
	CategoryID int foreign key references PostCategories(CategoryID),
)
