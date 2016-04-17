using UnityEngine;
using System.Collections;

public class TopShooterTower : MonoBehaviour {

	public float myHealth;
	bool takingDamage = false;
	public float shootFrequency;
	public float bulletSpeed;
	public GameObject bullet;
	public Transform spawnPoint;
	EnemyController enemy;
	// Use this for initialization
	void Start () {
		if (bullet != null) {
			InvokeRepeating ("Shoot", 0.01f, shootFrequency);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (myHealth <= 0) {
			enemy.startMovingAgain ();
			Destroy (this.gameObject);
		}
	}

	void Shoot(){
		GameObject projectile = Instantiate (bullet, spawnPoint.transform.position, Quaternion.identity) as GameObject;
		projectile.GetComponent<Rigidbody> ().velocity = new Vector3 (bulletSpeed, 0, 0);

	}

	void OnTriggerStay(Collider other){
		
		if (other.tag == "Enemy") {
			enemy = other.gameObject.GetComponent<EnemyController> ();

			if (!takingDamage) {
				takingDamage = true;
				StartCoroutine ("takeDamage");
			}
		} 
	}

	IEnumerator takeDamage(){
		myHealth -= enemy.GetDamage ();
		yield return new WaitForSeconds (1f);
		takingDamage = false;
	}
}
