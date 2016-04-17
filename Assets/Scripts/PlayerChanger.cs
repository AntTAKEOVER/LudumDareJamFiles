using UnityEngine;
using System.Collections;

public class PlayerChanger : MonoBehaviour {

	public float bufferTime;
	public float currentTimeRemaining;
	// Use this for initialization
	void Start () {
		currentTimeRemaining = bufferTime;
	}
	
	// Update is called once per frame
	void Update () {
		currentTimeRemaining -= Time.deltaTime;

	}

	void OnTriggerEnter(Collider other){
		if (other.tag != "Bullet" && other.tag != "Enemy" && other.tag != "SlowBullet") {
//			Debug.Log (other.tag);
			if (other.GetComponent<PlayerController> ().inUse) {
				PlayerController[] players = FindObjectsOfType<PlayerController> ();
				PlayerChanger[] changers = FindObjectsOfType<PlayerChanger> ();
				if (currentTimeRemaining <= 0) {
					foreach (PlayerChanger changer in changers) {
						changer.currentTimeRemaining = bufferTime;
					}

					foreach (PlayerController player in players) {
						//Debug.Log ("+1");
						player.ReverseActive ();
					}
				}
			}
		}
	}
}
