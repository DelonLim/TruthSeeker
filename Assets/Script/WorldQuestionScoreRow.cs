﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WorldQuestionScoreRow : MonoBehaviour
{
    void Start()
    {
        GameObject RowTemplate = transform.GetChild (0).gameObject;
        GameObject count;
        int N = 10; // How many Rows?
        for (int x = 0; x < N; x++)
        {
            count = Instantiate(RowTemplate, transform);
            count.transform.GetChild(1).GetComponent<Text>().text = "World   " + x; //World Name
            count.transform.GetChild(3).GetComponent<Text>().text = "Question   " + x; //Question Number
            count.transform.GetChild(5).GetComponent<Text>().text = "Score   " + x; //Score
        }

       //Destroy(RowTemplate);
    }

}