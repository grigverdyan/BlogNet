CREATE PROCEDURE [dbo].[GetPost]
	@id int
AS
	SELECT *
	FROM Posts
	WHERE PostID = @id
GO
