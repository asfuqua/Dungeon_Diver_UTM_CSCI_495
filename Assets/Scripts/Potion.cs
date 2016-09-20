using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour 
{
	private Player player;
	public static Potion instance;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		instance = this;
	}


	public void healingPotion()
	{
		player.gainHealth ((int)(player.maxHealth * 0.1));
	}

	public void manaPotion()
	{
		player.gainMana ((int)(player.maxMana * 0.1));
	}

	public void speedPotion()
	{
		Debug.Log ("Damage Increased!");
		player.movementIncrease += 10;
	}

	public void damagePotion()
	{
		Debug.Log ("Movement Increased!");
		player.damageIncrease += 10;
	}



}
