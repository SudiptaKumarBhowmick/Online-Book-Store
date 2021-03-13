IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ProductCategory] (
    [ProductCategroyId] int NOT NULL IDENTITY,
    [CategoryName] varchar(30) NULL,
    [CategoryDescription] varchar(50) NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductCategory] PRIMARY KEY ([ProductCategroyId])
);

GO

CREATE TABLE [RefInvoiceStatusCodes] (
    [InvoiceStatusCode] int NOT NULL,
    [InvoiceStatusDesc] varchar(10) NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_RefInvoiceStatusCodes] PRIMARY KEY ([InvoiceStatusCode])
);

GO

CREATE TABLE [RefOrderItemStatusCodes] (
    [OrderItemStatusCode] int NOT NULL,
    [OrderItemStatusDescription] varchar(15) NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_RefOrderItemStatusCodes] PRIMARY KEY ([OrderItemStatusCode])
);

GO

CREATE TABLE [RefOrderStatusCodes] (
    [OrderStatusCode] int NOT NULL,
    [OrderStatusDescription] varchar(10) NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_RefOrderStatusCodes] PRIMARY KEY ([OrderStatusCode])
);

GO

CREATE TABLE [UserRole] (
    [UserRoleId] int NOT NULL IDENTITY,
    [UserRoleName] varchar(20) NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserRoleId])
);

GO

