using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] GameObject coinPrefab;
    private Transform Player;
    private Animator animator;


    public float pushForce = 5f; // Lực đẩy
    public float pushDuration = 0.2f; // Thời gian đẩy

    private Rigidbody2D rb;
    private float pushTimer;
    private bool isPushed;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        pushTimer = 0f;
        isPushed = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isPushed)
        {
            pushTimer += Time.deltaTime;

            if (pushTimer >= pushDuration)
            {
                // Hủy đẩy sau khi hết thời gian đẩy
                isPushed = false;
                rb.velocity = Vector2.zero;
                pushTimer = 0f;
            }
        }
    }

    // Gọi hàm này khi game object bị đánh
    public void Hit()
    {
        isPushed = true;
        
        if (Player.position.x < transform.position.x)
        {
            Vector2 pushDirection = transform.right; // Đẩy về phía sau, bạn có thể điều chỉnh hướng đẩy dựa trên cách bạn muốn game object bị đẩy
            rb.velocity = pushDirection * pushForce;
            Debug.Log("Player ben trai enemy");
        }
        else if (Player.position.x > transform.position.x)
        {
            Vector2 pushDirection = -transform.right; // Đẩy về phía sau, bạn có thể điều chỉnh hướng đẩy dựa trên cách bạn muốn game object bị đẩy
            rb.velocity = pushDirection * pushForce;
            Debug.Log("Player nằm bên phải của Enemy");
        }
        //else
        //{
        //    Debug.Log("object1 và object2 ở cùng một vị trí theo trục X");
        //}
    }

    public void getDamage(int damage)
    {
        health -= damage;
        Hit();
        Debug.Log("Health: " + health);
        if (health <= 0)
        {           
            Death();
        }
    }

    public void Death()
    { 
        animator.SetTrigger("Die");
        gameObject.GetComponent<EnemyMovement>().speed = 0;
        Debug.Log(gameObject.name + "Die");
        StartCoroutine(WaitForAnimationEnd());
    }

    private IEnumerator WaitForAnimationEnd()
    {
        // Đợi cho đến khi animation hoàn thành
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.GetComponent<EnemyMovement>().speed = 0;
        //animator.SetTrigger("Die");
        GameObject coinInstance = Instantiate(coinPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
