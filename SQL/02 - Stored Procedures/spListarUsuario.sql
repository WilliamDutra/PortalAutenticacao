IF OBJECT_ID('spListarUsuario') IS NOT NULL
	DROP PROCEDURE spListarUsuario
GO
CREATE PROCEDURE spListarUsuario (
	@NOME VARCHAR(100) = NULL,
	@EMAIL VARCHAR(100) = NULL,
	@SENHA VARCHAR(100) = NULL,
	@TELEFONE VARCHAR(100) = NULL
)
AS
	BEGIN

		SELECT 
			UsuarioId, 
			Nome, 
			Email, 
			Senha, 
			Telefone, 
			Endereco 
		FROM 
			Usuario 
		WHERE
			Nome = ISNULL(@NOME, Nome) AND
			Senha = ISNULL(@Senha, Senha) AND
			Email = ISNULL(@Email, Email) AND
			Telefone = ISNULL(@Telefone, Telefone)

	END