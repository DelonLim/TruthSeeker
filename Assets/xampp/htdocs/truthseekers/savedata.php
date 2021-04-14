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
	$world = $_POST["world"];

	//double checks that there is only one user with this name
	$namecheckquery = "SELECT username FROM playerscoredb WHERE `username`='".$username."' AND `worldname` = '".$world."';";


	$namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed"); //error code #2 meaning namecheck query failed

	if (mysqli_num_rows($namecheck) > 0) //if user exists in score database already, just update the score 
	{	
		$triesquery = "SELECT nooftries FROM playerscoredb WHERE `username`='".$username."' AND `worldname` = '".$world."';";
		$tries = mysqli_query($con, $triesquery) or die("2: Name check query failed");
		$count = 1;
		$result = mysqli_fetch_assoc($tries);
		$sum = intval($result['nooftries']) + $count;
		$updatequery = "UPDATE playerscoredb SET score = '".$newscore."', timetaken = '".$timetaken ."', nooftries ='".$sum."' WHERE username = '".$username."' AND `worldname` = '".$world."';";
		mysqli_query($con, $updatequery) or die("7: Save query failed"); //error code #7 - UPDATE query failed
		echo "0";
		exit();
	}
	else
	{
		$biggestidquery = "SELECT MAX(id) AS id FROM playerscoredb;";
		$biggestid = mysqli_query($con, $biggestidquery) or die("2: Name check query failed");
		$count = 1;
		$id = mysqli_fetch_assoc($biggestid);
		$sum = intval($id['id']) + $count;
		$insertuserquery = "INSERT INTO `playerscoredb` (`id`, `worldname`, `nooftries`, `score`, `timetaken`, `username`) VALUES ('".$sum."','".$world."', '1', '".$newscore."', '".$timetaken."', '".$username."')";
		mysqli_query($con, $insertuserquery) or die("4: Insert player query failed");
		echo "0";
		exit();
			
	}



?>