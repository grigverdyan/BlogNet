CREATE PROCEDURE [dbo].[DeletePost]
	@id int
AS
	DELETE FROM Posts
	WHERE [PostID] = @id
    SELECT @@ROWCOUNT
GO
