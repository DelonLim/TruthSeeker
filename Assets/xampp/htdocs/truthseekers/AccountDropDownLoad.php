<?php

	$con = mysqli_connect('localhost', 'root', '', 'unityaccess');

	//check if the connection happens
	if (mysqli_connect_errno()) 
	{
		echo "1: Connection failed"; // error code #1 meaning connection failed
		exit();
	}

	$Namequery = "SELECT `Username` FROM `players` WHERE `Username`!='realadmin';";
	$findName = mysqli_query($con, $Namequery) or die("2: Fail to insert"); //error code #2 meaning namecheck query failed

	if(mysqli_num_rows($findName) < 1)
	{
		echo "No Such Name in Database";
		exit();
	}
	$allRow = array();
	
	while(($existinginfo = mysqli_fetch_assoc($findName)))
	{
		$allRow[] = $existinginfo['Username'];
	}	

	for($x = 0; $x<count($allRow);$x++)
	{
		echo $allRow[$x].",";
	}

	exit();
	
?>