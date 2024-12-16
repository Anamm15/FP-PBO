using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] public int specifiedHealthPoint;
    [SerializeField] public int specifiedAttackDamage;
    [SerializeField] public int specifiedArmor;
    public override void Start()
    {
        base.Start();

        Name = "Skeleton";
        HealthPoint = 30 + specifiedHealthPoint;
        Armor = 1 + specifiedArmor;
        AttackDamage = 10 + specifiedAttackDamage;
        MoveSpeed = 2f;
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