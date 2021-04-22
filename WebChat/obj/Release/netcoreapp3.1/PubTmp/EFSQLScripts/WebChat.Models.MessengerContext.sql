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
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE TABLE [Chats] (
        [Id] int NOT NULL IDENTITY,
        [ChatName] nvarchar(max) NULL,
        CONSTRAINT [PK_Chats] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [NickName] nvarchar(max) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE TABLE [GroupChats] (
        [Id] int NOT NULL IDENTITY,
        [ChatId] int NULL,
        [UserId] int NULL,
        CONSTRAINT [PK_GroupChats] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GroupChats_Chats_ChatId] FOREIGN KEY ([ChatId]) REFERENCES [Chats] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_GroupChats_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE TABLE [PrivateChats] (
        [Id] int NOT NULL IDENTITY,
        [ChatId] int NULL,
        [User1Id] int NULL,
        [User2Id] int NULL,
        CONSTRAINT [PK_PrivateChats] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PrivateChats_Chats_ChatId] FOREIGN KEY ([ChatId]) REFERENCES [Chats] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PrivateChats_Users_User1Id] FOREIGN KEY ([User1Id]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PrivateChats_Users_User2Id] FOREIGN KEY ([User2Id]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE TABLE [PrivatelyDeletedMessages] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NULL,
        CONSTRAINT [PK_PrivatelyDeletedMessages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PrivatelyDeletedMessages_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE TABLE [Messages] (
        [Id] int NOT NULL IDENTITY,
        [Text] nvarchar(max) NOT NULL,
        [Time] datetime2 NOT NULL,
        [ChatId] int NULL,
        [UserId] int NULL,
        [MessageToReplyId] int NULL,
        [PrivatelyDeletedMsgId] int NULL,
        CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Messages_Chats_ChatId] FOREIGN KEY ([ChatId]) REFERENCES [Chats] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Messages_Messages_MessageToReplyId] FOREIGN KEY ([MessageToReplyId]) REFERENCES [Messages] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Messages_PrivatelyDeletedMessages_PrivatelyDeletedMsgId] FOREIGN KEY ([PrivatelyDeletedMsgId]) REFERENCES [PrivatelyDeletedMessages] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Messages_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE INDEX [IX_GroupChats_ChatId] ON [GroupChats] ([ChatId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE INDEX [IX_GroupChats_UserId] ON [GroupChats] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE INDEX [IX_Messages_ChatId] ON [Messages] ([ChatId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE INDEX [IX_Messages_MessageToReplyId] ON [Messages] ([MessageToReplyId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE INDEX [IX_Messages_PrivatelyDeletedMsgId] ON [Messages] ([PrivatelyDeletedMsgId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE INDEX [IX_Messages_UserId] ON [Messages] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE INDEX [IX_PrivateChats_ChatId] ON [PrivateChats] ([ChatId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE INDEX [IX_PrivateChats_User1Id] ON [PrivateChats] ([User1Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE INDEX [IX_PrivateChats_User2Id] ON [PrivateChats] ([User2Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    CREATE INDEX [IX_PrivatelyDeletedMessages_UserId] ON [PrivatelyDeletedMessages] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414131323_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210414131323_InitialCreate', N'5.0.5');
END;
GO

COMMIT;
GO

