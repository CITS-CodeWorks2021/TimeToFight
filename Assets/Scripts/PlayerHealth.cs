using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    Text health;
    int hp;

    void Start()
    {
        hp = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().hp;
        health = GetComponent<Text>();
    }

    // Updates player health in the ui every frame
    void Update()
    {

        hp = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().hp;
        health.text = "Health: " + hp;
    }
}
