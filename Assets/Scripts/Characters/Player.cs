using UnityEngine;
public class Player : Character {


    public override void Start() {
        animator = GetComponent<Animator>();
    }

    public override void Update() {

    }

    public void Move() {
        float moveX = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;

        transform.Translate(new Vector3(moveX, moveY, 0), Space.World); 

        if (moveX > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0); 
        else if (moveX < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0); 
    }

    private void HandleInput() {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack1");
            Attack();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Attack2");
            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack3");
            Attack();
        }
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon || Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Epsilon)
        {
            animator.SetInteger("AnimState", 1);
        }
        else
        {
            animator.SetInteger("AnimState", 0);
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