using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{

    public bool TouchingObject;
    private bool keyDown;
    private Collider player;
    public float WaitPushAgainTime = 0.2f;
    public bool pushing = true;
    public float reduceSpeedAmount = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (TouchingObject )
        {
            float dot = Vector3.Dot(transform.forward, (player.transform.position - transform.position).normalized);
            if (pushing)
            {
               
                    // only move one direction
                    // if movement is null  let go
                    if (pushing)
                    {
                        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
                        {
                        
                        if (dot > 1f)
                        {
                            Debug.Log(dot);
                            transform.parent = player.transform;

                             
                        }
                        }

                        else
                        {
                            transform.parent = null;
                            pushing = false;
                            StartCoroutine(CanPushAgain());
                        }
                    }
                
            }
        }

    }

   public IEnumerator CanPushAgain()
    {
        yield return new WaitForSeconds(WaitPushAgainTime);
        pushing = true;
        Debug.Log("Can Push Again");

       
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TouchingObject = true;

            player = other;
            player.gameObject.GetComponent<Movement>().runSpeed = player.gameObject.GetComponent<Movement>().runSpeed / reduceSpeedAmount;
            player.gameObject.GetComponent<Movement>().walkSpeed = player.gameObject.GetComponent<Movement>().walkSpeed / reduceSpeedAmount;

            player.gameObject.GetComponent<Movement>().turnSmoothTime = player.gameObject.GetComponent<Movement>().turnSmoothTime * reduceSpeedAmount;
           
            transform.parent = other.transform;
            pushing = true;

        }
    }


    private void OnTriggerStay(Collider other)
    {
       // transform.parent = other.transform;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            TouchingObject = false;
            player.gameObject.GetComponent<Movement>().runSpeed = player.gameObject.GetComponent<Movement>().runSpeed * reduceSpeedAmount;
            player.gameObject.GetComponent<Movement>().walkSpeed = player.gameObject.GetComponent<Movement>().walkSpeed * reduceSpeedAmount;
            player.gameObject.GetComponent<Movement>().turnSmoothTime = player.gameObject.GetComponent<Movement>().turnSmoothTime / reduceSpeedAmount;
            pushing = false;
            transform.parent = null;
            player = null;
        }
    }

}
