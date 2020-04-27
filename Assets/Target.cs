using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
	public bool appear = true;
	public GameObject sphere;
	public int id;
	public bool hitOnce = false;
	public int score;
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void disappear(){
		if(appear){
			sphere.SetActive(false);
			appear = false;

			//StartCoroutine(AppearCoroutine());
			//appear = true;
		}
	}
	
	public void reappear(){
		sphere.SetActive(true);
		appear = true;
	}
	
	public void changeColor(){
		sphere.GetComponent<Renderer>().material.color = Color.green;
	}

}
