IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260126075021_initial migration'
)
BEGIN
    CREATE TABLE [Difficulties] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Difficulties] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260126075021_initial migration'
)
BEGIN
    CREATE TABLE [Regions] (
        [Id] uniqueidentifier NOT NULL,
        [Code] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [RegionImageUrl] nvarchar(max) NULL,
        CONSTRAINT [PK_Regions] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260126075021_initial migration'
)
BEGIN
    CREATE TABLE [Walks] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [LengthInKm] nvarchar(max) NOT NULL,
        [WalkImageUrl] nvarchar(max) NOT NULL,
        [RegionId] uniqueidentifier NOT NULL,
        [DifficultyId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Walks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Walks_Difficulties_DifficultyId] FOREIGN KEY ([DifficultyId]) REFERENCES [Difficulties] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Walks_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260126075021_initial migration'
)
BEGIN
    CREATE INDEX [IX_Walks_DifficultyId] ON [Walks] ([DifficultyId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260126075021_initial migration'
)
BEGIN
    CREATE INDEX [IX_Walks_RegionId] ON [Walks] ([RegionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260126075021_initial migration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260126075021_initial migration', N'10.0.2');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260128155856_initial 2'
)
BEGIN
    EXEC(N'DELETE FROM [Difficulties]
    WHERE [Id] = ''778e48f0-0ced-422b-94ae-488ceb3013bd'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260128155856_initial 2'
)
BEGIN
    EXEC(N'DELETE FROM [Difficulties]
    WHERE [Id] = ''964c558a-a288-476c-bc07-273682ed410c'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260128155856_initial 2'
)
BEGIN
    EXEC(N'DELETE FROM [Difficulties]
    WHERE [Id] = ''c43eb1fd-b703-4392-9332-2cd362feeb84'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260128155856_initial 2'
)
BEGIN
    EXEC(N'DELETE FROM [Regions]
    WHERE [Id] = ''14ceba71-4b51-4777-9b17-46602cf66153'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260128155856_initial 2'
)
BEGIN
    EXEC(N'DELETE FROM [Regions]
    WHERE [Id] = ''6884f7d7-ad1f-4101-8df3-7a6fa7387d81'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260128155856_initial 2'
)
BEGIN
    EXEC(N'DELETE FROM [Regions]
    WHERE [Id] = ''906cb139-415a-4bbb-a174-1a1faf9fb1f6'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260128155856_initial 2'
)
BEGIN
    EXEC(N'DELETE FROM [Regions]
    WHERE [Id] = ''cfa06ed2-bf65-4b65-93ed-c9d286ddb0de'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260128155856_initial 2'
)
BEGIN
    EXEC(N'DELETE FROM [Regions]
    WHERE [Id] = ''f077a22e-4248-4bf6-b564-c7cf4e250263'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260128155856_initial 2'
)
BEGIN
    EXEC(N'DELETE FROM [Regions]
    WHERE [Id] = ''f7248fc3-2585-4efb-8d1d-1c555f4087f6'';
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260128155856_initial 2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260128155856_initial 2', N'10.0.2');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260128155957_seed difficulties and regions'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Difficulties]'))
        SET IDENTITY_INSERT [Difficulties] ON;
    EXEC(N'INSERT INTO [Difficulties] ([Id], [Name])
    VALUES (''778e48f0-0ced-422b-94ae-488ceb3013bd'', N''Medium''),
    (''964c558a-a288-476c-bc07-273682ed410c'', N''Easy''),
    (''c43eb1fd-b703-4392-9332-2cd362feeb84'', N''Hard'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Difficulties]'))
        SET IDENTITY_INSERT [Difficulties] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260128155957_seed difficulties and regions'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name', N'RegionImageUrl') AND [object_id] = OBJECT_ID(N'[Regions]'))
        SET IDENTITY_INSERT [Regions] ON;
    EXEC(N'INSERT INTO [Regions] ([Id], [Code], [Name], [RegionImageUrl])
    VALUES (''14ceba71-4b51-4777-9b17-46602cf66153'', N''BOP'', N''Bay Of Plenty'', NULL),
    (''6884f7d7-ad1f-4101-8df3-7a6fa7387d81'', N''NTL'', N''Northland'', NULL),
    (''906cb139-415a-4bbb-a174-1a1faf9fb1f6'', N''NSN'', N''Nelson'', N''https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1''),
    (''cfa06ed2-bf65-4b65-93ed-c9d286ddb0de'', N''WGN'', N''Wellington'', N''https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1''),
    (''f077a22e-4248-4bf6-b564-c7cf4e250263'', N''STL'', N''Southland'', NULL),
    (''f7248fc3-2585-4efb-8d1d-1c555f4087f6'', N''AKL'', N''Auckland'', N''https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name', N'RegionImageUrl') AND [object_id] = OBJECT_ID(N'[Regions]'))
        SET IDENTITY_INSERT [Regions] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260128155957_seed difficulties and regions'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260128155957_seed difficulties and regions', N'10.0.2');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260204143238_create images table'
)
BEGIN
    CREATE TABLE [Images] (
        [Id] uniqueidentifier NOT NULL,
        [FileName] nvarchar(max) NOT NULL,
        [FileDescription] nvarchar(max) NULL,
        [FileExtension] nvarchar(max) NOT NULL,
        [FileSizeInBytes] bigint NOT NULL,
        [FilePath] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Images] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260204143238_create images table'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260204143238_create images table', N'10.0.2');
END;

COMMIT;
GO

