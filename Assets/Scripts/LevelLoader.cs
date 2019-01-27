﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private bool playerInZone;
    public string levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        playerInZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && playerInZone){
            SceneManager.LoadScene(levelToLoad);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.name == "Matty"){
            playerInZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.name == "Matty"){
            playerInZone = false;
        }
    }
}
