using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour
{

    // Add the platform that you want to activate to the slot
    // Add the Button On And button off gameobjects to the slot make sure the button off button is set

    [Header("Buttons to switch when triggerd")]
    public GameObject OffButtonObject;
    public GameObject OnButtonObject;

    public GameObject Platform;

    public bool CanActivateSwitch;

    public enum KeyTriggerType
    {
        KeyHole,
        Button,
        PressurePlate
    }

    public KeyTriggerType type;
    public KeyCode TriggerButton = KeyCode.E; 
    public bool canActivate;
    
    // Start is called before the first frame update
    void Start()
    {
        OffButtonObject.gameObject.SetActive(true);
        OnButtonObject.gameObject.SetActive(false);
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
                SwitchButtonObjects();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canActivate = true;
        if(type == KeyTriggerType.PressurePlate)
        {
            Platform.gameObject.GetComponentInChildren<PlatformsMove>().Activated = true;
            SwitchButtonObjects();
        }

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

    public void SwitchButtonObjects()
    {
        // play sound effect
        OffButtonObject.gameObject.SetActive(false);
        OnButtonObject.gameObject.SetActive(true);
    }
}
