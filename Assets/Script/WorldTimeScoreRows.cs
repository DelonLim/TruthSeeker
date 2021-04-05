using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WorldTimeScoreRows : MonoBehaviour
{
    void Start()
    {
        GameObject RowTemplate = transform.GetChild(0).gameObject;
        GameObject count;
        int N = 10; // How many Rows?
        for (int x = 0; x < N; x++)
        {
            count = Instantiate(RowTemplate, transform);
            count.transform.GetChild(1).GetComponent<Text>().text = "World   " + x; //World Name
            count.transform.GetChild(3).GetComponent<Text>().text = "Value   " + x; //Score or Time
        }

        //Destroy(RowTemplate);
    }
}
