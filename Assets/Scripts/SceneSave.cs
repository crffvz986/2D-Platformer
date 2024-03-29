﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSave : MonoBehaviour
{
    int startingSceneIndex;

    private void Awake()
    {
        int numSceneSave = FindObjectsOfType<SceneSave>().Length;
        if (numSceneSave > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start ()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
	}
	
	void Update ()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex != startingSceneIndex)
        {
            Destroy(gameObject);
        }
	}
}
