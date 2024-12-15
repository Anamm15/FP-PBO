using System.Collections;
using UnityEngine;

public class Player : Character {
    private int comboStep = 0;
    private float comboResetTime = 1f;
    private float comboTimer = 0f;
    private bool canAttack = true;
    public override void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Name = "Player";
        HealthPoint = 1000;
        Armor = 5;
        AttackDamage = 30;
        AttackCooldown = 0.5f;
        AttackRange = 2f;
        MoveSpeed = 5f;
    }

    public override void Update() {
        Move();
        HandleInput();
        if (comboStep > 0) {
            comboTimer += Time.deltaTime;
            if (comboTimer > comboResetTime) {
                comboStep = 0;
                comboTimer = 0f;
            }
        }
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
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J)) && canAttack) {
            StartCoroutine(startAttackCooldown());
        }
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.K)) {
            specialAttack();
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

    public IEnumerator startAttackCooldown() {
        Attack();
        canAttack = false;
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }

    public override void Attack() {
        if (comboStep < 4) {
            comboStep++;
        } else {
            comboStep = 1;
        }
        animator.SetTrigger($"Attack{comboStep}");
        comboTimer = 0f;
    }

    public void specialAttack() {
        animator.SetTrigger("SpecialAttack");
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
