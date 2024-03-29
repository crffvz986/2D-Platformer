﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chekpoint : MonoBehaviour
{
    private GameMaster gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            gm.lastChekPos = transform.position;
            Debug.Log("chek!");
        }
    }
}
