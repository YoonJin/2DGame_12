using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HP = 100;
    public float monSpeed;
    public Transform StartPositon;

    private bool _isScroll = true;


    // Update is called once per frame
    void Update()
    {
        // TODO : ���� ����
        if(GameManager.Instance.isScroll)
        {
            transform.Translate(Vector2.left * Time.deltaTime * monSpeed);
        }
    }

    public void SetScroll(bool isScroll)
    {
        this._isScroll = isScroll;
        this.monSpeed = (isScroll) ? 5 : 0;
    }
}
