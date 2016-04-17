using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject playerGood;
	public GameObject playerBad;

	public float minX;
	public float maxX;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minX, maxX), transform.position.y, transform.position.z);
		if (playerBad.GetComponent<PlayerController> ().inUse) {
			this.transform.SetParent (playerBad.transform);
		} else if (playerGood.GetComponent<PlayerController> ().inUse) {
			this.transform.SetParent (playerGood.transform);
		}
	}
}
