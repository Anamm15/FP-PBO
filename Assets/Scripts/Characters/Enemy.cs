public class Enemy : Character 
{
    private Transform player;
    private Vector2 targetPosition;
    private List<Vector2> path = new List<Vector2>();
    public float gridSize = 1f;
    public float pathUpdateInterval = 1f;
    private Coroutine pathUpdateCoroutine;

    public override void Start() {
        base.Start();

        try
        {
            GameObject playerObject = GameObject.Find("Player"); 
            player = playerObject.transform; 
            targetPosition = player.position; 
            lastPlayerPosition = player.position;
        }
        catch (System.Exception e)
        {
            throw e;
        }

        pathUpdateCoroutine = StartCoroutine(UpdatePathPeriodically());
    }

    public override void Update() {
        base.Update();

        if (path.Count > 0)
        {
            MoveAlongPath();
        }
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

    public void FindPath() {
        path.Clear();

        Vector2 startPos = transform.position;
        Vector2 endPos = player.position;

        path.Add(startPos);
        path.Add(endPos);

        yield return null;
    }

    private void MoveAlongPath()
    {
        if (path.Count > 0)
        {
            Vector2 target = path[0];
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            m_animator.SetInteger("AnimState", 1)        
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
                    AttackPlayer();
                    lastAttackTime = Time.time;
                }
            }
            else if (Vector2.Distance(transform.position, target) < 0.1f)
            {
                path.RemoveAt(0);
            }
        }
    }

    public override void Attack() {

    }

    public override void Run() {

    }

    public override void Hurt() {

    }

    public override void Die() {

    }
}