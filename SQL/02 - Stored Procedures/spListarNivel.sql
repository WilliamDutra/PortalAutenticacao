IF OBJECT_ID('spListarNivel') IS NOT NULL
	DROP PROCEDURE spListarNivel
GO
CREATE PROCEDURE spListarNivel (
	@ID INT = NULL,
	@NOME VARCHAR(100) = NULL,
	@DESCRICAO VARCHAR(255) = NULL
)
AS
	BEGIN
		
		SELECT 
			NivelId, 
			Nome, 
			Descricao 
		FROM 
			Nivel
		WHERE
			NivelId = ISNULL(@ID, NivelId)
		AND
			Nome = ISNULL(@NOME, Nome)
		AND
			Descricao = ISNULL(@DESCRICAO, Descricao)

	END