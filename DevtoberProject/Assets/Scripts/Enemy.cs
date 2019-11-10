using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    // needs partical effect for blood when hit
    // add enemy renderer to enemy renderer
    // needs a rigidbody attached and disable all rotation on the rigidbody 

    // movement and attack
    public float StopChaseingDistance = 5f;
    public float MinDistanceToAttack = 6f;
    NavMeshAgent nav;
    private GameObject Target;
    public bool isIdle;


    //Enemy Health and damage
    public int health;
    public int MaxEnemyHealth = 30;
    private Animator anim;
    public GameObject bloodEffect;

    public float Speed = 1.4f;
    private float dazedTime = 0;
    public float startDazedTime = 0.9f;

    [Header("Invencibility settings")]
    // Invincibilty for enemy
    public float invincibilityLength = 1f;
    private float invincibiliyCounter;
    public Renderer EnemyRenderer;
    private float flashCounter;
    public float flashLenght = 0.07f;
    public bool Invencible;
    public float enemieDieTime = 5f;

    // Use this for initialization
    void Start () {
        Invencible = false;
        health = MaxEnemyHealth;
        // get refrence to the animator of the enemy
        anim = GetComponent<Animator>();
        if (anim != null)
        {
            //anim.SetBool("isRunning", true);
        }

       
        // movement stuff 
        isIdle = true;
        anim.SetBool("IsIdle", true);
        nav = GetComponent<NavMeshAgent>();
        Target = GameObject.FindGameObjectWithTag("Player");
        nav.speed = Speed;
	}
	
	// Update is called once per frame
	void Update () {
        EnemyBehavior();
	}


    public void EnemyBehavior()
    {
        // check if enemy has died function
        updateHealth();

        
        if( dazedTime <= 0)
        {
            nav.speed = Speed;
           // anim.SetBool("IsIdle" ,false);
        }
        else
        {
          //  anim.SetBool("IsIdle", true);
            nav.speed = 0;
            dazedTime -= Time.deltaTime;

            nav.SetDestination(transform.position);
        }

        float distance = Vector3.Distance(transform.position, Target.transform.position);


        if (isIdle)
        {
           anim.SetBool("isIdle", true);
            if (distance <= MinDistanceToAttack)
            {
                isIdle = false;
                anim.SetBool("IsIdle", false);
            }
            if(nav != null)
            nav.SetDestination(transform.position);
        }
        else
        {
            //float distance = Vector3.Distance(transform.position, Target.transform.position);

            if (distance >= StopChaseingDistance)
            {
                isIdle = true;
                anim.SetBool("IsIdle", true);
            }
            else
            {
                if(nav != null)
                nav.SetDestination(Target.transform.position);
                anim.SetBool("IsIdle", false);
            }
        }
            
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           // Target = other.gameObject;
          //  isIdle = false;
        }
    }


    // Keeps track of enemy health in update
    public void updateHealth()
    {
        // if invencibility is over zero start counter cound down
        if(invincibiliyCounter > 0)
        {
            invincibiliyCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                Invencible = true;
                EnemyRenderer.enabled = !EnemyRenderer.enabled;
                flashCounter = flashLenght;
            }

            if(invincibiliyCounter <= 0)
            {
                EnemyRenderer.enabled = true;
                Invencible = false;
            }
        }


        if(health <= 0)
        {
            isIdle = true;
            anim.SetBool("Die", true);
            nav.SetDestination(transform.position);
            // set destroy enemy time
            Invoke("DestroyEnemy", enemieDieTime);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    // enemy received damage from player
    public void TakeDamage(int damage)
    {
        if (invincibiliyCounter <= 0)
        {
            dazedTime = startDazedTime;
            if (bloodEffect != null)
            {
                Instantiate(bloodEffect, transform.position, Quaternion.identity);

            }

            health -= damage;
            Debug.Log("damage TAKEN !");

            invincibiliyCounter = invincibilityLength;

            EnemyRenderer.enabled = false;
            flashCounter = flashLenght;
        }
    }

}
