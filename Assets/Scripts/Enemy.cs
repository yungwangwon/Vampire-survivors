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
		// target�� enemy�� x��ǥ���� ���ٸ� flip
		sprite.flipX = target.position.x < rigid.position.x;
	}

	// Ȱ��ȭ
	private void OnEnable()
	{
		target = GameManager.instance.player.GetComponent<Rigidbody2D>();
		isLive = true;
		coll.enabled = true;   // collider ��Ȱ��ȭ
		rigid.simulated = true; // rigidbody ��Ȱ��ȭ
		sprite.sortingOrder = 2;    // sprite order in layer �� ����
		ani.SetBool("Dead", false);
		hp = maxHp;
	}

	// �ʱ�ȭ
	public void Init(SpawnData data)
	{
		ani.runtimeAnimatorController = aniCon[data.spriteType];
		speed = data.speed;
		maxHp = data.hp;

		hp = data.hp;

	}

	// �浹
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Bullet") || !isLive)
			return;

		hp -= collision.GetComponent<Bullet>().dmg;
		StartCoroutine("KnockBack");    // �ڷ�ƾ�Լ��� ȣ���� ��쿡�� StartCoroutine()�� ����ϴ°���
		
		if (hp > 0) // �ǰ�
		{
			ani.SetTrigger("Hit");
		}
		else // ����
		{
			isLive = false;
			coll.enabled = false;	// collider ��Ȱ��ȭ
			rigid.simulated = false;	// rigidbody ��Ȱ��ȭ
			sprite.sortingOrder = 1;	// sprite order in layer �� ����
			ani.SetBool("Dead", true);

			GameManager.instance.kill++;
			GameManager.instance.GetExp();


		}

	}

	// �ڷ�ƾ
	IEnumerable KnockBack()
	{
		yield return wait;  // ���� �ϳ��� ���� ������ ������
		Vector3 playerPos = GameManager.instance.player.transform.position;
		Vector3 dirVec = transform.position - playerPos;

		rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);


	}

	// ����
	void Dead()
	{
		gameObject.SetActive(false);
	}

}
