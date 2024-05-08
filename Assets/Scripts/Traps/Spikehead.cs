using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header ("Spikehead attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    private float checkTimer;
    private Vector2 destination;
    private Vector2[] directions = new Vector2[4];
    private bool attacking;

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (attacking) { 
            transform.Translate(destination * Time.deltaTime * speed);
        } else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        // check if sees player
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        //Stop spikehead
        SFXManager.instance.PlaySound(impactSound);
        Stop();
    }
}
