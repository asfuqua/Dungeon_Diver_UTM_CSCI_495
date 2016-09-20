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
	public float levelStartDelay = 2f;
	public float turnDelay = 0.1f;
	public static Game instance = null;
	public Board boardScript;

	public bool playersTurn = true;




	private List<Enemy> enemies;
	private int level = 1;
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

		instance.enabled = true;
		DontDestroyOnLoad (gameObject);
		enemies = new List<Enemy> ();
		boardScript = GetComponent<Board> ();
		InitGame ();
	}

	private void levelLoaded(int index)
	{
		level++;
		InitGame ();
	}

	void InitGame()
	{
		//doingSetup = true;
		//Invoke ("hideLevelImage", levelStartDelay)

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
}
