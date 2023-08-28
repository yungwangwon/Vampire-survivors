using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public Player player;
	public PoolManager poolmanager;

	private void Awake()
	{
		instance = this;
	}
}
