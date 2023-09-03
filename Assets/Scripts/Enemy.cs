using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.U2D.Sprites;
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
	Collider2D coll;
	SpriteRenderer sprite;
	WaitForFixedUpdate wait;

	private void Awake()
	{
		isLive = false;
		rigid = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		ani = GetComponent<Animator>();
		wait = new WaitForFixedUpdate();
		coll = GetComponent<Collider2D>();
		speed = 2.5f;
	}

	private void FixedUpdate()
	{
		if (!isLive || ani.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
			return;

		Vector2 dirVec = target.position - rigid.position;
		Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
		rigid.MovePosition(rigid.position + nextVec);
		rigid.velocity = Vector2.zero;
		
	}

	private void LateUpdate()
	{
		// target이 enemy의 x좌표값이 낮다면 flip
		sprite.flipX = target.position.x < rigid.position.x;
	}

	// 활성화
	private void OnEnable()
	{
		target = GameManager.instance.player.GetComponent<Rigidbody2D>();
		isLive = true;
		coll.enabled = true;   // collider 비활성화
		rigid.simulated = true; // rigidbody 비활성화
		sprite.sortingOrder = 2;    // sprite order in layer 값 설정
		ani.SetBool("Dead", false);
		hp = maxHp;
	}

	// 초기화
	public void Init(SpawnData data)
	{
		ani.runtimeAnimatorController = aniCon[data.spriteType];
		speed = data.speed;
		maxHp = data.hp;

		hp = data.hp;

	}

	// 충돌
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Bullet") || !isLive)
			return;

		hp -= collision.GetComponent<Bullet>().dmg;
		StartCoroutine("KnockBack");    // 코루틴함수를 호출할 경우에는 StartCoroutine()를 사용하는것이
		
		if (hp > 0) // 피격
		{
			ani.SetTrigger("Hit");
		}
		else // 죽음
		{
			isLive = false;
			coll.enabled = false;	// collider 비활성화
			rigid.simulated = false;	// rigidbody 비활성화
			sprite.sortingOrder = 1;	// sprite order in layer 값 설정
			ani.SetBool("Dead", true);

			GameManager.instance.kill++;
			GameManager.instance.GetExp();


		}

	}

	// 코루틴
	IEnumerable KnockBack()
	{
		yield return wait;  // 다음 하나의 물리 프레임 딜레이
		Vector3 playerPos = GameManager.instance.player.transform.position;
		Vector3 dirVec = transform.position - playerPos;

		rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);


	}

	// 죽음
	void Dead()
	{
		gameObject.SetActive(false);
	}

}
