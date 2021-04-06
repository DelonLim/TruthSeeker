using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Login : MonoBehaviour
{
   public InputField nameField;
    public InputField passwordField;
    public Button submitButton;

    public void CallLogin() {
        StartCoroutine(LoginPlayer());
    }

    IEnumerator LoginPlayer() {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://localhost/truthseekers/login.php", form);
        yield return www;

        if (www.text[0] == '0')
        {
            DBManager.username = nameField.text;
            DBManager.score = int.Parse(www.text.Split('\t')[1]);

            if (nameField.text != "realadmin")
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(8);
            }
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }
        }

        public void VerifyInputs()
        {
            submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
        }

        public void GoToMenu() {
        if (nameField.text != "realadmin")
        {
            SceneManager.LoadScene(1);
        }
        else 
        {
            SceneManager.LoadScene(8);
        }
        }
    
}
