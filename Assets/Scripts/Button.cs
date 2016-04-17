using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Button : MonoBehaviour {

	public GameObject towerPrefab;
	public static GameObject selectedTower;

	private Button[] buttons;
	// Use this for initialization
	void Start () {
		buttons = GameObject.FindObjectsOfType<Button>();
	}
	
	// Update is called once per frame
	void Update () {
	 
	}



	void OnMouseDown(){

		foreach (Button thisButton in buttons) {
			thisButton.GetComponentInChildren<SpriteRenderer> ().color = Color.black;
		}

		GetComponentInChildren<SpriteRenderer> ().color = Color.white;

		selectedTower = towerPrefab;
//		Debug.Log (selectedTower);
	}
}
