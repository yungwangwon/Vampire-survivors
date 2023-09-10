using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[Header("# Game Object")]
	public Player player;
	public PoolManager poolmanager;
	public LevelUp levelUp;
	public Result uiResult;
	public GameObject enemyCleaner;
	public Transform joyStick;

	[Header("# Game Control")]
	public float gameTime;
	public float maxGameTime = 2 * 10f;
	// 게임 활성, 비활성 구분 변수
	public bool isEnable;

	[Header("# Player Info")]
	public int playerId;
	public float hp;
	public float maxHp;
	public int level;
	public int kill;
	public int exp;
	public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

	private void Awake()
	{
		instance = this;
		// 프레임 설정
		Application.targetFrameRate = 60;
	}

	#region [게임 제어]
	public void GameStart(int id)
	{
		playerId = id;
		maxHp = 100;
		hp = maxHp;
		player.gameObject.SetActive(true);	
		// Test Code
		levelUp.Select(playerId % 2);
		isEnable = true;
		Resume();

		AudioManager.instance.BgmPlay(true);
		AudioManager.instance.SfxPlay(AudioManager.Sfx.Select);

	}

	// 게임 종료
	public void GameOver()
	{
		StartCoroutine("GameOverRoutine");
	}

	IEnumerator GameOverRoutine()
	{
		isEnable = false;
		enemyCleaner.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		uiResult.gameObject.SetActive(true);
		uiResult.Lose();
		Stop();

		AudioManager.instance.BgmPlay(false);
		AudioManager.instance.SfxPlay(AudioManager.Sfx.Lose);

	}

	public void GameVictory()
	{
		StartCoroutine("GameVictoryRoutine");
	}

	IEnumerator GameVictoryRoutine()
	{
		isEnable = false;
		enemyCleaner.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		uiResult.gameObject.SetActive(true);
		uiResult.Win();
		Stop();

		AudioManager.instance.SfxPlay(AudioManager.Sfx.Win);

	}

	public void GameReStart()
	{
		SceneManager.LoadScene(0);
	}

	public void GameQuit()
	{
		Application.Quit();
	}
	#endregion [게임 제어]

	private void Update()
	{
		if (!isEnable)
			return;

		gameTime += Time.deltaTime;

		if (gameTime > maxGameTime)
		{
			gameTime = maxGameTime;
			GameVictory();
		}
	}

	// 경험치 획득
	public void GetExp()
	{
		if (!isEnable)
			return;

		exp++;

		if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
		{
			level++;
			exp = 0;
			levelUp.Show();
		}
	}

	public void Stop()
	{
		isEnable = false;
		// 유니티에서 제공하는 Time Class Property 시간흐름 조절기능
		Time.timeScale = 0;
		joyStick.localScale = Vector3.zero;
	}

	public void Resume()
	{
		isEnable = true;
		// 유니티에서 제공하는 Time Class Property 시간흐름 조절기능
		Time.timeScale = 1;
		joyStick.localScale = Vector3.one;

	}


}
