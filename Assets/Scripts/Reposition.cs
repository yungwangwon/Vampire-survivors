using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 각 타일맵에 들어있는 Reposition
public class Reposition : MonoBehaviour
{
	Collider2D coll;

	private void Awake()
	{
		coll = GetComponent<Collider2D>();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// Area(카메라 영역)에서 벗어났을경우
		if (!collision.CompareTag("Area"))
			return;

		// 플레이어와 타일의 거리와 방향값을 이용한 타일이동
		Vector3 playerPos = GameManager.instance.player.transform.position;
		Vector3 myPos = transform.position;
		float diffX = Mathf.Abs(playerPos.x - myPos.x);
		float diffY = Mathf.Abs(playerPos.y - myPos.y);

		Vector3 playerDir = GameManager.instance.player.inputVec;
		float dirX = playerDir.x > 0 ? 1 : -1;
		float dirY = playerDir.y > 0 ? 1 : -1;

		switch (transform.tag)
		{
			case "Ground":
				// 이동
				if (diffX > diffY)
					transform.Translate(Vector3.right * dirX * 40);
				else if (diffX < diffY)
					transform.Translate(Vector3.up * dirY * 40);
				break;
			case "Enemy":
				if (coll.enabled)
				{
					transform.Translate(playerDir * 20
						+ new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0)); ;
				}
				break;
		}


	}
}
