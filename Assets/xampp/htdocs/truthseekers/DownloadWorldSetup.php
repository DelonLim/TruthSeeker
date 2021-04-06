<?php

	$con = mysqli_connect('localhost', 'root', '', 'unityaccess');

	//check if the connection happens
	if (mysqli_connect_errno()) 
	{
		echo "1: Connection failed"; // error code #1 meaning connection failed
		exit();
	}

	$selectedWorld = $_POST["WorldName"];
	
	$SelectWorldquery = "SELECT `TXTFile` FROM `worldsetupdb` WHERE `WorldName` = '".$selectedWorld." Setup'";
	$findWorldContent = mysqli_query($con, $SelectWorldquery) or die("2: Fail to insert"); //error code #2 meaning namecheck query failed

	if(mysqli_num_rows($findWorldContent) < 1)
	{
		echo "No Such Name in Database";
		exit();
	}
	
	$existinginfo = mysqli_fetch_assoc($findWorldContent);
	
	echo $existinginfo["TXTFile"];
	
	/*$allRow = array();
	
	while(($existinginfo = mysqli_fetch_assoc($findWorldContent)))
	{
		$allRow[] = $existinginfo['WorldName'];
	}	

	for($x = 0; $x<count($allRow);$x++)
	{
		echo $allRow[$x].",";
	}*/

	exit();
	
?>