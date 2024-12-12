using System;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    [SerializeField] private string _name;
    [SerializeField] private int _healthPoint;
    [SerializeField] private int _armor;
    [SerializeField] private int _rangeAttack;
    [SerializeField] private int _cooldown;
    [SerializeField] private int _moveSpeed;
    protected Animator animator;
    protected Rigidbody2D rb;
    public static int totalCharacter = 0;

    public virtual void Start () {}

    public virtual void Update () {}

    public string Name {
        get { return _name; }
        set { 
            if (string.IsNullOrEmpty(value)) _name = "Unknown";
            else _name = value;
         }
        
    }

    public int HealthPoint {
        get => _healthPoint;
        set => _healthPoint = value;
    }

    public int Armor {
        get => _armor;
        set => _armor = value;
    }

    public int RangeAttack {
        get => _rangeAttack;
        set => _rangeAttack = value;
    }

    public int Cooldown {
        get => _cooldown;
        set => _cooldown = value;
    }

    public int MoveSpeed {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    public abstract void Attack();
    public abstract void Run();
    public abstract void Hurt();
    public abstract void Die();
}