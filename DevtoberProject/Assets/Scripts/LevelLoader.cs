using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject LoadingScreen;
    // public Slider LoadBar;
    public Text ProgressText;
    public int LevelToLoad;
    public float TimetilLoadLevel = 3f;

    public void Start()
    {

        LevelToLoad = PlayerPrefs.GetInt("LevelToLoad");
        ProgressText.text = "0%";
        Invoke("BeginLoadALevel", TimetilLoadLevel);
        Debug.Log("LevelLoading" + LevelToLoad.ToString());
    }



    public void BeginLoadALevel()
    {
        LoadLevel(LevelToLoad);
    }
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));

    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        LoadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            //  Debug.Log(operation.progress);
            //   LoadBar.value = progress;
            ProgressText.text = progress * 100f + "%";
            yield return null;
        }
    }
}
