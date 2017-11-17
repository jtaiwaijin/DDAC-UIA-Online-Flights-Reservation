IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] uniqueidentifier NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max),
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [Airports] (
        [AirportID] int NOT NULL IDENTITY,
        [City] nvarchar(max),
        [Country] nvarchar(max),
        [IATA] nvarchar(max),
        [Name] nvarchar(max),
        [Timezone] float NOT NULL,
        CONSTRAINT [PK_Airports] PRIMARY KEY ([AirportID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] uniqueidentifier NOT NULL,
        [ConcurrencyStamp] nvarchar(max),
        [Name] nvarchar(256),
        [NormalizedName] nvarchar(256),
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] uniqueidentifier NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [ConcurrencyStamp] nvarchar(max),
        [Discriminator] nvarchar(max) NOT NULL,
        [Email] nvarchar(256),
        [EmailConfirmed] bit NOT NULL,
        [Gender] nvarchar(1) NOT NULL,
        [LockoutEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset,
        [Name] nvarchar(100) NOT NULL,
        [NormalizedEmail] nvarchar(256),
        [NormalizedUserName] nvarchar(256),
        [PasswordHash] nvarchar(max),
        [Phone] nvarchar(12) NOT NULL,
        [PhoneNumber] nvarchar(max),
        [PhoneNumberConfirmed] bit NOT NULL,
        [SecurityStamp] nvarchar(max),
        [TwoFactorEnabled] bit NOT NULL,
        [UserName] nvarchar(256),
        [AddLine1] nvarchar(max),
        [AddLine2] nvarchar(max),
        [City] nvarchar(max),
        [Country] nvarchar(max),
        [DOB] datetime2,
        [Postcode] int,
        [Region] nvarchar(max),
        [Nationality] nvarchar(max),
        [PassportNo] nvarchar(max),
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [Planes] (
        [ID] int NOT NULL IDENTITY,
        [BusCap] int NOT NULL,
        [EconCap] int NOT NULL,
        [FirstCap] int NOT NULL,
        [Make] nvarchar(max),
        [Model] nvarchar(max),
        [TailNo] nvarchar(max),
        CONSTRAINT [PK_Planes] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [ClaimType] nvarchar(max),
        [ClaimValue] nvarchar(max),
        [RoleId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [ClaimType] nvarchar(max),
        [ClaimValue] nvarchar(max),
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max),
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [Bookings] (
        [ID] int NOT NULL IDENTITY,
        [CustomerId] uniqueidentifier NOT NULL,
        [Status] nvarchar(max),
        CONSTRAINT [PK_Bookings] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Bookings_AspNetUsers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [Flights] (
        [ID] int NOT NULL IDENTITY,
        [ArrivalDateTime] datetime2 NOT NULL,
        [DepartureDateTime] datetime2 NOT NULL,
        [DestAirportID] int NOT NULL,
        [FlightNo] nvarchar(max),
        [OriginAirportID] int NOT NULL,
        [PlaneID] int NOT NULL,
        CONSTRAINT [PK_Flights] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Flights_Airports_DestAirportID] FOREIGN KEY ([DestAirportID]) REFERENCES [Airports] ([AirportID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Flights_Airports_OriginAirportID] FOREIGN KEY ([OriginAirportID]) REFERENCES [Airports] ([AirportID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Flights_Planes_PlaneID] FOREIGN KEY ([PlaneID]) REFERENCES [Planes] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [Payments] (
        [ID] int NOT NULL IDENTITY,
        [Amount] money NOT NULL,
        [BookingID] int NOT NULL,
        CONSTRAINT [PK_Payments] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Payments_Bookings_BookingID] FOREIGN KEY ([BookingID]) REFERENCES [Bookings] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [Tariffs] (
        [ID] int NOT NULL IDENTITY,
        [AdultFare] money NOT NULL,
        [CabinCl] nvarchar(max),
        [FlightID] int NOT NULL,
        CONSTRAINT [PK_Tariffs] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Tariffs_Flights_FlightID] FOREIGN KEY ([FlightID]) REFERENCES [Flights] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE TABLE [Tickets] (
        [ID] int NOT NULL IDENTITY,
        [BookingID] int,
        [CabinCl] nvarchar(max),
        [FlightID] int NOT NULL,
        [PassengerId] uniqueidentifier NOT NULL,
        [Price] money NOT NULL,
        CONSTRAINT [PK_Tickets] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Tickets_Bookings_BookingID] FOREIGN KEY ([BookingID]) REFERENCES [Bookings] ([ID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Tickets_Flights_FlightID] FOREIGN KEY ([FlightID]) REFERENCES [Flights] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Tickets_AspNetUsers_PassengerId] FOREIGN KEY ([PassengerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [IX_Bookings_CustomerId] ON [Bookings] ([CustomerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [IX_Flights_DestAirportID] ON [Flights] ([DestAirportID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [IX_Flights_OriginAirportID] ON [Flights] ([OriginAirportID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [IX_Flights_PlaneID] ON [Flights] ([PlaneID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [IX_Payments_BookingID] ON [Payments] ([BookingID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [IX_Tariffs_FlightID] ON [Tariffs] ([FlightID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [IX_Tickets_BookingID] ON [Tickets] ([BookingID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [IX_Tickets_FlightID] ON [Tickets] ([FlightID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    CREATE INDEX [IX_Tickets_PassengerId] ON [Tickets] ([PassengerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101024739_initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171101024739_initial', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101025312_updatecode')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171101025312_updatecode', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101034034_removephone')
BEGIN
    ALTER TABLE [Tickets] DROP CONSTRAINT [FK_Tickets_AspNetUsers_PassengerId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101034034_removephone')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetUsers') AND [c].[name] = N'Phone');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Phone];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101034034_removephone')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetUsers') AND [c].[name] = N'Nationality');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Nationality];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101034034_removephone')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetUsers') AND [c].[name] = N'PassportNo');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [PassportNo];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101034034_removephone')
BEGIN
    DROP INDEX [IX_Tickets_PassengerId] ON [Tickets];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101034034_removephone')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Tickets') AND [c].[name] = N'PassengerId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Tickets] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Tickets] DROP COLUMN [PassengerId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101034034_removephone')
BEGIN
    ALTER TABLE [Tickets] ADD [PassengerId] int NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101034034_removephone')
BEGIN
    CREATE INDEX [IX_Tickets_PassengerId] ON [Tickets] ([PassengerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101034034_removephone')
BEGIN
    CREATE TABLE [Passengers] (
        [Id] int NOT NULL IDENTITY,
        [DOB] datetime2 NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Gender] nvarchar(1) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Nationality] nvarchar(max),
        [PassportNo] nvarchar(max),
        [Phone] nvarchar(12) NOT NULL,
        CONSTRAINT [PK_Passengers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101034034_removephone')
BEGIN
    ALTER TABLE [Tickets] ADD CONSTRAINT [FK_Tickets_Passengers_PassengerId] FOREIGN KEY ([PassengerId]) REFERENCES [Passengers] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101034034_removephone')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171101034034_removephone', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101115904_bookingtotal')
BEGIN
    ALTER TABLE [Bookings] ADD [Total] money NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101115904_bookingtotal')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171101115904_bookingtotal', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101120100_bookingtotalnullable')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Bookings') AND [c].[name] = N'Total');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Bookings] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Bookings] ALTER COLUMN [Total] money;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101120100_bookingtotalnullable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171101120100_bookingtotalnullable', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101182751_passengerRequired')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Passengers') AND [c].[name] = N'PassportNo');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Passengers] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Passengers] ALTER COLUMN [PassportNo] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101182751_passengerRequired')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Passengers') AND [c].[name] = N'Nationality');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Passengers] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Passengers] ALTER COLUMN [Nationality] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171101182751_passengerRequired')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171101182751_passengerRequired', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102075610_bookingdate')
BEGIN
    ALTER TABLE [Bookings] ADD [BookingDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102075610_bookingdate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171102075610_bookingdate', N'1.1.2');
END;

GO

