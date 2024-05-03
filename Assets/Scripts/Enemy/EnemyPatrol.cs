using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;
    [Header("Movement Animator")]
    [SerializeField] private Animator anim;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;

    [Header("Idle behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTime;

    private Vector2 initScale;
    private bool movingLeft;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            } else
            {
                DirectionChange();
            }
        } else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }

        }        
    }

    private void OnDisable()
    {
        anim.SetBool("Moving", false);
    }

    private void DirectionChange()
    {
        anim.SetBool("Moving", false);
        idleTime += Time.deltaTime;
        if (idleTime > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection(int _direction)
    {
        idleTime = 0;
        anim.SetBool("Moving", true);
        //make face direction
        enemy.localScale = new Vector2(Mathf.Abs(initScale.x) * _direction, initScale.y);

        //move in that direction
        enemy.position = new Vector2(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y);
    }
}
