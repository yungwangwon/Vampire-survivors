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
		// �⺻ ����
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

	// ����� ����
	public void RateUp()
	{
		// parent(�÷��̾�)�� ��� �ڽĿ�����Ʈ�� wepon ������Ʈ
		Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

		foreach (Weapon weapon in weapons)
		{
			switch (weapon.id)
			{
				// �ٰŸ�
				case 0:
					weapon.speed = 150 + (150 * rate);
					break;
				// ���Ÿ�
				default:
					weapon.speed = 0.5f * (1f - rate);
					break;
			}

		}
	}
	
	// �̵��ӵ� ����
	public void SpeedUp()
	{
		float speed = 3.0f;
		GameManager.instance.player.speed = speed + speed * rate;
	}

}
