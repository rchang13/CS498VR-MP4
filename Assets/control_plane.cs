using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_plane : MonoBehaviour
{
	float moveHorizontal;
	float moveVertical;
	
	float forwardSpeed = 0;
	float horizontalSpeed = 35;
	float rotatingSpeed = 180;
	private Vector3 rotation;
	Rigidbody rb;
	Collider coll;
	AudioClip engine;
	AudioSource []audio;
	
	float range = 600f;
	public Camera fpsCam;
	public ParticleSystem leftFlash;
	public ParticleSystem rightFlash;
	
	int totalScore = 0;
	
	GameObject[] pauseObjects;
	GameObject[] overObjects;
	GameObject[] pauseOverObjects;
	
	bool alive;
	float deltaTime;
	
    void Start()
    {
        rotation = transform.rotation.eulerAngles;
		rb = GetComponent<Rigidbody>();
		rb.mass = 0.0002f;

		audio = GetComponents<AudioSource>();
		audio[0].volume = 0.1f;
		audio[0].Play();
		audio[1].Play();
		
		rb.useGravity = false;
		coll = GetComponent<Collider>();
		
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		overObjects = GameObject.FindGameObjectsWithTag("ShowOnOver");
		pauseOverObjects = GameObject.FindGameObjectsWithTag("ShowOnPause&Over");
		
		show_score.currScore = totalScore;
		DisplayFPS.currFPS = 0.0f;
		
		alive = true;
		deltaTime = 0.0f;
		
		hidePaused();
		hideOver();
		hidePausedOver();
    }

    // Update is called once per frame
    void Update()
    {	
		if(this.transform.position.y >= 80.0 && forwardSpeed <= 5.0){
			if(rb.useGravity == false){
				rb.useGravity = true;
			}
		}
		else{
			if(rb.useGravity == true){
				rb.useGravity = false;
			}
		}
		
		rotation += new Vector3(Input.GetAxis("Pitch")*rotatingSpeed*Time.deltaTime,
		Input.GetAxis("Yaw")*rotatingSpeed*Time.deltaTime,
		Input.GetAxis("Roll")*rotatingSpeed*Time.deltaTime);
		this.transform.rotation = Quaternion.Euler(rotation);
		
        moveHorizontal = Input.GetAxis("Horizontal");
		moveVertical = Input.GetAxis("Vertical");
		
		if(moveVertical > 0){
			forwardSpeed = Mathf.Min((float)(forwardSpeed + 7.5*Time.deltaTime*moveVertical), 150);
		}
		else if(moveVertical < 0){
			forwardSpeed = Mathf.Max(forwardSpeed + 15*Time.deltaTime*moveVertical, 0);
		}
		else{
			forwardSpeed = Mathf.Max(forwardSpeed - 5*Time.deltaTime, 0);
		}
		
		audio[0].volume = (float)(0.1f + forwardSpeed*0.003);
		this.transform.position += (Time.deltaTime*forwardSpeed*this.transform.right*(-1));
		this.transform.position += (this.transform.forward * moveHorizontal * horizontalSpeed * Time.deltaTime);
		
		if(this.transform.position.y <= 36.0){
			gameOver();
		}
		
		if(Input.GetButtonDown("Fire1")){
			if(Time.timeScale == 1){
				shoot();
				StartCoroutine(shootCoroutine());
			}
		}
		
		if(totalScore >= 160){
			gameOver();
		}
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(Time.timeScale == 1 && alive == true)
			{
				Time.timeScale = 0;
				showPaused();
			} else if (Time.timeScale == 0 && alive == true){
				Time.timeScale = 1;
				hidePaused();
				hidePausedOver();
			}
		}
		
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
		DisplayFPS.currFPS = Mathf.Ceil(fps);
    }
	
	void shoot(){
		leftFlash.Play();
		rightFlash.Play();
		audio[2].Play();
		
		RaycastHit hit;
		if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
			Target target = hit.transform.GetComponent<Target>();
			if(target != null){
				if(target.appear){
					target.disappear();
					if(!target.hitOnce){
						totalScore += target.score;
						target.hitOnce = true;
						target.changeColor();
					}
					else{
						totalScore += (target.score/2);
					}
					show_score.currScore = totalScore;
					StartCoroutine(AppearCoroutine(target));
				}
			}
		}
	}
	
	IEnumerator AppearCoroutine(Target target)
    {
        yield return new WaitForSeconds(5);
        target.reappear();
    }
	
	IEnumerator shootCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        leftFlash.Stop();
		rightFlash.Stop();
		audio[2].Stop();
    }
	
	void gameOver(){
		alive = false;
		Time.timeScale = 0;
		showOver();
	}
	
	void showPaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
		
		foreach(GameObject g in pauseOverObjects){
			g.SetActive(true);
		}
	}
	
	void showOver(){
		foreach(GameObject g in overObjects){
			g.SetActive(true);
		}
		
		foreach(GameObject g in pauseOverObjects){
			g.SetActive(true);
		}
	}

	void hidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}
	
	void hideOver(){
		foreach(GameObject g in overObjects){
			g.SetActive(false);
		}
	}
	
	void hidePausedOver(){
		foreach(GameObject g in pauseOverObjects){
			g.SetActive(false);
		}
	}
	
	
	 void OnTriggerEnter(Collider other)
    {
		if(other.tag == "Terrain"){
			gameOver();
		}
    }
	
}
