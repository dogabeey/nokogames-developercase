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
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cd = GetComponent<Collider>();

    }

}