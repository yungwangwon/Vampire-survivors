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

		switch (transform.tag)
		{
			case "Ground":
				float diffX = (playerPos.x - myPos.x);
				float diffY = (playerPos.y - myPos.y);

				float dirX = diffX > 0 ? 1 : -1;
				float dirY = diffY > 0 ? 1 : -1;
				diffX = Mathf.Abs(diffX);
				diffY = Mathf.Abs(diffY);


				// 이동
				if (diffX > diffY)
					transform.Translate(Vector3.right * dirX * 40);
				else if (diffX < diffY)
					transform.Translate(Vector3.up * dirY * 40);
				break;
			case "Enemy":
				if (coll.enabled)
				{
					Vector3 distance = playerPos - myPos;
					Vector3 rand = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
					transform.Translate(rand + distance * 2);
				}
				break;
		}


	}
}
