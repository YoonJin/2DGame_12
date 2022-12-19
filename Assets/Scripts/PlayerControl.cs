using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int att = 20;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    int mobHP = 0;
    int cntLoop = 0;
    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            // normalizeTime : 정수부분 루프 횟수, 소수 부분 진행도
            float normalizeTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float normalizeTimeInProcess = normalizeTime - Mathf.Floor(normalizeTime);

            if(normalizeTimeInProcess >= 0.9f &&
               normalizeTime > cntLoop)
            {
                cntLoop += 1;
                mobHP = EnemyManager.Instance.Damaged(att);

                if(mobHP <= 0)
                {
                    animator.SetBool("attack", false);
                    cntLoop = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            GameManager.Instance.isScroll = false;

            animator.SetBool("attack", true);
        }
    }
}
