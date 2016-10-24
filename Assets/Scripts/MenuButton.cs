using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuButton : MonoBehaviour 
{
	private Player player;
	public LayerMask blockingLayer;
	private bool moveOn;
	private bool attackOn;

	private int x;
	private int y;



	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		moveOn = false;
		attackOn = false;

	
	}

	void Update()
	{
		if (moveOn == true)
		{
			if (Input.GetButtonDown ("Fire1"))
			{
				Ray click = Camera.main.ScreenPointToRay(Input.mousePosition);
				click.origin = new Vector3 (Mathf.Round (click.origin.x), Mathf.Round (click.origin.y), 0f);

				int x = (int)click.origin.x - (int)player.transform.position.x;
				int y = (int)click.origin.y - (int)player.transform.position.y;


				if (y == 0)
				{
					x = click.origin.x > player.transform.position.x ? 1 : -1;
				}
				else if (x == 0)
				{
					y = click.origin.y > player.transform.position.y ? 1 : -1;
				}

				if (x == 0 || y == 0)
				{
					player.getIntoAttemptMove (x, y);
				}
			}
		}
	
		if (attackOn == true)
		{

			/* 1,0 is right
			 * 0,1 is up
			 * -1, 0 is left
			 * 0, -1 is down
			 */
		

			if (Input.GetButtonDown ("Fire1"))
			{
				Enemy target = null;
				int x;
				int y;
			
				Ray click = Camera.main.ScreenPointToRay(Input.mousePosition);
				click.origin = new Vector3 (Mathf.Round (click.origin.x), Mathf.Round (click.origin.y), 0f);


				for (int i = 0; i < Game.instance.enemies.Count; i++)
				{
					if (Game.instance.enemies [i].transform.position == click.origin)
					{
						target = Game.instance.enemies [i];
					}
				}

				if (target != null)
				{

					x = (int)click.origin.x - (int)player.transform.position.x;
					y = (int)click.origin.y - (int)player.transform.position.y;

					if (y == 0)
					{
						x = click.origin.x > player.transform.position.x ? 1 : -1;
					}
					else if (x == 0)
					{
						y = click.origin.y > player.transform.position.y ? 1 : -1;
					}
						
					if (x == 0)
					{
						if (y > 0)
						{
							Player.instance.attack (90f, target);
						}
						else
						{
							Player.instance.attack (270f, target);
						}
					}
					else if (y == 0)
					{
						if (x > 0)
						{
							Player.instance.attack (0f, target);
						}
						else
						{
							Player.instance.attack (180f, target);
						}
					}

				}
			}
		}
	}

	public void move()
	{
		attackOn = false;
		moveOn = true;
	}

	public void attack()
	{
		moveOn = false;
		attackOn = true;
		Debug.Log ("Attack on!");
	}



	public void openSettings()
	{
		Debug.Log ("Settings opened!");
	}

}
