using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
	// SCRIPTS
	public static Game instance = null;
	public Board boardScript;
	public JackKnife jackKnifeScript;

	// BOOLEANS
	public bool playersTurn = true;
	private bool enemiesMoving = false;
	private bool doingSetup = false;
	public bool firstLevel = true;
	public bool isGameOver = false;

	// FLOATS
	public float levelStartDelay = 2f;
	public float turnDelay = 0.1f;

	// LISTS
	private List<int> themePermutation;
	public List<Enemy> enemies;

	// INTS
	public int level;


	// UNITY FUNCTIONS
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != null)
		{
			Destroy (gameObject);
		}


		Game.instance.level++;

		//inventory = new List<Slot> ();
		themePermutation = new List<int> ();

		for (int i = 0; i < 5; i++)
		{
			themePermutation.Add (i);
		}
		for (int i = 0; i < 5; i++)
		{
			swap (Random.Range (0, 5), Random.Range (0, 5));
		}


		instance.enabled = true;
		DontDestroyOnLoad (gameObject);
		enemies = new List<Enemy> ();
		boardScript = GetComponent<Board> ();
		jackKnifeScript = GetComponent<JackKnife> ();
		//InitBoss ();
		InitGame ();
	}

	void Update()
	{

		if (Game.instance.isGameOver == true)
		{
			if (Input.anyKey)
			{
				Application.Quit();
			}

		}

		if (playersTurn || enemiesMoving || doingSetup)
		{
			return;
		}



		StartCoroutine (moveEnemies ());

	}


	// MY FUNCTIONS
	void InitGame()
	{
		Debug.Log ("-------" + "Level " + Game.instance.level + "-------");


		//doingSetup = true;
		//Invoke ("hideLevelImage", levelStartDelay);

		Game.instance.enemies.Clear ();

		boardScript.makeLevel (Game.instance.level);
		//Player.instance.transform.position = new Vector3(3f, 3f, 0f);
		//Player.instance.updateBars ();

	}

	void InitBoss ()
	{
		Game.instance.enemies.Clear ();
		jackKnifeScript.makeBoard();
	}

	private void hideLevelImage()
	{
		doingSetup = false;
	}

	public void GameOver()
	{
		Game.instance.isGameOver = true;
		Player.instance.gameObject.SetActive (false);
		Player.instance.gameOverPanel.SetActive (true);
	}

	public void addEnemy(Enemy script)
	{
		enemies.Add (script);
	}

	public void removeEnemy(Enemy script)
	{
		enemies.Remove (script);
	}

	IEnumerator moveEnemies()
	{
		//Debug.Log ("Moving " + enemies.Count + " enemies.");



		enemiesMoving = true;
		yield return new WaitForSeconds (turnDelay);
		if (enemies.Count == 0)
		{
			yield return new WaitForSeconds (turnDelay);
		}

		for (int i = 0; i < enemies.Count; i++)
		{
			enemies [i].moveEnemy ();
			/*if (enemies [i].checkRange () == true)
			{
				enemies [i].attackPlayer ();
			}*/

			yield return new WaitForSeconds (0.1f);
			if (enemies [i].checkRange () == true)
			{
				enemies [i].attackPlayer ();
			}
		}

		playersTurn = true;
		enemiesMoving = false;

	}




	void swap(int first, int second)
	{
		int temp = themePermutation[first];
		themePermutation [first] = themePermutation [second];
		themePermutation [second] = temp;
	}
}
