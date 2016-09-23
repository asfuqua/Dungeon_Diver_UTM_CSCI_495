using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : Mover 
{

	// PLAYER STATS
	//public int health;
	public int maxHealth;
	//public int mana;
	public int maxMana;


	// ITEM TURN DECAYERS
	public int damageIncrease;
	public int movementIncrease;
	public int hasSword;
	public int hasSpear;
	public int hasBow;
	public int arrows;
	public int hasFireSpell;
	public int hasIceSpell;

	// UI ELEMENTS OF PLAYER
	public Slider healthBar;
	public Slider manaBar;
	public Text healthText;
	public Text manaText;

	public Button menuButton;
	public Button movementButton;
	public Button attackButton;
	public Button inventoryButton;


	// VARIABLES USED IN MOVEMENT INDICATORS
	//public Vector3 playerPosition;
	public Vector3 floorSpace;
	public float restartLevelDelay = 1f;
	private List<GameObject> movementSpaces = new List<GameObject>();


	// COMPONENT ACCESSORS
	public GameObject WeaponAnimation;
	private Transform player;
	private Animator animator;
	private SpriteRenderer spriterenderer;


	protected override void Start()
	{
		animator = GetComponent <Animator> ();
		spriterenderer = GetComponent<SpriteRenderer> ();
		maxHealth = 10;
		maxMana = 10;
		damageIncrease = 0;
		movementIncrease = 0;
		hasSword = 0;
		hasSpear = 0;
		hasBow = 0;
		arrows = 0;
		hasFireSpell = 0;
		hasIceSpell = 0;


		healthBar.maxValue = maxHealth;
		healthBar.value = (int)(maxHealth * 1);
		manaBar.maxValue = maxMana;
		manaBar.value = (int)(maxMana * 1);
		healthText.text = healthBar.value + " / " + maxHealth;
		manaText.text = manaBar.value + " / " + maxMana;



		/*
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		playerPosition.x = player.position.x;
		playerPosition.y = player.position.y;


		for (int i = 0; i < Game.instance.boardScript.floorSpaces.Count; i++)
		{
			floorSpace.x = Game.instance.boardScript.floorSpaces [i].transform.position.x;
			floorSpace.y = Game.instance.boardScript.floorSpaces [i].transform.position.y;

			if (
				floorSpace == playerPosition + new Vector3(0,1,0f) ||
				floorSpace == playerPosition + new Vector3(1,1,0f) ||
				floorSpace == playerPosition + new Vector3(1,0,0f) ||
				floorSpace == playerPosition + new Vector3(-1,1,0f) ||
				floorSpace == playerPosition + new Vector3(1,-1,0f) ||
				floorSpace == playerPosition + new Vector3(-1,0,0f) ||
				floorSpace == playerPosition + new Vector3(0,-1,0f) ||
				floorSpace == playerPosition + new Vector3(-1, -1, 0f)
			)
			{
				GameObject instance = Instantiate (Game.instance.boardScript.movementIndicator, floorSpace, Quaternion.identity) as GameObject;
				movementSpaces.Add(instance);
			}
		}


		*/



		base.Start ();

	}

	void Update()
	{
		if(!Game.instance.playersTurn)
		{
			return;
		}
			

		/*if (movementSpaces.Count != 0)
		{
			for (int i = 0; i < movementSpaces.Count; i++)
			{
				movementSpaces [i].

			}


		}*/



		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)Input.GetAxisRaw ("Horizontal");
		vertical = (int)Input.GetAxisRaw ("Vertical");

		if (horizontal != 0)
		{
			vertical = 0;
		}
			
		if (horizontal != 0 || vertical != 0)
		{
			AttemptMove<Enemy>(horizontal, vertical);
		}



	}



	protected override void AttemptMove<T>(int x, int y)
	{
		base.AttemptMove<T>(x, y);

		RaycastHit2D hit;
		/*if (Move (x, y, out hit))
		{
			SoundManager.instance.RandomizeSfx (moveSound1, moveSound2);
		}
		*/

		//CheckIfGameOver ();

		if (damageIncrease > 0)
		{
			damageIncrease--;
		}
		if (movementIncrease > 0)
		{
			movementIncrease--;
		}


		Game.instance.playersTurn = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{

		/*if (other.tag == "Health Potion")
		{
			Potion.instance.healingPotion ();
			other.gameObject.SetActive(false);
		}*/


		switch (other.tag)
		{
			case "Health Potion": 
				Potion.instance.healingPotion ();
				other.gameObject.SetActive(false);
				break;
			case "Mana Potion":
				Potion.instance.manaPotion ();
				other.gameObject.SetActive(false);
				break;
			case "Speed Potion":
				Potion.instance.speedPotion ();
				other.gameObject.SetActive(false);
				break;
			case "Damage Potion":
				Potion.instance.damagePotion ();
				other.gameObject.SetActive(false);
				break;
			case "Sword":
				Weapons.instance.sword ();
				other.gameObject.SetActive (false);
				break;
			case "Spear":
				Weapons.instance.spear ();
				other.gameObject.SetActive (false);
				break;
			case "Bow":
				Weapons.instance.bow ();
				other.gameObject.SetActive (false);
				break;
			case "Arrow Bundle":
				Weapons.instance.arrowBundle ();
				other.gameObject.SetActive (false);
				break;
			case "Fire Spell":
				Weapons.instance.fireSpell ();
				other.gameObject.SetActive (false);
				break;
			case "Ice Spell":
				Weapons.instance.iceSpell ();
				other.gameObject.SetActive (false);
				break;

			default:
				break;

		}


	}

	protected void Restart()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void gainHealth(int gain)
	{
		healthBar.value += gain;
		updateText ();
	}

	public void loseHealth(int loss)
	{
		healthBar.value -= loss;
		updateText ();
	}

	public void gainMana(int gain)
	{
		manaBar.value += gain;
		updateText ();
	}

	public void loseMana(int loss)
	{
		manaBar.value -= loss;
		updateText ();
	}

	public void updateText()
	{
		healthText.text = healthBar.value + " / " + maxHealth;
		manaText.text = manaBar.value + " / " + maxMana;
	}


	private void CheckIfGameOver()
	{
		/*if (health <= 0)
		{
			//Game.instance.GameOver ();
		}
		*/
	}


	protected override void OnCantMove<T>(T component)
	{
		Debug.Log("OnCantMove being called.");
	}
}
