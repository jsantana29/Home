using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private PlayerController player;
    public GameObject currentCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void RespawnPlayer(){
        Debug.Log("Player is supposed to respawn");
        player.transform.position = currentCheckpoint.transform.position;
    }
}
