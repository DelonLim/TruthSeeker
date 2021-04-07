using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class AssignmentMode : MonoBehaviour
{
    public Button AccessBtn, BackBtn;
    public InputField iField;
    string actualpath;

    static string AssignmentSel;
    static bool isAssignmentSel = false;

    private float sec = 1.0f;
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

    public string getAssignSel()
    {
        return AssignmentSel;
    }
    public bool getisAssignSel()
    {
        return isAssignmentSel;
    }
    private void LoadGameMode()
    {
        SceneManager.LoadScene("SelectGameMode");
    }
    private void LoadAccessGame()
    {
        actualpath = iField.text;
        AssignmentSel = actualpath;
        StartCoroutine(DownloadWorldSetup(actualpath));
        StartCoroutine(DownloadWorldData(actualpath));
        isAssignmentSel = true;
        //Most Prob need to have string here to indicate the file
        StartCoroutine(LoadGameScene());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene("World 1");
    }
    IEnumerator DownloadWorldSetup(string worldArray)
    {
        WWWForm form = new WWWForm();
        form.AddField("WorldName", worldArray);
        WWW www = new WWW("http://localhost/truthseekers/DownloadWorldSetup.php", form);
        yield return www;



        string test1;
        string[] test;

        test1 = www.text;
        test = test1.Split('\n');
        string path = "C:/xampp/tmp/" + worldArray + " Setup.csv";

        for (int x = 0; x < test.Length; x++)
        {
            if (File.Exists(path))
            {
                File.AppendAllText(path, test[x] + "\n");
            }
            else
            {
                File.WriteAllText(path, test[x] + "\n");
            }
        }
    }

    IEnumerator DownloadWorldData(string worldArray)
    {
        WWWForm form = new WWWForm();
        form.AddField("WorldName", worldArray);
        WWW www = new WWW("http://localhost/truthseekers/DownloadWorldData.php", form);
        yield return www;
        string test1;
        string[] test;

        test1 = www.text;
        test = test1.Split('\n');
        string path = "C:/xampp/tmp/" + worldArray + ".csv";

        for (int x = 0; x < test.Length; x++)
        {
            Debug.Log(x + " GG");
            if (File.Exists(path))
            {
                File.AppendAllText(path, test[x] + "\n");
            }
            else
            {
                File.WriteAllText(path, test[x] + "\n");
            }
        }
    }
}
