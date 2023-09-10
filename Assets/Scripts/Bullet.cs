using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dmg;
    public int per;

    Rigidbody2D rigid;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
	}

	public void Init(float dmg, int per, Vector3 dir)
    {
        this.dmg = dmg;
        this.per = per;

        if (per >= 0)
        {
            rigid.velocity = dir * 15f;
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        // Enemy�� �ƴϰų� per�� -100(��������) �ϰ�� �浹���� ����x
        if (!collision.CompareTag("Enemy") || per == -100)
            return;

        per--;
        if(per < 0)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.CompareTag("Area"))
			return;

        gameObject.SetActive(false);
	}

}
