using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostResultAnalysis : MonoBehaviour
{
    public Button BackButton;
    public Text PostResultAnalysisText;
    public ScrollRect WorldScoreTable, WorldTimeTable, WorldQuestionScoreTable;
    string mode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        mode = PlayerPrefs.GetString("mode");
        BackButton.onClick.AddListener(() => clicked("Back"));

        switch (mode)
        {
            case "T":
                WorldTimeTable.gameObject.SetActive(true);
                PostResultAnalysisText.text = "Time   Report";
                break;
            case "W":
                WorldScoreTable.gameObject.SetActive(true);
                PostResultAnalysisText.text = "World   Report";
                break;
            case "Q":
                WorldQuestionScoreTable.gameObject.SetActive(true);
                PostResultAnalysisText.text = "Question   Report";
                break;
        }

    }

    void clicked(string pressed)
    {
        switch (pressed)
        {
            case "Back":
                SceneManager.LoadScene("ResultAnalysis");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
