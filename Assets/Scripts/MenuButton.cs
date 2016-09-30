using UnityEngine;
using System.Collections;

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
				Debug.Log ("Mouse Worked");
				Debug.Log (Input.mousePosition);
				//RaycastHit2D click = Physics2D.Linecast (Input.mousePosition - new Vector3(0, 1, 0), Input.mousePosition + new Vector3(0, 1, 0), blockingLayer);
				//Debug.Log (click.transform);
				Ray click = Camera.main.ScreenPointToRay(Input.mousePosition);
				click.origin = new Vector3 (Mathf.Round (click.origin.x), Mathf.Round (click.origin.y), 0f);
				//click.origin.y = Mathf.Round (click.origin.y);
				Debug.Log (click);
				moveOn = false;
			}
		}


		if (attackOn == true)
		{

		}
	}

	public void move()
	{
		//Debug.Log ("Move!");
		moveOn = true;
	}

	public void attack()
	{
		//Debug.Log ("Attack!");
		attackOn = true;
	}

	public void openInventory()
	{
		Debug.Log ("Inventory Opened!");
	}

	public void openSettings()
	{
		Debug.Log ("Settings opened!");
	}


}
