﻿IF OBJECT_ID('Nivel') IS NOT NULL
	DROP TABLE Nivel
GO
CREATE TABLE Nivel (
	NivelId INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(100),
	Descricao VARCHAR(255)
);