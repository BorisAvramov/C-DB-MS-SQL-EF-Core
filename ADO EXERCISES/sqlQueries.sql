--INSERT INTO Minions WHERE Name='Ivan' (TownId) VALUES(8)

--SELECT * FROM Minions WHERE Name = 'NIki'

--UPDATE Minions SET TownId=10 WHERE Name='Niki';


--minion: George 22 Mezdra
--villain: Pesho

--INSERT INTO MinionsVillains VALUES(@MinionId, @VillainId)

--SELECT *
--	FROM Minions
--	JOIN Towns ON Towns.Id = Minions.TownId
--	WHERE Minions.Name = 'George'

--SELECT *
--	FROM Villains V
--	JOIN EvilnessFactors EF ON EF.Id = V.EvilnessFactorId
--	WHERE V.Name LIKE '%Pesho%'


--SELECT *
--	FROM MinionsVillains MV
--	JOIN Minions M ON MV.MinionId = M.Id
--	JOIN Villains V ON V.Id = MV.VillainId
--	WHERE M.Name = 'George'


----SELECT * FROM Countries WHERE ()   Name = 'Bulgaria'


--SELECT t.Name FROM Countries C JOIN Towns T ON T.CountryCode = C.Id WHERE C.Name = 'Bulgaria';


--UPDATE Towns SET Name = UPPER(Name) WHERE (SELECT Id FROM Countries WHERE Name = 'Bulgaria') = Towns.CountryCode



--SELECT Id FROM Countries WHERE Name = 'Bulgaria'

--SELECT NAME FROM Towns
--WHERE (SELECT Id FROM Countries WHERE Name = 'Bulgaria') = Towns.CountryCode