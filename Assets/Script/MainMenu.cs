using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  public Button registerButton;
  public Button loginButton;
  public Button playButton;
  public Button AccountSettingsButton;
  public Text playerDisplay;

    string worldname;

  private void Start()
  {
        StartCoroutine(LoadWorldName());
        if (DBManager.LoggedIn)
        {
            playerDisplay.text = "Player: " + DBManager.username;
            playButton.interactable = true;
            AccountSettingsButton.interactable = true;
            registerButton.interactable = false;
            loginButton.interactable = false;
        }
        else 
        {
            playButton.interactable = false;
            AccountSettingsButton.interactable = false;
            registerButton.interactable =true;
            loginButton.interactable = true;
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        PlayerPrefs.SetString("WorldName", worldname.Trim(','));
    }

    public void GoToRegister() {
      SceneManager.LoadScene(2);

  }
  public void GoToLogin() {
      SceneManager.LoadScene(3);
      
  }

  public void GoToGame() {
      SceneManager.LoadScene(5);      
  }

    public void GoToSettings()
    {
        SceneManager.LoadScene(26);
    }

    IEnumerator LoadWorldName()
    {
        WWWForm form = new WWWForm();
        WWW www = new WWW("http://localhost/truthseekers/LoadUniqueWorld.php", form);
        yield return www;


        worldname = www.text;

    }
}
