using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{
    //0 = normal, 1 = timeAttack, 2 = inverse
    public static int gameMode = 0;
    public Button NormalBtn, TimeBtn, InverseBtn, AssignmentBtn, CommunityBtn, BackBtn;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "SelectGameMode")
        {
            Button Nbtn = NormalBtn.GetComponent<Button>();
            Nbtn.onClick.AddListener(LoadNormalMode);

            Button Tbtn = TimeBtn.GetComponent<Button>();
            Tbtn.onClick.AddListener(LoadTimeAttackMode);

            Button Ibtn = InverseBtn.GetComponent<Button>();
            Ibtn.onClick.AddListener(LoadInverseMode);

            Button Abtn = AssignmentBtn.GetComponent<Button>();
            Abtn.onClick.AddListener(LoadAssignmentMode);

            Button Cbtn = CommunityBtn.GetComponent<Button>();
            Cbtn.onClick.AddListener(LoadCommunityMode);

            Button Bbtn = BackBtn.GetComponent<Button>();
            Bbtn.onClick.AddListener(LoadCharSelect);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getMode()
    {
        return gameMode;
    }
    void LoadNormalMode()
    {
        //go to world selection
        gameMode = 0;


        SceneManager.LoadScene("WorldSelection");
    }
    void LoadTimeAttackMode()
    {
        //go to world selection
        gameMode = 1;

        SceneManager.LoadScene("WorldSelection");
    }
    void LoadInverseMode()
    {
        //go to world selection
        gameMode = 2;

        SceneManager.LoadScene("WorldSelection");
    }
    void LoadAssignmentMode()
    {
        //go to assignment
        SceneManager.LoadScene("Assignment");
    }
    void LoadCommunityMode()
    {
        //go to Community
        SceneManager.LoadScene("CommunityMode");
    }
    void LoadCharSelect()
    {
        //go back to character selection
        SceneManager.LoadScene("CharacterSelection");
    }
}
