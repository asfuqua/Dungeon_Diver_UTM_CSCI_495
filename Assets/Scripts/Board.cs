using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
	// SINGLE USE VARIABLES
	public GameObject exit;																// The Exit GameObject "Ladder" that sends the player to the next level
	public int columns;																	// The number of columns in each rectangular room, is randomized for each room
	public int rows;																	// The number of rows in each rectangular room, is randomized for each room

	public GameObject instance;															// public accessor of singleton board script
	private int randomTheme;


	private Transform boardHolder;


	private List<Vector3> grid = new List<Vector3> ();
	private List<Vector3> playableArea = new List<Vector3> ();
	public List<GameObject> floorSpaces = new List<GameObject> ();

	private List<int> themePermutation;

	// ITEMS
	public GameObject[] items;

	// ENEMIES
	public GameObject[] enemies;

	// FIELD TILES
	public GameObject grassWall;
	public GameObject[] grassTiles;

	// LAVA TILES
	public GameObject[] lavaTiles;
	public GameObject[] lavaWall;

	// DESERT TILES
	public GameObject[] desertTiles;
	public GameObject desertWall;

	// ICE TILES
	public GameObject snowFloor;
	public GameObject iceWall;


	//DUNGEON TILES
	public GameObject dungeonFloor;
	public GameObject[] dungeonWalls;

	//FUNCTIONS--------------------------------------------------------------------------------------------------------

	public void makeLevel(int level)
	{
		boardHolder = new GameObject ("BoardHolder").transform;




		InitializeWholeBoard ();									// Places Vector3 points in empty list
		randomTheme = Random.Range (0, 5);							// Gets a random int between 0 and 4
		//randomLevel = 4;
		makeBoard (randomTheme);									// makes the rooms and hallways, places floor tiles on the points, moving each point that is used to another list
		FillEmptySpaceWithWalls (randomTheme);						// puts a wall tile on every point that is left in the original list
		placeObjectAtRandom (items, 5, 15);							// places random items at points that are available in rooms, ie FLoor tiles									
		//int enemyCount = (int)Mathf.Log (level, 2f);
		int enemyCount = level;
		Debug.Log (enemyCount);
		if (enemyCount == 0)
		{
			enemyCount = 1;
		}

		placeObjectAtRandom (enemies, enemyCount, enemyCount);
		Instantiate (exit, playableArea [Random.Range (0, playableArea.Count)], Quaternion.identity);

	}

	public void makeBoard(int theme)
	{
		int snakePositionY = 1;
		int snakePositionX = 1;
		int a = 0;
		int b = 0;
		int hallway;
		GameObject currentTile;
		GameObject instance;



		while (snakePositionX < 32 && snakePositionY < 32)
		{
			columns = 0;
			rows = 0;


			while (columns == rows)
			{
				columns = Random.Range (3, 9);
				rows = Random.Range (3, 9);
			}


			for (int x = snakePositionX; x <= snakePositionX + columns; x++)
			{
				for (int y = snakePositionY; y <= snakePositionY + rows; y++)
				{
					switch (theme)
					{
						case 0:
							currentTile = grassTiles [Random.Range (0, grassTiles.Length)];
							instance = Instantiate (currentTile, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
							instance.transform.SetParent (boardHolder);
							floorSpaces.Add (instance);
							break;
						case 1:
							instance = Instantiate (snowFloor, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
							instance.transform.SetParent (boardHolder);
							floorSpaces.Add (instance);
							break;
						case 2:
							instance = Instantiate (dungeonFloor, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
							instance.transform.SetParent (boardHolder);
							floorSpaces.Add (instance);
							break;
						case 3:
							currentTile = lavaTiles [Random.Range (0, lavaTiles.Length)];
							instance = Instantiate (currentTile, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
							instance.transform.SetParent (boardHolder);
							floorSpaces.Add (instance);
							break;
						case 4:
							currentTile = desertTiles [Random.Range (0, desertTiles.Length)];
							instance = Instantiate (currentTile, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
							instance.transform.SetParent (boardHolder);
							floorSpaces.Add (instance);
							break;
						default:
							break;
					}
					RemovePossibilityFromGrid (x, y);

				}

			}

			snakePositionX += columns;
			snakePositionY += rows;

			if (columns > rows)
			{
				a = snakePositionX + 1;
				b = Random.Range (snakePositionY - rows, snakePositionY - 1);
			}
			else
			{
				a = Random.Range (snakePositionX - columns, snakePositionX - 1);
				b = snakePositionY + 1;
			}



			for (hallway = Random.Range (2, 5); hallway >= 0; hallway--)
			{
				switch (theme)
				{
					case 0:
						currentTile = grassTiles [Random.Range (0, grassTiles.Length)];
						instance = Instantiate (currentTile, new Vector3 (a, b, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent (boardHolder);
						break;
					case 1:
						instance = Instantiate (snowFloor, new Vector3 (a, b, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent (boardHolder);
						break;
					case 2:
						instance = Instantiate (dungeonFloor, new Vector3 (a, b, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent (boardHolder);
						break;
					case 3:
						currentTile = lavaTiles [Random.Range (0, lavaTiles.Length)];
						instance = Instantiate (currentTile, new Vector3 (a, b, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent (boardHolder);
						break;
					case 4:
						currentTile = desertTiles [Random.Range (0, desertTiles.Length)];
						instance = Instantiate (currentTile, new Vector3 (a, b, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent (boardHolder);
						break;
					default:
						break;
				}



				RemovePossibilityFromGrid (a, b);
				if (columns > rows)
				{
					a++;
				}
				else
				{
					b++;
				}
			}

			snakePositionX = a;
			snakePositionY = b;
		}


	}

	Vector3 randomPosition()
	{
		int randomIndex = Random.Range (0, playableArea.Count);
		Vector3 randomPosition = playableArea [randomIndex];
		playableArea.RemoveAt (randomIndex);
		return randomPosition;

	}

	void placeObjectAtRandom(GameObject[] objects, int minimum, int maximum)
	{
		Vector3 position;

		int objectCount = Random.Range (minimum, maximum + 1);

		for (int i = 0; i < objectCount; i++)
		{
			position = randomPosition ();
			GameObject tileChoice = objects [Random.Range (0, objects.Length)];
			Instantiate (tileChoice, position, Quaternion.identity);

		}
	}

	void FillEmptySpaceWithWalls(int theme)
	{
		GameObject currentTile;


		for (int x = 0; x < grid.Count; x++)
		{
			Vector3 position = grid [x];

			switch (theme)
			{
				case 0:
					instance = Instantiate (grassWall, position, Quaternion.identity) as GameObject;
					instance.transform.SetParent (boardHolder);
					break;
				case 1:
					instance = Instantiate (iceWall, position, Quaternion.identity) as GameObject;
					instance.transform.SetParent (boardHolder);
					break;
				case 2:
					currentTile = dungeonWalls [Random.Range (0, dungeonWalls.Length)];
					instance = Instantiate (currentTile, position, Quaternion.identity) as GameObject;
					instance.transform.SetParent (boardHolder);
					break;
				case 3:
					currentTile = lavaWall [Random.Range (0, lavaWall.Length)];
					instance = Instantiate (currentTile, position, Quaternion.identity) as GameObject;
					instance.transform.SetParent (boardHolder);
					break;
				case 4:
					instance = Instantiate (desertWall, position, Quaternion.identity) as GameObject;
					instance.transform.SetParent (boardHolder);
					break;
				default:
					break;
			}
		}

		grid.Clear ();
	}

	void RemovePossibilityFromGrid(int x, int y)
	{
		for (int i = 0; i < grid.Count; i++)
		{
			if (grid [i].x == x && grid [i].y == y)
			{
				grid.RemoveAt (i);
				playableArea.Add (new Vector3 (x, y, 0f));
			}
		}
	}

	void InitializeWholeBoard()
	{
		grid.Clear ();

		for (int x = -10; x < 40; x++)
		{
			for (int y = -10; y < 40; y++)
			{
				grid.Add(new Vector3(x, y, 0f));
			}
		}

	}


}
