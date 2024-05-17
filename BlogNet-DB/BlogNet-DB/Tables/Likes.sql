﻿CREATE TABLE [dbo].[Likes]
(
	[LikesID] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[PostID] INT REFERENCES Posts([PostID]),
	[UserID] INT REFERENCES Users([UserID]),
	[CreationTime] DATETIME NOT NULL DEFAULT GETDATE()
);