using UnityEngine;

public class Wizard : Enemy
{
    public override void Start()
    {
        base.Start();

        MoveSpeed = 1f;
        AttackRange = 2f;
        AttackCooldown = 1;
        Name = "Wizard";
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