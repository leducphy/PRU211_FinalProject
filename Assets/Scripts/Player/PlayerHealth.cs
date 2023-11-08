using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private AudioClip impactSound;

    [SerializeField] public float Health;
    [SerializeField] Image HealthBar;
    [SerializeField] Text txtHealth;
    [SerializeField] float impactForce = 1.3f;

    public float CurrentHealth;
    private PlayerMovementController playerMovement;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovementController>();
        CurrentHealth = Health;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //spriteRenderer.color = Color.white;
        if(CurrentHealth > Health)
        {
            CurrentHealth = Health;
        }
        txtHealth.text = CurrentHealth + " / " + Health;
        HealthBar.fillAmount = CurrentHealth / Health;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            getDamage(10);
        }
    }

    public void getDamage(float hit)
    {
        CurrentHealth -= hit;
        //HealthBar.fillAmount = CurrentHealth / Health;
        rb.AddForce(new Vector2(0f, impactForce), ForceMode2D.Impulse);
        animator.SetTrigger("Is"+playerMovement.weapon+"Hit");
        spriteRenderer.color = new Color(1.0f, 0.71f, 0.71f); // Using RGB values for "FFB6B6"

        if (CurrentHealth <= 0)
        {
            Death();
        }
        StartCoroutine(ResetSpriteColor());
        SoundManagement.instance.PlaySound(impactSound);


    }



    private void Death()
    {
        gameObject.layer = 0;
        animator.SetTrigger(playerMovement.weapon + "Die");
        Debug.Log(playerMovement.weapon + "Die");
        StartCoroutine(WaitForAnimationEnd());
        //

    }
    private IEnumerator ResetSpriteColor()
    {
        // Đợi cho đến khi animation hoàn thành
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private IEnumerator WaitForAnimationEnd()
    {
        // Đợi cho đến khi animation hoàn thành
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }
}
