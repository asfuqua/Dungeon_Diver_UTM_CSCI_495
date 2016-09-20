using UnityEngine;
using System.Collections;

public abstract class Mover : MonoBehaviour 
{
	public LayerMask blockingLayer;
	public float moveTime = 0.1f;

	private Rigidbody2D orb;
	private BoxCollider2D obc;
	private float inverseMoveTime;

	protected virtual void Start()
	{
		obc = GetComponent<BoxCollider2D> ();
		orb = GetComponent<Rigidbody2D> ();
		inverseMoveTime = 1f / moveTime;
	}

	protected bool Move (int x, int y, out RaycastHit2D hit)
	{
		Vector2 startPoint = transform.position;
		Vector2 endPoint = startPoint + new Vector2 (x, y);

		obc.enabled = false;
		hit = Physics2D.Linecast (startPoint, endPoint, blockingLayer);
		obc.enabled = true;

		if (hit.transform == null)
		{
			
			StartCoroutine(SmoothMovement (endPoint));
			return true;
		}

		return false;
	}

	protected IEnumerator SmoothMovement(Vector3 endPoint)
	{
		
		//float remainingDistance = (transform.position - endPoint).sqrMagnitude;

		while (transform.position.x != endPoint.x || transform.position.y != endPoint.y)
		{
			
			Vector3 newPosition = Vector3.MoveTowards (orb.position, endPoint, inverseMoveTime * Time.deltaTime);
			orb.MovePosition (newPosition);
			//remainingDistance = (transform.position - endPoint).sqrMagnitude;
			yield return null;

		}
	}

	protected virtual void AttemptMove<T>(int x, int y)
		where T: Component
	{
		RaycastHit2D hit;
		bool canMove = Move (x, y, out hit);

		if (hit.transform == null)
		{
			return;
		}

		T hitComponent = hit.transform.GetComponent<T> ();

		if (!canMove && hitComponent != null)
		{
			OnCantMove (hitComponent);
		}


	}

	protected abstract void OnCantMove<T> (T component)
		where T : Component;


}
