--USE CDP_Dev3

DELETE FROM [TablesToDelete]

INSERT INTO [TablesToDelete] ([TableToDelete] ,[SQLSt],[NoSignUp] ,[NotKeepData])
     VALUES ('Persons','UPDATE Persons SET DateofBirth = NULL, Citizenship = NULL WHERE WAppUserId = <WUId>;UPDATE Persons_Addresses SET PersonId = NULL WHERE PersonId IN (SELECT PersonId FROM Persons WHERE WAppUserId = <WUId>)' ,1 , 1)  

INSERT INTO [TablesToDelete] ([TableToDelete] ,[SQLSt],[NoSignUp] ,[NotKeepData])
     VALUES ('WTripInfoCANDetailsP', 'DELETE FROM WTripInfoCANDetailsP WHERE WTripInfoCANId IN (SELECT WTripInfoCANId FROM WTripInfoCAN WHERE WAppUserId = <WUId>)'  ,1 ,0)          
   
INSERT INTO [TablesToDelete] ([TableToDelete] ,[SQLSt],[NoSignUp] ,[NotKeepData])
     VALUES ('WTripInfoCAN', 'DELETE FROM WTripInfoCAN WHERE WAppUserId = <WUId>' ,1 ,0)          

INSERT INTO [TablesToDelete] ([TableToDelete] ,[SQLSt],[NoSignUp] ,[NotKeepData])
     VALUES ('WTripInfoUSADetailsP', 'DELETE FROM WTripInfoUSADetailsP WHERE WTripInfoUSAId IN (SELECT WTripInfoUSAId FROM WTripInfoUSA WHERE WAppUserId = <WUId>)' ,1 ,0)          

INSERT INTO [TablesToDelete] ([TableToDelete] ,[SQLSt],[NoSignUp] ,[NotKeepData])
     VALUES ('WTripInfoUSA' ,'DELETE FROM WTripInfoUSA WHERE WAppUserId = <WUId>' ,1 ,0)          

INSERT INTO [TablesToDelete] ([TableToDelete] ,[SQLSt],[NoSignUp] ,[NotKeepData])
     VALUES ('Addresses' , 'DELETE FROM Addresses WHERE AddressId IN (SELECT Persons_Addresses.AddressId FROM Persons_Addresses INNER JOIN Persons ON Persons_Addresses.PersonId = Persons.PersonId INNER JOIN WAppUsers ON Persons.WAppUserId = WAppUsers.WAppUserId WHERE WAppUsers.WAppUserId = <WUId>)',1 ,1)          

INSERT INTO [TablesToDelete] ([TableToDelete] ,[SQLSt],[NoSignUp] ,[NotKeepData])
     VALUES ('Passports', 'DELETE FROM Passports WHERE PersonId IN (SELECT PersonId FROM Persons WHERE WAppUserId = <WUId>)' ,1 ,1)                                        


INSERT INTO [TablesToDelete] ([TableToDelete] ,[SQLSt],[NoSignUp] ,[NotKeepData])
     VALUES ('Persons' , 'DELETE FROM Persons WHERE WAppUserId = <WUId>',1 ,0)  

INSERT INTO [TablesToDelete] ([TableToDelete] ,[SQLSt],[NoSignUp] ,[NotKeepData])
     VALUES ('Persons_Addresses', 'DELETE FROM Persons_Addresses WHERE (PersonId NOT IN (SELECT PersonId FROM Persons) OR PersonId IS NULL) AND AddressId NOT IN (SELECT AddressId From Addresses)' ,1 ,1)          

INSERT INTO [TablesToDelete] ([TableToDelete] ,[SQLSt],[NoSignUp] ,[NotKeepData])
     VALUES ('WAppUsers', 'DELETE FROM WAppUsers WHERE WAppUserId = <WUId>' ,1 ,0)


SELECT * FROM [TablesToDelete]