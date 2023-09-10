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

	// ������ â Ȱ��ȭ(scale ����)
	public void Show()
	{
		Next();
		rect.localScale = Vector3.one;
		GameManager.instance.Stop();
		AudioManager.instance.SfxPlay(AudioManager.Sfx.LevelUp);
		AudioManager.instance.EffectBgm(true);
	}

	// ��Ȱ��ȭ
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
		// ��� ������ ��Ȱ��ȭ
		foreach (Item item in items) 
		{
			item.gameObject.SetActive(false);
		}
		// ���� 3�� ������ Ȱ��ȭ
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
			// ���� ������ �Һ���������� ��ü
			if (randItem.level == randItem.itemData.dmgs.Length)
				randItem = items[4];

			randItem.gameObject.SetActive(true);
		}
	}
}
