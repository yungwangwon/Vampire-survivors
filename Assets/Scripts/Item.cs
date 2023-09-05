using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
	public ItemData itemData;
	public int level;
	public Weapon weapon;

	Image icon;
	Text textLevel;

	private void Awake()
	{
		icon = GetComponentsInChildren<Image>()[1];
		icon.sprite = itemData.itemIcon;

		Text[] texts = GetComponentsInChildren<Text>();
		textLevel = texts[0];
	}

	private void LateUpdate()
	{
		textLevel.text =  "Lv." + (level + 1);
	}

	public void OnClick()
	{
		switch(itemData.itemType)
		{
			case ItemData.ItemType.Melee:
			case ItemData.ItemType.Range:

				break;
			case ItemData.ItemType.Glove:
				break;
			case ItemData.ItemType.Shoe:
				break;
			case ItemData.ItemType.Heal:
				break;
		}

		level++;

		if(level == itemData.dmgs.Length) 
		{
			GetComponent<Button>().interactable = false;

		}
	}
}
