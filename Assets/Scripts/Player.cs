using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Mover 
{
	public static Player instance;
	public Projectile arrow;

	// PLAYER STATS
	public int health;
	public int maxHealth;				// The max health of the player
	public int mana;
	public int maxMana;					// the max mana of the player
	public int atk;						// the attack value of the player
	public string equippedWeapon;		// An string code representing the currently equipped weapon

	// ITEM TURN DECAYERS
	public int damageIncrease;			// The value for the number of turns the Damage potion is active
	public int movementIncrease;		// The value for the number of turns the Movement potion is active

	// UI ELEMENTS OF PLAYER
	public Slider healthBar;			// The Ui Element to represent health
	public Slider manaBar;				// the Ui Element to represent mana
	public Text healthText;				// The overlay text for the health bar
	public Text manaText;				// The overlay text for the mana bar
	public GameObject AmmoPanel;		// The UI Element holding the inventory and their durability values 
	public Image CurrentEquip;			// the image showing the currently equipped weapon


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



	void Awake()
	{
		Debug.Log ("Awake is being called!");

		if (instance == null)
		{
			instance = this;
		}
		else if (instance != null)
		{
			Destroy (gameObject);
		}
			
		DontDestroyOnLoad (gameObject);

		/*healthBar = FindObjectOfType<UnityEngine.UI.Slider> ("Health Bar");
		manaBar = GameObject.FindGameObjectWithTag ("Mana Bar");			
		healthText = GameObject.FindGameObjectWithTag ("Health Text");		
		manaText = GameObject.FindGameObjectWithTag ("Mana Text");
		AmmoPanel = GameObject.FindGameObjectWithTag ("AmmoPanel");
		CurrentEquip = GameObject.FindGameObjectWithTag ("CurrentEquip");
*/
		//updateBars ();
	}


	protected override void Start()
	{
		arrow.shoot ();
		Debug.Log ("Start is being called!");

		if (Game.instance.firstLevel == true)
		{
			Debug.Log ("First Level Happened");
			animator = GetComponent <Animator> ();
			spriterenderer = GetComponent<SpriteRenderer> ();
			maxHealth = 10;
			maxMana = 10;
			damageIncrease = 0;
			movementIncrease = 0;
			atk = 4;
			equippedWeapon = "null";
			health = 10;
			mana = 10;


			healthBar.maxValue = maxHealth;
			healthBar.value = health;
			manaBar.maxValue = maxMana;
			manaBar.value = mana;

			//updateBars ();
		}


		updateBars ();
		
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
			case "Exit":
				Invoke("Restart", restartLevelDelay);
				break;
			default:
				break;

		}
	}

	protected void Restart()
	{
		//Application.LoadLevel (Application.loadedLevel);
		SceneManager.LoadScene("Main", LoadSceneMode.Single);
	}

	public void gainHealth(int gain)
	{
		healthBar.value += gain;
		updateBars ();
	}

	public void loseHealth(int loss)
	{
		healthBar.value -= loss;
		health = (int)healthBar.value;
		updateBars ();
	}

	public void gainMana(int gain)
	{
		manaBar.value += gain;
		updateBars ();
	}

	public void loseMana(int loss)
	{
		manaBar.value -= loss;
		updateBars ();
	}

	public void updateBars()
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



	protected override void OnCantMove<T>(T Component)
	{
		Enemy hitEnemy = Component as Enemy;
		WeaponAnimation.GetComponent<Animator>().SetTrigger ("swordSwing");
		hitEnemy.takeDamage (atk);

	}

	void OnDisable()
	{
		/*health = (int)healthBar.value;

		mana = (int)manaBar.value;

		Debug.Log (health + ", " + mana); */

		Game.instance.firstLevel = false;


	}

	public void getIntoAttemptMove(int x, int y)
	{
		AttemptMove<Enemy> (x, y);
	}
}
