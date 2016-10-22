using UnityEngine;
using System.Collections;

public class Canvas : MonoBehaviour 
{

	public static Canvas instance;


	void Start () 
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

}
