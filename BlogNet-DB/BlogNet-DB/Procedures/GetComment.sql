CREATE PROCEDURE [dbo].[GetComment]
	@id int
AS
	SELECT *
	FROM Comments
	WHERE CommentID = @id
GO
