using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePush : MonoBehaviour
{
    // for this to work add a rigidbody to the object and a sphere Collider for collision It prevents cliping of tile level assets that prevent movement
    // and a box cillider trigger where the  point the player pushes the object is
    // on the rigid body constrain all the rotations 
    // and set the direction the object is moveing in the direction option

    public enum Direction
    {
        Forward,
        Backwards,
        Left,
        Right
    }
    public Direction direction;
    public Vector3 moveDirection;
    Rigidbody rig;
    public float moveSpeed = 60f;
    public bool canPush;

    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
    }
    public void MoveObject()
    {
       
        // rig.velocity = new Vector3(1*moveSpeed * Time.deltaTime,0,0);
        if (direction == Direction.Forward)
        {
            moveDirection = Vector3.forward;
        }
        if (direction == Direction.Backwards)
        {
            moveDirection = -Vector3.forward;
        }
        if (direction == Direction.Left)
        {
            moveDirection = Vector3.left;
        }
        if (direction == Direction.Right)
        {
            moveDirection = Vector3.right;
        }

      rig.velocity = moveDirection * moveSpeed * Time.deltaTime;
        // rig.AddForce(moveDirection * moveSpeed * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        if (canPush)
        {
            MoveObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canPush = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canPush = false;
        }
    }
}
