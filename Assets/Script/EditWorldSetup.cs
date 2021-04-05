using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class EditWorldSetup : MonoBehaviour
{
    public Sprite BG1, BG2, BG3, BG4, BG5, BG6, BG7, BG8, BG9, BG10;
    public Image Boss1Image, Boss2Image, Boss3Image, Boss4Image, PopoutGroup, BGImage;
    public InputField WorldNameTextbox, BossHPTextbox;
    public Button FinishButton, BackButton, CancelButton, OkButton, ConfirmButton;
    public Dropdown BGDropdown, BossDropdown;
    public Text PanelText;
    string WorldName, prev, path;
    int BossHP, BG = 1, BossSprite = 1;

    void OnEnable()
    {
        path = PlayerPrefs.GetString("path");
        FinishButton.onClick.AddListener(() => clicked("Finish"));
        BackButton.onClick.AddListener(() => clicked("Back"));
        CancelButton.onClick.AddListener(() => clicked("Cancel"));
        OkButton.onClick.AddListener(() => clicked("Ok"));
        ConfirmButton.onClick.AddListener(() => clicked("Confirm"));
        BGDropdown.onValueChanged.AddListener(delegate { BGChange(BGDropdown.value); });
        BossDropdown.onValueChanged.AddListener(delegate { BossChange(BossDropdown.value); });
    }

    void BGChange(int change)
    {
        switch (change)
        {
            case 0:
                BGImage.sprite = BG1;
                BG = 1;
                break;
            case 1:
                BGImage.sprite = BG2;
                BG = 2;
                break;
            case 2:
                BGImage.sprite = BG3;
                BG = 3;
                break;
            case 3:
                BGImage.sprite = BG4;
                BG = 4;
                break;
            case 4:
                BGImage.sprite = BG5;
                BG = 5;
                break;
            case 5:
                BGImage.sprite = BG6;
                BG = 6;
                break;
            case 6:
                BGImage.sprite = BG7;
                BG = 7;
                break;
            case 7:
                BGImage.sprite = BG8;
                BG = 8;
                break;
            case 8:
                BGImage.sprite = BG9;
                BG = 9;
                break;
            case 9:
                BG = 10;
                BGImage.sprite = BG10;
                break;

        }
    }

    void BossChange(int boss)
    {
        switch (boss)
        {
            case 0:
                BossSprite = 1;
                Boss1Image.gameObject.SetActive(true);
                Boss2Image.gameObject.SetActive(false);
                Boss3Image.gameObject.SetActive(false);
                Boss4Image.gameObject.SetActive(false);
                break;

            case 1:
                BossSprite = 2;
                Boss1Image.gameObject.SetActive(false);
                Boss2Image.gameObject.SetActive(true);
                Boss3Image.gameObject.SetActive(false);
                Boss4Image.gameObject.SetActive(false);
                break;

            case 2:
                BossSprite = 3;
                Boss1Image.gameObject.SetActive(false);
                Boss2Image.gameObject.SetActive(false);
                Boss3Image.gameObject.SetActive(true);
                Boss4Image.gameObject.SetActive(false);
                break;

            case 3:
                BossSprite = 4;
                Boss1Image.gameObject.SetActive(false);
                Boss2Image.gameObject.SetActive(false);
                Boss3Image.gameObject.SetActive(false);
                Boss4Image.gameObject.SetActive(true);
                break;
        }
    }


    void clicked(string pressed)
    {
        switch (pressed)
        {
            case "Back":
                SceneManager.LoadScene("PostGameManagement");
                break;
            case "Finish":

                string checkTextbox1 = WorldNameTextbox.text;
                string checkTextbox2 = BossHPTextbox.text;
                if (string.IsNullOrWhiteSpace(checkTextbox1) || string.IsNullOrWhiteSpace(checkTextbox2))
                {
                    SetPopoutState("Ok");
                    PanelText.text = "Please   Do   Not   Leave   Textbox   Field   Empty.";
                }
                else
                {
                    SetPopoutState("Confirm");
                    PanelText.text = "Create   " + checkTextbox1 + "   with   " + BGDropdown.options[BGDropdown.value].text + "   and   " + BossDropdown.options[BossDropdown.value].text + "   ?";
                }
                break;
            case "Cancel":
                SetPopoutState("Close");
                break;
            case "Ok":
                SetPopoutState("Close");
                break;
            case "Confirm":
                SceneManager.LoadScene("EditWorld");
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
                FinishButton.interactable = false;
                BackButton.interactable = false;
                break;
            case "Confirm":
                PopoutGroup.gameObject.SetActive(true);
                ConfirmButton.gameObject.SetActive(true);
                CancelButton.gameObject.SetActive(true);
                OkButton.gameObject.SetActive(false);
                FinishButton.interactable = false;
                BackButton.interactable = false;
                break;
            case "Close":
                PopoutGroup.gameObject.SetActive(false);
                FinishButton.interactable = true;
                BackButton.interactable = true;
                break;
        }
    }

    void OnDisable()
    {
        WorldName = WorldNameTextbox.text;
        BossHP = int.Parse(BossHPTextbox.text);

        PlayerPrefs.SetString("name", WorldName);
        PlayerPrefs.SetInt("hp", BossHP);
        PlayerPrefs.SetInt("boss", BossSprite);
        PlayerPrefs.SetInt("bg", BG);
    }

    void loadFile(string actualpath)
    {
        string[] inText = File.ReadAllLines(actualpath);

        WorldNameTextbox.text = path;
        BGDropdown.SetValueWithoutNotify(int.Parse(inText[0])-1);
        BGChange(int.Parse(inText[0])-1);
        BossDropdown.SetValueWithoutNotify(int.Parse(inText[0]) - 1);
        BossChange(int.Parse(inText[1])-1);
        BossHPTextbox.text = inText[2];

    }

    // Start is called before the first frame update
    void Start()
    {
        string actualpath = Application.dataPath + "/" + path + " Setup.txt";
        loadFile(actualpath);
    }

    // Update is called once per frame
    void Update()
    {


    }
}
