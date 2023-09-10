using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
	public ItemData itemData;
	public int level;
	public Weapon weapon;
	public Gear gear;

	Image icon;
	Text textLevel;
	Text textName;
	Text textDesc;

	private void Awake()
	{
		icon = GetComponentsInChildren<Image>()[1];
		icon.sprite = itemData.itemIcon;

		Text[] texts = GetComponentsInChildren<Text>();
		textLevel = texts[0];
		textName = texts[1];
		textDesc = texts[2];

		textName.text = itemData.itemName;
	}

	// Ȱ��ȭ
	private void OnEnable()
	{
		textLevel.text =  "Lv." + (level + 1);

		switch (itemData.itemType)
		{
			case ItemData.ItemType.Melee:
			case ItemData.ItemType.Range:
				textDesc.text = string.Format(itemData.itemDesc, itemData.dmgs[level]*100, itemData.cnt[level]);
				break;
			case ItemData.ItemType.Glove:
			case ItemData.ItemType.Shoe:
				textDesc.text = string.Format(itemData.itemDesc, itemData.dmgs[level]*100);
				break;
			default:
				textDesc.text = string.Format(itemData.itemDesc);
				break;
		}
	}


	// ������ ��ư(LevelUp)
	public void OnClick()
	{
		switch(itemData.itemType)
		{
			// ����, ���Ÿ�����
			case ItemData.ItemType.Melee:
			case ItemData.ItemType.Range:
				if(level ==0)
				{
					// ���� �������� ó���϶�
					GameObject newWeapon = new GameObject();
					weapon = newWeapon.AddComponent<Weapon>();
					weapon.Init(itemData);
				}
				else
				{
					// ���� ������, ������, ���� ����� ����
					float nextDmg = itemData.baseDmg;
					int nextCnt = itemData.baseCnt;

					nextDmg = itemData.baseDmg * itemData.dmgs[level];
					nextCnt = itemData.cnt[level];

					weapon.LevelUp(nextDmg, nextCnt);
				}
				level++;
				break;
			// �尩, �Ź�
			case ItemData.ItemType.Glove:
			case ItemData.ItemType.Shoe:
				if(level == 0)
				{
					// �������� ó���϶�
					GameObject newGear = new GameObject();
					gear = newGear.AddComponent<Gear>();
					gear.Init(itemData);
				}
				else
				{
					float nextRate = itemData.dmgs[level];
					gear.LevelUp(nextRate);
				}
				level++;
				break;
			// ����
			case ItemData.ItemType.Heal:
				GameManager.instance.hp = GameManager.instance.maxHp;
				break;
		}

		// ���� ���� ���޽�
		if(level == itemData.dmgs.Length) 
		{
			// ��ư ��Ȱ��ȭ
			GetComponent<Button>().interactable = false;

		}
	}
}
