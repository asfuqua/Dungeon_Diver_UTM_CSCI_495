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





	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != null)
		{
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}



	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		inventory = Game.instance.inventory;
		updateInventory ();
	}

	void OnDisable()
	{
		Game.instance.inventory = inventory;
	}


	public void sword()
	{
		invSlot = new Slot (swordSprite, "Sword", 10);
		addSlot (invSlot);
		updateInventory ();
	}

	public void spear()
	{
		invSlot = new Slot (spearSprite, "Spear", 10);
		addSlot (invSlot);
		updateInventory ();
	}

	public void bow()
	{
		invSlot = new Slot (bowSprite, "Bow", 10);
		addSlot (invSlot);
		updateInventory ();
	}

	public void arrowBundle()
	{
		invSlot = new Slot (arrowSprite, "Arrow", 10);
		addSlot (invSlot);
		updateInventory ();
	}

	public void fireSpell()
	{
		invSlot = new Slot (fireSprite, "Fire", 10);
		addSlot (invSlot);
		updateInventory ();
	}

	public void iceSpell()
	{
		invSlot = new Slot (iceSprite, "Ice", 10);
		addSlot (invSlot);
		updateInventory ();
	}

	public bool addSlot(Slot passed)
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory [i].name == passed.name)
			{
				inventory [i].durability += passed.durability;
				return true;
			}
		}
			
		inventory.Add(invSlot);
		return false;
	}


	public void updateInventory()
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			inventoryTexts [i].text = inventory [i].name + " : " + inventory [i].durability.ToString().PadRight(3);
			inventoryButtons [i].interactable = true;
			inventoryButtons [i].image.sprite = inventory [i].sprite;
		}
	}


	public void equip(int index)
	{
		Debug.Log (index);

		Player.instance.equippedWeapon = inventory[index].name;
		Player.instance.CurrentEquip.sprite = inventory [index].sprite;
	}
}
