using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb = new Rigidbody();
    Animator animator;

    public float move_speed = 1.5f;
    public float jump_force = 3f;
    public float move_direction;
    public bool is_can_move = true;
    public bool is_on_ground;
    public int jump_counter = 0;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Move();
    }

    
    
    // MOVE
    public void Move()
    {
        if (is_can_move)
        {
            rb.transform.Translate(move_speed * move_direction * Time.deltaTime, 0, 0);
        }
    }

    public void OnMove(InputValue value)
    {
        move_direction = value.Get<float>();
        Debug.Log("OnMove " + move_direction);

        if (move_direction == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move_direction == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }


    // JUMP
    public void OnJump(InputValue value)
    {
        if (jump_counter < 2)
        {
            //rb.AddForce(new Vector3(0, jump_force, 0), ForceMode.VelocityChange);
            rb.velocity = new Vector3(rb.velocity.x, jump_force, rb.velocity.z);
            Debug.Log("OnJump");
        }
        jump_counter += 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            is_on_ground = true;
            jump_counter = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            is_on_ground = false;
        }
    }



    // ATTACK
    public void OnAttack(InputValue input)
    {
        Debug.Log("Attack ");
    }

    public void AttackStart()
    {
        is_can_move = false;
    }

    public void AttackEnd()
    {
        is_can_move = true;
    }



    
    // SPINTAIL
    public void OnSpintail(InputValue value)
    {
        Debug.Log("Spintail ");
    }



}
