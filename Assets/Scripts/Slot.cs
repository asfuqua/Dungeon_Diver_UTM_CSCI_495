using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slot
{
	public Sprite sprite;
	public string name;
	public int durability;
	public int damageModifier;

	public Slot(Sprite s, string n, int d, int m)
	{
		sprite = s;
		name = n;
		durability = d;
		damageModifier = m;
	}





}
