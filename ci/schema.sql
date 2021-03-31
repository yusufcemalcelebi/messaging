IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Users] (
    [ID] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [Blocks] (
    [ID] int NOT NULL IDENTITY,
    [FKBlockerUserId] int NOT NULL,
    [FKBlockedUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Blocks] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Blocks_Users_FKBlockedUserId] FOREIGN KEY ([FKBlockedUserId]) REFERENCES [Users] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Blocks_Users_FKBlockerUserId] FOREIGN KEY ([FKBlockerUserId]) REFERENCES [Users] ([ID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Messages] (
    [ID] int NOT NULL IDENTITY,
    [Text] nvarchar(max) NULL,
    [FKSenderId] int NOT NULL,
    [FKReceiverId] int NOT NULL,
    [Date] datetime2 NOT NULL,
    [IsSpam] bit NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Messages_Users_FKReceiverId] FOREIGN KEY ([FKReceiverId]) REFERENCES [Users] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Messages_Users_FKSenderId] FOREIGN KEY ([FKSenderId]) REFERENCES [Users] ([ID]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Blocks_FKBlockedUserId] ON [Blocks] ([FKBlockedUserId]);

GO

CREATE INDEX [IX_Blocks_FKBlockerUserId] ON [Blocks] ([FKBlockerUserId]);

GO

CREATE INDEX [IX_Messages_FKReceiverId] ON [Messages] ([FKReceiverId]);

GO

CREATE INDEX [IX_Messages_FKSenderId] ON [Messages] ([FKSenderId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201011102220_InitialCreate', N'3.1.8');
GO