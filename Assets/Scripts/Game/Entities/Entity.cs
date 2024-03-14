using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
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

    internal Rigidbody rb;
    internal Collider cd;

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
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        cd = GetComponent<Collider>();

    }

    internal void Move(Vector3 direction)
    {
        if (State == EntityState.Dead)
        {
            return;
        }

        if (direction != Vector3.zero)
        {
            transform.DORotateQuaternion(Quaternion.LookRotation(direction), 0.1f);
            rb.velocity = direction * MoveSpeed;
            State = EntityState.Run;
        }
        else
        {
            rb.velocity = Vector3.zero;
            State = EntityState.Idle;
        }
    }
}