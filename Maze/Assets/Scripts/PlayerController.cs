using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO: 
// To create enemy character, we can create an abstract class Character
// and let PlayerController and EnemyController implements Character class
public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Animation animation;
    public float walkSpeed = 6.0F;
    public float runSpeed = 10.0F;
    // public float jumpSpeed = 8.0F;
    // public float gravity = 20.0F;
    public VariableJoystick joystick;
    public int dir = 0;
    Vector3 cameraOffset;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animation = GetComponent<Animation>();
        joystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        this.cameraOffset = Camera.main.transform.position - this.transform.position;
    }

    // TODO: Add the other 4 directions.
    // Update is called once per frame
    void Update()
    {
        this.move();
    }

    void move() {
        // movement.y -= gravity * Time.deltaTime;
        // print (joystick.Direction);
        Vector3 movement = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        if (movement != Vector3.zero)
        {
            animation.Play("walk_00");
            characterController.Move(movement * runSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * 10);
            Camera.main.transform.position = transform.position + this.cameraOffset;
        }
        else
        {
            animation.Play();
        }
    }
}
