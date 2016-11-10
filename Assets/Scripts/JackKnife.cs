using UnityEngine;
using System.Collections;

public class JackKnife : MonoBehaviour
{
	//public static JackKnife instance;

	public GameObject[] dungeonWalls;
	public GameObject dungeonFloor;

	public GameObject jackKnife;

	void Awake ()
	{
		
	}

	void Start () 
	{
		
	}
	
	void Update () 
	{
		//jackKnife.GetComponent<Boss>().moveEnemy();

	}


	public void makeBoard()
	{
		for (int x = 0; x < 12; x++)
		{
			for (int y = 0; y < 10; y++)
			{
				if (x == 0 || x == 11 || y == 0 || y == 9 || x == 9)
				{
					Instantiate (dungeonWalls [Random.Range (0, dungeonWalls.Length)], new Vector3 (x, y, 0f), Quaternion.identity);
				}
				else
				{
					Instantiate (dungeonFloor, new Vector3 (x, y, 0f), Quaternion.identity);
				}
			}

		}

		jackKnife = Instantiate (jackKnife, new Vector3 (10, 5, 0f), Quaternion.identity) as GameObject;

	}

	public void makeProjectile()
	{

	}
}
