CREATE PROCEDURE [dbo].[UpsertUser]
	@username NVARCHAR(50) = NULL,
	@email NVARCHAR(50) = NULL,
	@password NVARCHAR(50) = NULL,
	@registrationDate DATETIME = NULL,
	@photoUrl VARCHAR(2048) = NULL,
	@userID int = 0 out
AS
BEGIN
	IF(@userID = 0)
	BEGIN
		INSERT INTO Users([Username], [Email], [Password], [RegistrationDate], [PhotoUrl])
		VALUES(@username, @email, @password, @registrationDate, @photoUrl)
		SET @userID = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		UPDATE Users
		SET [Username] = COALESCE(@username, [Username]),
			[Email] = COALESCE(@email, [Email]),
			[Password] = COALESCE(@password, [Password]),
			[RegistrationDate] = COALESCE(@registrationDate, [RegistrationDate]),
			[PhotoUrl] = COALESCE(@photoUrl, [PhotoUrl])
		WHERE [UserID] = @userID
	END
END
