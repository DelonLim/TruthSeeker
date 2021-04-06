using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AssignmentMode : MonoBehaviour
{
    public Button AccessBtn, BackBtn;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Assignment")
        {
            Button Abtn = AccessBtn.GetComponent<Button>();
            Abtn.onClick.AddListener(LoadAccessGame);

            Button bBtn = BackBtn.GetComponent<Button>();
            bBtn.onClick.AddListener(LoadGameMode);
        }
    }
    private void LoadGameMode()
    {
        SceneManager.LoadScene("SelectGameMode");
    }
    private void LoadAccessGame()
    {
        //Most Prob need to have string here to indicate the file
        SceneManager.LoadScene("World 1");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
