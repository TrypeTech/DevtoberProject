using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
      private MenuController menuController;
      private Animator animator;
      private AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;
    public string LoadingLevel = "TestLoadLevel";
    [SerializeField] int LevelToLoad;
    public float WaitTime = 2f;
    public enum ButtonType
    {
        LevelLoader,
        ExitGame
    }
    public ButtonType buttonType;
    // Start is called before the first frame update
    void Start()
    {
        menuController = FindObjectOfType<MenuController>();
        animatorFunctions = FindObjectOfType<AnimatorFunctions>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(menuController.index == thisIndex)
        {
            animator.SetBool("selected", true);
            if(Input.GetAxis("Submit") == 1)
            {
                animator.SetBool("pressed", true);
                Invoke("ButtonActivated", WaitTime);

            }else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
                animatorFunctions.disableOnce = true;
            }
        }else
        {
            animator.SetBool("selected", false);
        }
    }


    public void ButtonActivated()
    {
        if(buttonType == ButtonType.LevelLoader)
        {
           
             PlayerPrefs.SetInt("LevelToLoad",LevelToLoad);
            SceneManager.LoadScene(LoadingLevel);
        }
        else if(buttonType == ButtonType.ExitGame)
        {
            Application.Quit();
        }
    }
}
