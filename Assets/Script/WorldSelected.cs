using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class WorldSelected : MonoBehaviour
{
    ToggleGroup toogleGroupInstance;
    public Button ConfirmBtn;
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
            toogleGroupInstance = GetComponent<ToggleGroup>();
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

    //select world based on toggle
    //toggle interact is enable/disabled based on users progression
    public void SelectedWorld()
    {
        worldSel = currentSelection.name;
        loadlevel("World 1");
    }

    public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);
    }

}
