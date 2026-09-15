using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb = new Rigidbody();
    public InputActionAsset inputActions;
    Animator animator;

    public float move_speed = 1.5f;
    public float jump_force = 3f;
    public float move_direction;
    public bool is_can_move = true;
    public bool is_on_ground;
    public bool is_spintail_started = false;
    public int jump_counter = 0;

    
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        inputActions.FindActionMap("Side").FindAction("Spintail").canceled += OnSpintailCancelled;


        //inputActions.FindActionMap("Side").FindAction("Spintail").started += ctx => Debug.Log("Spintail Started " + ctx.started + "\n"
        //    + "is can move " + is_can_move);
        //inputActions.FindActionMap("Side").FindAction("Spintail").performed += ctx => animator.SetBool("is_spintail_started", true);



    }

    private void Update()
    {
        Move();
        //Spintail();
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
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move_direction == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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
        //Time.timeScale = 0.5f;
        is_spintail_started = true;
        animator.SetBool("is_spintail_started", true);
    }

    public void OnSpintailCancelled(InputAction.CallbackContext context)
    {
        is_can_move = true;
        //Time.timeScale = 1f;
        is_spintail_started = false;
        animator.SetBool("is_spintail_started", false);
    }


}
