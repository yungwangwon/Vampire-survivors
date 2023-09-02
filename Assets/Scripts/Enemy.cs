using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed;
	public float hp;
	public float maxHp;

	public RuntimeAnimatorController[] aniCon;
	public Animator ani;
	public Rigidbody2D target;

	bool isLive;

	Rigidbody2D rigid;
	SpriteRenderer sprite;

	private void Awake()
	{
		isLive = false;
		rigid = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		ani = GetComponent<Animator>();
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
		// targetÀÌ enemyÀÇ xÁÂÇ¥°ªÀÌ ³·´Ù¸é flip
		sprite.flipX = target.position.x < rigid.position.x;
	}

	private void OnEnable()
	{
		target = GameManager.instance.player.GetComponent<Rigidbody2D>();
		isLive = true;
		hp = maxHp;
	}

	public void Init(SpawnData data)
	{
		ani.runtimeAnimatorController = aniCon[data.spriteType];
		speed = data.speed;
		maxHp = data.hp;

		hp = data.hp;

	}

	// Ãæµ¹
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Bullet"))
			return;

		hp -= collision.GetComponent<Bullet>().dmg;

		if(hp > 0)
		{

		}
		else // Á×À½
		{
			Dead();
		}

	}

	// Á×À½
	void Dead()
	{
		gameObject.SetActive(false);
	}

}
