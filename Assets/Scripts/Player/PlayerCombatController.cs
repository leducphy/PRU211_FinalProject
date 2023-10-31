using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    string weapon = "";
    PlayerMovementController playerMovement;
    public float attackRate = 2f; //Tốc đánh
    float nextAttacktime = 0f;

    private float spearAttackRange = 1.37f;
    private float swordAttackRange = 0f;

    enum PlayerSate
    {
        Throw, Attack, Attack1, Attack2, Attack3
    };

    // Start is called before the first frame update
    void Start()
    {     
        playerMovement= GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        weapon = playerMovement.weapon;
        if (weapon.Equals("Spear"))
        {
            attackRange = spearAttackRange;
        }

        if (weapon.Equals("Sword"))
        {
            attackRange= swordAttackRange;
        }

        if (Time.time >= nextAttacktime)
        {
            // Logic Attack Code here
            if (Input.GetKeyDown(KeyCode.J))
            {
                int randomAttack = 0;
                if (weapon.Equals("Sword"))
                {
                    randomAttack = Random.Range(1, 4);
                }else if (weapon.Equals("Spear"))
                {
                    randomAttack = Random.Range(1, 3);
                }
                else
                {
                    randomAttack = 0;
                }
            
                string animName = weapon + PlayerSate.Attack + randomAttack.ToString();              
                animName = animName.Replace("0", "");
                Debug.Log(animName);
                Attack(animName);
                nextAttacktime= Time.time + 1f / attackRate;
            }
        }
    }

    void Attack(string animName)
    {
        // Play an attack animation
        playerMovement.changeAnimationState(animName);

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach (Collider2D item in hitEnemies)
        {
            Debug.Log("You hit " + item.name + " by " + weapon);
            Destroy(item.gameObject);
            GameObject coinInstance = Instantiate(coinPrefab, item.gameObject.transform.position, Quaternion.identity);
            //Destroy(coinInstance, coinLifetime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
