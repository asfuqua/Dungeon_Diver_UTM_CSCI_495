using UnityEngine;
using System.Collections;

public class Enemy : Mover 
{
	public int playerDamage;
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
		range = 1;
		//damage = 5;
		enemyHealth = 10;
		enemyMaxHealth = 10;
		Game.instance.addEnemy (this);
		animator = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		base.Start ();

	}

	/*protected override void AttemptMove(int x, int y)
	{
		base.AttemptMove (x, y);
	}
	*/
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
		Debug.Log ("Enemy Hit!");
		enemyHealth -= damage;
		float percentage = (float)enemyHealth / (float)enemyMaxHealth;

		this.transform.localScale = new Vector3 (percentage, percentage, 1f);
		if (this.transform.localScale.x < 0 || this.transform.localScale.y < 0)
		{
			this.transform.localScale = new Vector3 (0f, 0f, 1f);
		}

		if (this.transform.localScale == new Vector3 (0f, 0f, 1f))
		{
			this.gameObject.SetActive (false);
			Game.instance.removeEnemy (this);
		}
	
	}

	public void moveEnemy()
	{
		int x = 0;
		int y = 0;


		if (checkRange () == true)
		{
			attackPlayer ();
		}
		else
		{
			if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
			{
				y = target.position.y > transform.position.y ? 1 : -1;
			}
			else
			{
				x = target.position.x > transform.position.x ? 1 : -1;
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
