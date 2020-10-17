SELECT 
	CAST(gl.DateLogged as datetime) AS 'Date Logged',
	CASE
		WHEN gl.TeamScore > gl.OpponentScore THEN 'Win'
		WHEN gl.TeamScore < gl.OpponentScore THEN 'Loss'
		WHEN gl.TeamScore = gl.OpponentScore THEN 'Draw'
	END AS 'Match result',
	CAST(gl.Kills as float) / CAST(gl.Deaths as float) AS 'K/D',
	r.RankName AS 'Rank',
	ra.AdjustmentName AS 'Rank Adjustment'
FROM 
	GameLogs gl
	inner join Ranks r on
		r.RankID = gl.RankID
	LEFT JOIN RankAdjustments ra ON
		ra.AdjustmentID = gl.RankAdjustmentID
WHERE 
	gl.RankID is Not null AND Season = 2
ORDER BY 
	gl.DateLogged;