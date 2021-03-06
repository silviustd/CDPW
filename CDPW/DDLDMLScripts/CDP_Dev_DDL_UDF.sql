USE [CDPW]
GO
/****** Object:  UserDefinedFunction [dbo].[WTripInfoCAN_DurationOfStay]    Script Date: 5/31/2015 10:05:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[WTripInfoCAN_DurationOfStay]
(
	-- Add the parameters for the function here
	@WAppUserId BIGINT
)
RETURNS VARCHAR
AS
BEGIN
	-- Declare the return variable here

	DECLARE @DurationOfStay VARCHAR

	SELECT @DurationOfStay = COALESCE(CAST([DurationOfStay] As varchar), ' ')
	FROM dbo.WAppUsers
	INNER JOIN dbo.Persons ON WAppUsers.WAppUserId = Persons.WAppUserId 
	LEFT JOIN Persons_Addresses ON Persons.PersonId = Persons_Addresses.PersonId
	LEFT JOIN dbo.WTripInfoCAN on Persons.WAppUserId = WTripInfoCAN.WAppUserId
	WHERE Persons.WAppUser = 0 AND  Persons.PrintForm = 1
	AND Persons_Addresses.Residence = 1 AND WTripInfoCAN.WAppUserId = @WAppUserId

	RETURN @DurationOfStay

END

GO
/****** Object:  UserDefinedFunction [dbo].[Get_CAN_Goods1]    Script Date: 5/31/2015 10:05:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[Get_CAN_Goods1](@WTripInfoCANId bigint)
returns table
as
return
(
	SELECT a.PersonId, a.DateLeftCanada, a.ValueGoods
	from
	(SELECT TOP 2 WTripInfoCANDetailsP.PersonId, isnull(CONVERT(char(6), WTripInfoCANDetailsP.DateLeftCanada, 12),'      ') AS DateLeftCanada, 
	--REPLICATE(' ', 6 - LEN(CAST(CEILING(isnull(WTripInfoCANDetailsP.ValueGoods,0)) AS int))) + CAST(CAST(CEILING(isnull(WTripInfoCANDetailsP.ValueGoods,0)) AS int) AS varchar(6)) AS ValueGoods
	--REPLICATE(' ', 6 - LEN(COALESCE(CAST(CAST(CEILING((WTripInfoCANDetailsP.ValueGoods)) AS INT) AS VARCHAR), ' ')) 
	--REPLICATE(' ', 6 - LEN(COALESCE(CAST(CEILING((WTripInfoCANDetailsP.ValueGoods)) AS INT) , ' ')) 
	--+ COALESCE(CAST(CEILING((WTripInfoCANDetailsP.ValueGoods)) AS INT) , ' ')) AS ValueGoods
	REPLICATE(' ', 6 - LEN(CAST(CEILING(isnull(WTripInfoCANDetailsP.ValueGoods,0)) AS int)))
	+ COALESCE(CAST(CAST(CEILING((WTripInfoCANDetailsP.ValueGoods)) AS int) AS varchar(6)),' ') AS ValueGoods
	FROM dbo.WTripInfoCANDetailsP 
	INNER JOIN dbo.Persons ON WTripInfoCANDetailsP.PersonId = Persons.PersonId
	WHERE (Persons.PrintForm = 1) AND (WTripInfoCANDetailsP.WTripInfoCANId = @WTripInfoCANId) 
	ORDER BY WTripInfoCANDetailsP.PersonId) a
	
	--SELECT TOP 2 PersonId, CONVERT(char(6), DateLeftCanada, 12) AS DateLeftCanada, 
	--REPLICATE(' ', 6 - LEN(CAST(CEILING(ValueGoods) AS int))) + CAST(CAST(CEILING(ValueGoods) AS int) AS varchar(6)) AS ValueGoods
	--FROM dbo.WTripInfoCANDetailsP
	--WHERE WTripInfoCANDetailsP.WTripInfoCANId = @WTripInfoCANId
)

GO
/****** Object:  UserDefinedFunction [dbo].[Get_CAN_Goods2]    Script Date: 5/31/2015 10:05:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[Get_CAN_Goods2](@WTripInfoCANId bigint)
returns table
as
return
(
	SELECT a.PersonId, a.DateLeftCanada, a.ValueGoods
	from
	(SELECT TOP 2 WTripInfoCANDetailsP.PersonId, isnull(CONVERT(char(6), WTripInfoCANDetailsP.DateLeftCanada, 12),'      ') AS DateLeftCanada, 
	--REPLICATE(' ', 6 - LEN(CAST(CEILING(isnull(WTripInfoCANDetailsP.ValueGoods,0)) AS int))) + CAST(CAST(CEILING(isnull(WTripInfoCANDetailsP.ValueGoods,0)) AS int) AS varchar(6)) AS ValueGoods
	--REPLICATE(' ', 6 - LEN(COALESCE(CAST(CEILING((WTripInfoCANDetailsP.ValueGoods)) AS INT) , ' ')) 
	--+ COALESCE(CAST(CEILING((WTripInfoCANDetailsP.ValueGoods)) AS INT) , ' ')) AS ValueGoods
	REPLICATE(' ', 6 - LEN(CAST(CEILING(isnull(WTripInfoCANDetailsP.ValueGoods,0)) AS int)))
	+ COALESCE(CAST(CAST(CEILING((WTripInfoCANDetailsP.ValueGoods)) AS int) AS varchar(6)),' ') AS ValueGoods
	FROM dbo.WTripInfoCANDetailsP
	INNER JOIN dbo.Persons ON WTripInfoCANDetailsP.PersonId = Persons.PersonId
	WHERE (Persons.PrintForm = 1) AND (WTripInfoCANDetailsP.WTripInfoCANId = @WTripInfoCANId) 
	AND (WTripInfoCANDetailsP.PersonId NOT IN (SELECT PersonId FROM dbo.Get_CAN_Goods1(@WTripInfoCANId)))
	ORDER BY WTripInfoCANDetailsP.PersonId) a

)

GO
/****** Object:  UserDefinedFunction [dbo].[Get_CAN_FlightInfo]    Script Date: 5/31/2015 10:05:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE function [dbo].[Get_CAN_FlightInfo](@WAppUserId bigint)
returns table
as
return
(
	SELECT DISTINCT 
	--SUBSTRING(Airlines.AirlineCode + ' ' + Flights.FlightNo + REPLICATE(' ', 9 - LEN(Airlines.AirlineCode) - LEN(Flights.FlightNo) - 1), 1, 9) AS AirLineFlightNo, 
	substring(isnull(cast(WTripInfoCAN.TransportationId as CHAR(100)),'         '),1,9) AS AirLineFlightNo, 
	WTripInfoCAN.WTripInfoCANId as AddressId, 
	--CASE WHEN Addresses.UnitType = 'Apartment' OR Addresses.UnitType = 'Suite' THEN CAST(Addresses.UnitNumber AS nvarchar(5)) + '-' + CAST(Addresses.StreetNumber AS nvarchar(5)) + ' ' + CASE WHEN COALESCE (Addresses.StreetPredirection, '') = '' THEN '' ELSE Addresses.StreetPredirection + ' ' END + Addresses.StreetName + CASE WHEN COALESCE (Addresses.StreetType, '') = '' THEN '' ELSE ' ' + Addresses.StreetType END + CASE WHEN COALESCE (Addresses.StreetPostDirection, '') = '' THEN '' ELSE ' ' + Addresses.StreetPostDirection END ELSE CAST(Addresses.StreetNumber AS nvarchar(5)) + ' ' + CASE WHEN COALESCE (Addresses.StreetPredirection, '') = '' THEN '' ELSE Addresses.StreetPredirection + ' ' END + Addresses.StreetName + CASE WHEN COALESCE (Addresses.StreetType, '') = '' THEN '' ELSE ' ' + Addresses.StreetType END + CASE WHEN COALESCE (Addresses.StreetPostDirection, '') = '' THEN '' ELSE ' ' + Addresses.StreetPostDirection END END AS Address, 
	isnull(Addresses.StreetName,' ') as Address,
	SUBSTRING(isnull(Cities.City,' '), 1, 10) + CASE WHEN LEN(isnull(Cities.City,' '))<10 THEN REPLICATE(' ', 10 - LEN(isnull(Cities.City,' '))) ELSE ' ' END AS City, 
	case when LEN(isnull(Countries.CountryName,' ')) > 9 
		then (Select CountryCodeA3_9 FROM Countries C Where C.CountryId = Addresses.CountryId)
		else SUBSTRING(isnull(Countries.CountryName,' ') + REPLICATE(' ', 9 - LEN(SUBSTRING(isnull(Countries.CountryName,' '),1,9))), 1, 9) end AS CountryName,
	
	SUBSTRING(isnull(AdministrativeRegions.AdministrativeRegionCode,' ') + REPLICATE(' ', 4 - LEN(isnull(AdministrativeRegions.AdministrativeRegionCode,' '))), 1, 4) AS AdministrativeRegionCode, 
	SUBSTRING(isnull(PostalCodes.PostalCode,' ') + REPLICATE(' ', 7 - LEN(isnull(PostalCodes.PostalCode,' '))), 1, 7) AS PostalCode, 
	CONVERT(nchar(8), GETDATE(), 12) AS Date,
	1 as Rank,
	case when WTripInfoCAN.WArrivingBy = 0 then 'X' else ' ' end as  WArrivingBy1,
	case when WTripInfoCAN.WArrivingBy = 1 then 'X' else ' ' end as  WArrivingBy2,
	case when WTripInfoCAN.WArrivingBy = 2 then 'X' else ' ' end as  WArrivingBy3,
	case when WTripInfoCAN.WArrivingBy = 3 then 'X' else ' ' end as  WArrivingBy4,
	case when WTripInfoCAN.WTripPurpose = 0 then 'X' else ' ' end as  WTripPurpose1,
	case when WTripInfoCAN.WTripPurpose = 1 then 'X' else ' ' end as  WTripPurpose2,
	case when WTripInfoCAN.WTripPurpose = 2 then 'X' else ' ' end as  WTripPurpose3,
	case when WTripInfoCAN.WArrivingFrom = 0 then 'X' else ' ' end as  WArrivingFrom1,
	case when WTripInfoCAN.WArrivingFrom = 1 then 'X' else ' ' end as  WArrivingFrom2,
	case when WTripInfoCAN.WArrivingFrom = 2 then 'X' else ' ' end as  WArrivingFrom3,
	case when WTripInfoCAN.Firearms = 1 then 'X' else ' ' end as Firearms_Yes, 
	case when WTripInfoCAN.Firearms = 0 then 'X' else ' ' end as Firearms_No,	
	case when WTripInfoCAN.CommercialGoods = 1 then 'X' else ' ' end as CommercialGoods_Yes,
	case when WTripInfoCAN.CommercialGoods = 0 then 'X' else ' ' end as CommercialGoods_No,	
	case when WTripInfoCAN.MeatProducts = 1 then 'X' else ' ' end as MeatProducts_Yes,
	case when WTripInfoCAN.MeatProducts = 0 then 'X' else ' ' end as MeatProducts_No,	
	case when WTripInfoCAN.CurrencyValue = 1 then 'X' else ' ' end as CurrencyValue_Yes,
	case when WTripInfoCAN.CurrencyValue = 0 then 'X' else ' ' end as CurrencyValue_No,	
	case when WTripInfoCAN.Goods = 1 then 'X' else ' ' end as Goods_Yes,
	case when WTripInfoCAN.Goods = 0 then 'X' else ' ' end as Goods_No,	
	case when WTripInfoCAN.FarmVisit = 1 then 'X' else ' ' end as FarmVisit_Yes,
	case when WTripInfoCAN.FarmVisit = 0 then 'X' else ' ' end as FarmVisit_No,	
	case when WTripInfoCAN.ExceedDutyFree = 1 then 'X' else ' ' end as ExceedDutyFree_Yes,
	case when WTripInfoCAN.ExceedDutyFree = 0 then 'X' else ' ' end as ExceedDutyFree_No,	
	case when WTripInfoCAN.ExceedExemptions = 1 then 'X' else ' ' end as ExceedExemptions_Yes,
	case when WTripInfoCAN.ExceedExemptions = 0 then 'X' else ' ' end as ExceedExemptions_No,	
--	REPLICATE('0', 3 - LEN(substring(cast(isnull(WTripInfoCAN.DurationOfStay,0) as char(10)),1,3))) 
--	+ substring(cast(isnull(WTripInfoCAN.DurationOfStay,0) as char(10)),1,3)  as DurationOfStay
	REPLICATE(' ', 3 - LEN(COALESCE(CAST(WTripInfoCAN.[DurationOfStay] As varchar), ' '))) 
	+ COALESCE(CAST(WTripInfoCAN.[DurationOfStay] As varchar), ' ')  as DurationOfStay
	FROM WAppUsers
	INNER JOIN Persons ON WAppUsers.WAppUserId = Persons.WAppUserId 
	LEFT JOIN Persons_Addresses ON Persons.PersonId = Persons_Addresses.PersonId
	LEFT JOIN Addresses ON Persons_Addresses.AddressId = Addresses.AddressId 
	LEFT JOIN Cities ON Addresses.CityId = Cities.CityId 
	LEFT JOIN PostalCodes ON Addresses.PostalCodeId = PostalCodes.PostalCodeId
	LEFT JOIN AdministrativeRegions ON Addresses.AdministrativeRegionId = AdministrativeRegions.AdministrativeRegionId 
	LEFT JOIN Countries ON Addresses.CountryId = Countries.CountryId 
	LEFT JOIN dbo.WTripInfoCAN on Persons.WAppUserId = WTripInfoCAN.WAppUserId

	WHERE Persons.WAppUser = 0 AND  Persons.PrintForm = 1
	AND Persons_Addresses.Residence = 1 AND WTripInfoCAN.WAppUserId = @WAppUserId
	--@WAppUserId
	--WHERE (Flights.FlighCode = '111') 
	--AND (Flights.AirportCodeArrival = 'AC2') 
	--AND (Persons_Flights.Checked = 1)
	--where WAppUsers.WAppUserId = @WAppUserId
)


GO
/****** Object:  UserDefinedFunction [dbo].[Get_CAN_Persons]    Script Date: 5/31/2015 10:05:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[Get_CAN_Persons](@WTripInfoCANId bigint)
returns table
as
return
(
	SELECT Persons.PersonId, SUBSTRING(ISNULL(LastName,' ') + ' ' + ISNULL(FirstName,' ') + ' ' 
	+ SUBSTRING((ISNULL(MiddleName,' ')), 1, 1) 	
--	+ SUBSTRING((case when MiddleName is null then ' ' else MiddleName end), 1, 1) 
	+ REPLICATE(' ', 26 - LEN(ISNULL(LastName,' ')) - LEN(ISNULL(FirstName,' ')) - 2)
	, 1, 26) 
	 AS Name, 
	Isnull(CONVERT(nchar(6), DateofBirth, 12),'      ') AS DateofBirth, 
	isnull(SUBSTRING(Citizenship + REPLICATE(' ', 12 - LEN(Citizenship)), 1, 12),REPLICATE(' ', 12)) AS Citizenship 
	FROM dbo.WTripInfoCANDetailsP
	INNER JOIN dbo.Persons ON WTripInfoCANDetailsP.PersonId = Persons.PersonId
	WHERE (PrintForm = 1) AND (WTripInfoCANDetailsP.WTripInfoCANId = @WTripInfoCANId)
)

GO
/****** Object:  UserDefinedFunction [dbo].[Get_US_FlightInfo]    Script Date: 5/31/2015 10:05:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[Get_US_FlightInfo](@WAppUserId bigint)
returns table
as
return
(
SELECT 
	isnull(Persons.FirstName,'') as FirstName, 
	case when Persons.MiddleName is null then ' ' else Persons.MiddleName end as MiddleName, 
	isnull(Persons.LastName,'') as LastName, 
	isnull(CONVERT(nchar(6), Persons.DateofBirth, 12),'      ') AS DateofBirth, 
	--CASE WHEN Addresses.UnitType = 'Apartment' OR Addresses.UnitType = 'Suite' THEN CAST(Addresses.UnitNumber AS nvarchar(5)) + '-' + CAST(Addresses.StreetNumber AS nvarchar(5)) + ' ' + CASE WHEN COALESCE (Addresses.StreetPredirection, '') = '' THEN '' ELSE Addresses.StreetPredirection + ' ' END + Addresses.StreetName + CASE WHEN COALESCE (Addresses.StreetType, '') = '' THEN '' ELSE ' ' + Addresses.StreetType END + CASE WHEN COALESCE (Addresses.StreetPostDirection, '') = '' THEN '' ELSE ' ' + Addresses.StreetPostDirection END ELSE CAST(Addresses.StreetNumber AS nvarchar(5)) + ' ' + CASE WHEN COALESCE (Addresses.StreetPredirection, '') = '' THEN '' ELSE Addresses.StreetPredirection + ' ' END + Addresses.StreetName + CASE WHEN COALESCE (Addresses.StreetType, '') = '' THEN '' ELSE ' ' + Addresses.StreetType END + CASE WHEN COALESCE (Addresses.StreetPostDirection, '') = '' THEN '' ELSE ' ' + Addresses.StreetPostDirection END END AS Address, 
	isnull(Addresses.StreetName,'') as Address,
	isnull(Cities.City,'') as City, 
	isnull(AdministrativeRegions.AdministrativeRegion,'') AS State, 
	isnull(Passports.PassportIssueAuthority,'') as PassportIssueAuthority, 
	isnull(Passports.PassportNo,'') as PassportNo, 
	isnull(Countries.CountryName,'') as CountryName, 
	isnull(PCountries.CountryName,'') as PCountry, 
	--ISNULL(SUBSTRING(Airlines.AirlineCode + ' ' + Flights.FlightNo + REPLICATE(' ', 9 - LEN(Airlines.AirlineCode) - LEN(Flights.FlightNo) - 1), 1, 9),'') AS AirLineFlightNo, 
	substring(cast(isnull(WTripInfoUSA.TransportationId,'') as CHAR(100)),1,9) AS AirLineFlightNo,
	GETDATE() AS Date, 
	--isnull(WTripInfoUSA.NoFamMembers,'') as NoFamMembers, 
	case when WTripInfoUSA.NoFamMembers IS NULL THEN '' ELSE WTripInfoUSA.NoFamMembers END as NoFamMembers, 
	isnull(WTripInfoUSA.ContriesVisited,'') as CountriesVisited, 
	isnull(WTripInfoUSA.CountriesVisited2,'') as CountriesVisited2, 
	case when WTripInfoUSA.TripPPurposeBusiness = 1 then 'X' else ' ' end as TripPurposeBusiness_Yes, 
	case when WTripInfoUSA.TripPPurposeBusiness = 0 then 'X' else ' ' end as TripPurposeBusiness_No, 
	case when WTripInfoUSA.BringFruits = 1 then 'X' else ' ' end as BringFruits_Yes, 
	case when WTripInfoUSA.BringFruits = 0 then 'X' else ' ' end as BringFruits_No, 
	case when WTripInfoUSA.BringMeats = 1 then 'X' else ' ' end as BringMeats_Yes, 
	case when WTripInfoUSA.BringMeats = 0 then 'X' else ' ' end as BringMeats_No, 
	case when WTripInfoUSA.BringDiseaseAgents = 1 then 'X' else ' ' end as BringDiseaseAgents_Yes, 
	case when WTripInfoUSA.BringDiseaseAgents = 0 then 'X' else ' ' end as BringDiseaseAgents_No, 
	case when WTripInfoUSA.BringSoil = 1 then 'X' else ' ' end as BringSoil_Yes, 
	case when WTripInfoUSA.BringSoil = 0 then 'X' else ' ' end as BringSoil_No, 
	case when WTripInfoUSA.Livestock = 1 then 'X' else ' ' end as Livestock_Yes, 
	case when WTripInfoUSA.Livestock = 0 then 'X' else ' ' end as Livestock_No, 
	case when WTripInfoUSA.CurrencyValue = 1 then 'X' else ' ' end as CurrencyValue_Yes, 
	case when WTripInfoUSA.CurrencyValue = 0 then 'X' else ' ' end as CurrencyValue_No, 
	case when WTripInfoUSA.CommercialMerchandise = 1 then 'X' else ' ' end as CommercialMerchandise_Yes, 
	case when WTripInfoUSA.CommercialMerchandise = 0 then 'X' else ' ' end as CommercialMerchandise_No,
	isnull(WTripInfoUSA.RGoodsValue,'') as RGoodsValue, 
	isnull(WTripInfoUSA.VGoodsValue,'') as VGoodsValue 
	FROM WAppUsers
	INNER JOIN Persons ON WAppUsers.WAppUserId = Persons.WAppUserId 
	LEFT JOIN Persons_Addresses ON Persons.PersonId = Persons_Addresses.PersonId
	LEFT JOIN Addresses ON Persons_Addresses.AddressId = Addresses.AddressId 
	LEFT JOIN Cities ON Addresses.CityId = Cities.CityId 
	LEFT JOIN AdministrativeRegions ON Addresses.AdministrativeRegionId = AdministrativeRegions.AdministrativeRegionId 
	LEFT JOIN Passports ON Persons.PersonId = Passports.PersonId
	LEFT JOIN Countries PCountries ON Passports.CountryId = PCountries.CountryId 
	LEFT JOIN WTripInfoUSA ON WTripInfoUSA.WAppUserId = WAppUsers.WAppUserId
	LEFT JOIN Countries ON WTripInfoUSA.CountryResidence = Countries.CountryId 
	WHERE Persons.WAppUser = 0 AND  Persons.PrintForm = 1	
	AND Persons_Addresses.Destination = 1 AND WAppUsers.WAppUserId = @WAppUserId
	
)

GO
