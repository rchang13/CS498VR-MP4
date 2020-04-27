using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_in_menu : MonoBehaviour
{
    // Start is called before the first frame update
	float moveHorizontal;
	float moveVertical;
	
	public float movingSpeed = 5;
	public float rotatingSpeed = 180;
	
    void Start()
    {		
		
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
		moveVertical = Input.GetAxis("Vertical");
		
		transform.Translate(moveVertical*Time.deltaTime*movingSpeed*Vector3.forward);
		transform.Rotate(Vector3.up, moveHorizontal*Time.deltaTime*rotatingSpeed);
    }
}
