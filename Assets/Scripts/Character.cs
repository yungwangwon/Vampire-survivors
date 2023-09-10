using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public static float Speed
	{
		get { return GameManager.instance.playerId == 0 ? 1.1f : 1.0f; }
	}

	public static float WeaponSpeed
	{
		get { return GameManager.instance.playerId == 0 ? 1.1f : 1.0f; }
	}
	public static float WeaponRate
	{
		get { return GameManager.instance.playerId == 0 ? 0.9f : 1.0f; }
	}
	public static float Damge
	{
		get { return GameManager.instance.playerId == 0 ? 1.1f : 1.0f; }
	}

	public static int Count
	{
		get { return GameManager.instance.playerId == 0 || GameManager.instance.playerId == 2 ? 1 : 0; }
	}
}
