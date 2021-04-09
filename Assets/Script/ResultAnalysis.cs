using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultAnalysis : MonoBehaviour
{
    public Button BackButton, QReportButton, TReportButton, WReportButton;
    string mode;

    string test1,test2="",testt,testlast="";
    string[] test,testtime;
    string[] AvgTime = new string[20];
    List<string> newlist;
    int z = 0;


    // Start is called before the first frame update
    void Start()
    {
        test1 = PlayerPrefs.GetString("WorldName");
        test = test1.Split(',');

        for (int x = 0; x < test.Length; x++)
        {
            StartCoroutine(LoadScore(test[x]));
            StartCoroutine(LoadTime(test[x]));
        }



    }

    private void OnEnable()
    {
        
        BackButton.onClick.AddListener(() => clicked("Back"));
        QReportButton.onClick.AddListener(() => clicked("QReport"));
        TReportButton.onClick.AddListener(() => clicked("TReport"));
        WReportButton.onClick.AddListener(() => clicked("WReport"));
    }

    private void OnDisable()
    {
        
        Debug.Log(testlast);
        PlayerPrefs.SetString("AvgTime", testlast.Trim(','));
        PlayerPrefs.SetString("AvgScore", test2.Trim(','));
        PlayerPrefs.SetString("WorldName", test1);
        PlayerPrefs.SetString("mode",mode);
    }

    void clicked(string pressed)
    {
        switch (pressed)
        {
            case "Back":
                SceneManager.LoadScene("AdminMenu");
                break;
            case "QReport":
                mode = "Q";
                SceneManager.LoadScene("PostResultAnalysis");
                break;
            case "TReport":
                mode = "T";
                SceneManager.LoadScene("PostResultAnalysis");
                break;
            case "WReport":
                mode = "W";
                SceneManager.LoadScene("PostResultAnalysis");
                break;
        }
    }



    IEnumerator LoadScore(string WorldName)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", WorldName);

        WWW www = new WWW("http://localhost/truthseekers/LoadScore.php", form);
        yield return www;
        test2 = test2+ www.text+",";


    }

    IEnumerator LoadTime(string WorldName)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", WorldName);
        WWW www = new WWW("http://localhost/truthseekers/LoadTime.php", form);
        yield return www;
        
        testt =  www.text;
        testtime = testt.Split(',', ':');
        //Debug.Log(testtime[0] + " " + testtime[1]);
        
        newlist = new List<string>(testtime);
        
        for (int x = 0; x< newlist.Count;x++)
        {
            if(newlist[x] == "00")
            {
                newlist.RemoveAt(x);
            }
        }
        for (int y=0;y< newlist.Count;y++)
        {
            testlast = testlast + "," + newlist[y];
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
