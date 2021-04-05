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
            DBManager.score = int.Parse(www.text.Split('\t')[1]);

            if (nameField.text != "realadmin")
            {
                SceneManager.LoadScene(1);
            }
            else
            {
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
