using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    public int maxHealthPoint;
    public int currentHealthPoint;
    public int attackDamage;
    public float attackSpeed = 1;
    public bool attacked = false;
    public Image currentHealthBar;

    //MonoBehaviour - It means that Player Class can use Functions or variables that provided by Unity
    // Start is called before the first frame update
    void Start()
    {
        maxHealthPoint = 50;
        currentHealthPoint = 50;
        attackDamage = 10;

        animator = GetComponent<Animator>();
        SetAttackSpeed(1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // currentHealthBar.fillAmount = (float)currentHealthPoint / (float)maxHealthPoint;

        float horizontal = 0.0f;

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            horizontal = -1.0f;
            transform.localScale = new Vector2(-horizontal, 1);
            animator.SetBool("moving", true);
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
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
