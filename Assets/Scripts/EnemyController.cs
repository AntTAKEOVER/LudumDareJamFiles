using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float damageToTowers;
	bool isAttackingTower = false;
	GameObject target;
	public float moveSpeed;
	public float myHealth;
	public bool isBadEnemy;
	Vector3 dstToTarget;
	bool isSlowed = false;
	bool hasHitOnce = false;
	bool reducingCrystal = false;
	// Use this for initialization
	void Start () {
		
		if (isBadEnemy) {
			target = GameObject.FindGameObjectWithTag ("CrystalBad");
		} else if (!isBadEnemy) {
			target = GameObject.FindGameObjectWithTag ("CrystalGood");
		}
		dstToTarget = new Vector3(((transform.position.x - target.transform.position.x ) * Time.deltaTime), 0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isAttackingTower) {
			transform.Translate (dstToTarget * -moveSpeed);
		}
		//transform.position = new Vector3(Mathf.Lerp (transform.position.x, target.transform.position.x, moveSpeed / 100f), transform.position.y);
		if (transform.position.x <= target.transform.position.x) {
			transform.position = new Vector3 (target.transform.position.x , transform.position.y);
			if (!reducingCrystal) {
				reducingCrystal = true;
				InvokeRepeating ("reduceCrystalHealth", 0.01f, 2f);

			}

		}
	}

	void OnTriggerEnter(Collider other){
		Projectile bullet = other.gameObject.GetComponent<Projectile> ();
		if (bullet != null && other.tag != "SlowBullet") {
			myHealth -= bullet.GetDamage ();
			bullet.Hit ();
			if (myHealth <= 0) {
				CancelInvoke();
				Destroy (gameObject);
				FindObjectOfType<GameManager> ().money += 5f;
				FindObjectOfType<GameManager> ().score += 100f;
			}
		}

		if (other.tag == "SlowBullet") {

			if (!isSlowed) {
				StartCoroutine ("slowMovementSpeed");

			}
			if (!hasHitOnce) {
				
				hasHitOnce = true;
			}
		}

		if (other.tag == "Tower") {
			isAttackingTower = true;
		}

	}

	void reduceCrystalHealth(){
		FindObjectOfType<GameManager> ().health -= 5f;
	}


	IEnumerator slowMovementSpeed(){
		//Debug.Log ("SLowed");
		isSlowed = true;
		float oldMoveSpeed = moveSpeed;
		moveSpeed = 0.02f;
		yield return new WaitForSeconds (3f);
		moveSpeed = oldMoveSpeed;
		isSlowed = false;
		hasHitOnce = false;
		
	}

	public float GetDamage(){
		return damageToTowers;

	}

	public void startMovingAgain(){
		isAttackingTower = false;
	}
}
