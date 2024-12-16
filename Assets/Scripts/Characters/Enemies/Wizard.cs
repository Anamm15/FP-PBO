using UnityEngine;

public class Wizard : Enemy
{
    [SerializeField] public int specifiedHealthPoint;
    [SerializeField] public int specifiedAttackDamage;
    [SerializeField] public int specifiedArmor;
    public override void Start()
    {
        base.Start();

        Name = "Wizard";
        HealthPoint = 25 + specifiedHealthPoint;
        AttackDamage = 20 + specifiedAttackDamage;
        Armor = 1 + specifiedArmor;
        MoveSpeed = 1f;
        AttackRange = 2f;
        AttackCooldown = 1;
    }

    public override void Update()
    {
        base.Update(); 
        if (path.Count > 0)
        {
            MoveAlongPath(AttackRange, AttackCooldown);
        }
    }
}