CREATE TABLE [UserTypes] (
    [UserTypeId] int NOT NULL IDENTITY,
    [UserTypeName] varchar(20) NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_UserTypes] PRIMARY KEY ([UserTypeId])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Products] (
    [ProductId] int NOT NULL IDENTITY,
    [ProductCode] varchar(8) NULL,
    [ProductName] varchar(100) NULL,
    [ProductPrice] decimal(8,3) NOT NULL,
    [ProductImage] nvarchar(max) NULL,
    [ProductCategroyId] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId]),
    CONSTRAINT [FK_Products_ProductCategory_ProductCategroyId] FOREIGN KEY ([ProductCategroyId]) REFERENCES [ProductCategory] ([ProductCategroyId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [User] (
    [UserId] int NOT NULL IDENTITY,
    [FullName] varchar(100) NULL,
    [UserName] varchar(50) NULL,
    [Email] varchar(50) NULL,
    [Password] varchar(50) NULL,
    [PhoneNumber] varchar(15) NULL,
    [UserRoleId] int NULL,
    [UserTypeId] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_User_UserRole_UserRoleId] FOREIGN KEY ([UserRoleId]) REFERENCES [UserRole] ([UserRoleId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_User_UserTypes_UserTypeId] FOREIGN KEY ([UserTypeId]) REFERENCES [UserTypes] ([UserTypeId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ProductDescriptions] (
    [ProductDescId] int NOT NULL IDENTITY,
    [Title] varchar(100) NULL,
    [CategoryName] varchar(30) NULL,
    [AuthorName] varchar(100) NULL,
    [AuthorDescription] varchar(1000) NULL,
    [AuthorImage] nvarchar(max) NULL,
    [ProductSummary] varchar(1000) NULL,
    [PublisherName] varchar(50) NULL,
    [Edition] varchar(50) NULL,
    [NumOfPages] int NOT NULL,
    [Country] varchar(20) NULL,
    [Language] varchar(20) NULL,
    [ProductId] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductDescriptions] PRIMARY KEY ([ProductDescId]),
    CONSTRAINT [FK_ProductDescriptions_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ProductInStocks] (
    [ProductStockId] int NOT NULL IDENTITY,
    [InStock] varchar(3) NULL,
    [Quantity] int NOT NULL,
    [ProductId] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductInStocks] PRIMARY KEY ([ProductStockId]),
    CONSTRAINT [FK_ProductInStocks_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ProductReviews] (
    [ProductReviewId] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [UserName] varchar(50) NULL,
    [Comment] varchar(500) NULL,
    [Review] int NOT NULL,
    [ProductId] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductReviews] PRIMARY KEY ([ProductReviewId]),
    CONSTRAINT [FK_ProductReviews_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [FullName] nvarchar(150) NULL,
    [UserRoleId] int NULL,
    [UserTypeId] int NULL,
    [UserId] int NULL,
    [CreatedAt] datetimeoffset NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUsers_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AspNetUsers_UserRole_UserRoleId] FOREIGN KEY ([UserRoleId]) REFERENCES [UserRole] ([UserRoleId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AspNetUsers_UserTypes_UserTypeId] FOREIGN KEY ([UserTypeId]) REFERENCES [UserTypes] ([UserTypeId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Orders] (
    [OrderId] int NOT NULL IDENTITY,
    [DateOrderPlaced] datetime2 NOT NULL,
    [LocationOrderPlaced] varchar(200) NULL,
    [UserId] int NULL,
    [OrderStatusCode] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId]),
    CONSTRAINT [FK_Orders_RefOrderStatusCodes_OrderStatusCode] FOREIGN KEY ([OrderStatusCode]) REFERENCES [RefOrderStatusCodes] ([OrderStatusCode]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Orders_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [PaymentDetailsUser] (
    [PaymentDetailsUserId] int NOT NULL IDENTITY,
    [Name] varchar(100) NULL,
    [CardNumber] varchar(20) NULL,
    [ExpirationDate] varchar(15) NULL,
    [FullName] varchar(100) NULL,
    [AddressLineOne] varchar(150) NULL,
    [AddressLineTwo] varchar(150) NULL,
    [City] varchar(20) NULL,
    [State] varchar(20) NULL,
    [Zip] varchar(20) NULL,
    [Country] varchar(20) NULL,
    [PhoneNumber] varchar(15) NULL,
    [UserId] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_PaymentDetailsUser] PRIMARY KEY ([PaymentDetailsUserId]),
    CONSTRAINT [FK_PaymentDetailsUser_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [UserDetails] (
    [UserDetailsId] int NOT NULL IDENTITY,
    [FirstName] varchar(100) NULL,
    [LastName] varchar(100) NULL,
    [DateOfBirth] varchar(20) NULL,
    [Address] varchar(100) NULL,
    [City] varchar(20) NULL,
    [Country] varchar(20) NULL,
    [Email] varchar(50) NULL,
    [PhoneNumber] varchar(15) NULL,
    [UserTypeName] varchar(20) NULL,
    [UserId] int NULL,
    [UserTypeId] int NULL,
    [UserRoleId] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_UserDetails] PRIMARY KEY ([UserDetailsId]),
    CONSTRAINT [FK_UserDetails_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UserDetails_UserRole_UserRoleId] FOREIGN KEY ([UserRoleId]) REFERENCES [UserRole] ([UserRoleId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UserDetails_UserTypes_UserTypeId] FOREIGN KEY ([UserTypeId]) REFERENCES [UserTypes] ([UserTypeId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Invoices] (
    [InvoiceId] int NOT NULL IDENTITY,
    [InvoiceNumber] varchar(8) NULL,
    [InvoiceDate] datetime2 NOT NULL,
    [OrderId] int NULL,
    [InvoiceStatusCode] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Invoices] PRIMARY KEY ([InvoiceId]),
    CONSTRAINT [FK_Invoices_RefInvoiceStatusCodes_InvoiceStatusCode] FOREIGN KEY ([InvoiceStatusCode]) REFERENCES [RefInvoiceStatusCodes] ([InvoiceStatusCode]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Invoices_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [OrderItems] (
    [OrderItemId] int NOT NULL IDENTITY,
    [OrderItemQnt] int NOT NULL,
    [OrderItemPrice] decimal(8,3) NOT NULL,
    [TotalAmount] decimal(8,3) NOT NULL,
    [ProductId] int NULL,
    [OrderId] int NULL,
    [OrderItemStatusCode] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([OrderItemId]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrderItems_RefOrderItemStatusCodes_OrderItemStatusCode] FOREIGN KEY ([OrderItemStatusCode]) REFERENCES [RefOrderItemStatusCodes] ([OrderItemStatusCode]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Payments] (
    [PaymentId] int NOT NULL IDENTITY,
    [PaymentDate] datetime2 NOT NULL,
    [PaymentAmount] decimal(8,3) NOT NULL,
    [InvoiceNum] varchar(10) NULL,
    [InvoiceId] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY ([PaymentId]),
    CONSTRAINT [FK_Payments_Invoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices] ([InvoiceId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Shipments] (
    [ShipmentId] int NOT NULL IDENTITY,
    [ShipmentTrackNum] varchar(10) NULL,
    [ShipmentDate] datetime2 NOT NULL,
    [InvoiceNumber] varchar(10) NULL,
    [OrderId] int NULL,
    [InvoiceId] int NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastUpdateAt] datetimeoffset NOT NULL,
    [LastUpdateBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Shipments] PRIMARY KEY ([ShipmentId]),
    CONSTRAINT [FK_Shipments_Invoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices] ([InvoiceId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Shipments_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE UNIQUE INDEX [IX_AspNetUsers_UserId] ON [AspNetUsers] ([UserId]) WHERE [UserId] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUsers_UserRoleId] ON [AspNetUsers] ([UserRoleId]);

GO

CREATE INDEX [IX_AspNetUsers_UserTypeId] ON [AspNetUsers] ([UserTypeId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE UNIQUE INDEX [IX_Invoices_InvoiceStatusCode] ON [Invoices] ([InvoiceStatusCode]) WHERE [InvoiceStatusCode] IS NOT NULL;

GO

CREATE UNIQUE INDEX [IX_Invoices_OrderId] ON [Invoices] ([OrderId]) WHERE [OrderId] IS NOT NULL;

GO

CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);

GO

CREATE INDEX [IX_OrderItems_OrderItemStatusCode] ON [OrderItems] ([OrderItemStatusCode]);

GO

CREATE UNIQUE INDEX [IX_OrderItems_ProductId] ON [OrderItems] ([ProductId]) WHERE [ProductId] IS NOT NULL;

GO

CREATE UNIQUE INDEX [IX_Orders_OrderStatusCode] ON [Orders] ([OrderStatusCode]) WHERE [OrderStatusCode] IS NOT NULL;

GO

CREATE INDEX [IX_Orders_UserId] ON [Orders] ([UserId]);

GO

CREATE UNIQUE INDEX [IX_PaymentDetailsUser_UserId] ON [PaymentDetailsUser] ([UserId]) WHERE [UserId] IS NOT NULL;

GO

CREATE UNIQUE INDEX [IX_Payments_InvoiceId] ON [Payments] ([InvoiceId]) WHERE [InvoiceId] IS NOT NULL;

GO

CREATE UNIQUE INDEX [IX_ProductDescriptions_ProductId] ON [ProductDescriptions] ([ProductId]) WHERE [ProductId] IS NOT NULL;

GO

CREATE UNIQUE INDEX [IX_ProductInStocks_ProductId] ON [ProductInStocks] ([ProductId]) WHERE [ProductId] IS NOT NULL;

GO

CREATE UNIQUE INDEX [IX_ProductReviews_ProductId] ON [ProductReviews] ([ProductId]) WHERE [ProductId] IS NOT NULL;

GO

CREATE INDEX [IX_Products_ProductCategroyId] ON [Products] ([ProductCategroyId]);

GO

CREATE UNIQUE INDEX [IX_Shipments_InvoiceId] ON [Shipments] ([InvoiceId]) WHERE [InvoiceId] IS NOT NULL;

GO

CREATE UNIQUE INDEX [IX_Shipments_OrderId] ON [Shipments] ([OrderId]) WHERE [OrderId] IS NOT NULL;

GO

CREATE INDEX [IX_User_UserRoleId] ON [User] ([UserRoleId]);

GO

CREATE INDEX [IX_User_UserTypeId] ON [User] ([UserTypeId]);

GO

CREATE UNIQUE INDEX [IX_UserDetails_UserId] ON [UserDetails] ([UserId]) WHERE [UserId] IS NOT NULL;

GO

CREATE INDEX [IX_UserDetails_UserRoleId] ON [UserDetails] ([UserRoleId]);

GO

CREATE INDEX [IX_UserDetails_UserTypeId] ON [UserDetails] ([UserTypeId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201023124852_InitialMigration', N'3.1.8');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shipments]') AND [c].[name] = N'InvoiceNumber');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Shipments] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Shipments] DROP COLUMN [InvoiceNumber];

GO

ALTER TABLE [Shipments] ADD [AddressLineOne] varchar(1000) NULL;

GO

ALTER TABLE [Shipments] ADD [AddressLineTwo] varchar(1000) NULL;

GO

ALTER TABLE [Shipments] ADD [City] varchar(20) NULL;

GO

ALTER TABLE [Shipments] ADD [Country] varchar(50) NULL;

GO

ALTER TABLE [Shipments] ADD [FullName] varchar(100) NULL;

GO

ALTER TABLE [Shipments] ADD [State] varchar(30) NULL;

GO

ALTER TABLE [Shipments] ADD [Zip] varchar(20) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201026050201_ShipmentTableUpdateMigrations', N'3.1.8');

GO

