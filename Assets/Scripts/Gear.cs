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
			float speed;
			switch (weapon.id)
			{
				// �ٰŸ�
				case 0:
					speed = 150 * Character.WeaponSpeed;
					weapon.speed = speed + (speed * rate);
					break;
				// ���Ÿ�
				default:
					speed = 0.5f * Character.WeaponRate;
					weapon.speed = rate * (1f - rate);
					break;
			}

		}
	}
	
	// �̵��ӵ� ����
	public void SpeedUp()
	{
		float speed = 3.0f * Character.Speed;
		GameManager.instance.player.speed = speed + speed * rate;
	}

}
