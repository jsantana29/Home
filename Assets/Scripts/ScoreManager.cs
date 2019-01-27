using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int gears;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        gears = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ""+gears;
    }

    public void addGears(int gearsToAdd){
        gears += gearsToAdd;
    }

    public void reset(){
        gears = 0;
    }
}
