using System;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class Enemy : MonoBehaviour
{
    public event Action<EnemyState> OnStateChanged;
    public enum EnemyState
    {
        Move,
        Attack,
        Hit,
        Knockback,
        Dead
    }

    public EnemyState State { get; private set; }

    public bool CanMove { get; private set; }
    public bool CanAttack { get; private set; }

    public Rigidbody2D RB { get; private set; }
    public Animator Ani { get; private set; }


    public Vector2 Dir { get; set; }

    void Awake()
    {
        State = EnemyState.Move;
        CanMove = true;
        CanAttack = true;

        RB = GetComponent<Rigidbody2D>();
        Ani = GetComponentInChildren<Animator>();


        RB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void ChangeState(EnemyState newState)
    {
        if (State == EnemyState.Dead) return;

        State = newState;

        switch (State)
        {
            case EnemyState.Move:
                CanMove = true;
                CanAttack = true;
                break;

            case EnemyState.Attack:
                CanMove = true;
                CanAttack = true;
                break;

            case EnemyState.Hit:
                CanMove = false;
                CanAttack = false;
                break;

            case EnemyState.Knockback:
                CanMove = false;
                CanAttack = false;
                break;

            case EnemyState.Dead:
                CanMove = false;
                CanAttack = false;
                break;
        }
        OnStateChanged?.Invoke(newState);
    }
}