using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    private string _name;
    private int _healthPoint;
    private int _attackDamage;
    private int _armor;
    private float _attackRange;
    private float _attackCooldown;
    private float _moveSpeed;
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
        get { return _healthPoint; }
        set { if (value < 0) _healthPoint = 0; else _healthPoint = value; }
    }

    public int AttackDamage {
        get { return _attackDamage; }
        set {
            if (value < 0) _attackDamage = 0;
            else if (value < 500)  _attackDamage = value; 
        }
    }

    public int Armor {
        get { return _armor; }
        set { if (value < 0) _armor = 0; else _armor = value; }
    }

    public float AttackRange {
        get { return _attackRange; }
        set { if (value < 0) _attackRange = 0; else _attackRange = value; }
    }

    public float AttackCooldown {
        get { return _attackCooldown; }
        set { if (value < 0) _attackCooldown = 0; else _attackCooldown = value; }
    }

    public float MoveSpeed {
        get { return _moveSpeed; }
        set { if (value < 0) _moveSpeed = 0; else _moveSpeed = value; }
    }

    public abstract void TakeDamage(int damage);

    public abstract void Attack();
    public abstract void Walk(bool state);
    public abstract void Hurt();
    public abstract IEnumerator Die();
}