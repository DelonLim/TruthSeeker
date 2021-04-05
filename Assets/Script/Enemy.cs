using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Enemy : MonoBehaviour
{
    public GameObject EnemyMob, EnemyBoss0, EnemyBoss1, EnemyBoss2, EnemyBoss3, currBoss;
    public int enemyHP = 0;
    public bool isBossSpawn = false;
    public List<GameObject> enemies = new List<GameObject>();
    public Animator animator;

    private float sec = 3.0f;
    private int enemyMobCount = 0, enemyMobLeft = 0;
    private bool isPrepareBoss = false;
    private GameObject gameHandler;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void countEnemy(string[] MCQs)
    {
        int qnsCount = MCQs.Length / 9;

        gameHandler = GameObject.FindWithTag("GameController");
        

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        //foreach (string s in MCQs)
        //{
        //    Debug.Log(s);
        //}
        if (sceneName == "World 1")
        {
            enemyMobCount = qnsCount - gameHandler.GetComponent<GameHandler>().getBossHP();
            enemyMobLeft = qnsCount - gameHandler.GetComponent<GameHandler>().getBossHP();
            enemyHP = 5;
        }
        else if (sceneName == "World 2")
        {
            enemyMobCount = qnsCount - gameHandler.GetComponent<GameHandler>().getBossHP();
            enemyMobLeft = qnsCount - 6;
            enemyHP = 6;
        }
        else if (sceneName == "World 3")
        {
            enemyMobCount = qnsCount - 7;
            enemyMobLeft = qnsCount - 7;
            enemyHP = 7;
        }
        else if (sceneName == "World 4")
        {
            enemyMobCount = qnsCount - 8;
            enemyMobLeft = qnsCount - 8;
            enemyHP = 8;
        }

        spawnEnemyMob();
        //attack animation when player made wrong answer
    }
    //private bool isAdd = true;
    void spawnEnemyMob()
    {
        double x = -2.0, y = 1.5, z = 0;
        double adjustment = -2;
        //if (isAdd)
        //{
        //    isAdd = false;
        //    enemyMobCount += 6;
        //}

        int enemyMobloop = enemyMobCount;

        for (int i = 0; i < enemyMobloop; i++)
        {
            adjustment += 2;
            if (adjustment == 6)
            {
                adjustment = 0;
            }
            if (i >= 3)
            {
                GameObject newGO = (GameObject)Instantiate(EnemyMob, new Vector3((float)x - 2, (float)y - (float)adjustment, (float)z), Quaternion.identity);
                enemies.Add(newGO);
            }
            else if (i <= 2)
            {
                GameObject newGO = (GameObject)Instantiate(EnemyMob, new Vector3((float)x, (float)y - (float)adjustment, (float)z), Quaternion.identity);
                enemies.Add(newGO);
            }
            enemyMobCount--;
            //if enemyMobCount = 0, prepare to spawn boss after next enemy mob being clear
            if(enemyMobCount == 0)
            {
                Debug.Log("Boss Spawn true");
                isPrepareBoss = true;
            }
            //spawn at most only 6 enemy in one scene.
            if (i == 5)
            {
                break;
            }
        }
    }

    void spawnEnemyBoss()
    {
        isBossSpawn = true;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "World 1")
        {
            currBoss = (GameObject)Instantiate(EnemyBoss0, new Vector3(-2.0f, -0.5f, 0), Quaternion.identity);
        }
        else if (sceneName == "World 2")
        {
            currBoss = (GameObject)Instantiate(EnemyBoss1, new Vector3(-2.0f, -0.5f, 0), Quaternion.identity);
        }
        else if (sceneName == "World 3")
        {
            currBoss = (GameObject)Instantiate(EnemyBoss2, new Vector3(-2.0f, -0.5f, 0), Quaternion.identity);
        }
        else if (sceneName == "World 4")
        {
            currBoss = (GameObject)Instantiate(EnemyBoss3, new Vector3(-2.0f, -0.5f, 0), Quaternion.identity);
        }
        
    }
    //public int getEnemyHP()
    //{
    //    return enemyHP;
    //}
    public void deductHP(int attackPower)
    {
        enemyHP -= attackPower;
    }
    public void EnemyDeath()
    {
        //for checking if there is any normal mob left in the scene
        if (enemies.Any())
        {
            enemies[0].GetComponent<Enemy>().playDeathAni();
            StartCoroutine(DeleteMobCall());
            enemyMobLeft -= 1;
        }
        //if is boss death do it here
        if(enemyHP == 0 && isBossSpawn)
        {
            currBoss.GetComponent<Enemy>().playDeathAni();
            gameHandler = GameObject.FindWithTag("GameController");
            gameHandler.GetComponent<GameHandler>().setGameComplete();
            StartCoroutine(DeleteBossCall());
        }
    }

    public void EnemyAtk()
    {
        //for checking if there is any normal mob left in the scene
        if (enemies.Any())
        {
            enemies[0].GetComponent<Enemy>().playAtkAni();
        }
        //if is boss attack do it here
        if (enemyHP != 0 && isBossSpawn)
        {
            currBoss.GetComponent<Enemy>().playAtkAni();
        }
    }

    IEnumerator DeleteMobCall()
    {
        yield return new WaitForSeconds(sec);
        GameObject Temp = enemies[0];
        enemies.RemoveAt(0);
        Destroy(Temp);

        if (!enemies.Any() && isPrepareBoss)
        {
            Debug.Log("Current scene no enemies mob left. Spawning boss");
            spawnEnemyBoss();
        }
        else if (!enemies.Any())
        {
            Debug.Log("Current enemies die spawning remaining enemies.");
            spawnEnemyMob();
        }
    }
    IEnumerator DeleteBossCall()
    {
        yield return new WaitForSeconds(sec);
        Destroy(GameObject.FindWithTag("EnemyBoss"));
        Debug.Log("GameEnd");
    }

    private void playDeathAni()
    {
        animator.SetBool("Die", true);
    }
    private void playAtkAni()
    {
        animator.SetBool("Attack", true);
    }
}