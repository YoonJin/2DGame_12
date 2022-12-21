using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public int att = 20;
    Animator animator;

    public float curHp = 100;
    public float maxHp = 100;

    public Image hp_bar;


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

    public float Damage(int att)
    {
        this.curHp -= att;
        // 0 ~ 1까지의 값으로 이미지 크기를 조정 (비율)
        this.hp_bar.fillAmount = curHp / maxHp;

        if(curHp <= 0)
        {
            // TODO : 추후 처리
        }

        return curHp;
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
