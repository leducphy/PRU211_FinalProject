using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D rb;
    public GameObject Impact;
    public int hit;
    // Start is called before the first frame update
    void Start()
    {     
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Impact= Instantiate(Impact, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
        collision.gameObject.GetComponent<EnemyHealth>().getDamage(hit);
        Destroy(gameObject);
        
        //Destroy(Impact);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
