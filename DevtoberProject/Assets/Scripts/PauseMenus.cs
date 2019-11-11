using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenus : MonoBehaviour
{
    // set all hud and button animation animator on componenet from normal to unscaled time 

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public AudioSource[] gameAudio;
    public string MainMenuLevelName = "TestMainMenu";
    // Start is called before the first frame update
    void Start()
    {

        disablePauseMenu();
        gameAudio = FindObjectsOfType<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        foreach (AudioSource sound in gameAudio)
        {
            if(sound != null)
            sound.volume = 1f;
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        foreach(AudioSource sound in gameAudio)
        {
            if(sound != null)
            sound.volume = 0.2f;
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        foreach (AudioSource sound in gameAudio)
        {
            if(sound != null)
            sound.volume = 1f;
        }
        Debug.Log("Loading Menu");
        SceneManager.LoadScene(MainMenuLevelName);
    }

    public void disablePauseMenu()
    {
         pauseMenuUI.gameObject.SetActive(false);
    }
}
