using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostStudentAccountManagement : MonoBehaviour
{
    public Button BackButton, ChangeableButton, OkButton, ConfirmButton, CancelButton;
    public Text TitleText, InstructionText, PanelText;
    public InputField StuEmailInput;
    public Dropdown AccSelDropdown;
    public Image PopoutGroup;
    string mode,test1;
    string[] test;
    int check = 0;
    // Start is called before the first frame update

    private void OnEnable()
    {
        mode = PlayerPrefs.GetString("mode");
        BackButton.onClick.AddListener(() => clicked("Back"));
        ChangeableButton.onClick.AddListener(() => clicked("Change"));
        OkButton.onClick.AddListener(() => clicked("Ok"));
        ConfirmButton.onClick.AddListener(() => clicked("Confirm"));
        CancelButton.onClick.AddListener(() => clicked("Cancel"));

        if (mode == "Add")
        {
            TitleText.text = "Add   New   Student   Account";
            InstructionText.text = "Enter   Student   Email   Below";
            ChangeableButton.GetComponentInChildren<Text>().text = "Generate   Registration        Code";
            StuEmailInput.gameObject.SetActive(true);
        }
        else if (mode == "Delete")
        {
            TitleText.text = "Delete   An   Existing   Student   Account";
            InstructionText.text = "Select   Account   To   Delete   Below";
            ChangeableButton.GetComponentInChildren<Text>().text = "Delete   Account";
            AccSelDropdown.gameObject.SetActive(true);
        }

    }

    void clicked(string pressed)
    {
        
        switch (pressed)
        {
            case "Back":
                SceneManager.LoadScene("StudentAccountManagement");
                break;
            case "Change":
                if (mode == "Add")
                {
                    string inputText = StuEmailInput.text;

                    if (string.IsNullOrWhiteSpace(inputText))
                    {
                        check = 0;
                        SetPopoutState("Ok");
                        PanelText.text = "Please   Do   not   leave   the   email   field   empty.";
                    }
                    else
                    {
                        check = 1;
                        SetPopoutState("Ok");
                        PanelText.text = "Account   Added,   Registration   Code   Sent   To    Email.";
                    }
                }
                else if (mode == "Delete") 
                {
                    SetPopoutState("Confirm");
                    PanelText.text = "Are   you   sure   you   want   to   delete   student   account   " + AccSelDropdown.options[AccSelDropdown.value].text + "?";
                }
                break;
            case "Ok":
                if (check != 1)
                {
                    SetPopoutState("Close");
                }
                else 
                {
                    SceneManager.LoadScene("StudentAccountManagement");
                }
                         
                break;
            case "Cancel":
                SetPopoutState("Close");
                break;
            case "Confirm":
                SetPopoutState("Ok");
                check = 1;
                StartCoroutine(DeleteSelected());
                PanelText.text = "Account   Deleted!";
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

    IEnumerator LoadDropdown()
    {
        WWWForm form = new WWWForm();
        WWW www = new WWW("http://localhost/truthseekers/AccountDropDownLoad.php", form);
        yield return www;
        int x = 0;

        test1 = www.text;
        test = test1.Split(',');
        AccSelDropdown.ClearOptions();

        List<string> newlist = new List<string>(test);
        newlist.RemoveAt(newlist.Count - 1);

        AccSelDropdown.AddOptions(newlist);

    }

    IEnumerator DeleteSelected()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", AccSelDropdown.options[AccSelDropdown.value].text);

        WWW www = new WWW("http://localhost/truthseekers/DeleteAccount.php", form);
        yield return www;

    }

    void Start()
    {
        StartCoroutine(LoadDropdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
