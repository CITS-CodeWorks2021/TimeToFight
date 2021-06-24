using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    Text health;
    int hp;

    void Start()
    {
        hp = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>().hp;
        health = GetComponent<Text>();
    }

    // Updates enemy health in the ui every frame
    void Update()
    {
        hp = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>().hp;
        health.text = "Enemy: " + hp;
    }
}
