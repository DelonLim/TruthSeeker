<?php

	$con = mysqli_connect('localhost', 'root', '', 'unityaccess');

	//check if the connection happens
	if (mysqli_connect_errno()) 
	{
		echo "1: Connection failed"; // error code #1 meaning connection failed
		exit();
	}
	
	$WorldName = $_POST["WorldName"];
	$FileName = $_POST["FileName"];
	$WorldNameSetup = $_POST["WorldNameSetup"];
	$FileNameSetup = $_POST["FileNameSetup"];

	$includeWorldNameSetupquery = "INSERT INTO `worldsetupdb`(`WorldName`, `TXTFile`) VALUES ('" .$WorldNameSetup."','')";


	$includeWorldNameSetup = mysqli_query($con, $includeWorldNameSetupquery) or die("2: Fail to insert"); //error code #2 meaning namecheck query failed


	$updateSetupquery = "UPDATE `worldsetupdb` SET `TXTFile`=LOAD_FILE('C:/xampp/tmp/" .$FileNameSetup. "') WHERE `WorldName`='".$WorldNameSetup."';";
	
	mysqli_query($con, $updateSetupquery) or die("7: Save query failed"); //error code #7 - UPDATE query failed

	//double checks that there is only one user with this name
	$includeWorldNamequery = "INSERT INTO `questionsdb`(`WorldName`, `TXTFile`) VALUES ('" .$WorldName."','')";


	$includeWorldName = mysqli_query($con, $includeWorldNamequery) or die("2: Fail to insert"); //error code #2 meaning namecheck query failed


	$updatequery = "UPDATE `questionsdb` SET `TXTFile`=LOAD_FILE('C:/xampp/tmp/" .$FileName. "') WHERE `WorldName`='".$WorldName."';";
	
	mysqli_query($con, $updatequery) or die("7: Save query failed"); //error code #7 - UPDATE query failed

	echo "0";
?>