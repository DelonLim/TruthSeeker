<?php

	$con = mysqli_connect('localhost', 'root', '', 'unityaccess');

	//check if the connection happens
	if (mysqli_connect_errno()) 
	{
		echo "1: Connection failed"; // error code #1 meaning connection failed
		exit();
	}

	$WorldName = $_POST["name"];

	$SelectScorequery = "SELECT AVG(score) AS score FROM playerscoredb WHERE `worldname` = '".$WorldName."';";
	$findScore = mysqli_query($con, $SelectScorequery) or die("2: Fail to insert"); //error code #2 meaning namecheck query failed

	if(mysqli_num_rows($findScore) < 1)
	{
		echo "No Such Name in Database";
		exit();
	}
	
	$existinginfo = mysqli_fetch_assoc($findScore);
	echo $existinginfo['score'];
	
	/*
	$allRow = array();
	
	while(($existinginfo = mysqli_fetch_assoc($findScore)))
	{
		$allRow[] = $existinginfo['score'];
	}	

	for($x = 0; $x<count($allRow);$x++)
	{
		echo $allRow[$x].",";
	}
	*/
	exit();
	
?>