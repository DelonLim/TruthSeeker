<?php

	$con = mysqli_connect('localhost', 'root', '', 'unityaccess');

	//check if the connection happens
	if (mysqli_connect_errno()) 
	{
		echo "1: Connection failed"; // error code #1 meaning connection failed
		exit();
	}
	
	$username = $_POST["name"];
	$newscore = $_POST["score"];
	$timetaken = $_POST["time"];

	//double checks that there is only one user with this name
	$namecheckquery = "SELECT username FROM playerscoredb WHERE username='"  . $username . "';";


	$namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed"); //error code #2 meaning namecheck query failed

	if (mysqli_num_rows($namecheck) > 0) //if user exists in score database already, just update the score 
	{
		$updatequery = "UPDATE playerscoredb SET score = '".$newscore."', timetaken = '".$timetaken ."' WHERE username = '" . $username . "' ";
		mysqli_query($con, $updatequery) or die("7: Save query failed"); //error code #7 - UPDATE query failed
		echo "0";
		exit();
	}

	$insertuserquery = "INSERT INTO playerscoredb (username,score,timetaken) VALUES ('" . $username .  "', '" . $newscore . "', '" . $timetaken. "')";
	mysqli_query($con, $insertuserquery) or die("4: Insert player query failed");
	echo "0";
	exit();

?>