using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WorldQuestionScoreRow : MonoBehaviour
{
    string worldname, score, scoreuser, time, timeuser;
    string[] TempWorldNames, TempScores, TempScoreUsers, TempTimes, TempTimeUsers;
    string[][] Scores, ScoreUsers, Times, TimeUsers;
    int z = 0, y = 0, check = 0;
    int[] counting = { 0,0,0};
    public Dropdown WorldDropdown, SortDropdown, OrderDropdown;
    
    GameObject count;

    void Start()
    {
        worldname = PlayerPrefs.GetString("WorldName");
        score = PlayerPrefs.GetString("Score");
        scoreuser = PlayerPrefs.GetString("ScoreUser");
        time = PlayerPrefs.GetString("Time");
        timeuser = PlayerPrefs.GetString("TimeUser");

        Debug.Log("Score is " + score);
        Debug.Log("scoreuser is " + scoreuser);
        Debug.Log("time is " + time);
        Debug.Log("timeuser is " + timeuser);


        TempWorldNames = worldname.Split(',');
        TempScores = score.Split(',');
        TempScoreUsers = scoreuser.Split(',');
        TempTimes = time.Split(',');
        TempTimeUsers = timeuser.Split(',');

        Scores = new string[TempWorldNames.Length][];
        ScoreUsers = new string[TempWorldNames.Length][];
        Times = new string[TempWorldNames.Length][];
        TimeUsers = new string[TempWorldNames.Length][];

        for (int y = 0; y < TempWorldNames.Length; y++)
        {
            for (int x = z; x < TempScores.Length; x++)
            {
                if (TempScores[x] != "")
                {
                    counting[y]++;
                    z++;
                    
                }
                else
                {
                    z++;
                    break;
                }
            }
            
        }

        for (int x = 0; x< Scores.Length;x++)
        {
            Scores[x] = new string[counting[x]];
            ScoreUsers[x] = new string[counting[x]];
            Times[x] = new string[counting[x]];
            TimeUsers[x] = new string[counting[x]];
        }


        for (int z = 0; z < TempWorldNames.Length; z++)
        {
            for (int x = 0; x < Scores[z].Length; x++)
            {
                if (TempScores[x] != "")
                {
                    Scores[z][x] = Scores[z][x] + TempScores[y];
                    ScoreUsers[z][x] = ScoreUsers[z][x] + TempScoreUsers[y];
                    Times[z][x] = Times[z][x] + TempTimes[y];
                    TimeUsers[z][x] = TimeUsers[z][x] + TempTimeUsers[y];
                    y++;
                }
                else 
                {
                    break;
                }
            }
            y++;
        }
        
    }
    private void OnEnable()
    {
        WorldDropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(); });
        SortDropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(); });
        OrderDropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(); });
    }
    void CreateRows(int world, string type, string order)
    {
        if (check != 0)
        {
            var clones = GameObject.FindGameObjectsWithTag("clone");
            foreach (var clone in clones)
            {
                Destroy(clone);
            }
        }

        GameObject RowTemplate = transform.GetChild(0).gameObject;

        if (type == "Score")
        {
            if (order == "Descending")
            {
                int N = Scores[world].Length; // How many Rows?
                for (int x = 0; x < N; x++)
                {
                    count = Instantiate(RowTemplate, transform);
                    count.gameObject.tag = "clone";
                    count.transform.GetChild(1).GetComponent<Text>().text = TempWorldNames[world]; //World Name
                    count.transform.GetChild(3).GetComponent<Text>().text = ScoreUsers[world][x].ToString(); //Studtent Name
                    count.transform.GetChild(5).GetComponent<Text>().text = Scores[world][x].ToString(); //Score
                    check++;
                }
            }
            else
            {
                int N = (Scores[world].Length)-1; // How many Rows?
                for (int x = N; x >= 0; x--)
                {
                    count = Instantiate(RowTemplate, transform);
                    count.gameObject.tag = "clone";
                    count.transform.GetChild(1).GetComponent<Text>().text = TempWorldNames[world]; //World Name
                    count.transform.GetChild(3).GetComponent<Text>().text = ScoreUsers[world][x].ToString(); //Studtent Name
                    count.transform.GetChild(5).GetComponent<Text>().text = Scores[world][x].ToString(); //Score
                    check++;
                }
            }
        }
        else if(type == "Time")
        {
            if (order == "Descending")
            {
                int N = Times[world].Length; // How many Rows?
                for (int x = 0; x < N; x++)
                {
                    count = Instantiate(RowTemplate, transform);
                    count.gameObject.tag = "clone";
                    count.transform.GetChild(1).GetComponent<Text>().text = TempWorldNames[world]; //World Name
                    count.transform.GetChild(3).GetComponent<Text>().text = TimeUsers[world][x].ToString(); //Studtent Name
                    count.transform.GetChild(5).GetComponent<Text>().text = Times[world][x].ToString(); //time
                    check++;
                }
            }
            else
            {
                int N = (Times[world].Length) - 1; // How many Rows?
                for (int x = N; x >= 0; x--)
                {
                    count = Instantiate(RowTemplate, transform);
                    count.gameObject.tag = "clone";
                    count.transform.GetChild(1).GetComponent<Text>().text = TempWorldNames[world]; //World Name
                    count.transform.GetChild(3).GetComponent<Text>().text = TimeUsers[world][x].ToString(); //Studtent Name
                    count.transform.GetChild(5).GetComponent<Text>().text = Times[world][x].ToString(); //time
                    check++;
                }
            }
        }
    }


    void DropdownValueChanged()
    {
        CreateRows(WorldDropdown.value, SortDropdown.options[SortDropdown.value].text, OrderDropdown.options[OrderDropdown.value].text);      
    }

}
