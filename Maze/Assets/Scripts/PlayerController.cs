using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Development List
// Attack Button Improvement
// Enemy Creation

public enum ActionState
{
    Idle,
    Walk,
    Attack,
}

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;
    public float walkSpeed = 6.0F;
    public float runSpeed = 10.0F;
    // public float jumpSpeed = 8.0F;
    // public float gravity = 20.0F;
    public VariableJoystick joystick;
    public int dir = 0;
    public ActionState state;
    Vector3 cameraOffset;
    public bool isAttack = false;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        joystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        state = ActionState.Idle;
        this.cameraOffset = Camera.main.transform.position - this.transform.position;
    }
    void Update()
    {　
        // TODO: Do we really need ActionState?
        Vector3 movement = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        if (isAttack) {
            state = ActionState.Attack;
        } else {
            if (movement != Vector3.zero) {
                state = ActionState.Walk;
            } else {
                state = ActionState.Idle;
            }
        }

        switch (state)
        {
            case (ActionState.Walk):
                move(movement);
                break;
            case (ActionState.Attack):
                attack();
                break;
            default:
                animator.SetBool("isWalk", false);
                break;
        }
    }

    void move(Vector3 movement) {
        animator.SetBool("isWalk", true);
        characterController.Move(movement * runSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * 10);
        Camera.main.transform.position = transform.position + this.cameraOffset;
    }

    public void attack() {
        animator.SetTrigger("attack");
        isAttack = false;
    }
}
