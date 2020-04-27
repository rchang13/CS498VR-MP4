using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class back_to_main_menu : MonoBehaviour
{
    public void backToMainMenu(){
		SceneManager.LoadScene(sceneName: "MenuScene");
	}
    
}
