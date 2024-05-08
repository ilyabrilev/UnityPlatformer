using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("Audio")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private bool invulnerable;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable)
        {
            return;
        }

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invulerability());
            SFXManager.instance.PlaySound(hurtSound);
        } else
        {
            if (!dead)
            {
                //Deactivate all attached behaviour
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                anim.SetBool("Grounded", true);
                anim.SetTrigger("Die");

                dead = true;
                SFXManager.instance.PlaySound(deathSound);
            }            
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        AddHealth(startingHealth);
        anim.ResetTrigger("Die");
        anim.Play("Idle");
        StartCoroutine(Invulerability());

        //Deactivate all attached behaviour
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }

        dead = false;
    }
}
