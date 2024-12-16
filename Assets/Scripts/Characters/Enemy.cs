using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy : Character 
{
    private Transform player;
    protected List<Vector2> path = new List<Vector2>();
    public float gridSize;
    public float pathUpdateInterval;
    private Coroutine pathUpdateCoroutine;
    private float lastAttackTime;
    private AStarPathfinding pathfinding;
    private bool _ideDead;


    public override void Start() {
        base.Start();

        try {
            GameObject playerObject = GameObject.Find("Player"); 
            player = playerObject.transform; 
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            gridSize = 1f;
            pathUpdateInterval = 1f;
            _ideDead = false;
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

        List<Vector2> newPath = null;
        yield return Task.Run(() => {
            newPath = pathfinding.FindPath(startPos, endPos);
        });

        path = newPath ?? new List<Vector2>();
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
        Hurt();
        int restDamage = damage - Armor;
        HealthPoint -= restDamage;

        if (HealthPoint <= 0 && !_ideDead) {
            _ideDead = true;
            StartCoroutine(Die());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(AttackDamage);
            }
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
        Statistics.IncrementKillRecord();
        Destroy(gameObject);
    }
}