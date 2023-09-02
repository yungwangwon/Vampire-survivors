using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform minrangeTarget;

	private void FixedUpdate()
	{
		// ��������ĳ���� CircleCastAll(���� ��ġ, ���� ������, ĳ���ù���, ĳ���ñ���, ��� ���̾�)
		targets = Physics2D.CircleCastAll(transform.position, scanRange
			, Vector2.zero, 0, targetLayer); ;

		minrangeTarget = SearchMinRangeTarget();
	}

	// ���� ����� target �˻�
	public Transform SearchMinRangeTarget()
	{
		Transform result = null;

		float diff = 100;
		foreach(RaycastHit2D target in targets)
		{
			Vector3 myPos = transform.position;
			Vector2 targetPos = target.transform.position;
			float distance = Vector3.Distance(myPos, targetPos);

			if(distance < diff) 
			{ 
				diff = distance;
				result = target.transform;
			}
		}

		return result;
	}

}
