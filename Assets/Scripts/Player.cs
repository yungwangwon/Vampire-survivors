using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	public Vector2 inputVec;
	public float speed;
	public Hand[] hands;
	public Scanner scanner;
	public RuntimeAnimatorController[] aniCon;

	Rigidbody2D rigid;
	SpriteRenderer sprite;
	Animator ani;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		ani = GetComponent<Animator>();
		scanner = GetComponent<Scanner>();
		hands = GetComponentsInChildren<Hand>(true);
		speed = 3.0f;
	}

	private void FixedUpdate()
	{
		if (!GameManager.instance.isEnable)
			return;
		//¿Ãµø	
		Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime ;
		rigid.MovePosition(rigid.position + nextVec);
	}

	private void LateUpdate()
	{
		if (!GameManager.instance.isEnable)
			return;

		ani.SetFloat("Speed", inputVec.magnitude);

		if(inputVec.x != 0)
		{
			sprite.flipX = inputVec.x < 0;
		}
	}

	private void OnMove(InputValue val)
	{
		inputVec = val.Get<Vector2>();
	}

	private void OnEnable()
	{
		speed *= Character.Speed;
		ani.runtimeAnimatorController = aniCon[GameManager.instance.playerId];
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (!GameManager.instance.isEnable)
			return;

		GameManager.instance.hp -= Time.deltaTime * 10.0f;

		if(GameManager.instance.hp < 0)
		{
			for(int i =2; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}

			ani.SetTrigger("Dead");
			GameManager.instance.GameOver();
		}
	}

}
