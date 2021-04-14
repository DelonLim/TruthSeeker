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
    public Dropdown WorldSelDropdown;
    static string worldSel;
    static bool isWorldSel = false;
    string worldname;
    string[] WorldNames;
    List<string> newlist;

    /*public Toggle currentSelection
    {
        get { return toogleGroupInstance.ActiveToggles().FirstOrDefault(); }
    }*/
    // Start is called before the first frame update
    private void OnEnable()
    {
        worldname = PlayerPrefs.GetString("WorldName");
        Debug.Log(worldname);
    }

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

            WorldNames = worldname.Split(',');
            WorldSelDropdown.ClearOptions();

            newlist = new List<string>(WorldNames);
            WorldSelDropdown.AddOptions(newlist);


            for (int i = 0; i < newlist.Count; i++)
            {
                StartCoroutine(DownloadWorldSetup(newlist[i]));
                StartCoroutine(DownloadWorldData(newlist[i]));
            }
        }
        
    }
    public string getWorldSel()
    {
        return worldSel;
    }
    public bool getisWorldSel()
    {
        return isWorldSel;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void deleteAllFiles()
    {
        for (int x=0;x<newlist.Count;x++)
        {
            File.Delete("C:/xampp/tmp/"+newlist[x]+".csv");
            File.Delete("C:/xampp/tmp/"+ newlist[x]+" Setup.csv");
        }
    }
    //select world based on toggle
    //toggle interact is enable/disabled based on users progression
    public void SelectedWorld()
    {
        worldSel = WorldSelDropdown.options[WorldSelDropdown.value].text;
        isWorldSel = true;
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
    IEnumerator DownloadWorldSetup(string worldname)
    {
        WWWForm form = new WWWForm();
        form.AddField("WorldName", worldname);
        WWW www = new WWW("http://localhost/truthseekers/DownloadWorldSetup.php", form);
        yield return www;

        string test1;
        string[] test;

        test1 = www.text;
        test = test1.Split('\n');
        string path = "C:/xampp/tmp/" + worldname + " Setup.csv";

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

    IEnumerator DownloadWorldData(string worldname)
    {
        WWWForm form = new WWWForm();
        form.AddField("WorldName", worldname);
        WWW www = new WWW("http://localhost/truthseekers/DownloadWorldData.php", form);
        yield return www;
        string test1;
        string[] test;

        test1 = www.text;
        test = test1.Split('\n');
        string path = "C:/xampp/tmp/" + worldname + ".csv";

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
