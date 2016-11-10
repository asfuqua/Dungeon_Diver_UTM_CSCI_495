using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : Mover 
{
	public int enemyHealth;
	public int enemyMaxHealth;
	public int range;
	public int damage;

		



	private Animator animator;
	private Transform target;
	private Player player;
		//private bool skipMove;




	protected override void Start()
	{
		Debug.Log ("Boss: Start is being called.");
		animator = GetComponent<Animator> ();

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		target = GameObject.FindGameObjectWithTag ("Player").transform;

		enemyMaxHealth = 777;

		if (enemyHealth == 1000)
		{
			Debug.Log (target);
			Debug.Log (player);
		}
		base.Start ();
	}


	public bool checkRange()
	{
		Vector2 p = target.position;
		Vector2 e = this.transform.position;

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

		if (enemyHealth <= 0)
		{
			enemyHealth = 0;
			
			this.gameObject.SetActive (false);
		}
		else			
		{
			float percentage = (float)enemyHealth / (float)enemyMaxHealth;

			this.transform.localScale = new Vector3 (percentage, percentage, 1f);

			if (this.transform.localScale.x < 0 || this.transform.localScale.y < 0)
			{					
				this.transform.localScale = new Vector3 (0f, 0f, 1f);
			}

		}
	}

	public void moveEnemy()		
	{
		if (enemyHealth == 1000)
		{
			Debug.Log (player);
			Debug.Log (target);
		}

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
		//SoundManager.instance.RandomizeSx (enemyAttack1, enemyAttack2);\
		//hitPlayer.loseHealth(playerDamage);
	}

}