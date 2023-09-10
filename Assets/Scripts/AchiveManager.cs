using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
	public GameObject[] lockChar;
	public GameObject[] unlockChar;
	public GameObject uiNotice;

	WaitForSecondsRealtime wait;

	enum Achive
	{
		UnlockPotato,
		UnlockBean
	}

	Achive[] achives;

	private void Awake()
	{
		achives = (Achive[])Enum.GetValues(typeof(Achive));
		wait = new WaitForSecondsRealtime(5.0f);
		if (!PlayerPrefs.HasKey("MyData"))
		{
			Init();
		}
	}
	
	// 초기 설정
	void Init()
	{
		PlayerPrefs.SetInt("MyData", 1);

		foreach (Achive achive in achives)
		{
			PlayerPrefs.SetInt(achive.ToString(), 0);
		}
	}

	private void Start()
	{
		UnLockChar();
	}

	// 캐릭터 해금
	void UnLockChar()
	{
		for (int i = 0; i < lockChar.Length; i++)
		{
			string achiveName = achives[i].ToString();
			bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
			lockChar[i].SetActive(!isUnlock);
			unlockChar[i].SetActive(isUnlock);
		}
	}

	private void LateUpdate()
	{
		foreach (Achive achive in achives)
		{
			CheckAchive(achive);
		}
	}

	private void CheckAchive(Achive achive)
	{
		bool isAchive = false;

		// 해금 조건 판별
		switch (achive)
		{
			case Achive.UnlockPotato:
				isAchive = GameManager.instance.kill >= 10;
				break;
			case Achive.UnlockBean:
				isAchive = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
				break;
		}

		// 활성화
		if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
		{
			PlayerPrefs.SetInt(achive.ToString(), 1);

			for(int i =0; i < uiNotice.transform.childCount; i++)
			{
				bool isActive = i == (int)achive;
				uiNotice.transform.GetChild(i).gameObject.SetActive(isActive);
			}

			StartCoroutine("NoticeRoutine");
		}
	}

	IEnumerator NoticeRoutine()
	{
		uiNotice.SetActive(true);
		AudioManager.instance.SfxPlay(AudioManager.Sfx.LevelUp);

		yield return wait;
		uiNotice.SetActive(false);

	}

}
