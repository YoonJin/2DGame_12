using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : ManagerSingleton2<EnemyManager>
{
    public GameObject pPrefabMob = null;
    public Transform StartPos;

    private ObjectPool _pPool;

    private Queue<GameObject> pMobQueue = new Queue<GameObject>();

    // 골드 포지션
    public ItemFx prefabItem;
    public Transform toTweenPos;
    public Transform goldParent;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = new GameObject();
        obj.transform.SetParent(this.transform);
        obj.name = typeof(ObjectPool).Name;
        this._pPool = obj.AddComponent<ObjectPool>();
        this._pPool.Initalize(this.pPrefabMob, this.StartPos);

        this.StartCoroutine(CreateEnemy());
    }

    IEnumerator CreateEnemy()
    {
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));

        while(true)
        {
            if (GameManager.Instance.isScroll)
            {
                GameObject mob = this._pPool.GetQueue();
                Monster cMob = mob.GetComponent<Monster>();
                cMob.HP = 100;

                this.pMobQueue.Enqueue(mob);
                yield return new WaitForSeconds(Random.Range(1f, 1.5f));
            }
            else
                yield break;
        }
        yield return null;
    }

    public int Damaged (int att)
    {
        if(this.pMobQueue.Count != 0)
        {
            GameObject obj = this.pMobQueue.Peek();
            Monster mob = obj.GetComponent<Monster>();
            mob.HP -= att;

            if(mob.HP <= 0)
            {
                // 돈 올라가는 애니메이션
                this.SetMoney();

                GameManager.Instance.isScroll = true;
                this.StartCoroutine(CreateEnemy());
                this._pPool.InsertQueue(this.pMobQueue.Dequeue());
            }

            return mob.HP;
        }

        return 0;
    }

    void SetMoney ()
    {
        int randCount = Random.Range(5, 10);
        for(int i = 0; i < randCount; i++)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            ItemFx itemFx = Instantiate(prefabItem, screenPos, Quaternion.identity);
            itemFx.transform.SetParent(goldParent);
            itemFx.Explosion(screenPos, toTweenPos.position, 150f);
        }
    }
    
}
