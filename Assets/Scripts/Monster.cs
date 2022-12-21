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
        // TODO : 추후 수정
        if(GameManager.Instance.isScroll)
        {
            transform.Translate(Vector2.left * Time.deltaTime * monSpeed);
        }

        // 애니메이터의 애니메이션 이름이 Attack일때
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            // normalizedTime은 float형으로 정수부분은 애니메이션의 루프횟수,
            // 소수부분은 현재 애니메이션의 진행정도를 의미한다.
            float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            // Mathf.Floor로 소수부분을 내림한후 정수부분을 빼준다.
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
