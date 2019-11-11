using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    // add a trigger collider box and set it to trigger
    // place it at win spot

    PlayerStats playerStats;
    public string LevelToLoad = "TestMenu";
    public float TimeBetweenLoadingLevel = 3f;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelToLoad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DoPlayerWinStuff();
            Invoke("LoadLevel", TimeBetweenLoadingLevel);
        }
    }

    public void DoPlayerWinStuff()
    {
        // you win banner
        // sound effects
        // partical effects
    }
}
