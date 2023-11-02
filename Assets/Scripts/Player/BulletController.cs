using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
    public float speed = 20f;
    public float gravity = 9.8f; // Gravitational acceleration
    private Rigidbody2D rb;
    public GameObject Impact;
    public int hit;
    
    private Vector2 initialVelocity;

    // Start is called before the first frame update
    void Start()
    {     
        rb = GetComponent<Rigidbody2D>();

        // Calculate the initial velocity in the X and Y direction
        initialVelocity = transform.right * speed;

        // Set the initial velocity
        rb.velocity = initialVelocity;

        // Lặp qua tất cả các đối tượng trong scene
        foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            // Kiểm tra xem đối tượng là clone hay không
            if (obj != null && (obj.name.StartsWith("AttackImpact")))
            {
                Destroy(obj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    private void FixedUpdate()
    {
        // Apply gravity to the bullet to make it drop over time
        rb.velocity += Vector2.down * gravity * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Impact = Instantiate(Impact, collision.contacts[0].point, Quaternion.identity);
        if (collision.gameObject.GetComponent<EnemyHealth>() != null)
        {
            collision.gameObject.GetComponent<EnemyHealth>().getDamage(hit);
        }
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

