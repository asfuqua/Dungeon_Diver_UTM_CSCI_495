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


	private Sprite uiMask;
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
		uiMask = inventoryButtons [0].image.sprite;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		inventory = new List<Slot> ();
		updateInventory ();
	}

	public void sword()
	{
		invSlot = new Slot (swordSprite, "Sword", 10, 2);
		addSlot (invSlot);
		updateInventory ();
	}

	public void spear()
	{
		invSlot = new Slot (spearSprite, "Spear", 10, 3);
		addSlot (invSlot);
		updateInventory ();
	}

	public void bow()
	{
		invSlot = new Slot (bowSprite, "Bow", 10, 2);
		addSlot (invSlot);
		updateInventory ();
	}

	public void arrowBundle()
	{
		invSlot = new Slot (arrowSprite, "Arrow", 10, 0);
		addSlot (invSlot);
		updateInventory ();
	}

	public void fireSpell()
	{
		invSlot = new Slot (fireSprite, "Fire", 10, 3);
		addSlot (invSlot);
		updateInventory ();
	}

	public void iceSpell()
	{
		invSlot = new Slot (iceSprite, "Ice", 10, 4);
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

	public void checkToRemove(int index)
	{
		if (inventory [index].durability == 0)
		{
			inventory.Remove (inventory [index]);
			Player.instance.CurrentEquip.sprite = uiMask;
			Player.instance.equippedWeapon = "Dagger";
		}
	}


	public void updateInventory()
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			inventoryTexts [i].text = inventory [i].name + " : " + inventory [i].durability.ToString().PadRight(3);
			inventoryButtons [i].interactable = true;
			inventoryButtons [i].image.sprite = inventory [i].sprite;
		}

		for (int i = inventory.Count; i < 6; i++)
		{
			inventoryTexts [i].text = "";
			inventoryButtons [i].interactable = false;
			inventoryButtons [i].image.sprite = uiMask;
		}
	}


	public void equip(int index)
	{
		Debug.Log (index);

		Player.instance.equippedIndex = index;
		Player.instance.equippedWeapon = inventory[index].name;
		Player.instance.CurrentEquip.sprite = inventory [index].sprite;
	}
}
