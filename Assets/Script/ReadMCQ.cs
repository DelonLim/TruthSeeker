using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ReadMCQ : MonoBehaviour
{
    public string[] AllQnsAns;
    public TMP_Text MCQ_Qns, Ans1, Ans2, Ans3, Ans4;

    private GameObject EnemyMCQ, gameHandler, playerHandler;
    private int qnsLocation = 0;

    List<string> currQns = new List<string>();
    List<List<string>> WrongQns = new List<List<string>>();
    
    // Start is called before the first frame update
    void Start()
    {
        
        string path = "Assets\\Resources\\MCQ_dummyQuestions.txt";
        AllQnsAns = System.IO.File.ReadAllLines(path);

        EnemyMCQ = GameObject.FindWithTag("EnemyHandler");
        EnemyMCQ.GetComponent<Enemy>().countEnemy(AllQnsAns);
        gameHandler = GameObject.FindWithTag("GameController");
        playerHandler = GameObject.FindWithTag("Player");
        playerHandler.GetComponent<PlayerCharacter>().setPlayerHP(AllQnsAns);
        gameHandler.GetComponent<GameHandler>().getPlayerHPDisplay();
        //Debug.Log(AllQnsAns.Length);
        //each qns should have 9 lines 1st line qns follow by each answer and it's True False value

        PutinIndividual(AllQnsAns);
    }
    void DisplayQNS()
    {
        //Debug.Log();
        MCQ_Qns.text = "Q." + currQns[0];
        Ans1.text = "1. " + currQns[1];
        Ans2.text = "2. " + currQns[3];
        Ans3.text = "3. " + currQns[5];
        Ans4.text = "4. " + currQns[7];
        gameHandler.GetComponent<GameHandler>().getcurrMCQ(currQns);
    }
    void PutinIndividual(string[] AllQnsAns)
    {
        //int qnsCount = AllQnsAns.Length / 9;
        //Debug.Log(AllQnsAns.Length);

        if(AllQnsAns.Length > qnsLocation)
        {
            currQns.Clear();
            for (int i = qnsLocation; i < qnsLocation + 9; i++)
            {
                currQns.Add(AllQnsAns[i]);
            }
            DisplayQNS();
        }
        else if(WrongQns.Count == 0)
        {
            Debug.Log("End game from MCQ");
            //for loop to display wrong qeustions
            //wrong questions --;
            //wrong questions ++; should be set at incorrect in gamehandler
        }//else end game // by right won't go into this conditions
        
    }
    public void nextQns()
    {
        qnsLocation += 9;
        PutinIndividual(AllQnsAns);
    }

    public void saveWrongQns(List<string> currWrongQns)
    {
        WrongQns.Add(currWrongQns);
    }
}
