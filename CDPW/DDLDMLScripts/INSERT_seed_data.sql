SET IDENTITY_INSERT [Countries] ON


INSERT INTO [Countries]
           (CountryId ,[CountryISOn],[CountryCodeA2] ,[CountryCodeA3], [CountryCodeA3_9] ,[CountryName],[rowguid])
           
     VALUES (0 ,0 ,'' ,'', '' ,'' ,NULL)

GO

SET IDENTITY_INSERT [Countries] OFF
GO

SELECT * FROM [Countries]
GO


SET IDENTITY_INSERT [AdministrativeRegions] ON

INSERT INTO [AdministrativeRegions]
           (AdministrativeRegionId
           ,[AdministrativeRegionCode]
           ,[AdministrativeRegion]
           ,[AdministrativeRegionCode1]
           ,[AdministrativeRegion1]
           ,[AdministrativeRegionCode2]
           ,[AdministrativeRegion2]
           ,[AdministrativeRegionCode3]
           ,[AdministrativeRegion3]
           ,[AdministrativeRegionCode4]
           ,[AdministrativeRegion4]
           ,[CountryId])
     VALUES
           (0
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,0)
GO


SET IDENTITY_INSERT [AdministrativeRegions] OFF
GO

SELECT * FROM [AdministrativeRegions]           
GO

SET IDENTITY_INSERT [Cities] ON

INSERT INTO [Cities]
           (CityId
           ,[City]
           --,[CountryId]
           --,[AdministrativeRegionId]
           ,[Latitude]
           ,[Longitude]
           ,[TimeZone]
           ,[UTC])
     VALUES
           (0
           ,''
           --,0
           --,0
           ,0
           ,0
           ,''
           ,'')
GO

SET IDENTITY_INSERT [Cities] OFF
GO

SELECT * FROM [Cities] 
GO

SET IDENTITY_INSERT [PostalCodes] ON

INSERT INTO [PostalCodes]
           (PostalCodeId
           ,[CityId]
           ,[PostalCode]
           ,[PostalBarCode])
     VALUES
           (0
           ,0
           ,''
           ,'')
GO

SET IDENTITY_INSERT [PostalCodes] OFF
GO


SELECT * FROM [PostalCodes]
GO

SET IDENTITY_INSERT [Gender] ON
GO
INSERT INTO [Gender] ([GenderId],[GenderCode],[Gender])
     VALUES (1, 'M', 'Male')

INSERT INTO [Gender] ([GenderId],[GenderCode],[Gender])
     VALUES (2, 'F', 'Female')     

INSERT INTO [Gender] ([GenderId],[GenderCode],[Gender])
     VALUES (3, 'N', 'Rather not say')          
     
GO

SET IDENTITY_INSERT [Gender] OFF
GO

SELECT * FROM [Gender]
GO

INSERT INTO [PLicensesTypes]
           ([PLicensesType]
           ,[Frequency]
           ,[TrialPeriod]
           ,[TrialPeriodMU]
           ,[Price]
           ,[BeginDate]
           ,[EndDate]
           )
     VALUES
           ('USCBP',
           'monthly',
           2,
           'W',
           3.99,
           GETDATE(),
           NULL
           )
GO
INSERT INTO [PLicensesTypes]
           ([PLicensesType]
           ,[Frequency]
           ,[TrialPeriod]
           ,[TrialPeriodMU]
           ,[Price]
           ,[BeginDate]
           ,[EndDate]
           )
     VALUES
           ('CBSA',
           'monthly',
           2,
           'W',
           3.99,
           GETDATE(),
           NULL
           )