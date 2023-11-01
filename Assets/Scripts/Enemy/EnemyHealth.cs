using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] GameObject coinPrefab;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void getDamage(int damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);
        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        animator.Play(gameObject.name + "Die");
        Debug.Log(gameObject.name + "Die");
        StartCoroutine(WaitForAnimationEnd());
    }

    private IEnumerator WaitForAnimationEnd()
    {
        // Đợi cho đến khi animation hoàn thành
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        GameObject coinInstance = Instantiate(coinPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);       
    }
}
