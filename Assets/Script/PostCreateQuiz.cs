using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class PostCreateQuiz : MonoBehaviour
{
    public Button ShareButton, AdminmenuButton;
    public InputField CodeHere;
    string UniqueCode = "", location1, location2;
    private void OnEnable()
    {
        location1 = PlayerPrefs.GetString("location1");
        location2 = PlayerPrefs.GetString("location2");
        if (location1 != "" && location2 != "")
        {
            File.Delete(location1);
            File.Delete(location2);
        }
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
                SceneManager.LoadScene("MainMenu");
                break;
            case "Share":
                string share_url = "https://boblight.github.io/?user=" + "1" + "&code=" + UniqueCode;
                Application.OpenURL(share_url);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
