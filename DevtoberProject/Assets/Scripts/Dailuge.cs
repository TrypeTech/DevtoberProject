using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dailuge : MonoBehaviour {

   public Text textDisplay;
   public string[] sentences;
   private int index;
   public float typeingSpeed = 0.02f;
   public string levelToLoad = "TestLevel";

    public GameObject ContinueButton;

	// Use this for initialization
	void Start () {
		StartCoroutine(Type());
	}
  

    void Update(){

		if(textDisplay.text == sentences[index]){
			ContinueButton.SetActive(true);
		}
	}
   IEnumerator Type(){
	   foreach (char letter in sentences[index].ToCharArray()){
         textDisplay.text += letter;
		 yield return new WaitForSeconds(typeingSpeed);
	   }
	  
   }

	
	public void NextSentence(){
		ContinueButton.SetActive(false);
		if(index < sentences.Length - 1){
			index++;
			textDisplay.text = "";
			StartCoroutine(Type());
		}
		else{
			textDisplay.text = "";
				SceneManager.LoadScene(levelToLoad);
			ContinueButton.SetActive(false);
		}
	}
}
