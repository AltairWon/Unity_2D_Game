using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Animator animator;
    public int maxHealthPoint;
    public int currentHealthPoint;
    public int attackDamage;
    public float attackSpeed = 1;
    public bool attacked = false;
    public Image currentHealthBar;

    bool inputRight = false;
    bool inputLeft = false;
    private Rigidbody2D rigidbody2D;
    int moveSpeed = 2;

    BoxCollider2D collider2D;

    public float jumpPower = 50;
    bool inputJump = false;

    //MonoBehaviour - It means that Player Class can use Functions or variables that provided by Unity
    // Start is called before the first frame update
    void Start()
    {
        maxHealthPoint = 50;
        currentHealthPoint = 50;
        attackDamage = 10;

        animator = GetComponent<Animator>();
        SetAttackSpeed(1.5f);

        rigidbody2D = GetComponent<Rigidbody2D>();

        collider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = (float)currentHealthPoint / (float)maxHealthPoint;

        float horizontal = 0.0f;

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            inputLeft = true;
            horizontal = -1.0f;
            transform.localScale = new Vector2(-horizontal, 1);
            animator.SetBool("moving", true);
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            inputRight = true;
            horizontal = 1.0f;
            transform.localScale = new Vector2(-horizontal, 1);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        if(Keyboard.current.zKey.isPressed && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("attack"); 
        }

        Vector2 position = transform.position;
        //Vector2 localScale = transform.localScale;
        position.x = position.x + 0.01f * horizontal;
        //localScale.x = localScale.x + 0.1f * horizontal;
        transform.position = position;

        RaycastHit2D raycastHit = Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));

        if(Keyboard.current.spaceKey.isPressed && !animator.GetBool("jumping"))
        {
            inputJump = true;
        }
    }

    void FixedUpdate()
    {
        if(inputRight)
        {
            inputRight = false;
            //MovePosition - when Rigidbody for bodyType is Kinematic, it is useful to use
            rigidbody2D.MovePosition(rigidbody2D.position + Vector2.right * moveSpeed * Time.deltaTime);
        }

        if (inputLeft)
        {
            inputLeft = false;
            rigidbody2D.MovePosition(rigidbody2D.position + Vector2.left * moveSpeed * Time.deltaTime);
        }

        if (inputJump)
        {
            inputJump = false;
            rigidbody2D.AddForce(Vector2.up * jumpPower);
            //rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
        }
    }

    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }
    void SetAttackSpeed(float speed)
    {
        animator.SetFloat("attackSpeed", speed);
        attackSpeed = speed;
    }

}
