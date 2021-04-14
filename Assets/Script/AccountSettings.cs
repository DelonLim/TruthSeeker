using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AccountSettings : MonoBehaviour
{
    public Button BackButton, LeaderboardButton;
    string worldname,score = "",time = "",scoreuser = "",timeuser = "";
    string[] WorldNames;


    // Start is called before the first frame update
    void Start()
    {
        worldname = PlayerPrefs.GetString("WorldName");
        WorldNames = worldname.Split(',');

        for (int x = 0; x < WorldNames.Length; x++)
        {
            
            StartCoroutine(LoadScore(WorldNames[x]));
            StartCoroutine(LoadScoreUser(WorldNames[x]));
            StartCoroutine(LoadTime(WorldNames[x]));
            StartCoroutine(LoadTimeUser(WorldNames[x]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        BackButton.onClick.AddListener(() => clicked("Back"));
        LeaderboardButton.onClick.AddListener(() => clicked("Leaderboard"));
    }
    void OnDisable()
    {
        PlayerPrefs.SetString("WorldName", worldname);
        PlayerPrefs.SetString("Score",score);
        PlayerPrefs.SetString("ScoreUser", scoreuser);
        PlayerPrefs.SetString("Time", time);
        PlayerPrefs.SetString("TimeUser", timeuser);
    }

    void clicked(string pressed)
    {
        switch (pressed)
        {
            case "Back":
                SceneManager.LoadScene("MainMenu");
                break;
            case "Leaderboard":
                SceneManager.LoadScene("Leaderboard");
                break;
        }
    }

    IEnumerator LoadScore(string WorldName)
    {
        WWWForm form = new WWWForm();
        form.AddField("WorldName", WorldName);

        WWW www = new WWW("http://localhost/truthseekers/LeaderboardScore.php", form);
        yield return www;
        score = score + www.text + ",";

    }

    IEnumerator LoadScoreUser(string WorldName)
    {
        WWWForm form = new WWWForm();
        form.AddField("WorldName", WorldName);

        WWW www = new WWW("http://localhost/truthseekers/LeaderboardScoreUser.php", form);
        yield return www;
        scoreuser = scoreuser + www.text + ",";

    }

    IEnumerator LoadTime(string WorldName)
    {
        WWWForm form = new WWWForm();
        form.AddField("WorldName", WorldName);

        WWW www = new WWW("http://localhost/truthseekers/LeaderboardTime.php", form);
        yield return www;
        time = time + www.text + ",";

    }

    IEnumerator LoadTimeUser(string WorldName)
    {
        WWWForm form = new WWWForm();
        form.AddField("WorldName", WorldName);

        WWW www = new WWW("http://localhost/truthseekers/LeaderboardTimeUser.php", form);
        yield return www;
        timeuser = timeuser + www.text + ",";

    }

}
