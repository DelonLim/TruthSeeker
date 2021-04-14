using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WorldTimeScoreRows : MonoBehaviour
{

    string test1,test2,test4,mode;
    string[] test,test3,test5;
    int[] test6= { 0,0,00,00,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    int z=0,counting =0;

    private void OnEnable()
    {
       
    }

    void Start()
    {
        mode = PlayerPrefs.GetString("mode");
        test1 = PlayerPrefs.GetString("WorldName");
        test = test1.Split(',');
        test2 = PlayerPrefs.GetString("AvgScore");
        test3 = test2.Split(',');
        test4 = PlayerPrefs.GetString("AvgTime");
        test5 = test4.Split(',');
        
       
       
        for (int x =0;x<test5.Length;x++)
        {
            if (test5[x]!="")
            {
                test6[z] = test6[z] + int.Parse(test5[x]);
                counting++;
            }
            else 
            {
                test6[z] = test6[z] / counting;
                z++;
                counting = 0;
            }
        }
        


        GameObject RowTemplate = transform.GetChild(0).gameObject;
        GameObject count;

        for (int x = 0; x < test.Length; x++)
        {
            count = Instantiate(RowTemplate, transform);
            count.transform.GetChild(1).GetComponent<Text>().text = test[x]; //World Name
            if (mode == "W")
            {
                count.transform.GetChild(3).GetComponent<Text>().text = test3[x];//Score 
            }
            else 
            {
                count.transform.GetChild(3).GetComponent<Text>().text = test6[x].ToString(); //Time
            }
             
        }


        //Destroy(RowTemplate);
    }

   
        void Update()
    {

    }

}
