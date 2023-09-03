using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[Header("# Game Object")]
	public Player player;
	public PoolManager poolmanager;

	[Header("# Game Control")]
	public float gameTime;
	public float maxGameTime = 2 * 10f;

	[Header("# Player Info")]
	public int hp;
	public int maxHp;
	public int level;
	public int kill;
	public int exp;
	public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		maxHp = 100;
		hp = maxHp;
	}

	private void Update()
	{
		gameTime += Time.deltaTime;

		if (gameTime > maxGameTime)
		{
			gameTime = maxGameTime;
		}
	}

	// °æÇèÄ¡ È¹µæ
	public void GetExp()
	{
		exp++;

		if (exp == nextExp[level])
		{
			level++;
			exp = 0;
		}
	}

}
