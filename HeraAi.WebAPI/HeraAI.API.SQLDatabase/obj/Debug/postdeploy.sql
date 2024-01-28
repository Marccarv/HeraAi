-- DEFAULT USERS
IF (SELECT COUNT('-') FROM [dbo].[Users]) = 0
BEGIN

        INSERT INTO [dbo].[Users] ([Id], [CountryId], [Language], [Email], [Password], [FirstName], [LastName], [Phone], [Inactive], [CreationDate], [LastUpdate], [LastUserId])
        VALUES ('000000', NULL, [dbo].[uFN_GlobalVars]('BASELANGUAGE'), 'teste@email.pt', 'tZiiwVQPc0IUsja1v2Xx+g==', 'Hera', 'AI', '963333333', CAST('false' AS BIT), GETDATE(), GETDATE(), [dbo].[uFN_GlobalVars]('ADMINUSER'));

END
GO
