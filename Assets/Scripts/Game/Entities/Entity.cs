using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{

    public enum EntityState
    {
        Idle,
        Run,
        Dead
    }

    public EntityState state;
    public float moveSpeedMultiplier = 1;

    internal Rigidbody2D rb;
    internal Collider2D cd;

    public virtual float MoveSpeed
    {
        get
        {
            float finalMoveSpeed = moveSpeedMultiplier;

            return finalMoveSpeed;
        }
    }
    public virtual EntityState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
                
                
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();

    }

    public void MovePosition()
    {
        // Move the entity to the new position
    }
}