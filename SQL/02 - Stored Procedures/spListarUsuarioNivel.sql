IF OBJECT_ID('spListarUsuarioNivel') IS NOT NULL
	DROP PROCEDURE spListarUsuarioNivel
GO
CREATE PROCEDURE spListarUsuarioNivel (
	@FKUSUARIO INT = NULL,
	@FKNIVEL INT = NULL
)
AS
	BEGIN

		SELECT
			IdUsuarioNivel,
			FkUsuario,
			FkNivel
		FROM
			UsuarioNivel
		WHERE
			FkUsuario = ISNULL(@FKUSUARIO, FkUsuario)
		AND
			FkNivel = ISNULL(@FKNIVEL, FkNivel)

	END