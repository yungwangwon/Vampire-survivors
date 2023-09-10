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

	// 활성화
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


	// 아이템 버튼(LevelUp)
	public void OnClick()
	{
		switch(itemData.itemType)
		{
			// 근접, 원거리무기
			case ItemData.ItemType.Melee:
			case ItemData.ItemType.Range:
				if(level ==0)
				{
					// 무기 레벨업이 처음일때
					GameObject newWeapon = new GameObject();
					weapon = newWeapon.AddComponent<Weapon>();
					weapon.Init(itemData);
				}
				else
				{
					// 무기 레벨업, 데미지, 관통 계산후 적용
					float nextDmg = itemData.baseDmg;
					int nextCnt = itemData.baseCnt;

					nextDmg = itemData.baseDmg * itemData.dmgs[level];
					nextCnt = itemData.cnt[level];

					weapon.LevelUp(nextDmg, nextCnt);
				}
				level++;
				break;
			// 장갑, 신발
			case ItemData.ItemType.Glove:
			case ItemData.ItemType.Shoe:
				if(level == 0)
				{
					// 레벨업이 처음일때
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
			// 포션
			case ItemData.ItemType.Heal:
				GameManager.instance.hp = GameManager.instance.maxHp;
				break;
		}

		// 무기 만렙 도달시
		if(level == itemData.dmgs.Length) 
		{
			// 버튼 비활성화
			GetComponent<Button>().interactable = false;

		}
	}
}
