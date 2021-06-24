using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Transform enemy;
    Transform player;
    
    private Animator an;
    private Collider2D shove;
    private bool hurtTimer;

    public int hp;

    void Start()
    {
        enemy = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        an = GetComponent<Animator>();
        shove = GameObject.FindGameObjectWithTag("ShoveAttack").GetComponent<BoxCollider2D>();
        
        hp = 100;
        hurtTimer = false;
    }

    void Update()
    {

        //Here we subtract the enemy's X Coordinate with the Players
        //If the resulting number is less greater than 0, then we know
        //that the player is to the left of the enemy, so we flip the enemy sprite
        //otherwise, we know the enemy should be facing right
        Vector3 enemyScale = transform.localScale;

        if (enemy.position.x - player.position.x > 0f)
            enemyScale.x = -2f;
        else
            enemyScale.x = 2f;

        transform.localScale = enemyScale;

        //Here we use a method called Distance that calculates a float distance between two transforms
        //If the player is close enough (Less than 2.0 units) then we call a method MoveTowards
        //That will return a Vector 3 slightly closer to the target vector, depending on your speed
        //Here we use 1.0f * Time.deltaTime as the speed
        if (Vector3.Distance(enemy.position, player.position) < 2.0f)
        {
            enemy.position = Vector3.MoveTowards(enemy.position, player.position, 1.0f * Time.deltaTime);
           
            if (Vector3.Distance(enemy.position, player.position) < .5f)
            {
                an.SetBool("isAttack", true);
                shove.enabled = true;
            }

            else
                an.SetBool("isWalking", true);
        }
        else
        {
            an.SetBool("isWalking", false);
        }

        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //If the triggering collider is the Slash Attack and the hurtTimer boolean is false
        //Then we subtract health from the enemy
        if (col.gameObject.tag == "SlashAttack" && !hurtTimer)
        {
            hp -= 10;
            hurtTimer = true;

            StartCoroutine("Hurt");
        }
    }

    //A function that will wait .5 seconds before turning 
    //the hurtTimer variable to false and returning
    IEnumerator Hurt()
    {

        yield return new WaitForSeconds(.5f);
        hurtTimer = false;

    }

    public void endAttack()
    {
        an.SetBool("isAttack", false);
        shove.enabled = false;
    }
}
