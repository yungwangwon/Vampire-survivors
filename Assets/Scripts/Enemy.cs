using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed;
	public Rigidbody2D target;

	bool isLive = true;

	Rigidbody2D rigid;
	SpriteRenderer sprite;

	private void Awake()
	{
		//isLive = false;
		rigid = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		speed = 2.5f;
	}

	private void FixedUpdate()
	{
		if (!isLive)
			return;

		Vector2 dirVec = target.position - rigid.position;
		Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
		rigid.MovePosition(rigid.position + nextVec);
		rigid.velocity = Vector2.zero;
		
	}

	private void LateUpdate()
	{
		// target¿Ã enemy¿« x¡¬«•∞™¿Ã ≥∑¥Ÿ∏È flip
		sprite.flipX = target.position.x < rigid.position.x;
	}

	private void OnEnable()
	{
		target = GameManager.instance.player.GetComponent<Rigidbody2D>();
	}
}
