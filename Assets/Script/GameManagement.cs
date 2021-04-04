using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public Button createWorld, editWorld, deleteWorld, back;
    string selection,prev;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
        createWorld.onClick.AddListener(() => clicked("CreateWorld"));
        editWorld.onClick.AddListener(() => clicked("EditWorld"));
        deleteWorld.onClick.AddListener(() => clicked("DeleteWorld"));
        back.onClick.AddListener(() => clicked("Back"));
    }

    private void OnDisable()
    {
        PlayerPrefs.SetString("Selection", selection);
        PlayerPrefs.SetString("prev", prev);
    }

    void clicked(string pressed)
    {
        switch (pressed)
        {
            case "CreateWorld":
                selection = "Create";
                prev = "CreateWorld";
                SceneManager.LoadScene("GameSetup");
                break;
            case "EditWorld":
                selection = "Edit";
                SceneManager.LoadScene("PostGameManagement");
                break;
            case "DeleteWorld":
                selection = "Delete";
                SceneManager.LoadScene("PostGameManagement");
                break;
            case "Back":
                SceneManager.LoadScene("AdminMenu");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
