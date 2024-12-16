using System.Collections;
using UnityEngine;

public class Player : Character {
    private int comboStep = 0;
    private float comboResetTime = 1f;
    private float comboTimer = 0f;
    private bool canAttack = true;
    private bool canSpecialAttack = true;
    private float specialAttackCooldown = 10f;
    private int specialAttackDamage = 70;
    public GameObject attackPoint;
    public GameObject specialAttackPoint;
    public override void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Name = PlayerPrefs.GetString("PlayerName") ?? "Player";
        HealthPoint = 1000;
        Armor = 5;
        AttackDamage = 20;
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
        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.K)) && canSpecialAttack) {
            StartCoroutine(startSpecialAttackCooldown());
        }
    }

    public void IncreaseHealth() {
        HealthPoint += 20;
    }

    public void IncreaseDamage() {
        AttackDamage += 5;
    }

    public void IncreaseArmor() {
        Armor += 1;
    }

    private void CollectItem(Collider2D collision) {
        Item item = collision.GetComponent<Item>();
        if (item != null) {
            item.ApplyEffect(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        CollectItem(collision);
    }

    public override void TakeDamage(int damage)
    {
        Hurt();
        int restDamage = damage - Armor;
        if (restDamage > 0) HealthPoint -= restDamage;

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
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, AttackRange);
        foreach (Collider2D enemy in hitEnemies) {
            if (enemy.CompareTag("Enemy")) {
                enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
            }
        }
    }

    public IEnumerator startSpecialAttackCooldown() {
        specialAttack();
        canSpecialAttack = false;
        yield return new WaitForSeconds(specialAttackCooldown);
        canSpecialAttack = true;
    }

    public void specialAttack() {
        animator.SetTrigger("SpecialAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(specialAttackPoint.transform.position, AttackRange);
        foreach (Collider2D enemy in hitEnemies) {
            if (enemy.CompareTag("Enemy")) {
                enemy.GetComponent<Enemy>().TakeDamage(specialAttackDamage); 
            }
        }
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
        InformationUI.PlayerDied();
    }
}