using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StudentAccountManagement : MonoBehaviour
{
    public Button AddButton, DeleteButton, BackButton;
    string mode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        BackButton.onClick.AddListener(() => clicked("Back"));
        AddButton.onClick.AddListener(() => clicked("Add"));
        DeleteButton.onClick.AddListener(() => clicked("Delete"));
    }

    void OnDisable()
    {
        PlayerPrefs.SetString("mode",mode);
    }

    void clicked(string pressed)
    {
        switch (pressed)
        {
            case "Back":
                SceneManager.LoadScene("AdminMenu");
                break;
            case "Add":
                SceneManager.LoadScene("PostStudentAccountManagement");
                mode = "Add";
                break;
            case "Delete":
                SceneManager.LoadScene("PostStudentAccountManagement");
                mode = "Delete";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
