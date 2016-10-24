using UnityEngine;
using System.Collections;

public class Projectile : Mover 
{
	public int damage;
	public int xDirection;
	public int yDirection;



	void Update()
	{
		shoot (xDirection, yDirection);
	}



	public void shoot(int x, int y)
	{
		//Debug.Log ("Fired!");
		AttemptMove<Player> (x, y);
	}



	private void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "Player":
				Debug.Log ("Hit a Player!");
				Player p = other.gameObject.GetComponent<Player> ();
				p.loseHealth (damage);
				this.gameObject.SetActive (false);
				break;

			case "Enemy":
				Enemy e = other.gameObject.GetComponent<Enemy> ();
				e.takeDamage (damage);
				this.gameObject.SetActive (false);
				break;

			case "Wall":
				this.gameObject.SetActive (false);
				break;

		}
	}

	protected override void OnCantMove<T>(T Component)
	{


	}

}
