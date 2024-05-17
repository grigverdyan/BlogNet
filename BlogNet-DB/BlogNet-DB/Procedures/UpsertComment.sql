CREATE PROCEDURE [dbo].[UpsertComment]
	@content NVARCHAR(MAX) = NULL,
	@commentDate DATETIME = NULL,
	@userID int = NULL,
	@postID int = NULL,
	@commentID int = 0 out
AS
BEGIN
	IF(@commentID = 0)
	BEGIN
		INSERT INTO Comments([Content], [CommentDate], [UserID], [PostID])
		VALUES(@content, @commentDate, @userID, @postID)
		SET @postID = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		UPDATE Comments
		SET [Content] = COALESCE(@content, [Content]),
			[CommentDate] = COALESCE(@commentDate, [CommentDate]),
			[UserID] = COALESCE(@userID, [UserID]),
			[PostID] = COALESCE(@postID, [PostID])
		WHERE [CommentID] = @commentID
	END
END


