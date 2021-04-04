﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public GameObject Alert, MCQ_UI, Clear_Msg, GameOver_MSG;
    public Button Ans1, Ans2, Ans3, Ans4, cfmbtn;
    public string[] worldSetup;
    private int bossHP = 1;
    public List<GameObject> BG;
    public List<GameObject> EnemyBoss;
    public TMP_Text timerDisplay;

    private GameObject enemyHandler, gameHandler, player;
    private bool Ans1Sel = false, Ans2Sel = false, Ans3Sel = false, Ans4Sel = false, isGameEnd = false, isPlayerDead =
        false;
    private float sec = 2.0f;
    private List<string> currMCQs;
    private int score = 0, gameMode = 0;
    private float timeAtkMode = 5.0f;
    private bool timeUP = false;
    //public GameObject GameManager;
    // Start is called before the first frame update
    void Start()
    {
        setWorldStr();
        SetupWorld();

        timeUP = false;

        gameHandler = GameObject.FindWithTag("GameController");
        enemyHandler = GameObject.FindWithTag("EnemyHandler");
        player = GameObject.FindWithTag("Player");

        gameMode = 1; //gameHandler.GetComponent<ModeSelection>().getMode();

        Alert.SetActive(false);
        Clear_Msg.SetActive(false);
        GameOver_MSG.SetActive(false);

        Button btn = Ans1.GetComponent<Button>();
        btn.onClick.AddListener(SelectedAns1);
        Button btn2 = Ans2.GetComponent<Button>();
        btn2.onClick.AddListener(SelectedAns2);
        Button btn3 = Ans3.GetComponent<Button>();
        btn3.onClick.AddListener(SelectedAns3);
        Button btn4 = Ans4.GetComponent<Button>();
        btn4.onClick.AddListener(SelectedAns4);

        Button cfmbtn1 = cfmbtn.GetComponent<Button>();
        cfmbtn1.onClick.AddListener(confirmAns);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMode == 1)
        {
            float seconds = Mathf.FloorToInt(timeAtkMode % 60);
            timerDisplay.text = string.Format("{0:00}:{1:00}", "0", seconds);
            if (!timeUP)
            {
                if (timeAtkMode > 0)
                {
                    timeAtkMode -= Time.deltaTime;
                }
                else
                {
                    Debug.Log("Time has run out!");
                    timeAtkMode = 0;
                    timeUP = true;
                    gameHandler.GetComponent<ReadMCQ>().saveWrongQns(currMCQs);
                    player.GetComponent<PlayerCharacter>().deductHP();
                    enemyHandler.GetComponent<Enemy>().EnemyAtk();
                    MCQ_UI.SetActive(false);
                    StartCoroutine(DisplayCall());
                }
            }
        }
    }
    private void setWorldStr()
    {
        string path = "Assets\\Resources\\WorldSetup.txt";
        worldSetup = System.IO.File.ReadAllLines(path);
    }
    public int getBossHP()
    {
        setWorldStr();
        bossHP = int.Parse(worldSetup[2]);
        return bossHP;
    }
    public GameObject setBoss()
    {
        switch (worldSetup[1])
        {
            case "1": 
                return EnemyBoss[0];
            case "2":
                return EnemyBoss[1];
            case "3":
                return EnemyBoss[2];
            case "4":
                return EnemyBoss[3];
            default:
                return EnemyBoss[0];
        }
    }
    void SetupWorld()
    {
        if(worldSetup[0] == "1")
        {
            Debug.Log("");
            Instantiate(BG[8], new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
    void SelectedAns1()
    {
        //Debug.Log("You have clicked the Ans1!");
        Ans1Sel = true;
        Ans2Sel = false;
        Ans3Sel = false;
        Ans4Sel = false;
        Ans1.GetComponent<TextBG>().Selectedbtn();
        Ans2.GetComponent<TextBG>().Unselectedbtn();
        Ans3.GetComponent<TextBG>().Unselectedbtn();
        Ans4.GetComponent<TextBG>().Unselectedbtn();
    }
    void SelectedAns2()
    {
        Ans1Sel = false;
        Ans2Sel = true;
        Ans3Sel = false;
        Ans4Sel = false;
        Ans1.GetComponent<TextBG>().Unselectedbtn();
        Ans2.GetComponent<TextBG>().Selectedbtn();
        Ans3.GetComponent<TextBG>().Unselectedbtn();
        Ans4.GetComponent<TextBG>().Unselectedbtn();
    }
    void SelectedAns3()
    {
        Ans1Sel = false;
        Ans2Sel = false;
        Ans3Sel = true;
        Ans4Sel = false;
        Ans1.GetComponent<TextBG>().Unselectedbtn();
        Ans2.GetComponent<TextBG>().Unselectedbtn();
        Ans3.GetComponent<TextBG>().Selectedbtn();
        Ans4.GetComponent<TextBG>().Unselectedbtn();
    }
    void SelectedAns4()
    {
        Ans1Sel = false;
        Ans2Sel = false;
        Ans3Sel = false;
        Ans4Sel = true;
        Ans1.GetComponent<TextBG>().Unselectedbtn();
        Ans2.GetComponent<TextBG>().Unselectedbtn();
        Ans3.GetComponent<TextBG>().Unselectedbtn();
        Ans4.GetComponent<TextBG>().Selectedbtn();
    }

    void confirmAns()
    {
        if(Ans1Sel || Ans2Sel || Ans3Sel || Ans4Sel)
        {
            checkAns();
        }
        else
        {
            Alert.SetActive(true);
            StartCoroutine(LateCall());
            Debug.Log("Please select one of the answer");
        }
    }

    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(sec);
        Alert.SetActive(false);
    }

    public void getcurrMCQ(List<string> currQns)
    {
        currMCQs = currQns;
    }
    void checkAns()
    {
        if (Ans1Sel && currMCQs[2] == "T")
        {
            //Debug.Log("Correct");
            score++;
            if (enemyHandler.GetComponent<Enemy>().isBossSpawn)
            {
                enemyHandler.GetComponent<Enemy>().deductHP(1);
            }
            enemyHandler.GetComponent<Enemy>().EnemyDeath();
            player.GetComponent<PlayerCharacter>().playAtkAni();
        }
        else if (Ans2Sel && currMCQs[4] == "T")
        {
            score++;
            if (enemyHandler.GetComponent<Enemy>().isBossSpawn)
            {
                enemyHandler.GetComponent<Enemy>().deductHP(1);
            }
            enemyHandler.GetComponent<Enemy>().EnemyDeath();
            player.GetComponent<PlayerCharacter>().playAtkAni();
        }
        else if (Ans3Sel && currMCQs[6] == "T")
        {
            score++;
            if (enemyHandler.GetComponent<Enemy>().isBossSpawn)
            {
                enemyHandler.GetComponent<Enemy>().deductHP(1);
            }
            enemyHandler.GetComponent<Enemy>().EnemyDeath();
            player.GetComponent<PlayerCharacter>().playAtkAni();
        }
        else if (Ans4Sel && currMCQs[8] == "T")
        {
            score++;
            if (enemyHandler.GetComponent<Enemy>().isBossSpawn)
            {
                enemyHandler.GetComponent<Enemy>().deductHP(1);
            }
            enemyHandler.GetComponent<Enemy>().EnemyDeath();
            player.GetComponent<PlayerCharacter>().playAtkAni();
        }
        else
        {
            Debug.Log("Incorrect");
            //Save the questions and reuse the questions at the back
            gameHandler.GetComponent<ReadMCQ>().saveWrongQns(currMCQs);
            player.GetComponent<PlayerCharacter>().deductHP();
            enemyHandler.GetComponent<Enemy>().EnemyAtk();
        }
        MCQ_UI.SetActive(false);
        StartCoroutine(DisplayCall());
    }

    public void setGameComplete()
    {
        isGameEnd = true;
        //Transfer current score and time to the server
    }
    public void setGameOver()
    {
        isPlayerDead = true;
    }
    IEnumerator DisplayCall()
    {
        yield return new WaitForSeconds(sec);
        gameHandler.GetComponent<ReadMCQ>().nextQns();
        if (isPlayerDead)
        {
            GameOver_MSG.SetActive(true);
        }
        else if(isGameEnd)
        {
            Clear_Msg.SetActive(true);
            //put method and go back to world selection
        }else if (!isGameEnd)
        {
            MCQ_UI.SetActive(true);
            timeUP = false;
            timeAtkMode = 5.0f;
        }
    }
}
