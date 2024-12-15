using UnityEngine;

public class Skeleton : Enemy
{
    public override void Start()
    {
        base.Start();

        Name = "Skeleton";
        HealthPoint = 50;
        Armor = 1;
        AttackDamage = 10;
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