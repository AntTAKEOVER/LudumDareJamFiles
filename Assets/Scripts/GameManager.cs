using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

	public GameObject mainMenuButton;
	public GameObject restartButton;
	public GameObject gaemOverPanel;
	public Text gameOverText;
	public Text CrystalHealth;
	public Text scoreText;
	public Text moneyText;
	public float money;
	public float health;
	public float score;
	// Use this for initialization
	void Start () {
		gameOverText.text = " ";
	}
	
	// Update is called once per frame
	void Update () {
		CrystalHealth.text = "Health " + health;
		moneyText.text = "Money: " + money;
		scoreText.text = "Score: " + score;

		if (health <= 0) {
			health = 0;
			gameOverText.text = "Game Over!";
			gaemOverPanel.SetActive (true);
			restartButton.SetActive (true);
			mainMenuButton.SetActive (true);
		}
	}

	public void restartGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
	}

	public void mainMenuLoad(){
		SceneManager.LoadScene ("MainMenu");
	}
}
