IF OBJECT_ID('spSalvarUsuarioNivel') IS NOT NULL
	DROP PROCEDURE spSalvarUsuarioNivel
GO
CREATE PROCEDURE spSalvarUsuarioNivel (
	@FKUSUARIO INT,
	@FKNIVEL INT
)
AS
	BEGIN
		
		INSERT UsuarioNivel 
		(
			FkUsuario,
			FkNivel
		)
		VALUES
		(
			@FKUSUARIO,
			@FKNIVEL
		)




			

	END
