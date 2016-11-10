using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : Mover 
{
	
	public int enemyHealth;
	public int enemyMaxHealth;
	public int range;
	public int damage;
	public bool boss;


	public Slider healthBar;



	private Animator animator;
	private Transform target;
	private Player player;
	//private bool skipMove;


	protected override void Start()
	{
		Game.instance.addEnemy (this);
		animator = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		target = GameObject.FindGameObjectWithTag ("Player").transform;

		healthBar.value = enemyHealth;
		healthBar.maxValue = enemyMaxHealth;

		//healthBar = Instantiate (healthBar, new Vector3 (0f, 1f, 1f), Quaternion.identity) as GameObject;
		//healthBar.transform.SetParent (this.transform);
		//healthBar.value = enemyMaxHealth;


		base.Start ();

	}
	public bool checkRange()
	{
		Vector2 p = target.position;
		Vector2 e = this.transform.position;
		//Debug.Log ("Player is " + p);
		//Debug.Log ("Enemy is " + e);

		if (p == e + new Vector2 (range, 0) || p == e + new Vector2 (0, range) || p == e - new Vector2 (range, 0) || p == e - new Vector2 (0, range))
		{
			return true;
		}

		return false;
	}

	public void attackPlayer()
	{
		player.loseHealth (damage);
	}

	public void takeDamage(int damage)
	{
		enemyHealth -= damage;

		healthBar.value = enemyHealth;


		if (enemyHealth <= 0)
		{
			enemyHealth = 0;
			Game.instance.removeEnemy (this);
			this.gameObject.SetActive (false);
		}
		else
		{
			float percentage = (float)enemyHealth / (float)enemyMaxHealth;


			if (boss == true)
			{
				this.transform.localScale = new Vector3 (percentage + 1, percentage + 1, 1f);
			}
			else
			{
				this.transform.localScale = new Vector3 (percentage, percentage, 1f);
			}



			healthBar.transform.localScale = new Vector3 (1 / this.transform.localScale.x, 1 / this.transform.localScale.y, 0f);
			healthBar.transform.position = new Vector3 (healthBar.transform.position.x, (float)(healthBar.transform.position.y + 0.2), 0f);
			
			if (this.transform.localScale.x < 0 || this.transform.localScale.y < 0)
			{
				this.transform.localScale = new Vector3 (0f, 0f, 1f);
			}
		
		}
	}

	public void moveEnemy()
	{
		int x = 0;
		int y = 0;
		RaycastHit2D hit;
		bool didMove = false;

		if (checkRange () == true)
		{
			attackPlayer ();
		}
		else
		{
			if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
			{
				y = target.position.y > transform.position.y ? 1 : -1;

				if(Move(x, y, out hit) == false)
				{
					x = target.position.x > transform.position.x ? 1 : -1;
					y = 0;
				}
			
			}
			else
			{
				x = target.position.x > transform.position.x ? 1 : -1;

				if(Move(x, y, out hit) == false)
				{
					y = target.position.y > transform.position.y ? 1 : -1;
					x = 0;
				}
			}

			// PASSES PLAYER AS T VARIABLE IN ORIGINAL CODE
			AttemptMove <Player>(x, y);


		}
	}

	protected override void OnCantMove <T>(T component)
	{
		Player hitPlayer = component as Player;
		//animator.SetTrigger ();
		//SoundManager.instance.RandomizeSfx (enemyAttack1, enemyAttack2);\
		//hitPlayer.loseHealth(playerDamage);

	}

}
