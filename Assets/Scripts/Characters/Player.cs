using UnityEngine;
public class Player : Character {


    public void Start() {
        animator = 
    }

    public override void Update() {

    }

    public void Move() {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Translate(new Vector3(moveX, moveY, 0), Space.World); 

        if (moveX > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0); 
        else if (moveX < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0); 
    }

    private void HandleInput {
        if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Attack1");
            Attack();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            m_animator.SetTrigger("Attack2");
            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            m_animator.SetTrigger("Attack3");
            Attack();
        }
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon || Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Epsilon)
        {
            m_animator.SetInteger("AnimState", 1);
        }
        else
        {
            m_animator.SetInteger("AnimState", 0);
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