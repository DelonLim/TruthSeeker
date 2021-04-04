using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultAnalysis : MonoBehaviour
{

    public Button BackButton, QReportButton, TReportButton, WReportButton;
    string mode;
    // Start is called before the first frame update
    void Start()
    {

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
    // Update is called once per frame
    void Update()
    {
        
    }
}
