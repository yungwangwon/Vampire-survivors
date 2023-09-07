using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	public bool isLeft;
	public SpriteRenderer sprite;

	SpriteRenderer playerSprite;

	Vector3 rightHandPos = new Vector3(0.35f, -0.15f, 0);
	Vector3 rightHandPosReverse = new Vector3(-0.35f, -0.15f, 0);
	Quaternion leftRot = Quaternion.Euler(0, 0, -35);
	Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);
	Vector3 leftHandPos = new Vector3(-0.17f, -0.35f, 0);
	Vector3 leftHandPosReverse = new Vector3(0.17f, -0.35f, 0);



	private void Awake()
	{
		playerSprite = GetComponentsInParent<SpriteRenderer>()[1];
	}

	private void LateUpdate()
	{
		bool isReverse = playerSprite.flipX;

		if(isLeft)	//근거리
		{
			transform.localRotation = isReverse ? leftRotReverse : leftRot;
			transform.localPosition = isReverse ? leftHandPosReverse : leftHandPos;

			sprite.flipY = isReverse;
			//sprite.sortingOrder = isReverse ? 4 : 6;
		}
		else //원거리
		{
			transform.localPosition = isReverse ? rightHandPosReverse : rightHandPos;
			sprite.flipX = isReverse;
			sprite.sortingOrder = isReverse ? 6 : 4;

		}
	}
}
