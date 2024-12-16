using UnityEngine;

public class Orc : Enemy
{
    [SerializeField] public int specifiedHealthPoint;
    [SerializeField] public int specifiedAttackDamage;
    [SerializeField] public int specifiedArmor;
    public override void Start()
    {
        base.Start();

        Name = "Orc";
        HealthPoint = 35 + specifiedHealthPoint;
        AttackDamage = 15 + specifiedAttackDamage;
        Armor = 1 + specifiedArmor;
        MoveSpeed = 3f;
        AttackRange = 2f;
        AttackCooldown = 1f;
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