USE LaborExchange
GO

UPDATE Auth
SET Login = ENCRYPTBYPASSPHRASE('doedatel', Login);

UPDATE Auth
SET Password = ENCRYPTBYPASSPHRASE('doedatel', Password);

CREATE PROCEDURE GetDecryptLoginAndPassword AS
SELECT CONVERT(varchar(50), DECRYPTBYPASSPHRASE('doedatel', Login)) AS Login, 
CONVERT(varchar(50), DECRYPTBYPASSPHRASE('doedatel', Password)) AS Password, 
Access 
FROM Auth;

SELECT * FROM Auth