using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public GameObject Alert, MCQ_UI, Clear_Msg, GameOver_MSG, TimerBG;
    public Button Ans1, Ans2, Ans3, Ans4, cfmbtn, skill_btn;
    public string[] worldSetup;
    private int bossHP = 1;
    public List<GameObject> BG, skillHighlight, ansSkill;
    public List<GameObject> EnemyBoss;
    public List<TMP_Text> topTextDisplay;

    private GameObject enemyHandler, gameHandler, player, playerHandler;
    private bool Ans1Sel = false, Ans2Sel = false, Ans3Sel = false, Ans4Sel = false, isGameEnd = false, isPlayerDead =
        false, timeUP = false, isSkillUsed = false, isHarderMode = false, isIncreaseBoss = false, isHarderBoss = false;
    private float sec = 2.0f;
    private List<string> currMCQs;
    private string HP, timerUpload;
    private int score = 0, passingpoint = 0, gameMode = 0, HPRemain = 0;
    private float timeAtkMode = 50.0f, timeAtkMode1 = 50.0f, timerMinutes = 0.0f, timerSeconds = 0.0f;

    //public GameObject GameManager;
    // Start is called before the first frame update
    void Start()
    {
        setWorldStr();
        SetupWorld();

        timeUP = false;
        string[] world1234 = { "World 1", "World 2", "World 3", "World 4" };

        for(int i = 0; i < 4; i++)
        {
            StartCoroutine(DownloadWorldSetup(world1234[i]));
            StartCoroutine(DownloadWorldData(world1234[i])); 
        }

        gameHandler = GameObject.FindWithTag("GameController");
        enemyHandler = GameObject.FindWithTag("EnemyHandler");
        player = GameObject.FindWithTag("Player");

        gameMode = gameHandler.GetComponent<ModeSelection>().getMode();

        if(gameMode == 2)
        {
            topTextDisplay[1].text = "Inverse";
            TimerBG.SetActive(false);
            Button btn = Ans1.GetComponent<Button>();
            btn.onClick.AddListener(SelectedAns1Inverse);
            Button btn2 = Ans2.GetComponent<Button>();
            btn2.onClick.AddListener(SelectedAns2Inverse);
            Button btn3 = Ans3.GetComponent<Button>();
            btn3.onClick.AddListener(SelectedAns3Inverse);
            Button btn4 = Ans4.GetComponent<Button>();
            btn4.onClick.AddListener(SelectedAns4Inverse);

            Button cfmbtn1 = cfmbtn.GetComponent<Button>();
            cfmbtn1.onClick.AddListener(confirmAnsInverse);
        }
        else if(gameMode == 1)
        {
            topTextDisplay[1].text = "Time Attack";
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

            Button skill_btn1 = skill_btn.GetComponent<Button>();
            skill_btn1.onClick.AddListener(skill);
        }
        else
        {
            topTextDisplay[1].text = "Normal";
            TimerBG.SetActive(false);
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

            Button skill_btn1 = skill_btn.GetComponent<Button>();
            skill_btn1.onClick.AddListener(skill);
        }
        
        Alert.SetActive(false);
        Clear_Msg.SetActive(false);
        GameOver_MSG.SetActive(false);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        float Tmin = Mathf.FloorToInt(timerMinutes / 60);
        float TSec = Mathf.FloorToInt(timerSeconds % 60);
        timerMinutes += Time.deltaTime;
        timerSeconds += Time.deltaTime;

        timerUpload = string.Format("{0:00}:{1:00}", Tmin, TSec);
        //Debug.Log(timerUpload);

        if (gameMode == 1 || isHarderMode)
        {
            float seconds = Mathf.FloorToInt(timeAtkMode % 60);
            topTextDisplay[0].text = string.Format("{0:00}:{1:00}", "0", seconds);
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
        gameHandler = GameObject.FindWithTag("GameController");
        
        string path = "C:/xampp/tmp/" + gameHandler.GetComponent<WorldSelected>().getWorldSel() + " Setup.csv";
        worldSetup = System.IO.File.ReadAllLines(path);
        Debug.Log(path);
    }
    public void setPassingpoint(int totalNumQns)
    {
        passingpoint = totalNumQns / 2;
    }
    public void getPlayerHPDisplay()
    {
        player = GameObject.FindWithTag("Player");
        playerHandler = GameObject.FindWithTag("PlayerHandler");

        //if player char is knight
        if(playerHandler.GetComponent<CharactersSel>().getChar() == 2)
        {
            HP = (player.GetComponent<PlayerCharacter>().getHP() + 1).ToString();
            HPRemain = player.GetComponent<PlayerCharacter>().getHP() + 1;
        }
        else
        {
            HP = player.GetComponent<PlayerCharacter>().getHP().ToString();
            HPRemain = player.GetComponent<PlayerCharacter>().getHP();
        }
        
        topTextDisplay[2].text = "HP : " + HP + " / " + HP;
    }
    private void updateHP()
    {
        topTextDisplay[2].text = "HP : " + HPRemain.ToString() + " / " + HP;
    }
    public int getBossHP()
    {
        setWorldStr();
        Debug.Log(bossHP);
        bossHP = int.Parse(worldSetup[2]);
        return bossHP;
    }
    public GameObject setBoss()
    {
        switch (worldSetup[1])
        {
            case "0": 
                return EnemyBoss[0];
            case "1":
                return EnemyBoss[1];
            case "2":
                return EnemyBoss[2];
            case "3":
                return EnemyBoss[3];
            default:
                return EnemyBoss[0];
        }
    }
    void SetupWorld()
    {
        switch (worldSetup[0])
        {
            case "0":
                Instantiate(BG[0], new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case "1":
                Instantiate(BG[1], new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case "2":
                Instantiate(BG[2], new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case "3":
                Instantiate(BG[3], new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case "4":
                Instantiate(BG[4], new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case "5":
                Instantiate(BG[5], new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case "6":
                Instantiate(BG[6], new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case "7":
                Instantiate(BG[7], new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case "8":
                Instantiate(BG[8], new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case "9":
                Instantiate(BG[9], new Vector3(0, 0, 0), Quaternion.identity);
                break;
            default:
                Instantiate(BG[0], new Vector3(0, 0, 0), Quaternion.identity);
                break;
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
    void SelectedAns1Inverse()
    {
        //Debug.Log("You have clicked the Ans1!");
        if (Ans1Sel)
        {
            Ans1Sel = false;
            Ans1.GetComponent<TextBG>().Unselectedbtn();
        }
        else
        {
            Ans1Sel = true;
            Ans1.GetComponent<TextBG>().Selectedbtn();
        }
    }
    void SelectedAns2Inverse()
    {
        if (Ans2Sel)
        {
            Ans2Sel = false;
            Ans2.GetComponent<TextBG>().Unselectedbtn();
        }
        else
        {
            Ans2Sel = true;
            Ans2.GetComponent<TextBG>().Selectedbtn();
        }
    }
    void SelectedAns3Inverse()
    {
        if (Ans3Sel)
        {
            Ans3Sel = false;
            Ans3.GetComponent<TextBG>().Unselectedbtn();
        }
        else
        {
            Ans3Sel = true;
            Ans3.GetComponent<TextBG>().Selectedbtn();
        }
    }
    void SelectedAns4Inverse()
    {
        if (Ans4Sel)
        {
            Ans4Sel = false;
            Ans3.GetComponent<TextBG>().Unselectedbtn();
        }
        else
        {
            Ans4Sel = true;
            Ans4.GetComponent<TextBG>().Selectedbtn();
        }
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

    void confirmAnsInverse()
    {
        if (Ans1Sel || Ans2Sel || Ans3Sel || Ans4Sel)
        {
            checkAnsInverse();
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
    public void skill()
    {
        playerHandler = GameObject.FindWithTag("PlayerHandler");

        //if player char is warrior, magician, or thief
        if (playerHandler.GetComponent<CharactersSel>().getChar() == 0)
        {
            warriorSkill();
        }
        else if (playerHandler.GetComponent<CharactersSel>().getChar() == 1)
        {
            if (currMCQs[2] == "T")
            {
                Ans1.GetComponent<Image>().color = Color.red;
            }
            else if (currMCQs[4] == "T")
            {
                Ans2.GetComponent<Image>().color = Color.red;
            }
            else if (currMCQs[6] == "T")
            {
                Ans3.GetComponent<Image>().color = Color.red;
            }
            else if (currMCQs[8] == "T")
            {
                Ans4.GetComponent<Image>().color = Color.red;
            }
            magicianSkill();
        }
        else if (playerHandler.GetComponent<CharactersSel>().getChar() == 3)
        {
            thiefSkill();
        }
        isSkillUsed = true;
        skill_btn.interactable = false;
    }
    private void warriorSkill()
    {
        int RadAns = Random.Range(0, 2);
        if(RadAns == 0)
        {
            if (currMCQs[2] == "T")
            {
                Ans1.GetComponent<Image>().color = Color.red;
            }
            else if (currMCQs[4] == "T")
            {
                Ans2.GetComponent<Image>().color = Color.red;
            }
            else if (currMCQs[6] == "T")
            {
                Ans3.GetComponent<Image>().color = Color.red;
            }
            else if (currMCQs[8] == "T")
            {
                Ans4.GetComponent<Image>().color = Color.red;
            }
        }
        else
        {
            magicianSkill();
        }
    }

    private void magicianSkill()
    {
        int RadAns = Random.Range(0, 4);

        if (RadAns == 0)
        {
            if (currMCQs[2] == "F")
            {
                Ans1.GetComponent<Image>().color = Color.red;
            }
            else
            {
                magicianSkill();
            }
        } 
        else if (RadAns == 1)
        {
            if (currMCQs[4] == "F")
            {
                Ans2.GetComponent<Image>().color = Color.red;
            }
            else
            {
                magicianSkill();
            }
        }
        else if (RadAns == 2)
        {
            if (currMCQs[6] == "F")
            {
                Ans3.GetComponent<Image>().color = Color.red;
            }
            else
            {
                magicianSkill();
            }
        }
        else if (RadAns == 3)
        {
            if (currMCQs[8] == "F")
            {
                Ans4.GetComponent<Image>().color = Color.red;
            }
            else
            {
                magicianSkill();
            }
        }

    }
    private void thiefSkill()
    {
        int RadAns = Random.Range(0, 4);

        if (RadAns == 0)
        {
            if (currMCQs[2] == "F")
            {
                ansSkill[RadAns].SetActive(false);
            }
            else
            {
                thiefSkill();
            }
        }
        else if (RadAns == 1)
        {
            if (currMCQs[4] == "F")
            {
                ansSkill[RadAns].SetActive(false);
            }
            else
            {
                thiefSkill();
            }
        }
        else if (RadAns == 2)
        {
            if (currMCQs[6] == "F")
            {
                ansSkill[RadAns].SetActive(false);
            }
            else
            {
                thiefSkill();
            }
        }
        else if (RadAns == 3)
        {
            if (currMCQs[8] == "F")
            {
                ansSkill[RadAns].SetActive(false);
            }
            else
            {
                thiefSkill();
            }
        }
        
    }
        public void getcurrMCQ(List<string> currQns)
    {
        currMCQs = currQns;
    }
    private void setColor()
    {
        Ans1.GetComponent<Image>().color = Color.white;
        Ans2.GetComponent<Image>().color = Color.white;
        Ans3.GetComponent<Image>().color = Color.white;
        Ans4.GetComponent<Image>().color = Color.white;
    }
    private void increaseDiff()
    {
        if (score >= passingpoint && !isHarderMode)
        {
            isHarderMode = true;
            topTextDisplay[1].text = "Time Attack";
            TimerBG.SetActive(true);
        }else if (enemyHandler.GetComponent<Enemy>().isBossSpawn && !isHarderBoss)
        {
            isHarderBoss = true;
            timeAtkMode1 = timeAtkMode1 - 20.0f;
        }

        if(enemyHandler.GetComponent<Enemy>().isBossSpawn && !isIncreaseBoss)
        {
            isIncreaseBoss = true;
            timeAtkMode1 = timeAtkMode1 - 20.0f;
        }
    }
    void checkAns()
    {
        if (Ans1Sel && currMCQs[2] == "T")
        {
            //Debug.Log("Correct");
            score++;
            increaseDiff();
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
            increaseDiff();
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
            increaseDiff();
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
            increaseDiff();
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
            HPRemain--;
            updateHP();
        }
        setColor();
        MCQ_UI.SetActive(false);
        StartCoroutine(DisplayCall());
    }
    void checkAnsInverse()
    {
        if ((Ans1Sel && currMCQs[2] == "F") && (Ans2Sel && currMCQs[4] == "F") && (Ans3Sel && currMCQs[6] == "F") && (!Ans4Sel && currMCQs[8] == "T"))
        {
            //Debug.Log("Correct");
            score++;
            increaseDiff();
            if (enemyHandler.GetComponent<Enemy>().isBossSpawn)
            {
                enemyHandler.GetComponent<Enemy>().deductHP(1);
            }
            enemyHandler.GetComponent<Enemy>().EnemyDeath();
            player.GetComponent<PlayerCharacter>().playAtkAni();
        }
        else if ((Ans1Sel && currMCQs[2] == "F") && (Ans2Sel && currMCQs[4] == "F") && (Ans4Sel && currMCQs[8] == "F") && (!Ans3Sel && currMCQs[6] == "T"))
        {
            score++;
            increaseDiff();
            if (enemyHandler.GetComponent<Enemy>().isBossSpawn)
            {
                enemyHandler.GetComponent<Enemy>().deductHP(1);
            }
            enemyHandler.GetComponent<Enemy>().EnemyDeath();
            player.GetComponent<PlayerCharacter>().playAtkAni();
        }
        else if ((Ans1Sel && currMCQs[2] == "F") && (Ans3Sel && currMCQs[6] == "F") && (Ans4Sel && currMCQs[8] == "F") && (!Ans2Sel && currMCQs[4] == "T"))
        {
            score++;
            increaseDiff();
            if (enemyHandler.GetComponent<Enemy>().isBossSpawn)
            {
                enemyHandler.GetComponent<Enemy>().deductHP(1);
            }
            enemyHandler.GetComponent<Enemy>().EnemyDeath();
            player.GetComponent<PlayerCharacter>().playAtkAni();
        }
        else if ((Ans2Sel && currMCQs[4] == "F") && (Ans3Sel && currMCQs[6] == "F") && (Ans4Sel && currMCQs[8] == "F") && (!Ans1Sel && currMCQs[2] == "T"))
        {
            score++;
            increaseDiff();
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
            HPRemain--;
            updateHP();
        }
        setColor();
        MCQ_UI.SetActive(false);
        StartCoroutine(DisplayCall());
    }
    public void setGameComplete()
    {
        StartCoroutine(SavePlayerData());
        isGameEnd = true;
        //Transfer current score and time to the server
    }

    IEnumerator SavePlayerData() 
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score", score); //user's current score uploaded to playerscoreDB
        form.AddField("time", timerUpload); //time taken by user uploaded to playerscoreDB

        WWW www = new WWW("http://localhost/truthseekers/savedata.php", form);
        yield return www;

        if (www.text == "0")
        {
            Debug.Log("Game Saved.");
        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }
        DBManager.LogOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void setGameOver()
    {
        StartCoroutine(SavePlayerData());
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
            Ans1Sel = false;
            Ans2Sel = false;
            Ans3Sel = false;
            Ans4Sel = false;
            Ans1.GetComponent<TextBG>().Unselectedbtn();
            Ans2.GetComponent<TextBG>().Unselectedbtn();
            Ans3.GetComponent<TextBG>().Unselectedbtn();
            Ans4.GetComponent<TextBG>().Unselectedbtn();
            MCQ_UI.SetActive(true);
            timeUP = false;
            timeAtkMode = timeAtkMode1;
        }
    }
    
    IEnumerator DownloadWorldSetup(string worldArray)
    {
        WWWForm form = new WWWForm();
        form.AddField("WorldName", worldArray);
        WWW www = new WWW("http://localhost/truthseekers/DownloadWorldSetup.php", form);
        yield return www;



        string test1;
        string[] test;

        test1 = www.text;
        test = test1.Split('\n');
        string path = "C:/xampp/tmp/" + worldArray + " Setup.csv";

        for (int x = 0; x < test.Length; x++)
        {
            if (File.Exists(path))
            {
                File.AppendAllText(path, test[x]);
            }
            else
            {
                File.WriteAllText(path, test[x]);
            }
        }
    }

    IEnumerator DownloadWorldData(string worldArray)
    {
        WWWForm form = new WWWForm();
        form.AddField("WorldName", worldArray);
        WWW www = new WWW("http://localhost/truthseekers/DownloadWorldData.php", form);
        yield return www;
        string test1;
        string[] test;

        test1 = www.text;
        test = test1.Split('\n');
        string path = "C:/xampp/tmp/" + worldArray + ".csv";

        for (int x = 0; x < test.Length; x++)
        {
            if (File.Exists(path))
            {
                File.AppendAllText(path, test[x]);
            }
            else
            {
                File.WriteAllText(path, test[x]);
            }
        }
    }
}
