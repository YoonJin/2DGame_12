using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HP = 100;
    public float monSpeed;

    private Animator animator;

    private float playerHp = 0;
    private int cntLoop = 0;
    private PlayerControl player;

    public int att = 5;

    void Start()
    {
        animator = GetComponent<Animator>();       
    }

    // Update is called once per frame
    void Update()
    {
        // TODO : ���� ����
        if(GameManager.Instance.isScroll)
        {
            transform.Translate(Vector2.left * Time.deltaTime * monSpeed);
        }

        // �ִϸ������� �ִϸ��̼� �̸��� Attack�϶�
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            // normalizedTime�� float������ �����κ��� �ִϸ��̼��� ����Ƚ��,
            // �Ҽ��κ��� ���� �ִϸ��̼��� ���������� �ǹ��Ѵ�.
            float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            // Mathf.Floor�� �Ҽ��κ��� �������� �����κ��� ���ش�.
            float currentState = normalizedTime - Mathf.Floor(normalizedTime);

            if(currentState >= 0.9f &&
                normalizedTime > cntLoop)
            {
                playerHp = player.Damage(att);
                cntLoop += 1;

                if(playerHp <= 0)
                {
                    animator.SetBool("attack", false);
                    cntLoop = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            animator.SetBool("attack", true);
            player = collision.gameObject.GetComponent<PlayerControl>();
        }
    }
}
