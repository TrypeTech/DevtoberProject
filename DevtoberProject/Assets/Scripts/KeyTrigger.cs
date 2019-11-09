using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour
{

    // Add the platform that you want to activate to the slot

    public GameObject Platform;

    public bool CanActivateSwitch;

    public enum KeyTriggerType
    {
        KeyHole,
        Button
    }

    public KeyTriggerType type;
    public KeyCode TriggerButton = KeyCode.E; 
    public bool canActivate;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canActivate == true)
        {
            if (Input.GetKeyDown(TriggerButton))
            {
                // activate platform
                Platform.gameObject.GetComponentInChildren<PlatformsMove>().Activated = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canActivate = true;
    }

    private void OnTriggerStay(Collider other)
    {
        canActivate = true;

        // create player display to activate button
        Debug.Log("Press Button to activate trigger");
    }
    private void OnTriggerExit(Collider other)
    {
        canActivate = false;
    }
}
