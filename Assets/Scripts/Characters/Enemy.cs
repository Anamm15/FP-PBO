using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character 
{
    private Transform player;
    protected List<Vector2> path = new List<Vector2>();
    public float gridSize = 1f;
    public float pathUpdateInterval = 1f;
    private Coroutine pathUpdateCoroutine;
    private float lastAttackTime;
    private AStarPathfinding pathfinding;


    public override void Start() {
        base.Start();

        try {
            GameObject playerObject = GameObject.Find("Player"); 
            player = playerObject.transform; 
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        } catch (System.Exception e) {
            throw e;
        }

        pathfinding = new AStarPathfinding(gridSize);
        pathUpdateCoroutine = StartCoroutine(UpdatePathPeriodically());
    }


    public override void Update() {
        base.Update();
    }

    private IEnumerator UpdatePathPeriodically()
    {
        while (true)
        {
            if (player != null)
            {
                StartCoroutine(FindPath());
            }
            yield return new WaitForSeconds(pathUpdateInterval);
        }
    }

    public IEnumerator FindPath() {
    Vector2 startPos = new Vector2(Mathf.Round(transform.position.x / gridSize) * gridSize, 
                                   Mathf.Round(transform.position.y / gridSize) * gridSize);
    Vector2 endPos = new Vector2(Mathf.Round(player.position.x / gridSize) * gridSize, 
                                 Mathf.Round(player.position.y / gridSize) * gridSize);

    path = pathfinding.FindPath(startPos, endPos);
    yield return null;
}


    protected void MoveAlongPath(float attackRange, float attackCooldown)
    {
        if (path.Count > 0)
        {
            Vector2 target = path[0];
            transform.position = Vector2.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime);
            Walk(true);
            if (target.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    Walk(false);
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
            else if (Vector2.Distance(transform.position, target) < 0.1f)
            {
                Walk(false);
                path.RemoveAt(0);
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        int restDamage = damage - Armor;
        HealthPoint -= restDamage;

        if (HealthPoint <= 0) {
            StartCoroutine(Die());
        }
    }

    public override void Attack() {
        animator.SetTrigger("Attack");
    }

    public override void Walk(bool state) {
        animator.SetBool("Walk", state);
    }

    public override void Hurt() {
        animator.SetTrigger("Hurt");
    }

    public override IEnumerator Die() {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}