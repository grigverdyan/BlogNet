CREATE PROCEDURE [dbo].[UpsertPost]
	@title NVARCHAR(100) = NULL,
	@content NVARCHAR(MAX) = NULL,
	@postDate DATETIME = NULL,
	@userID int = NULL,
	@postID int = 0 out
AS
BEGIN
	IF(@postID = 0)
	BEGIN
		INSERT INTO Posts([Title], [Content], [PostDate], [UserID])
		VALUES(@title, @content, @postDate, @userID)
		SET @postID = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		UPDATE Posts
		SET [Title] = COALESCE(@title, [Title]),
			[Content] = COALESCE(@content, [Content]),
			[PostDate] = COALESCE(@postDate, [PostDate]),
			[UserID] = COALESCE(@userID, [UserID])
		WHERE [PostID] = @postID
	END
END

