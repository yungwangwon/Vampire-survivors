using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
	Item[] items;

	private void Awake()
	{
		rect = GetComponent<RectTransform>();
		items = GetComponentsInChildren<Item>(true);
	}

	// 레벨업 창 활성화(scale 조정)
	public void Show()
	{
		Next();
		rect.localScale = Vector3.one;
		GameManager.instance.Stop();
		AudioManager.instance.SfxPlay(AudioManager.Sfx.LevelUp);
		AudioManager.instance.EffectBgm(true);
	}

	// 비활성화
	public void Hide()
	{
		rect.localScale = Vector3.zero;
		GameManager.instance.Resume();
		AudioManager.instance.SfxPlay(AudioManager.Sfx.Select);
		AudioManager.instance.EffectBgm(false);
	}

	public void Select(int index)
	{
		items[index].OnClick();
	}

	public void Next()
	{
		// 모든 아이템 비활성화
		foreach (Item item in items) 
		{
			item.gameObject.SetActive(false);
		}
		// 랜덤 3개 아이템 활성화
		int[] rand = new int[3];
		while(true)
		{
			rand[0] = Random.Range(0, items.Length);
			rand[1] = Random.Range(0, items.Length);
			rand[2] = Random.Range(0, items.Length);

			if (rand[0] != rand[1] && rand[1] != rand[2] && rand[2] != rand[0])
				break;
		}
		for (int i = 0; i < rand.Length;i++)
		{
			Item randItem = items[rand[i]];
			// 만렙 아이템 소비아이템으로 대체
			if (randItem.level == randItem.itemData.dmgs.Length)
				randItem = items[4];

			randItem.gameObject.SetActive(true);
		}
	}
}
