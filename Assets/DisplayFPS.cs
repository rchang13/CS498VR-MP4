using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFPS : MonoBehaviour
{
    // Start is called before the first frame update
	public static float currFPS;
	Text fps;
	
    void Start()
    {
        fps = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        fps.text = "FPS: " + currFPS;
    }
}
