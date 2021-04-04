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
    
    // Start is called before the first frame update
    void Start()
    {
        //string name = Update();
        //Button btn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        //btn.onClick.AddListener(Load);

    }

    private void OnDisable()
    {
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
                SceneManager.LoadScene("MainMenu");
                break;
        }
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
