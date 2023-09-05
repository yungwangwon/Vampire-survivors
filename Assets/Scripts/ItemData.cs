using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
	public enum ItemType
	{
		Melee,
		Range,
		Glove,
		Shoe,
		Heal
	}

	[Header("# Main Info")]
	public ItemType	itemType;
	public int		itemId;
	public string	itemName;
	public string	itemDesc;
	public Sprite	itemIcon;

	[Header("# Level Dara")]
	public float	baseDmg;
	public int		baseCnt;
	public float[]	dmgs;
	public int[]	cnt;

	[Header("# Weapon")]
	public GameObject projectile;

}
