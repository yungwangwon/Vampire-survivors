using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public Player player;
	public PoolManager poolmanager;

	public float gameTime;
	public float maxGameTime = 2 * 10f;

	private void Awake()
	{
		instance = this;
	}

	private void Update()
	{
		gameTime += Time.deltaTime;

		if (gameTime > maxGameTime)
		{
			gameTime = maxGameTime;
		}
	}
}
