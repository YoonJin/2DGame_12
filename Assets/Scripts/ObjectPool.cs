using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject pPrefabMob = null; // ������ ������

    public Queue<GameObject> pQueue = new Queue<GameObject>(); // ��ü�� ������ ť

    private Transform StartPos;


    public void Initalize(GameObject pPrefabMob, Transform StartPos)
    {
        this.pPrefabMob = pPrefabMob;
        this.StartPos = StartPos;

        for(int i = 0; i < 15; i++)
        {
            GameObject obj = Instantiate(pPrefabMob, this.transform);
            pQueue.Enqueue(obj);
            obj.transform.position = StartPos.position;
            obj.SetActive(false);
        }
    }

    // ����� ��ü�� Pool(Queue)�� �ݳ�
    public void InsertQueue (GameObject obj)
    {
        pQueue.Enqueue(obj);
        obj.transform.position = this.StartPos.position;
        obj.SetActive(false);
    }

    // ��ü�� �������� �Լ�
    public GameObject GetQueue()
    {
        GameObject obj = pQueue.Dequeue();
        obj.SetActive(true);
        return obj;
    }
}
