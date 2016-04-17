using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float bulletSpeed;
	Transform spawner;
	bool hasAdjustedSpawner = false;
	public GameObject bullet;
	public float shootSpeed;
	public float moveSpeed;
	public GameObject otherEntity;
	public bool inUse;
	bool isGrounded;
	public float jumpHeight;
	Rigidbody myrb;
	public float jumpTimer;
	float jumpTimeRemaining;

	// Use this for initialization
	void Start () {
		if (otherEntity.tag == "PlayerGood") {
			spawner = GameObject.FindGameObjectWithTag ("SpawnerBad").transform;
		} else if (otherEntity.tag == "PlayerBad") {
			spawner = GameObject.FindGameObjectWithTag ("SpawnerGood").transform;
		}


		myrb = GetComponent<Rigidbody> ();
		jumpTimeRemaining = jumpTimer;
	}
	
	// Update is called once per frame
	void Update () {


		jumpTimeRemaining -= Time.deltaTime;
		if (Input.GetKey (KeyCode.W) && isGrounded && jumpTimeRemaining <= 0) {
			myrb.AddForce (new Vector3 (0, jumpHeight * 10, 0));
			jumpTimeRemaining = jumpTimer;
		}

		if (inUse) {
			transform.position += new Vector3 (Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
			otherEntity.transform.position = new Vector3 (transform.position.x, otherEntity.transform.position.y, otherEntity.transform.position.z);
		} else if (!inUse) {
		//	this.GetComponent<MeshRenderer> ().enabled = false;

			this.GetComponentInChildren<SpriteRenderer> ().enabled = false;

		}

		if (Input.GetAxisRaw ("Horizontal") > 0) {
			GetComponentInChildren<Animator> ().SetBool ("isMoving", true);
			GetComponentInChildren<SpriteRenderer> ().flipX = false;
			bulletSpeed = Mathf.Abs (bulletSpeed);
			if (!hasAdjustedSpawner) {
				hasAdjustedSpawner = true;
				spawner.transform.position = new Vector3 (transform.position.x + 0.45f, spawner.transform.position.y, 0);
			}


		} else if (Input.GetAxisRaw ("Horizontal") < 0) {
			
			GetComponentInChildren<Animator> ().SetBool ("isMoving", true);
			GetComponentInChildren<SpriteRenderer> ().flipX = true;
			bulletSpeed =  Mathf.Abs (bulletSpeed) * -1f;
			if (hasAdjustedSpawner) {
				hasAdjustedSpawner = false;
				spawner.transform.position = new Vector3 (transform.position.x - 0.45f, spawner.transform.position.y, 0);
			}


		} else {
			GetComponentInChildren<Animator> ().SetBool ("isMoving", false );
		}


		if (Input.GetKeyDown (KeyCode.Space) && inUse) {
			InvokeRepeating ("Shoot", 0.01f, shootSpeed);
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ();
		}

	}

	void changeSpawnerLocation(){
		
	}

	void Shoot(){
		GameObject bulletSpawned =	Instantiate (bullet, spawner.transform.position, Quaternion.identity) as GameObject;
		bulletSpawned.GetComponent<Rigidbody>().velocity = new Vector3(bulletSpeed,0, 0);
	}




	void OnCollisionStay(Collision other){
		isGrounded = true;
	}

	void OnCollisionExit(Collision other){
		isGrounded = false;
	}

	public void ReverseActive(){
		inUse = !inUse;
		//this.GetComponent<MeshRenderer> ().enabled = inUse;
		this.GetComponentInChildren<SpriteRenderer> ().enabled = inUse;
	}
		
}
