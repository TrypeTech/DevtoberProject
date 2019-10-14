using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {


    public float StopChaseingDistance = 5f;
    public float MinDistanceToAttack = 6f;
    NavMeshAgent nav;
    private GameObject Target;
    public bool isIdle;
	// Use this for initialization
	void Start () {
        isIdle = true;
        nav = GetComponent<NavMeshAgent>();
        Target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        EnemyBehavior();
	}


    public void EnemyBehavior()
    {
        float distance = Vector3.Distance(transform.position, Target.transform.position);


        if (isIdle)
        {
            if(distance <= MinDistanceToAttack)
            {
                isIdle = false;
            }
            nav.SetDestination(transform.position);
        }
        else
        {
            //float distance = Vector3.Distance(transform.position, Target.transform.position);

            if (distance >= StopChaseingDistance)
            {
                isIdle = true;
            }
            else
            {
                nav.SetDestination(Target.transform.position);
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


}
