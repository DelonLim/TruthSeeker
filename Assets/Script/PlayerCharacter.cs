using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    public Animator animator;
    public int HP = 0;
    private GameObject gameHandler;

    
    //private GameObject player
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setPlayerHP(string[] MCQs)
    {
        int qnsCount = MCQs.Length / 9;
        HP = qnsCount / 2;
    }
    public void deductHP()
    {
        HP -= 1;
        if(HP == 0)
        {
            //Alert game end
            playDeathAni();
            gameHandler = GameObject.FindWithTag("GameController");
            gameHandler.GetComponent<GameHandler>().setGameOver();
            
        }
    }
    public int getHP()
    {
        return HP;
    }

    private void playDeathAni()
    {
        animator.SetBool("Die", true);
    }
    public void playAtkAni()
    {
        animator.SetBool("Attack", true);
    }
}
