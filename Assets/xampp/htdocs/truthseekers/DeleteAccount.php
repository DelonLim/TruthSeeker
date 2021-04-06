<?php

	$con = mysqli_connect('localhost', 'root', '', 'unityaccess');

	//check if the connection happens
	if (mysqli_connect_errno()) 
	{
		echo "1: Connection failed"; // error code #1 meaning connection failed
		exit();
	}
	$selectedName = $_POST["name"];
	
	$DeleteNamequery = "DELETE FROM `players` WHERE `Username` = '".$selectedName."'";
	$DeleteName = mysqli_query($con, $DeleteNamequery) or die("2: Fail to insert"); //error code #2 meaning namecheck query failed

	if(mysqli_num_rows($DeleteName) < 1)
	{
		echo "No Such Name in Database";
		exit();
	}
	exit();
	
?>