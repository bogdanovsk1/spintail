using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb = new Rigidbody();
    public InputActionAsset inputActions;
    ToMouseRotator toMouseRotator;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public float move_speed = 1.5f;
    public float jump_force = 3f;
    public float launch_force = 5f;
    public float move_direction;
    public bool is_can_move = true;
    public bool is_on_ground;
    public bool is_spintail_started = false;
    public int jump_counter = 0;

    
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        toMouseRotator = GetComponentInChildren<ToMouseRotator>();

        inputActions.FindActionMap("Side").FindAction("Spintail").canceled += OnSpintailCancelled;


        //inputActions.FindActionMap("Side").FindAction("Spintail").started += ctx => Debug.Log("Spintail Started " + ctx.started + "\n"
        //    + "is can move " + is_can_move);
        //inputActions.FindActionMap("Side").FindAction("Spintail").performed += ctx => animator.SetBool("is_spintail_started", true);



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

        if (move_direction == 1)
        {
            //transform.localScale = new Vector3(1, 1, 1);
            //transform.Rotate(0f, 0f, 0f);
            spriteRenderer.flipX = false;
        }
        else if (move_direction == -1)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            //transform.Rotate(0f, 180f, 0f);
            spriteRenderer.flipX = true;
        }

        animator.SetFloat("is_running", Mathf.Abs(move_direction));
    }


    // JUMP
    public void OnJump(InputValue value)
    {
        if (jump_counter < 2 && is_can_move)
        {
            rb.velocity = new Vector3(rb.velocity.x, jump_force, rb.velocity.z);
            jump_counter += 1;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            is_on_ground = true;
            jump_counter = 0;
            animator.SetBool("is_on_ground", is_on_ground);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            is_on_ground = false;
            animator.SetBool("is_on_ground", is_on_ground);
        }
    }



    // ATTACK
    public void OnAttack(InputValue input)
    {
        animator.SetTrigger("attack_trigger_anim");

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
        is_can_move = false;

        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        //Time.timeScale = 0.1f;

        is_spintail_started = true;
        animator.SetBool("is_spintail_started", true);

        Debug.Log("OnSpintail");
    }

    public void OnSpintailCancelled(InputAction.CallbackContext context)
    {
        
        
        //Time.timeScale = 1f;
        
        animator.SetBool("is_spintail_started", false);

        
        if (is_spintail_started)
        {
            rb.velocity = launch_force * toMouseRotator.difference;
        }
            
        rb.useGravity = true;

        is_can_move = true;
        is_spintail_started = false;
        Debug.Log("OnSpintail Cancelled");
    }


}
