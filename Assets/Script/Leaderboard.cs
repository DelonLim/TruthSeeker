using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    public Button BackButton;
    public ScrollRect WorldQuestionScoreTable;
    public Dropdown WorldDropdown, SortDropdown, OrderDropdown;
    string worldname;
    string[] WorldNames;

    // Start is called before the first frame update
    void Start()
    {
        worldname = PlayerPrefs.GetString("WorldName");
        WorldNames = worldname.Split(',');

        WorldDropdown.ClearOptions();
        SortDropdown.ClearOptions();
        OrderDropdown.ClearOptions();

        List<string> AllWorldNames = new List<string>(WorldNames);
        List<string> Sort = new List<string>() {"Score","Time" };
        List<string> Order = new List<string>() { "Descending", "Ascending" };

        WorldDropdown.AddOptions(AllWorldNames);
        SortDropdown.AddOptions(Sort);
        OrderDropdown.AddOptions(Order);

    }

    // Update is called once per frame
    void Update()
    {
     
    }
    private void OnEnable()
    {
        BackButton.onClick.AddListener(() => clicked("Back"));
    }

    void clicked(string pressed)
    {
        switch (pressed)
        {
            case "Back":
                SceneManager.LoadScene("AccountSettings");
                break;
        }
    }

}
