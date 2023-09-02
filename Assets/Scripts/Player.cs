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

	public Scanner scanner;

	Rigidbody2D rigid;
	SpriteRenderer sprite;
	Animator ani;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		ani = GetComponent<Animator>();
		scanner = GetComponent<Scanner>();
		speed = 5.0f;
	}

	private void FixedUpdate()
	{
		//¿Ãµø	
		Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime ;
		rigid.MovePosition(rigid.position + nextVec);
	}

	private void LateUpdate()
	{
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
}
