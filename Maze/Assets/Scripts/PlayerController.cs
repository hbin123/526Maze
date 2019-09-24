using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DIRECTION
{
    UP = 0,
    UPRIGHT = 1,
    RIGHT = 2,
    DOWNRIGHT = 3,
    DOWN = 4,
    DOWNLEFT = 5,
    LEFT = 6,
    UPLEFT = 7,
}

enum ActionState
{
    Walk,
    Idle,
}

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Animation animation;
    public float speed = 6.0F;
    // public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 movement = Vector3.zero;
    public int dir = 0;
    Vector3 cameraOffset;

    public int[] dx = new int[8] {0, 1, 1, 1, 0, -1, -1, -1};
    public int[] dy = new int[8] {1, 1, 0, -1, -1, -1, 0, 1};
    public int[] rotation = new int[8] {0, 45, 90, 135, 180, -135, -90, -45};
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animation = GetComponent<Animation>();
        this.cameraOffset = Camera.main.transform.position - this.transform.position;
    }

    // TODO: Add the other 4 directions.
    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;
        // if (characterController.isGrounded)
        // {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");

            int dir = this.GetDirection(moveHorizontal, moveVertical);
            if (dir == 0 || dir == 4) {
                movement = new Vector3(moveHorizontal * this.dx[dir], 0, moveVertical * this.dy[dir]);
            } else {
                movement = new Vector3(moveVertical * this.dy[dir], 0, moveHorizontal * this.dx[dir]);
            }
            movement = transform.TransformDirection(movement);
            movement *= speed;
            // if (Input.GetButton("Jump"))
                // movement.y = jumpSpeed;
        // }
        movement.y -= gravity * Time.deltaTime;
        if (Mathf.Abs(moveHorizontal) > 0.2 || Mathf.Abs(moveVertical) > 0.2) {
            animation.Play("walk_00");
            characterController.Move(movement * Time.deltaTime);

            Vector3 e_rot = transform.eulerAngles;
            // print(dir);
            e_rot.y = rotation[dir];
            transform.eulerAngles = e_rot;
            // set camera position
            Camera.main.transform.position = transform.position + this.cameraOffset;
            // 
        } else {
            animation.Play();
        }
    }

    private int GetDirection(float x, float y) 
    {
        if (Mathf.Abs(x) >= Mathf.Abs(y)) {
            if (x >= 0) return (int)DIRECTION.RIGHT;
            else return (int)DIRECTION.LEFT;
        }
        else {
            if (y >= 0) return (int)DIRECTION.UP;
            else return (int)DIRECTION.DOWN;
        }
        return 0;
    }
}
