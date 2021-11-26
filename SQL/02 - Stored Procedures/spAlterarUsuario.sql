IF OBJECT_ID('spAlterarUsuario') IS NOT NULL
	DROP PROCEDURE spAlterarUsuario
GO
CREATE PROCEDURE spAlterarUsuario (
	@ID INT,
	@NOME VARCHAR(100) = NULL,
	@EMAIL VARCHAR(100) = NULL,
	@SENHA VARCHAR(100) = NULL,
	@TELEFONE VARCHAR(30) = NULL,
	@ENDERECO VARCHAR(100) = NULL,
	@PASSWORDRESETTOEKN VARCHAR(255) = NULL
)
AS
	BEGIN

		UPDATE 
			Usuario
		SET
			Nome = ISNULL(@NOME, Nome),
			Email = ISNULL(@EMAIL, Email),
			Senha = ISNULL(@SENHA, Senha),
			Telefone = ISNULL(@TELEFONE, Telefone),
			Endereco = ISNULL(@ENDERECO, Endereco),
			PasswordResetToken = ISNULL(@PASSWORDRESETTOEKN, PasswordResetToken)
		WHERE
			UsuarioId = @ID

	END