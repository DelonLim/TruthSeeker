using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class EditWorld : MonoBehaviour
{

    public Button NextQtButton, FinishButton, CancelButton, OkButton, PCancelButton, ConfirmButton;
    public InputField QuestionTextBox, AnsOneTextBox, AnsTwoTextBox, AnsThreeTextBox, AnsFourTextBox;
    public Image PopoutGroup;
    public Text PanelText;
    public Toggle ToggleOne, ToggleTwo, ToggleThree, ToggleFour;
    string WorldName = "", actualpath;
    int BossHP, Boss, BG, QCount = 0, end = 0, NextStart = 0, once = 0,NumQuest =0;
    string[] inText;
    // Start is called before the first frame update
    void Start()
    {
        actualpath = Application.dataPath + "/" + WorldName + ".txt";
        inText = File.ReadAllLines(actualpath);
        NumQuest = (inText.Length + 1) / 9;
        loadFile();
    }

    void OnEnable()
    {
        BossHP = PlayerPrefs.GetInt("hp");
        Boss = PlayerPrefs.GetInt("boss");
        BG = PlayerPrefs.GetInt("bg");
        WorldName = PlayerPrefs.GetString("name");

        NextQtButton.onClick.AddListener(() => clicked("Next"));
        FinishButton.onClick.AddListener(() => clicked("Finish"));
        CancelButton.onClick.AddListener(() => clicked("Cancel"));
        OkButton.onClick.AddListener(() => clicked("Ok"));
        PCancelButton.onClick.AddListener(() => clicked("PCancel"));
        ConfirmButton.onClick.AddListener(() => clicked("Confirm"));
    }


    private void OnDisable()
    {

    }

    void clicked(string pressed)
    {

        switch (pressed)
        {

            case "Next":
                int selected = 99;
                string Q, one, two, three, four;

                Q = QuestionTextBox.text;
                one = AnsOneTextBox.text;
                two = AnsTwoTextBox.text;
                three = AnsThreeTextBox.text;
                four = AnsFourTextBox.text;

                if (ToggleOne.isOn)
                {
                    selected = 1;
                }
                else if (ToggleTwo.isOn)
                {
                    selected = 2;
                }
                else if (ToggleThree.isOn)
                {
                    selected = 3;
                }
                else if (ToggleFour.isOn)
                {
                    selected = 4;
                }


                if (string.IsNullOrWhiteSpace(Q) || string.IsNullOrWhiteSpace(one) || string.IsNullOrWhiteSpace(two) || string.IsNullOrWhiteSpace(three) || string.IsNullOrWhiteSpace(four))
                {
                    SetPopoutState("Ok");
                    PanelText.text = "Please   do   not   leave   any   input   fields   empty.";
                }
                else if (selected == 99)
                {
                    SetPopoutState("Ok");
                    PanelText.text = "Please   select   a   correct   answer.";
                }
                else
                {
                    QCount++;
                    SetPopoutState("Confirm");
                    PanelText.text = "Go   To   Next   Question   ?";
                }
                break;

            case "Finish":
                if (QCount < BossHP)
                {
                    SetPopoutState("Ok");
                    PanelText.text = "Please   Add   " + (BossHP - QCount) + "   more   Questions.";
                }
                else
                {
                    CreateGameSetupTxt();
                    SetPopoutState("Ok");
                    PanelText.text = "World   Created!!!   Total:   " + QCount + "   Questions,  " + BossHP + "   Boss   Questions.";
                    end = 1;
                }
                break;
            case "Cancel":
                SceneManager.LoadScene("GameManagement");
                break;
            case "Ok":
                if (end == 1)
                {
                    SceneManager.LoadScene("GameManagement");
                }
                else
                {
                    SetPopoutState("Close");
                }
                break;
            case "PCancel":
                SetPopoutState("Close");
                break;
            case "Confirm":


                if ((NextStart / 9) < NumQuest)
                {
                    SetPopoutState("Ok");
                    PanelText.text = "Changes   Updated!";
                    CreateQuestionTxt();
                    loadFile();
                }
                else if ((NextStart / 9) == NumQuest)
                {
                    SetPopoutState("Ok");
                    PanelText.text = "Changes   Updated!,   Adding   New   Question   Now.";
                    CreateQuestionTxt();
                    setInitialState();
                }
                else
                {
                    SetPopoutState("Ok");
                    PanelText.text = "Adding   new   questions.";
                    setInitialState();
                }
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
                PCancelButton.gameObject.SetActive(false);
                OkButton.gameObject.SetActive(true);
                NextQtButton.interactable = false;
                FinishButton.interactable = false;
                CancelButton.interactable = false;
                ToggleOne.interactable = false;
                ToggleTwo.interactable = false;
                ToggleThree.interactable = false;
                ToggleFour.interactable = false;
                break;
            case "Confirm":
                PopoutGroup.gameObject.SetActive(true);
                ConfirmButton.gameObject.SetActive(true);
                PCancelButton.gameObject.SetActive(true);
                OkButton.gameObject.SetActive(false);
                NextQtButton.interactable = false;
                FinishButton.interactable = false;
                CancelButton.interactable = false;
                ToggleOne.interactable = false;
                ToggleTwo.interactable = false;
                ToggleThree.interactable = false;
                ToggleFour.interactable = false;
                break;
            case "Close":
                PopoutGroup.gameObject.SetActive(false);
                NextQtButton.interactable = true;
                FinishButton.interactable = true;
                CancelButton.interactable = true;
                ToggleOne.interactable = true;
                ToggleTwo.interactable = true;
                ToggleThree.interactable = true;
                ToggleFour.interactable = true;
                break;
        }
    }
    void setInitialState()
    {
        QuestionTextBox.text = "";
        AnsOneTextBox.text = "";
        AnsTwoTextBox.text = "";
        AnsThreeTextBox.text = "";
        AnsFourTextBox.text = "";
    }

    void CreateGameSetupTxt()
    {
        string path = Application.dataPath + "/" + WorldName + " Setup.txt";
        string content = BG.ToString() + "\n" + Boss.ToString() + "\n" + BossHP.ToString();
        File.WriteAllText(path, content);
    }

    void CreateQuestionTxt()
    {
        string path = Application.dataPath + "/" + WorldName + ".txt";
        string one, two, three, four;

        if (ToggleOne.isOn)
        {
            one = "T";
        }
        else
        {
            one = "F";
        }
        if (ToggleTwo.isOn)
        {
            two = "T";
        }
        else
        {
            two = "F";
        }
        if (ToggleThree.isOn)
        {
            three = "T";
        }
        else
        {
            three = "F";
        }
        if (ToggleFour.isOn)
        {
            four = "T";
        }
        else
        {
            four = "F";
        }

        string content = QuestionTextBox.text + "\n" + AnsOneTextBox.text + "\n" + one + "\n" + AnsTwoTextBox.text + "\n" + two + "\n" + AnsThreeTextBox.text + "\n" + three + "\n" + AnsFourTextBox.text + "\n" + four + "\n";



        if (File.Exists(path) && once == 1)
        {
            File.AppendAllText(path, content);
        }
        else
        {
            File.WriteAllText(path, content);
            once = 1;
        }

    }

    void loadFile()
    {          
        QuestionTextBox.text = inText[NextStart];
        AnsOneTextBox.text = inText[NextStart + 1];
        AnsTwoTextBox.text = inText[NextStart + 3];
        AnsThreeTextBox.text = inText[NextStart + 5];
        AnsFourTextBox.text = inText[NextStart + 7];
        /*
        if (inText[NextStart + 2] == "T")
        {
            ToggleOne.SetIsOnWithoutNotify(true);
        }
        else 
        {
            ToggleOne.SetIsOnWithoutNotify(false);
        }
        if (inText[NextStart + 4] == "T")
        {
            ToggleTwo.SetIsOnWithoutNotify(true);
        }
        else
        {
            ToggleTwo.SetIsOnWithoutNotify(false);
        }
        if (inText[NextStart + 6] == "T")
        {
            ToggleThree.SetIsOnWithoutNotify(true);
        }
        else
        {
            ToggleThree.SetIsOnWithoutNotify(false);
        }
        if (inText[NextStart + 8] == "T")
        {
            ToggleFour.SetIsOnWithoutNotify(true);
        }
        else 
        {
            ToggleFour.SetIsOnWithoutNotify(false);
        }
        */
        NextStart = NextStart + 9;

    }



    // Update is called once per frame
    void Update()
    {

    }
}
