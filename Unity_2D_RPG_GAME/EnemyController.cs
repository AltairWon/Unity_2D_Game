using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameObject prefabHpBar;
    public GameObject canvas;

    RectTransform hpBar;

    public float height = 1.7f;

    public string enemyName;
    public int maxHealthPoint;
    public int currentHealthPoint;

    public Player player;
    Image currentHpBar;

    public Animator enemyAnimator;
    public float atkRange;
    public int atkDmg;
    public float attackSpeed;
    public float moveSpeed;
    public float fieldOfVision;

    // Start is called before the first frame update
    void Start()
    {
        hpBar = Instantiate(prefabHpBar, canvas.transform).GetComponent<RectTransform>();

        if (name.Equals("Enemy1"))
        {
            SetEnemyStatus("Enemy1", 100, 10, 1.5f, 2, 1.5f, 7f);
        }
        currentHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();

        SetAttackSpeed(attackSpeed);
    }

    void SetAttackSpeed(float attackSpeed)
    {
        enemyAnimator.SetFloat("attackSpeed", attackSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = hpBarPos;
        currentHpBar.fillAmount = (float)currentHealthPoint / (float)maxHealthPoint;
    }

    private void SetEnemyStatus(string _enemyName, int _maxHealthPoint, int _attackDamage,
        float _attackSpeed, float _moveSpeed, float _attackRange, float _fieldOfVision)
    {
        enemyName = _enemyName;
        maxHealthPoint = _maxHealthPoint;
        currentHealthPoint = _maxHealthPoint;
        atkDmg = _attackDamage;
        attackSpeed = _attackSpeed;
        moveSpeed = _moveSpeed;
        atkRange = _attackRange;
        fieldOfVision = _fieldOfVision;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.attacked)
            {
                //a -= b -> a = a - b 
                currentHealthPoint -= player.attackDamage;
                Debug.Log(currentHealthPoint);
                player.attacked = false;
                if (currentHealthPoint <= 0)
                {
                    Die();
                }
            }

        }
    }

    void Die()
    {
        enemyAnimator.SetTrigger("die");
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        //Destory(gameObject, number) -> gameObject will be destoryed after number seconds
        Destroy(gameObject, 3);
        Destroy(hpBar.gameObject, 3);
    }


}
