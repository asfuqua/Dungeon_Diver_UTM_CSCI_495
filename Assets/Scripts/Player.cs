using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Mover 
{
	public static Player instance;
	public Image manaBarColor;
	public bool levelTransition;


	// PLAYER STATS
	public int health;
	public int maxHealth;				// The max health of the player
	public int mana;
	public int maxMana;					// the max mana of the player
	public int atk;						// the attack value of the player
	public string equippedWeapon;		// An string code representing the currently equipped weapon
	public int equippedIndex;			// A int variable representing the index of the currently equipped weapon from the inventory array



	// PROJECTILES
	public GameObject projectile;		// Arrow Projectile Prefab
	public Sprite arrow;				// arrow Sprite for projectile prefab
	public Sprite ice;					// ice spike sprite for projectile prefab
	public Sprite fire;					// fireball sprite for projectile prefab

	// ITEM TURN DECAYERS
	public int damageIncrease;			// The value for the number of turns the Damage potion is active
	public int movementIncrease;		// The value for the number of turns the Movement potion is active

	// UI ELEMENTS OF PLAYER
	public Slider healthBar;			// The Ui Element to represent health
	public Slider manaBar;				// the Ui Element to represent mana
	public Text healthText;				// The overlay text for the health bar
	public Text manaText;				// The overlay text for the mana bar
	public Text dmgBuffText;
	public Text moveBuffText;
	public GameObject AmmoPanel;		// The UI Element holding the inventory and their durability values 
	public GameObject gameOverPanel;
	public Image CurrentEquip;			// the image showing the currently equipped weapon
	public Image moveBuff;				
	public Image dmgBuff;
	public Sprite uiMask;
	public Sprite moveSprite;
	public Sprite damageSprite;

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


	// UNITY DRIVING FUNCTIONS
	void Awake()
	{
		//Debug.Log ("Player: Awake is being called!");

		if (instance == null)
		{
			instance = this;
		}
		else if (instance != null)
		{
			Destroy (gameObject);
		}

		Debug.Log ("Player: Awake is being called!");	
		Player.instance.transform.position = new Vector3(3f, 3f, 0f);

		DontDestroyOnLoad (gameObject);
		uiMask = CurrentEquip.sprite;
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
		Debug.Log ("Player: Start is being called.");
		//animator = GetComponent <Animator> ();
		//spriterenderer = GetComponent<SpriteRenderer> ();
		maxHealth = 50;
		maxMana = 10;
		damageIncrease = 0;
		movementIncrease = 0;
		atk = 1;
		equippedWeapon = "Dagger";
		health = 50;
		mana = 10;


		healthBar.maxValue = maxHealth;
		healthBar.value = health;
		manaBar.maxValue = maxMana;
		manaBar.value = mana;
		levelTransition = false;


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

		if (levelTransition == false)
		{
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
				AttemptMove<Enemy> (horizontal, vertical);
			}
		}
	}




	// SELF WRITTEN FUNCTIONS
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

		decreaseBuffs ();

		Game.instance.playersTurn = false;
	}

	public void attack(float cardinal, Enemy target)
	{
		if (cardinal == 0f)
		{
			WeaponAnimation.transform.rotation = Quaternion.Euler (0f, 0f, cardinal);
		}
		else if (cardinal == 90f)
		{
			WeaponAnimation.transform.rotation = Quaternion.Euler (0f, 0f, cardinal);
		}
		else if (cardinal == 180f)
		{
			WeaponAnimation.transform.rotation = Quaternion.Euler (0f, 0f, cardinal);
		}
		else if (cardinal == 270f)
		{
			WeaponAnimation.transform.rotation = Quaternion.Euler (0f, 0f, cardinal);
		}


		switch (equippedWeapon)
		{

			case "Dagger": 
				WeaponAnimation.GetComponent<Animator> ().SetTrigger ("swordSwing");
				calculateDamage (target);
				Game.instance.playersTurn = false;
				break;
			case "Sword": 
				WeaponAnimation.GetComponent<Animator> ().SetTrigger ("swordSwing");
				calculateDamage (target);
				loseDurability();
				Game.instance.playersTurn = false;
				break;
			case "Spear":
				WeaponAnimation.GetComponent<Animator> ().SetTrigger ("spearStab");
				calculateDamage (target);
				loseDurability();
				Game.instance.playersTurn = false;
				break;
			case "Bow":
				WeaponAnimation.GetComponent<Animator> ().SetTrigger ("fireBow");
				makeProjectile (cardinal);
				loseDurability();
				//Game.instance.playersTurn = false;
				break;
			case "Fire":
				if (mana >= 2)
				{
					WeaponAnimation.GetComponent<Animator> ().SetTrigger ("fireCast");
					loseMana (2);
					makeProjectile (cardinal);
					loseDurability ();
					//Game.instance.playersTurn = false;
				}

				break;
			case "Ice":
				if (mana >= 2)
				{
					WeaponAnimation.GetComponent<Animator> ().SetTrigger ("iceCast");
					loseMana (2);
					makeProjectile (cardinal);
					loseDurability();
					//Game.instance.playersTurn = false;
				}
				break;
		}

		decreaseBuffs();

	}

	private void makeProjectile(float cardinal)
	{
		Vector3 position = new Vector3 (0f, 0f, 0f);
		if (damageIncrease > 0)
		{
			projectile.GetComponent<Projectile> ().damage = (atk + Weapons.instance.inventory [equippedIndex].damageModifier) * 2;
		}
		else
		{
			projectile.GetComponent<Projectile> ().damage = atk + Weapons.instance.inventory [equippedIndex].damageModifier;
		}

		Debug.Log ("Damage = " + projectile.GetComponent<Projectile> ().damage);

		if (cardinal == 0)
		{
			projectile.GetComponent<Projectile> ().xDirection = 1;
			projectile.GetComponent<Projectile> ().yDirection = 0;

			position = new Vector3 (this.transform.position.x + 1, this.transform.position.y, 0f);
		}
		else if (cardinal == 90)
		{
			projectile.GetComponent<Projectile> ().xDirection = 0;
			projectile.GetComponent<Projectile> ().yDirection = 1;

			position = new Vector3 (this.transform.position.x, this.transform.position.y + 1, 0f);
		}
		else if (cardinal == 180)
		{
			projectile.GetComponent<Projectile> ().xDirection = -1;
			projectile.GetComponent<Projectile> ().yDirection = 0;

			position = new Vector3 (this.transform.position.x - 1, this.transform.position.y, 0f);
		}
		else if (cardinal == 270)
		{
			projectile.GetComponent<Projectile> ().xDirection = 0;
			projectile.GetComponent<Projectile> ().yDirection = -1;

			position = new Vector3 (this.transform.position.x, this.transform.position.y - 1, 0f);
		}


		switch (equippedWeapon)
		{
			case "Bow":
				projectile.GetComponent<SpriteRenderer> ().sprite = arrow;
				Instantiate(projectile, position, Quaternion.Euler(0f, 0f, cardinal));
				break;
			case "Fire":
				projectile.GetComponent<SpriteRenderer> ().sprite = fire;
				Instantiate(projectile, position, Quaternion.Euler(0f, 0f, cardinal));
				break;
			case "Ice":
				projectile.GetComponent<SpriteRenderer> ().sprite = ice;
				Instantiate(projectile, position, Quaternion.Euler(0f, 0f, cardinal));
				break;
		}

		//Game.instance.playersTurn = false;

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
				moveBuff.sprite = moveSprite;
				moveBuffText.text =	movementIncrease.ToString();
				other.gameObject.SetActive(false);
				break;
			case "Damage Potion":
				Potion.instance.damagePotion ();
				dmgBuff.sprite = damageSprite;
				dmgBuffText.text = damageIncrease.ToString();
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
				levelTransition = true;
				Invoke("Restart", restartLevelDelay);

				break;
			default:
				break;

		}
	}

	public void gainHealth(int gain)
	{
		healthBar.value += gain;
		health = (int)healthBar.value;
		updateBars ();
	}

	public void loseHealth(int loss)
	{
		healthBar.value -= loss;
		health = (int)healthBar.value;
		updateBars ();
		if (health == 0)
		{
			Game.instance.GameOver ();
		}
	}

	public void gainMana(int gain)
	{
		manaBar.value += gain;
		mana = (int)manaBar.value;
		updateBars ();
	}

	public void loseMana(int loss)
	{
		manaBar.value -= loss;
		mana = (int)manaBar.value;
		updateBars ();
	}

	private void loseDurability()
	{
		Weapons.instance.inventory [equippedIndex].durability--;
		Weapons.instance.checkToRemove (equippedIndex);
		Weapons.instance.updateInventory ();
	}

	private void calculateDamage(Enemy target)
	{
		int temp;

		if (equippedWeapon == "Dagger")
		{
			if (damageIncrease > 0)
			{
				target.takeDamage (atk * 2);
			}
			else
			{
				target.takeDamage (atk);
			}
		}
		else
		{
			if (damageIncrease > 0)
			{
				target.takeDamage ((atk + Weapons.instance.inventory [equippedIndex].damageModifier) * 2);
				temp = (atk + Weapons.instance.inventory [equippedIndex].damageModifier) * 2;
				Debug.Log("Damage " + temp);
			}
			else
			{
				target.takeDamage (atk + Weapons.instance.inventory [equippedIndex].damageModifier);
				temp = atk + Weapons.instance.inventory [equippedIndex].damageModifier;
				Debug.Log("Damage: " + temp);
			}

		}
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

	private void decreaseBuffs()
	{
		if (damageIncrease > 0)
		{
			damageIncrease--;

			dmgBuffText.text = damageIncrease.ToString();

			if (damageIncrease == 0)
			{
				dmgBuff.sprite = uiMask;
				dmgBuffText.text = " ";
			}
		}
		if (movementIncrease > 0)
		{
			movementIncrease--;

			moveBuffText.text = movementIncrease.ToString();

			if (movementIncrease == 0)
			{
				moveBuff.sprite = uiMask;
				moveBuffText.text = " ";
			}
		}
	}

	protected override void OnCantMove<T>(T Component)
	{
		Enemy hitEnemy = Component as Enemy;
		WeaponAnimation.GetComponent<Animator>().SetTrigger ("swordSwing");
		hitEnemy.takeDamage (atk);

	}

	protected void Restart()
	{
		SceneManager.LoadScene("Main", LoadSceneMode.Single);
		levelTransition = false;
	}
		
	public void getIntoAttemptMove(int x, int y)
	{
		AttemptMove<Enemy> (x, y);
	}
}
