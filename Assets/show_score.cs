using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class show_score : MonoBehaviour
{
    // Start is called before the first frame update
	public static int currScore;
	Text score;
	
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + currScore;
    }
}
