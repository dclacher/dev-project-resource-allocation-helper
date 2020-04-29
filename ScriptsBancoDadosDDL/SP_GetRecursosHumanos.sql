IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SP_GetRecursosHumanos')
BEGIN
    DROP PROCEDURE dbo.SP_GetRecursosHumanos
    IF OBJECT_ID('dbo.SP_GetRecursosHumanos') IS NOT NULL
        PRINT '<<< FAILED DROPPING PROCEDURE dbo.SP_GetRecursosHumanos >>>'
    ELSE
        PRINT '<<< DROPPED PROCEDURE dbo.SP_GetRecursosHumanos >>>'
END
GO
	
CREATE PROCEDURE [dbo].[SP_GetRecursosHumanos]
	@comp0		int = null,	
	@comp1		int = null,
	@comp2		int = null,
	@comp3		int = null,
	@comp4		int = null,
	@comp5		int = null,
	@comp6		int = null,
	@comp7		int = null,
	@comp8		int = null,
	@comp9		int = null
	
AS

-- é necessário usar table variable por causa que o entity framework não enxerga o resultado de uma temp table. 
	DECLARE @ConsultaRHTemp TABLE 
	(
		tmp_id				int,
		tmp_nome			varchar(50),
		tmp_sobrenome		varchar(50),
		tmp_comp_nivel0		int,
		tmp_comp_id0		int,		
		tmp_comp_nivel1		int,
		tmp_comp_id1		int,
		tmp_comp_nivel2		int,
		tmp_comp_id2		int,
		tmp_comp_nivel3		int,
		tmp_comp_id3		int,
		tmp_comp_nivel4		int,
		tmp_comp_id4		int,
		tmp_comp_nivel5		int,
		tmp_comp_id5		int,
		tmp_comp_nivel6		int,
		tmp_comp_id6		int,
		tmp_comp_nivel7		int,
		tmp_comp_id7		int,
		tmp_comp_nivel8		int,
		tmp_comp_id8		int,
		tmp_comp_nivel9		int,
		tmp_comp_id9		int 				
	)
	

	INSERT INTO @ConsultaRHTemp (tmp_id, tmp_nome, tmp_sobrenome, tmp_comp_nivel0, tmp_comp_id0)
	SELECT  usu_id, usu_nome, usu_sobrenome, aval_nivel, uac_comp_id  
	FROM Usuario
	INNER JOIN 	Usu_Aval_Comp
		ON usu_id = uac_usu_id
	INNER JOIN Avaliacao
		ON uac_aval_id = aval_id
	WHERE (@comp0 IS NULL OR [uac_comp_id] = @comp0)
	AND aval_atual = 1 

	IF @comp1 IS NOT NULL
	BEGIN
		UPDATE @ConsultaRHTemp
		SET tmp_comp_nivel1 = aval_nivel, tmp_comp_id1 = uac_comp_id 
		FROM @ConsultaRHTemp
		INNER JOIN dbo.Usuario
			ON tmp_id = usu_id
		INNER JOIN 	Usu_Aval_Comp
			ON usu_id = uac_usu_id
		INNER JOIN Avaliacao
			ON uac_aval_id = aval_id
		WHERE uac_comp_id = @comp1
		AND aval_atual = 1		 	
	END 

	IF @comp2 IS NOT NULL
	BEGIN
		UPDATE @ConsultaRHTemp
		SET tmp_comp_nivel2 = aval_nivel , tmp_comp_id2 = uac_comp_id 
		FROM @ConsultaRHTemp
		INNER JOIN dbo.Usuario
			ON tmp_id = usu_id
		INNER JOIN 	Usu_Aval_Comp
			ON usu_id = uac_usu_id
		INNER JOIN Avaliacao
			ON uac_aval_id = aval_id
		WHERE uac_comp_id = @comp2
		AND aval_atual = 1		 	
	END 

	IF @comp3 IS NOT NULL
	BEGIN
		UPDATE @ConsultaRHTemp
		SET tmp_comp_nivel3 = aval_nivel, tmp_comp_id3 = uac_comp_id 
		FROM @ConsultaRHTemp
		INNER JOIN dbo.Usuario
			ON tmp_id = usu_id
		INNER JOIN 	Usu_Aval_Comp
			ON usu_id = uac_usu_id
		INNER JOIN Avaliacao
			ON uac_aval_id = aval_id
		WHERE uac_comp_id = @comp3
		AND aval_atual = 1		 	
	END 

	IF @comp4 IS NOT NULL
	BEGIN
		UPDATE @ConsultaRHTemp
		SET tmp_comp_nivel4 = aval_nivel, tmp_comp_id4 = uac_comp_id 
		FROM @ConsultaRHTemp
		INNER JOIN dbo.Usuario
			ON tmp_id = usu_id
		INNER JOIN 	Usu_Aval_Comp
			ON usu_id = uac_usu_id
		INNER JOIN Avaliacao
			ON uac_aval_id = aval_id
		WHERE uac_comp_id = @comp4
		AND aval_atual = 1		 	
	END 

	IF @comp5 IS NOT NULL
	BEGIN
		UPDATE @ConsultaRHTemp
		SET tmp_comp_nivel5 = aval_nivel, tmp_comp_id5 = uac_comp_id 
		FROM @ConsultaRHTemp
		INNER JOIN dbo.Usuario
			ON tmp_id = usu_id
		INNER JOIN 	Usu_Aval_Comp
			ON usu_id = uac_usu_id
		INNER JOIN Avaliacao
			ON uac_aval_id = aval_id
		WHERE uac_comp_id = @comp5
		AND aval_atual = 1		 	
	END
	
	IF @comp6 IS NOT NULL
	BEGIN
		UPDATE @ConsultaRHTemp
		SET tmp_comp_nivel6 = aval_nivel, tmp_comp_id6 = uac_comp_id 
		FROM @ConsultaRHTemp
		INNER JOIN dbo.Usuario
			ON tmp_id = usu_id
		INNER JOIN 	Usu_Aval_Comp
			ON usu_id = uac_usu_id
		INNER JOIN Avaliacao
			ON uac_aval_id = aval_id
		WHERE uac_comp_id = @comp6
		AND aval_atual = 1		 	
	END

	IF @comp7 IS NOT NULL
	BEGIN
		UPDATE @ConsultaRHTemp
		SET tmp_comp_nivel7 = aval_nivel, tmp_comp_id7 = uac_comp_id 
		FROM @ConsultaRHTemp
		INNER JOIN dbo.Usuario
			ON tmp_id = usu_id
		INNER JOIN 	Usu_Aval_Comp
			ON usu_id = uac_usu_id
		INNER JOIN Avaliacao
			ON uac_aval_id = aval_id
		WHERE uac_comp_id = @comp7
		AND aval_atual = 1		 	
	END

	IF @comp8 IS NOT NULL
	BEGIN
		UPDATE @ConsultaRHTemp
		SET tmp_comp_nivel8 = aval_nivel, tmp_comp_id8 = uac_comp_id 
		FROM @ConsultaRHTemp
		INNER JOIN dbo.Usuario
			ON tmp_id = usu_id
		INNER JOIN 	Usu_Aval_Comp
			ON usu_id = uac_usu_id
		INNER JOIN Avaliacao
			ON uac_aval_id = aval_id
		WHERE uac_comp_id = @comp8
		AND aval_atual = 1		 	
	END

	IF @comp9 IS NOT NULL
	BEGIN
		UPDATE @ConsultaRHTemp
		SET tmp_comp_nivel9 = aval_nivel, tmp_comp_id9 = uac_comp_id 
		FROM @ConsultaRHTemp
		INNER JOIN dbo.Usuario
			ON tmp_id = usu_id
		INNER JOIN 	Usu_Aval_Comp
			ON usu_id = uac_usu_id
		INNER JOIN Avaliacao
			ON uac_aval_id = aval_id
		WHERE uac_comp_id = @comp9
		AND aval_atual = 1		 	
	END

	SELECT tmp_id, tmp_nome, tmp_sobrenome, tmp_comp_nivel0, tmp_comp_id0, tmp_comp_nivel1, tmp_comp_id1,
		tmp_comp_nivel2, tmp_comp_id2, tmp_comp_nivel3, tmp_comp_id3, tmp_comp_nivel4, tmp_comp_id4,
		tmp_comp_nivel5, tmp_comp_id5, tmp_comp_nivel6, tmp_comp_id6, tmp_comp_nivel7, tmp_comp_id7,
		tmp_comp_nivel8, tmp_comp_id8, tmp_comp_nivel9, tmp_comp_id9 
	FROM @ConsultaRHTemp
	WHERE (@comp0 IS NULL OR tmp_comp_id0 = @comp0)
	AND (@comp1 IS NULL OR tmp_comp_id1 = @comp1)
	AND (@comp2 IS NULL OR tmp_comp_id2 = @comp2)
	AND (@comp3 IS NULL OR tmp_comp_id3 = @comp3)
	AND (@comp4 IS NULL OR tmp_comp_id4 = @comp4)
	AND (@comp5 IS NULL OR tmp_comp_id5 = @comp5)
	AND (@comp6 IS NULL OR tmp_comp_id6 = @comp6)
	AND (@comp7 IS NULL OR tmp_comp_id7 = @comp7)
	AND (@comp8 IS NULL OR tmp_comp_id8 = @comp8)
	AND (@comp9 IS NULL OR tmp_comp_id9 = @comp9) 
	
GO



IF OBJECT_ID('dbo.SP_GetRecursosHumanos') IS NOT NULL
    PRINT '<<< CREATED PROCEDURE dbo.SP_GetRecursosHumanos >>>'
ELSE
    PRINT '<<< FAILED CREATING PROCEDURE dbo.SP_GetRecursosHumanos >>>'
GO