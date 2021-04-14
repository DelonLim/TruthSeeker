using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class AdminMenu : MonoBehaviour
{
    public Button LogoutBtn, gameMgnBtn, resultBtn, stuAccBtn, assignmentBtn;
    string prev;

    string test1;
    string[] test;
    List<string> newlist;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadWorldName());
    }

    private void OnDisable()
    {
        PlayerPrefs.SetString("WorldName", test1.Trim(','));
        PlayerPrefs.SetString("prev", prev);
    }
    void OnEnable()
    {
        
        LogoutBtn.onClick.AddListener(() => clicked("Logout"));
        gameMgnBtn.onClick.AddListener(() => clicked("GameManagement"));
        resultBtn.onClick.AddListener(() => clicked("Result"));
        stuAccBtn.onClick.AddListener(() => clicked("StudentAccount"));
        assignmentBtn.onClick.AddListener(() => clicked("Assignment"));
    }

    void clicked(string pressed)
    {
        switch (pressed)
        {
            case "GameManagement":
                SceneManager.LoadScene("GameManagement");
                break;
            case "Result":
                SceneManager.LoadScene("ResultAnalysis");
                break;
            case "StudentAccount":
                SceneManager.LoadScene("StudentAccountManagement");
                break;
            case "Assignment":
                prev = "AssignmentCreation";
                SceneManager.LoadScene("GameSetup");
                break;
            case "Logout":
                DBManager.LogOut();
                SceneManager.LoadScene("MainMenu");
                Debug.Log("Logged out Succesfully.");

                break;
        }
    }

    IEnumerator LoadWorldName()
    {
        WWWForm form = new WWWForm();
        WWW www = new WWW("http://localhost/truthseekers/LoadUniqueWorld.php", form);
        yield return www;

        test1 = www.text;
        test = test1.Split(',');

        newlist = new List<string>(test);
        newlist.RemoveAt(newlist.Count - 1);

    }

    void Load()
	{
		//SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.name);

    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }
}
