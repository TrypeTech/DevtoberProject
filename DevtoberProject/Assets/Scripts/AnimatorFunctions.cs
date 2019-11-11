using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{

    private MenuController menuController;
    public bool disableOnce;

    private void Start()
    {
        menuController = FindObjectOfType<MenuController>();
    }
    void PlaySound(AudioClip whichSound)
    {
        if (!disableOnce)
        {
            menuController.audioSource.PlayOneShot(whichSound);
        }
        else
        {
            disableOnce = false;
        }
    }
}
