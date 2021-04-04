using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostCreateQuiz : MonoBehaviour
{
    public Button ShareButton, AdminmenuButton;
    public InputField CodeHere;
    string UniqueCode = "";
    private void OnEnable()
    {
        UniqueCode = PlayerPrefs.GetString("code");
        ShareButton.onClick.AddListener(() => clicked("Share"));
        AdminmenuButton.onClick.AddListener(() => clicked("Menu"));
        CodeHere.text = UniqueCode;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    void clicked(string pressed)
    {
        switch (pressed)
        {
            case "Menu":
                SceneManager.LoadScene("AdminMenu");
                break;
            case "Share":
                Debug.Log("Online Sharing");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
