using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D body;
    Collider2D attack;
    Animator an;

    private float horizontal;
    private float vertical;
    private bool hurtTimer;

    public float speed = 20.0f;

    public int hp;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        attack = GameObject.FindGameObjectWithTag("SlashAttack").GetComponent<BoxCollider2D>();
        an = GetComponent<Animator>();
       
        hp = 100;
        hurtTimer = false;
    }

    void Update()
    {
        //We use the GetAxis method to take in WASD and arrow key inputs
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.RightShift))
        {

            attack.enabled = true;
            an.SetBool("isAttack", true);
        }

        if (horizontal != 0 || vertical != 0)
        {

            an.SetBool("IsWalk", true);

            //Here we check the horizontal float to see whether the player
            //is facing left or right. The player's sprite is flipped accordingly
            Vector3 characterScale = transform.localScale;
            if (horizontal < 0)
            {
                characterScale.x = -1f;
            }
            else if (horizontal > 0)
                characterScale.x = 1f;

            transform.localScale = characterScale;
        }
        else
        {
            an.SetBool("IsWalk", false);
        }

       
    }

    //Good practice to have any physics related operations in fixedUpdate,
    //as physics are updated immediately after this method (not the case with Update)
    private void FixedUpdate()
    {
        //add velocity in the direction of the movement keys being pressed
        body.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        //If the triggering collider is the Shove Attack and the hurtTimer boolean is false
        //Then we subtract health from the player
        if (col.gameObject.tag == "ShoveAttack" && !hurtTimer)
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

    void finishAttack()
    {
        attack.enabled = false;
        an.SetBool("isAttack", false);
    }

}
