
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/18/2021 20:30:59
-- Generated from EDMX file: G:\MetPushWorkWithDB\Entities\ModelDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [UserDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Users_Genders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Genders];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Roles];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Genders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genders];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Genders'
CREATE TABLE [dbo].[Genders] (
    [IdGender] int IDENTITY(1,1) NOT NULL,
    [Gender] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [IdRole] int IDENTITY(1,1) NOT NULL,
    [Role] nvarchar(50)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [IdUser] int IDENTITY(1,1) NOT NULL,
    [FName] nvarchar(100)  NOT NULL,
    [LName] nvarchar(100)  NOT NULL,
    [MName] nvarchar(100)  NOT NULL,
    [IdGender] int  NOT NULL,
    [IdRole] int  NOT NULL,
    [Login] nvarchar(100)  NOT NULL,
    [Password] nvarchar(100)  NOT NULL,
    [Photo] varbinary(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdGender] in table 'Genders'
ALTER TABLE [dbo].[Genders]
ADD CONSTRAINT [PK_Genders]
    PRIMARY KEY CLUSTERED ([IdGender] ASC);
GO

-- Creating primary key on [IdRole] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([IdRole] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [IdUser] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([IdUser] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdGender] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_Genders]
    FOREIGN KEY ([IdGender])
    REFERENCES [dbo].[Genders]
        ([IdGender])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_Genders'
CREATE INDEX [IX_FK_Users_Genders]
ON [dbo].[Users]
    ([IdGender]);
GO

-- Creating foreign key on [IdRole] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_Roles]
    FOREIGN KEY ([IdRole])
    REFERENCES [dbo].[Roles]
        ([IdRole])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_Roles'
CREATE INDEX [IX_FK_Users_Roles]
ON [dbo].[Users]
    ([IdRole]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------