<?php

	$con = mysqli_connect('localhost', 'root', '', 'unityaccess');

	//check if the connection happens
	if (mysqli_connect_errno()) 
	{
		echo "1: Connection failed"; // error code #1 meaning connection failed
		exit();
	}

	$WorldName = $_POST["WorldName"];

	$SelectScorequery = "SELECT * FROM `playerscoredb` WHERE `worldname` = '".$WorldName."' ORDER BY `timetaken` DESC";
	$findScore = mysqli_query($con, $SelectScorequery) or die("2: Fail to insert"); //error code #2 meaning namecheck query failed

	if(mysqli_num_rows($findScore) < 1)
	{
		echo "No Such Name in Database";
		exit();
	}
	
	$allRow = array();
	
	while(($existinginfo = mysqli_fetch_assoc($findScore)))
	{
		$allRow[] = $existinginfo['timetaken'];
	}	

	for($x = 0; $x<count($allRow);$x++)
	{
		echo $allRow[$x].",";
	}
	
	
	exit();
	
?>