using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private PlayerController _PlayerController;
    Animator animator;

    void Start()
    {
        
    }

    public void Awake()
    {
        animator = GetComponent<Animator>();
        _PlayerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        Move();
    }

    // MOVE ANIMATION
    public void Move()
    {
        if ((Mathf.Abs(_PlayerController.move_direction) == 1) && _PlayerController.is_on_ground && _PlayerController.is_can_move)
        {
            animator.Play("Player_Run");
            Debug.Log("pussy tight " + _PlayerController.move_direction);
        }
        else if (!_PlayerController.is_on_ground && _PlayerController.is_can_move)
        {
            animator.Play("Player_Jump");
        }
        else if ()
        else animator.Play("Player_Idle");
    }

    // JUMP ANIMATION
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        animator.Play("Player_Idle");
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        animator.Play("Player_Jump");
    //    }
    //}
}
