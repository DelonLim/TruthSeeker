using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;

public class WorldSelected : MonoBehaviour
{
    ToggleGroup toogleGroupInstance;
    public Button ConfirmBtn, backBtn;
    static string worldSel;

    public Toggle currentSelection
    {
        get { return toogleGroupInstance.ActiveToggles().FirstOrDefault(); }
    }
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "WorldSelection")
        {
            Button btn = ConfirmBtn.GetComponent<Button>();
            btn.onClick.AddListener(SelectedWorld);

            Button bBtn = backBtn.GetComponent<Button>();
            bBtn.onClick.AddListener(backChar);
            toogleGroupInstance = GetComponent<ToggleGroup>();

            string[] world1234 = { "World 1", "World 2", "World 3", "World 4" };

            for (int i = 0; i < 4; i++)
            {
                StartCoroutine(DownloadWorldSetup(world1234[i]));
                StartCoroutine(DownloadWorldData(world1234[i]));
            }
        }
        
    }
    public string getWorldSel()
    {
        return worldSel;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void deleteAllFiles()
    {
        File.Delete("C:/xampp/tmp/World 1.csv");
        File.Delete("C:/xampp/tmp/World 1 Setup.csv");
        File.Delete("C:/xampp/tmp/World 2.csv");
        File.Delete("C:/xampp/tmp/World 2 Setup.csv");
        File.Delete("C:/xampp/tmp/World 3.csv");
        File.Delete("C:/xampp/tmp/World 3 Setup.csv");
        File.Delete("C:/xampp/tmp/World 4.csv");
        File.Delete("C:/xampp/tmp/World 4 Setup.csv");
    }
    //select world based on toggle
    //toggle interact is enable/disabled based on users progression
    public void SelectedWorld()
    {
        worldSel = currentSelection.name;
        loadlevel("World 1");
    }
    public void backChar()
    {
        deleteAllFiles();
        loadlevel("SelectGameMode");
    }
    public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);
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
                File.AppendAllText(path, test[x]);
            }
            else
            {
                File.WriteAllText(path, test[x]);
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
            if (File.Exists(path))
            {
                File.AppendAllText(path, test[x]);
            }
            else
            {
                File.WriteAllText(path, test[x]);
            }
        }
    }
}
