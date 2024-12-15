using UnityEngine;

public class Orc : Enemy
{
    public override void Start()
    {
        base.Start();

        Name = "Orc";
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