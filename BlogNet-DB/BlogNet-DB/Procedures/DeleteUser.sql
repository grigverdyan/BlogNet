CREATE PROCEDURE [dbo].[DeleteUser]
	@id int
AS
	DELETE FROM Users
	WHERE [UserID] = @id
    SELECT @@ROWCOUNT
GO
