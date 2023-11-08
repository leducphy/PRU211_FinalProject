using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject targetObject;
    [SerializeField] public float speed;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = targetObject.transform.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);       
        FlipSprite(direction.x < 0);
    }

    private void FlipSprite(bool isFacingLeft)
    {
        Vector3 localScale = transform.localScale;
        localScale.x = isFacingLeft ? -1 : 1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            return;
        }
        else
        {

            // Lấy collider của đối tượng của bạn
            Collider2D myCollider = GetComponent<Collider2D>();


            // Lấy collider của đối tượng va chạm
            Collider2D otherCollider = collision.collider;


            // Tạm thời tắt va chạm giữa hai collider
            Physics2D.IgnoreCollision(myCollider, otherCollider);
        }
    }
}
