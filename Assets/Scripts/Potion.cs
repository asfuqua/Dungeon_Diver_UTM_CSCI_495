using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour 
{
	public static Potion instance;

	void Start()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != null)
		{
			Destroy (gameObject);
		}
	}


	public void healingPotion()
	{
		Player.instance.gainHealth ((int)(Player.instance.maxHealth * 0.1));
	}

	public void manaPotion()
	{
		Player.instance.gainMana ((int)(Player.instance.maxMana * 0.1));
	}

	public void speedPotion()
	{
		Player.instance.movementIncrease += 20;
	}

	public void damagePotion()
	{
		Player.instance.damageIncrease += 20;
	}



}
