using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float dmg;
    public int cnt;
    public float speed;

    float timer;

    Player player;
	private void Awake()
	{
		player = GetComponentInParent<Player>();
	}

	private void Start()
	{
        Init();
	}

	void Update()
    {
		switch (id)
		{
			case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
				break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0;
                    Fire();
                }
                break;
		}

        // .. Test Code
        if (Input.GetButtonDown("Jump"))
            LevelUp(10, 1);
	}


    // 초기화
	public void Init()
	{
        switch (id)
        {
            case 0:
                speed = 150;
                Positioning();

				break;
            default:
                // 원거리무기 speed값 설정(주기)
                speed = 0.3f;
                break;
        }
    }

    // 무기 레벨업
    void LevelUp(float dmg, int cnt)
	{
		this.dmg += dmg;
		this.cnt += cnt;

        if (id == 0)
            Positioning();
	}

    // 무기 배치
	void Positioning()
    {
        for (int i = 0; i < cnt; i++)
        {
            Transform bullet;
            
            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
				bullet = GameManager.instance.poolmanager.Get(prefabId).transform;
			    bullet.parent = transform;
            }

            bullet.parent = transform;

            // 위치 초기화
            bullet.localPosition = Vector3.zero; // 0,0,0
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / cnt;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            
            bullet.GetComponent<Bullet>().Init(dmg, -1, Vector3.zero);    // -1 - 근접공격
        
        }


    }

    // 원거리 총알 발사
    void Fire()
    {
        if (!player.scanner.SearchMinRangeTarget())
            return;

        Vector3 targetPos = player.scanner.SearchMinRangeTarget().position;
        Vector3 dir = (targetPos - transform.position).normalized;

        // bullet 생성 위치, 회전 설정
		Transform bullet = GameManager.instance.poolmanager.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        // 초기화
		bullet.GetComponent<Bullet>().Init(dmg, cnt, dir);    // -1 - 근접공격

	}

}
