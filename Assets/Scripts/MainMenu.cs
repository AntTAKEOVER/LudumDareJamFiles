using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject information;
	public GameObject infoPanel;
	public bool instructions;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (instructions) {
			information.SetActive (true);
			infoPanel.SetActive (true);
		} else if (!instructions) {
			information.SetActive (false);
			infoPanel.SetActive (false);
		}
	}



	public void showInstructions(){
		instructions = !instructions;
	}

	public void StartGame(){
		SceneManager.LoadScene ("Game1");
	}
}
