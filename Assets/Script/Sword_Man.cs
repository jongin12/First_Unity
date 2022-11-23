using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Sword_Man : MonoBehaviour
{
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public float atkSpeed = 1;
    public bool attacked = false;
    public Image nowHpbar;
    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }
    Animator animator;

    Rigidbody2D rigid2D;
    BoxCollider2D col2D;

    public float jumpPower = 40;
    bool inputJump = false;

    void Start()
    {
        maxHp = 50;
        nowHp = 50;
        atkDmg = 10;

        transform.position = new Vector3(0, 0, 0);
        animator = GetComponent<Animator>();
        SetAttackSpeed(1.5f);

        rigid2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<BoxCollider2D>();
    }
    bool inputRight = false;
    bool inputLeft = false;
    void Update()
    {
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
        float moveSpeed = 3;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("moving", true);
            inputRight = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("moving", true);
            inputLeft = true;
        }
        else animator.SetBool("moving", false);

        if (Input.GetKeyDown(KeyCode.A) &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("attack");
        }
        if (inputRight)
        {
            inputRight = false;
            rigid2D.velocity = new Vector2(moveSpeed, rigid2D.velocity.y);
        }
        if (inputLeft)
        {
            inputLeft = false;
            rigid2D.velocity = new Vector2(-moveSpeed, rigid2D.velocity.y);
        }
        RaycastHit2D raycastHit = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
        if (raycastHit.collider != null)
            animator.SetBool("jumping", false);
        else animator.SetBool("jumping", true);

        if(Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("jumping"))
        {
            inputJump = true;
        }
    }
    void SetAttackSpeed(float speed)
    {
        animator.SetFloat("attackSpeed", speed);
        atkSpeed = speed;
    }
    void FixedUpdate()
    {
        if(inputJump)
        {
            inputJump= false;
            rigid2D.AddForce(Vector2.up * jumpPower);
        }
    }
}

