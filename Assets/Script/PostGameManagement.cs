using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class PostGameManagement : MonoBehaviour
{
    public Button backButton, changeableButton, OkButton, CancelButton, ConfirmButton;
    public Text PostGMText, IntructionText, PanelText;
    public Dropdown WorldSelDropdown;
    public Image PopoutGroup;
    string Selection, test1;
    string[] test;
    int check = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadDropdown());
    }
    private void OnDisable()
    {
        string path = WorldSelDropdown.options[WorldSelDropdown.value].text;
        PlayerPrefs.SetString("path", path);
    }
    private void OnEnable()
    {
        Selection = PlayerPrefs.GetString("Selection");
        changeableButton.onClick.AddListener(() => clicked("Changeable"));
        backButton.onClick.AddListener(() => clicked("Back"));
        OkButton.onClick.AddListener(() => clicked("Ok"));
        CancelButton.onClick.AddListener(() => clicked("Cancel"));
        ConfirmButton.onClick.AddListener(() => clicked("Confirm"));

        
        
        //WorldSelDropdown.RefreshOptions();

        switch (Selection)
        {
            case "Edit":
                PostGMText.text = "Edit   World";
                IntructionText.text = "Select   World   to   Edit";
                WorldSelDropdown.gameObject.SetActive(true);
                changeableButton.GetComponentInChildren<Text>().text = "Edit";
                changeableButton.gameObject.SetActive(true);
                break;
            case "Delete":
                PostGMText.text = "Delete   World";
                IntructionText.text = "Select   World   to   Delete";
                WorldSelDropdown.gameObject.SetActive(true);
                changeableButton.GetComponentInChildren<Text>().text = "Delete";
                changeableButton.gameObject.SetActive(true);
                break;
        }
    }

    void clicked(string pressed)
    {
        
        switch (pressed)
        {
            case "Changeable":
                switch (Selection)
                {
                    case "Edit":
                        if (checkforFile())
                        {
                            SceneManager.LoadScene("EditWorldSetup");
                        }
                        else
                        {
                            SetPopoutState("Ok");
                            check = 1;
                            PanelText.text = "Unable   to   Load   World,   please   try   a   different   world.";
                        }

                        break;
                    case "Delete":
                        SetPopoutState("Confirm");
                        PanelText.text = WorldSelDropdown.options[WorldSelDropdown.value].text + "   Deletion   Confirmation";
                        break;
                }
                break;
            case "Back":
                SceneManager.LoadScene("GameManagement");
                break;
            case "Ok":
                if (check == 1)
                {
                    PopoutGroup.gameObject.SetActive(false);
                }
                else 
                {
                    SceneManager.LoadScene("GameManagement");
                }
                
                break;
            case "Confirm":
                StartCoroutine(DeleteSelected());
                PanelText.text = "World   Successfully   Deleted!!";
                check = 0;
                SetPopoutState("Ok");
                break;
            case "Cancel":
                PopoutGroup.gameObject.SetActive(false);
                break;
        }
    }

    bool checkforFile()
    {
        string path = Application.dataPath + "/" + WorldSelDropdown.options[WorldSelDropdown.value].text + ".csv";
        string path2 = Application.dataPath + "/" + WorldSelDropdown.options[WorldSelDropdown.value].text+" Setup.csv";

        return (File.Exists(path)&& File.Exists(path2));
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

    IEnumerator DeleteSelected()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", WorldSelDropdown.options[WorldSelDropdown.value].text);

        WWW www = new WWW("http://localhost/truthseekers/DeleteWorld.php", form);
        yield return www;

    }

    IEnumerator LoadDropdown()
    {
        WWWForm form = new WWWForm();
        WWW www = new WWW("http://localhost/truthseekers/DropDownLoad.php", form);
        yield return www;
        int x = 0;

        test1 = www.text;
        test = test1.Split( ',');
        WorldSelDropdown.ClearOptions();

        List<string> newlist = new List<string>(test);
        newlist.RemoveAt(newlist.Count-1);

        WorldSelDropdown.AddOptions(newlist);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
