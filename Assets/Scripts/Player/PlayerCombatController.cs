using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerCombatController : MonoBehaviour
{
    public GameObject Impact;
    [SerializeField] Transform ArrowPoint;
    [SerializeField] GameObject ArrowPrefab;
    [SerializeField] GameObject SpearPrefab;
    [SerializeField] Transform SwordPoint;
    [SerializeField] Transform SpearPoint;
    [SerializeField] Transform ThrowPoint;
    [SerializeField] int hit = 10;

    private Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    string weapon = "";
    PlayerMovementController playerMovement;
    public float attackRate = 2f; //Tốc đánh
    float nextAttacktime = 0f;

    private float spearAttackRange = 1f;
    private float swordAttackRange = 0.6f;

    String currentState = "";
    Animator animator;

    enum PlayerSate
    {
        Throw, Attack, Attack1, Attack2, Attack3
    };

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            weapon = "";
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            weapon = "Bow";
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            weapon = "Spear";
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            weapon = "Sword";
        }


        if (weapon.Equals("Spear"))
        {
            attackRange = spearAttackRange;
            attackPoint = SpearPoint;
        }

        if (weapon.Equals("Sword"))
        {
            attackRange = swordAttackRange;
            attackPoint = SwordPoint;
        }

        if (Time.time >= nextAttacktime)
        {

            if (Input.GetKeyDown(KeyCode.L) && weapon.Equals("Spear"))
            {
                Attack(weapon + PlayerSate.Throw);
                attackRate = 1f;
                nextAttacktime = Time.time + 1f / attackRate;
            }
            // Logic Attack Code here
            if (Input.GetKeyDown(KeyCode.J))
            {
                int randomAttack = 0;
                string animName = "";
                if (weapon.Equals("Sword"))
                {
                    randomAttack = UnityEngine.Random.Range(1, 4);
                    animName = weapon + PlayerSate.Attack + randomAttack.ToString();
                    attackRate = 5f;
                    Attack(animName);
                }
                else if (weapon.Equals("Spear"))
                {
                    randomAttack = UnityEngine.Random.Range(1, 3);
                    animName = weapon + PlayerSate.Attack + randomAttack.ToString();
                    attackRate = 2f;
                    Attack(animName);
                }
                else if (weapon.Equals("Bow"))
                {
                    animName = weapon + PlayerSate.Attack;
                    attackRate = 1.2f;
                    Attack(animName);
                }
                Debug.Log(animName);
                nextAttacktime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack(string animName)
    {
        // Play an attack animation
        animator.SetTrigger(animName);
        if (weapon.Equals("Bow") || animName.Equals("SpearThrow")) 
        {
            Shoot();
        }
        else
        {
            // Detect enemies in range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            // Damage enemies
            foreach (Collider2D item in hitEnemies)
            {
                Debug.Log("You hit " + item.name + " by " + weapon);
                item.gameObject.GetComponent<EnemyHealth>().getDamage(hit);

                GameObject impactInstance = Instantiate(Impact, item.gameObject.transform.position, item.transform.rotation);
                //Destroy(coinInstance, coinLifetime);
                StartCoroutine(RemoveAttackImpact(impactInstance));
            }
        }
    }

    IEnumerator RemoveAttackImpact(GameObject impactInstance)
    {
        yield return new WaitForSeconds(2f);
        Destroy(impactInstance);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Shoot()
    {
        if (weapon.Equals("Bow"))
        {
            Instantiate(ArrowPrefab, ArrowPoint.position, ArrowPoint.rotation);
        }else if (weapon.Equals("Spear"))
        {
            Instantiate(SpearPrefab, ThrowPoint.position, ThrowPoint.rotation);
        }
       
    }

    public void onCompleteAttack()
    {

    }

}
