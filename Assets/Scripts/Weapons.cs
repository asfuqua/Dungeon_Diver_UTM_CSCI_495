using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{

	public Button[] inventoryButtons;
	public Text[] inventoryTexts;
	public List<Slot> inventory;
	private Slot invSlot;

	public Sprite swordSprite;
	public Sprite spearSprite;
	public Sprite bowSprite;
	public Sprite fireSprite;
	public Sprite iceSprite;
	public Sprite arrowSprite;



	private Player player;
	public static Weapons instance;






	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		instance = this;

		inventory = new List<Slot> ();
	}


	public void sword()
	{
		invSlot = new Slot (swordSprite, "Sword", 10);
		

		inventory.Add(invSlot);
		//Debug.Log(inventory[
		updateInventory ();
		//player.hasSword += 10;
		//player.updateAmmo ();
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





	public void updateInventory()
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			inventoryTexts [i].text = inventory [i].name + " " + inventory [i].durability;
		}
	}


}
