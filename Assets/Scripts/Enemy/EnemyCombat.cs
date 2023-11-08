using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] Transform AttackPoint;
    [SerializeField] float AttackRange;
    [SerializeField] public LayerMask PlayerLayer;
    [SerializeField] public float hit;
    [SerializeField] float AttackRadius;
    [SerializeField] float attackCooldown = 1.0f; // Độ trễ giữa các lần tấn công
    [SerializeField] bool isLongRangeAttack = false;
    [SerializeField] GameObject BulletPrefab;
    private Animator animator;
    private GameObject Player;
    private float lastAttackTime; // Thời điểm lần tấn công trước đó
    float originSpeed;
    float distance;


    void Start()
    {
        Player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        lastAttackTime = -attackCooldown; // Đặt thời điểm tấn công trước đó là một khoảng thời gian âm để cho phép tấn công ngay lập tức khi bắt đầu
        originSpeed = GetComponent<EnemyMovement>().speed;
    }

    void Update()
    {
        if (GetComponent<EnemyHealth>().health <= 0)
        {
            PlayerLayer.value = 0;
        }
        distance = Vector2.Distance(transform.position, Player.transform.position);
        if (distance <= AttackRadius && Time.time - lastAttackTime >= attackCooldown)
        {
            GetComponent<EnemyMovement>().speed = 0;
            Attack();
        }
        else if (distance > AttackRadius)
        {
            animator.SetTrigger("Run");
            GetComponent<EnemyMovement>().speed = originSpeed;
        }
    }

    public void Attack()
    {
        GetComponent<EnemyMovement>().speed = 0;
        animator.SetTrigger("Attack");
        if (distance > AttackRadius)
        {
            animator.SetTrigger("Run");
            GetComponent<EnemyMovement>().speed = originSpeed;
        }

        if (isLongRangeAttack)
        {
            Instantiate(BulletPrefab, AttackPoint.position, AttackPoint.rotation);
        }
        else
        {
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, PlayerLayer);
            if (hitPlayer.Length != 0)
            {
                foreach (Collider2D item in hitPlayer)
                {
                    Debug.Log("You are hitted");
                    item.gameObject.GetComponent<PlayerHealth>().getDamage(hit);
                }
            }
        }

        // Cập nhật thời điểm tấn công trước đó
        lastAttackTime = Time.time;

    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<EnemyMovement>().speed = originSpeed;
        animator.SetTrigger("Run");
    }
}
