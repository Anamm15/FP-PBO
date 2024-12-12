using UnityEngine;
public class Player : Character {
    
    public override void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Update() {
        Move();
        HandleInput();
    }

    public void Move() {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
            transform.localScale = new Vector3(-4.15f, 4.15f, 4.15f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
            transform.localScale = new Vector3(4.15f, 4.15f, 4.15f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
        }
        dir.Normalize();
        animator.SetBool("walk", dir.magnitude > 0);
        rb.velocity = MoveSpeed * dir;
    }

    private void HandleInput() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("attack");
            Attack();
        }
        // else if (Input.GetMouseButtonDown(1))
        // {
        //     animator.SetTrigger("Attack2");
        //     Attack();
        // }
        // else if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     animator.SetTrigger("Attack3");
        //     Attack();
        // }
        // else if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon || Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Epsilon)
        // {
        //     animator.SetInteger("AnimState", 1);
        // }
        // else
        // {
        //     animator.SetInteger("AnimState", 0);
        // }
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