using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsMove : MonoBehaviour {

    public Transform TargetPositionMarker;
    public float PlatformMoveSpeed = 0.2f;
    public Transform StartPostion;
    public Vector3 nextPos;
    public bool PlayerOnPlatform;
    public bool AtStartPoint;

    public enum PlatformType
    {
        PinPong,
        PlayerTriggerd

    }
    public PlatformType platformType;

    public bool Activated = true;
	// Use this for initialization
	void Start () {
        PlayerOnPlatform = false;
        AtStartPoint = true;

        nextPos = StartPostion.position;
	}
	
	// Update is called once per frame
	void Update () {
        MovePlatform();
	}
    private void LateUpdate()
    {
        
    }


    public void MovePlatform()
    {
        if (!Activated)
            return;
        if (platformType == PlatformType.PlayerTriggerd)
        {
            if (PlayerOnPlatform)
            {
                if (transform.position == TargetPositionMarker.position)
                    return;
                else
                {
                    nextPos = TargetPositionMarker.position;

                }


                transform.position = Vector3.MoveTowards(transform.position, nextPos, PlatformMoveSpeed * Time.deltaTime);
            }
            else
            {
                nextPos = StartPostion.position;
                transform.position = Vector3.MoveTowards(transform.position, nextPos, PlatformMoveSpeed * Time.deltaTime);
            }
        }
        else if (platformType == PlatformType.PinPong)
        {
            // Bounce effect
             if (transform.position == StartPostion.position)
            {
                nextPos = TargetPositionMarker.position;

              }
           if (transform.position == TargetPositionMarker.position)
               {
                 nextPos = StartPostion.position;
                }
             transform.position = Vector3.MoveTowards(transform.position, nextPos, PlatformMoveSpeed * Time.deltaTime);
        }
    }


   

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerOnPlatform = true;
            other.transform.parent = transform;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerOnPlatform = false;
            other.transform.parent = null;
        }
    }

}
