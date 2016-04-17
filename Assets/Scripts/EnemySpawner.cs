using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	bool hasRandom;
	public Wave[] waves;
	Wave currentWave;
	int currentWaveNumber = 1;
	GameObject formation; 
	int enmemiesCount;
	//bool allEnemiesDead = false;
	float enemiesRemaining;
	float totalEnemies;
	GameObject enemyTransform;
	float enemySpawnSpeed;


	void Start(){
		initializeWave ();
		enemySpawnSpeed = currentWave.spawnSpeed;
	}

	void initializeWave(){
		currentWave = waves [currentWaveNumber - 1];
		totalEnemies = currentWave.enemyCount;
		enemiesRemaining = totalEnemies;
		enemySpawnSpeed = currentWave.spawnSpeed;
//		Debug.Log (enemySpawnSpeed);

		InvokeRepeating ("spawnEnemies", 0.01f, enemySpawnSpeed);



		//spawnEnemies ();
	}



	void progressWave(){
		//currentWaveNumber += 1;


		if (currentWaveNumber <= waves.Length) {
			currentWave = waves [currentWaveNumber - 1];

			totalEnemies = currentWave.enemyCount;
			enemiesRemaining = totalEnemies;
			enemySpawnSpeed = currentWave.spawnSpeed;
			//Debug.Log ("Wave Progressed!");


			//spawnEnemies ();
		}
	}

	void Update(){
		if (currentWave.infinite ) {
			InvokeRepeating ("spawnRandomEnemies", 0.01f, enemySpawnSpeed);
		}

		if(currentWave.infinite == false){
			if (enemiesRemaining < 0) {
				CancelInvoke ();
			}
		}
	}

	public void spawnRandomEnemies(){
		if (!hasRandom) {
			StartCoroutine ("randomSpawn");
			hasRandom = true;
		}
	}

	IEnumerator randomSpawn(){
		float i = Random.Range (1f, 4f);
		yield return new WaitForSeconds (i);
		enemyTransform = Instantiate (currentWave.enemyObj, transform.position, Quaternion.identity) as GameObject;
		enemyTransform.transform.parent = transform;
		hasRandom = false;

	}
		

	public void spawnEnemies(){
		/*for(int i = 0; i <= totalEnemies; i++){
			enemyTransform = Instantiate (currentWave.enemyObj, transform.position, Quaternion.identity) as GameObject;
			enemiesRemaining -= 1;
			enemyTransform.transform.parent = transform;
		}
		*/

		if (enemiesRemaining > 0 ) {
		
			/*if (enemyTransform != null) {
				Destroy (enemyTransform.gameObject);
			}*/

			enemyTransform = Instantiate (currentWave.enemyObj, transform.position, Quaternion.identity) as GameObject;
//			Debug.Log (enemiesRemaining);
			enemiesRemaining -= 1;
			enemyTransform.transform.parent = transform;

		}else if (enemiesRemaining <= 0 && currentWaveNumber <= waves.Length ) {
			//Debug.Log ("Progressed Wave");
			currentWaveNumber += 1;
			progressWave ();
		}
	}


	[System.Serializable]
	public class Wave{
		public float enemyCount;
		public GameObject enemyObj;
		public float spawnSpeed;
		public bool infinite;
	}
}