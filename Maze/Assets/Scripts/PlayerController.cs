using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Development List
// Attack Button Improvement
// Enemy Creation

public enum ActionState
{
    Idle,
    Walk,
    Attack,
    Die,
}
// TODO:
// 解决人物漂浮问题

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;
    public float walkSpeed = 6.0F;
    public const float ATTACK_RATE = 2.0f;
    public const float ATTACK_RANGE = 2.0f;
    // public float jumpSpeed = 8.0F;
    // public float gravity = 20.0F;
    public VariableJoystick joystick;
    public int dir = 0;
    public ActionState state;
    Vector3 cameraOffset;
    Camera camera;
    Vector3 movement;
    public int hp = 100;
    

    void Start()
    {                 
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        joystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        state = ActionState.Idle;
        camera = GameObject.Find("FirstPersonView").GetComponent<Camera>();
        this.cameraOffset = Camera.main.transform.position - this.transform.position;
    }
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector3 pos = Input.GetTouch(0).deltaPosition;
            transform.localEulerAngles += new Vector3(0, pos.x);
        }
    }

    void FixedUpdate() {
        // TODO: Do we really need ActionState?
        movement = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        switch (state)
        {
            case (ActionState.Idle):
                animator.SetBool("isWalk", false);
                check();
                break;
            case (ActionState.Walk):
                move();
                break;
            case (ActionState.Attack):
                hit();
                break;
        }
    }

    void check() {
        if (movement != Vector3.zero) {
            this.state = ActionState.Walk;
        } else {
            this.state = ActionState.Idle;
        }
    }
    void move() {
        float rotateY = 0f;
        if (joystick.Vertical > 0) {
            rotateY = Mathf.Atan(joystick.Horizontal / joystick.Vertical) * 180 / Mathf.PI;
        }else if (joystick.Vertical < 0 && joystick.Horizontal < 0)
        {
            rotateY = -180 + Mathf.Atan(joystick.Horizontal / joystick.Vertical) * 180 / Mathf.PI;
        }
        else if (joystick.Vertical < 0 && joystick.Horizontal > 0)
        {
            rotateY = 180 + Mathf.Atan(joystick.Horizontal / joystick.Vertical) * 180 / Mathf.PI;
        }
        this.transform.Rotate(0, rotateY * 1 / 180, 0);
        characterController.SimpleMove(this.transform.forward * 5);
        animator.SetBool("isWalk", true);
        
        // characterController.Move(movement * runSpeed * Time.deltaTime);
        // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * 10);
        Camera.main.transform.position = transform.position + this.cameraOffset;
        check();
    }

    public void resetHP()
    {
        this.hp = 100;
    }

    public void loseHP(int toLose)
    {
        this.hp -= toLose;
        print(this.hp);
        // need to do failure check
        if(this.hp <= 0)
        {
            GameManager.instance.LoseGame();
        }
    }

    private void hit() {
        Collider[] nearByObject = Physics.OverlapSphere(transform.position, ATTACK_RANGE);
        animator.SetTrigger("attack");
        foreach (Collider obj in nearByObject)
        {
            if ("Zombie" == obj.gameObject.tag)
            {
                obj.GetComponent<EnemyAI>().loseHP(10);
                break; // only attack one zombie
            }
        }
        check();
    }

    public void triggerAttack() {
        this.state = ActionState.Attack;
    }
}
