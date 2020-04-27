using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resume_game : MonoBehaviour
{
    // Start is called before the first frame update
    public void resumeGame(){
		Time.timeScale = 1;
		
		GameObject[] pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		GameObject[] pauseOverObjects = GameObject.FindGameObjectsWithTag("ShowOnPause&Over");
		
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
		
		foreach(GameObject g in pauseOverObjects){
			g.SetActive(false);
		}
	}
}
