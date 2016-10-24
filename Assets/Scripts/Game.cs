/*
 * 
 * FIX ANYTHING RELATED TO ENEMIES
 * NEED TO ADD ENEMY SCRIPT
 * 
 * 
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
	//public List<Slot> inventory;
	private List<int> themePermutation;



	public bool firstLevel = true;
	public float levelStartDelay = 2f;
	public float turnDelay = 0.1f;
	public static Game instance = null;
	public Board boardScript;

	public bool playersTurn = true;




	public List<Enemy> enemies;
	public int level = 1;
	private bool enemiesMoving = false;
	private bool doingSetup = false;

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
		InitGame ();
	}

	private void OnLevelWasLoaded(int index)
	{
		level++;
		Player.instance.transform.position = new Vector3(3f, 3f, 0f);
		Player.instance.updateBars ();
		//Player.instance.transform.position = 0f;
		//InitGame ();
	}

	void InitGame()
	{
		//doingSetup = true;
		//Invoke ("hideLevelImage", levelStartDelay);
		//Weapons.instance.inventory = inventory;
		//enemies.Clear ();

		boardScript.makeLevel (level);

	}

	private void hideLevelImage()
	{
		doingSetup = false;
	}

	public void GameOver()
	{
		//enabled = false;
	}

	void Update()
	{

		if (playersTurn || enemiesMoving || doingSetup)
		{
			return;
		}

		StartCoroutine (moveEnemies ());

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
