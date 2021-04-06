<?php

	$con = mysqli_connect('localhost', 'root', '', 'unityaccess');

	//check if the connection happens
	if (mysqli_connect_errno()) 
	{
		echo "1: Connection failed"; // error code #1 meaning connection failed
		exit();
	}
	$selectedWorld = $_POST["name"];
	
	$DeleteSetupquery = "DELETE FROM `worldsetupdb` WHERE `WorldName` = '".$selectedWorld." Setup'";
	$DeleteSetup = mysqli_query($con, $DeleteSetupquery) or die("2: Fail to insert"); //error code #2 meaning namecheck query failed
	
	$DeleteWorldquery = "DELETE FROM `questionsdb` WHERE `WorldName` = '".$selectedWorld."'";
	$DeleteWorld = mysqli_query($con, $DeleteWorldquery) or die("2: Fail to insert"); //error code #2 meaning namecheck query failed

	if(mysqli_num_rows($DeleteWorld) < 1)
	{
		echo "No Such Wolrd in Database";
		exit();
	}
	else if (mysqli_num_rows($DeleteSetup) < 1)
	{
		echo "No Such Setup in Database";
		exit();
		
	}
	exit();
	
?>