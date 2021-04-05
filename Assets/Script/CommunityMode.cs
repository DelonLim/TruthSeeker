using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class CommunityMode : MonoBehaviour
{
    public Button OkButton, CancelButton, ConfirmButton, BackButton, FindQuizButton, CreateQuizButton;
    public InputField CodeTextbox;
    public Image PopoutGroup;
    public Text PanelText;
    int mode = 0;
    string prev;

    private void OnEnable()
    {
        FindQuizButton.onClick.AddListener(() => clicked("Find"));
        CreateQuizButton.onClick.AddListener(() => clicked("Create"));
        CancelButton.onClick.AddListener(() => clicked("Cancel"));
        OkButton.onClick.AddListener(() => clicked("Ok"));
        BackButton.onClick.AddListener(() => clicked("Back"));
        ConfirmButton.onClick.AddListener(() => clicked("Confirm"));
    }

    void clicked(string pressed)
    {
        switch (pressed)
        {
            case "Back":
                if (mode == 0)
                {
                    SceneManager.LoadScene("MainMenu");
                }
                else if (mode == 1)
                {
                    CodeTextbox.gameObject.SetActive(false);
                    CreateQuizButton.gameObject.SetActive(true);
                    FindQuizButton.GetComponentInChildren<Text>().text = "Find Quiz";
                }
                
                break;
            case "Find":
                if (mode == 0)
                {
                    CodeTextbox.gameObject.SetActive(true);
                    CreateQuizButton.gameObject.SetActive(false);
                    FindQuizButton.GetComponentInChildren<Text>().text = "Search";
                    mode = 1;
                }
                else if (mode == 1)
                {
                    string path = Application.dataPath + "/" + CodeTextbox.text +".txt";


                    if (File.Exists(path))
                    {
                        SetPopoutState("Confirm");
                        PanelText.text = "Quiz   Found!   Start    Quiz?";
                    }
                    else
                    {
                        SetPopoutState("Ok");
                        PanelText.text = "Invalid   Quiz   Code,   Please   try   again.";
                        
                    }
                }

                break;
            case "Create":
                SceneManager.LoadScene("GameSetup");

                break;
            case "Cancel":
                SetPopoutState("Close");

                break;
            case "Ok":
                SetPopoutState("Close");

                break;
            case "Confirm":
                Debug.Log("Load Quiz");

                break;
        }
    }

    void SetPopoutState(string Style)
    {
        switch (Style)
        {
            case "Ok":
                PopoutGroup.gameObject.SetActive(true);
                ConfirmButton.gameObject.SetActive(false);
                CancelButton.gameObject.SetActive(false);
                OkButton.gameObject.SetActive(true);

                break;
            case "Confirm":
                PopoutGroup.gameObject.SetActive(true);
                ConfirmButton.gameObject.SetActive(true);
                CancelButton.gameObject.SetActive(true);
                OkButton.gameObject.SetActive(false);

                break;
            case "Close":
                PopoutGroup.gameObject.SetActive(false);
                break;
        }
    }

    private void OnDisable()
    {
        prev = "Community";
        PlayerPrefs.SetString("prev",prev);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
