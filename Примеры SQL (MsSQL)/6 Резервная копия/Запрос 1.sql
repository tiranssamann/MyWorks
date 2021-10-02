
--проверка на наличие ошибок
DBCC CHECKDB ('InternetShop_Zakarin_Artur') WITH NO_INFOMSGS
DBCC CHECKDB ('InternetShop_Zakarin_Artur') WITH ALL_ERRORMSGS
GO
-- создание устройства дл€ резервного копировани€
EXECUTE sp_addumpdevice 'disk','InternetShop_Zakarin_Artur_backup','E:\InternetShop_Zakarin_Artur_backup.bak'
GO
-- проверка на наличие устройства
EXECUTE sp_helpdevice --@devname='MyLibrarymy_backup111'
GO
-- —оздание бэкапа базы данных
BACKUP DATABASE InternetShop_Zakarin_Artur TO InternetShop_Zakarin_Artur_backup WITH INIT
GO
BACKUP DATABASE InternetShop_Zakarin_Artur TO InternetShop_Zakarin_Artur_backup WITH DIFFERENTIAL,NOINIT
GO
--¬осстановление базы данных
RESTORE DATABASE InternetShop_Zakarin_Artur FROM InternetShop_Zakarin_Artur_backup WITH REPLACE
GO
