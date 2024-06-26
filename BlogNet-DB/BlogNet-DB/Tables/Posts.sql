﻿CREATE TABLE [dbo].[Posts]
(
	[PostID] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[UserID] INT REFERENCES Users([UserID]),
	[Title] NVARCHAR(100) NOT NULL,
	[Content] NVARCHAR(MAX) NOT NULL,
	[PostDate] DATETIME NOT NULL DEFAULT GETDATE(),
);