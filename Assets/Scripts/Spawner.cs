using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public Transform[] spawnPoint;
	public SpawnData[] spawnData;

	public float levelTime;
	int level;
	float timer;

	private void Awake()
	{
		spawnPoint = GetComponentsInChildren<Transform>();
		levelTime = GameManager.instance.maxGameTime / spawnData.Length;
	}

	private void Update()
	{
		if (!GameManager.instance.isEnable)
			return;

		timer += Time.deltaTime;
		//FloorToInt �Ҽ��� ������
		level = Mathf.Min( Mathf.FloorToInt( GameManager.instance.gameTime / levelTime), spawnData.Length - 1);

		if(timer > spawnData[level].spawnTime)
		{
			Spawn();
			timer = 0f;
		}
	}

	void Spawn()
	{
		GameObject enemy = GameManager.instance.poolmanager.Get(0);
		enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;
		enemy.GetComponent<Enemy>().Init(spawnData[level]);
	}



}

// spawndata�� ���� Ŭ����
[System.Serializable]
public class SpawnData
{
	public float spawnTime;	// spawn�ֱ�

	public int spriteType;	// �� sprite
	public int hp;			// �� hp
	public float speed;		// �� speed
}