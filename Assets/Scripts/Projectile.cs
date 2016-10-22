using UnityEngine;
using System.Collections;

public class Projectile : Mover 
{

	void Update()
	{
		shoot ();
	}



	public void shoot()
	{
		Debug.Log ("Fired!");
		AttemptMove<Player> (1, 0);
	}





	protected override void OnCantMove<T>(T Component)
	{


	}

}
