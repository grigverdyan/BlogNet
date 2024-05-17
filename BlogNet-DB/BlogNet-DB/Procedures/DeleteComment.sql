CREATE PROCEDURE [dbo].[DeleteComment]
	@id int
AS
	DELETE FROM Comments
	WHERE [CommentID] = @id
    SELECT @@ROWCOUNT
GO
