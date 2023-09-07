using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float dmg;
    public int cnt; // ����
    public float speed; // �ٰŸ� - ȸ�� �ӵ�, ���Ÿ� - �߻� �ӵ�

    float timer;

    Player player;
	private void Awake()
	{
        player = GameManager.instance.player;
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


    // �ʱ�ȭ
	public void Init(ItemData itemData)
	{
        // Basic Set
        name = "Weapon " + itemData.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        // Property Set
        id = itemData.itemId;
        dmg = itemData.baseDmg;
        cnt = itemData.baseCnt;

        // prefabId �˻� > ����
        for(int i =0;i< GameManager.instance.poolmanager.prefabs.Length;i++)
        {
            if (itemData.projectile == GameManager.instance.poolmanager.prefabs[i])
            {
                prefabId = i;
                break;
            }
		}

		switch (id)
        {
            case 0:
                speed = 150;
                Positioning();

				break;
            default:
                // ���Ÿ����� speed�� ����(�ֱ�)
                speed = 0.5f;
                break;
        }

        // Hand Setting
        Hand hand = player.hands[(int)itemData.itemType];
        hand.sprite.sprite = itemData.hand;
        hand.gameObject.SetActive(true);

        // GameObject�� �ڽ� ������Ʈ������ �ش� �Լ��� ã�Ƽ� ���� 
        // DontRequireReceiver = SendMessage�� ���� �����ڰ� �� �ʿ����� �ʾƵ� �� ���
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    // ���� ������
    public void LevelUp(float dmg, int cnt)
	{
		this.dmg += dmg;
		this.cnt += cnt;

        if (id == 0)
            Positioning();

		player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);

	}

	// ���� ��ġ
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

            // ��ġ �ʱ�ȭ
            bullet.localPosition = Vector3.zero; // 0,0,0
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / cnt;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            
            bullet.GetComponent<Bullet>().Init(dmg, -1, Vector3.zero);    // -1 - ��������
        
        }


    }

    // ���Ÿ� �Ѿ� �߻�
    void Fire()
    {
        if (!player.scanner.SearchMinRangeTarget())
            return;

        Vector3 targetPos = player.scanner.SearchMinRangeTarget().position;
        Vector3 dir = (targetPos - transform.position).normalized;

        // bullet ���� ��ġ, ȸ�� ����
		Transform bullet = GameManager.instance.poolmanager.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        // �ʱ�ȭ
		bullet.GetComponent<Bullet>().Init(dmg, cnt, dir);    // -1 - ��������

	}

}
