USE LaborExchange
GO

BACKUP DATABASE LaborExchange
	TO DISK = 'C:\Users\shume\Desktop\LE_BackUp.bak'


RESTORE HEADERONLY
		FROM DISK = 'C:\Users\shume\Desktop\LE_BackUp.bak'

RESTORE DATABASE LaborExchange
		FROM DISK = 'C:\Users\shume\Desktop\LE_BackUp.bak'
		WITH FILE = 1

