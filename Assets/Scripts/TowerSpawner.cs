using UnityEngine;
using System.Collections;

public class TowerSpawner : MonoBehaviour {

	GameObject currentTower;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (currentTower != null) {
			Destroy (currentTower.gameObject);
		} 

		if (FindObjectOfType<GameManager> ().money >= 30) {
			currentTower = Instantiate (Button.selectedTower, transform.position, Quaternion.identity) as GameObject;
			currentTower.transform.SetParent (this.transform);
			FindObjectOfType<GameManager> ().money -= 30;
		}
	}
}
