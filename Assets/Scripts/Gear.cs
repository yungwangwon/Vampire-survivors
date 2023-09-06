using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Gear : MonoBehaviour
{
	public ItemData.ItemType type;
	public float rate;

	public void Init(ItemData itemData)
	{
		// 기본 셋팅
		name = "Gear " + itemData.name;
		transform.parent = GameManager.instance.player.transform;
		transform.localPosition = Vector3.zero;

		// Property Set
		type = itemData.itemType;
		rate = itemData.dmgs[0];
		ApplyGear();
	}

	void ApplyGear()
	{
		switch (type)
		{
			case ItemData.ItemType.Glove:
				RateUp();
				break;
			case ItemData.ItemType.Shoe:
				SpeedUp();
				break;
		}
	}

	public void LevelUp(float rate)
	{
		this.rate = rate;
		ApplyGear();
	}

	// 연사력 증가
	public void RateUp()
	{
		// parent(플레이어)의 모든 자식오브젝트의 wepon 컴포넌트
		Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

		foreach (Weapon weapon in weapons)
		{
			switch (weapon.id)
			{
				// 근거리
				case 0:
					weapon.speed = 150 + (150 * rate);
					break;
				// 원거리
				default:
					weapon.speed = 0.5f * (1f - rate);
					break;
			}

		}
	}
	
	// 이동속도 증가
	public void SpeedUp()
	{
		float speed = 3.0f;
		GameManager.instance.player.speed = speed + speed * rate;
	}

}
