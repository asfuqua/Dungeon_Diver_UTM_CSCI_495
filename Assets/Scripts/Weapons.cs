using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapons : MonoBehaviour
{


	private Player player;
	public static Weapons instance;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		instance = this;
	}


	public void sword()
	{
		player.hasSword += 10;
		player.updateAmmo ();
	}

	public void spear()
	{
		player.hasSpear += 10;
		player.updateAmmo ();
	}

	public void bow()
	{
		player.hasBow += 10;
		player.updateAmmo ();
	}

	public void arrowBundle()
	{
		player.arrows += Random.Range (3, 10);
		player.updateAmmo ();
	}

	public void fireSpell()
	{
		player.hasFireSpell += 10;
		player.updateAmmo ();
	}

	public void iceSpell()
	{
		player.hasIceSpell += 10;
		player.updateAmmo ();
	}





}
