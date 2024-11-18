using UnityEngine;

public abstract class Character : MonoBehaviour, IAttackable {
    private string _name;
    private int _healthPoint;
    private int _armor;
    private int _rangeAttack;
    private int _cooldown;
    private int _moveSpeed;
    protected Animator animator;
    public static int totalCharacter = 0;

    public virtual void Update () {}

    public string Name {
        get => _name;
        set => if (!string.IsNullOrEmpty(value)) _name = value;
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