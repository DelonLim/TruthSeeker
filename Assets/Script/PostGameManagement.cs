using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostGameManagement : MonoBehaviour
{
    public Button backButton, changeableButton, OkButton, CancelButton, ConfirmButton;
    public Text PostGMText, IntructionText, PanelText;
    public Dropdown WorldSelDropdown;
    public Image PopoutGroup;
    string Selection;
    int check = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Selection = PlayerPrefs.GetString("Selection");
        changeableButton.onClick.AddListener(() => clicked("Changeable"));
        backButton.onClick.AddListener(() => clicked("Back"));
        OkButton.onClick.AddListener(() => clicked("Ok"));
        CancelButton.onClick.AddListener(() => clicked("Cancel"));
        ConfirmButton.onClick.AddListener(() => clicked("Confirm"));

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
                        Debug.Log("Link to Edit");
                        break;
                    case "Delete":
                        PopoutGroup.gameObject.SetActive(true);
                        PanelText.text = WorldSelDropdown.options[WorldSelDropdown.value].text + "   Deletion   Confirmation";
                        ConfirmButton.gameObject.SetActive(true);
                        CancelButton.gameObject.SetActive(true);
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
                PanelText.text = "World   Successfully   Deleted!!";
                ConfirmButton.gameObject.SetActive(false);
                CancelButton.gameObject.SetActive(false);
                OkButton.gameObject.SetActive(true);
                break;
            case "Cancel":
                PopoutGroup.gameObject.SetActive(false);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
