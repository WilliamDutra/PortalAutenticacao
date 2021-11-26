IF OBJECT_ID('UsuarioNivel') IS NOT NULL
	DROP TABLE UsuarioNivel
GO
CREATE TABLE UsuarioNivel (
	IdUsuarioNivel INT PRIMARY KEY IDENTITY,
	FkUsuario INT,
	FkNivel INT
);