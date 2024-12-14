using System.Collections;
using UnityEngine;

public class Player : Character {
    public override void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Name = "Player";
        HealthPoint = 1000;
        Armor = 5;
        AttackDamage = 30;
        AttackCooldown = 1;
        AttackRange = 2f;
        MoveSpeed = 5f;
    }

    public override void Update() {
        Move();
        HandleInput();
    }

    public void Move() {
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.A)) {
            dir.x = -1;
            transform.localScale = new Vector3(-4.15f, 4.15f, 4.15f);
        } 
        else if (Input.GetKey(KeyCode.D)) {
            dir.x = 1;
            transform.localScale = new Vector3(4.15f, 4.15f, 4.15f);
        }

        if (Input.GetKey(KeyCode.W)) {
            dir.y = 1;
        } 
        else if (Input.GetKey(KeyCode.S)) {
            dir.y = -1;
        }

        dir.Normalize();
        Walk(dir.magnitude > 0);
        rb.velocity = MoveSpeed * dir;
    }

    private void HandleInput() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J)) {
            animator.SetTrigger("Attack");
            Attack();
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
