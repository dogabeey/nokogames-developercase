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
    internal NavMeshAgent agent;

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

        TryGetComponent(out agent);
    }

    protected virtual void Update()
    {
        if(agent)
        {
            if (agent.velocity.sqrMagnitude < 0.1f)
            {
                State = EntityState.Idle;
            }
            else
            {
                State = EntityState.Run;
            }
        }
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
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            State = EntityState.Idle;
        }
    }

    internal void MoveToPosition(Vector3 position)
    {
        if (State == EntityState.Dead)
        {
            return;
        }

        if(agent)
        {
            agent.SetDestination(position);

        }
    }
}