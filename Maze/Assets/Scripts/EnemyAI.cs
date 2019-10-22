using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    IDLE,
    WALK,
    CHASE,
    ATTACK,
    RETURN,
}

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    private State state;
    private Animator animator;
    private Vector3 initialPosition;
    private int distance;
    private Quaternion targetRotation;

    private NavMeshAgent agent;
    public const float DEFEND_RADIUS = 4.0f;
    public const float CHASE_RADIUS = 8.0f;
    public const float ATTACK_RANGE = 0.5f;

    public float runSpeed = 0.5f;

    public float rotateSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        initialPosition = this.transform.position;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        state = State.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO:
        // 1. Add wander state.
        // 2. Add HP System
        switch (state)
        {
            case State.IDLE:
                playerDistanceCheck();
                break;

            case State.CHASE:
                chase();
                break;

            case State.RETURN:
                returnStart();
                break;
                
        }
    }

    void chase() {
        animator.SetBool("isWalking", true);
        // transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
        // targetRotation = Quaternion.LookRotation(player.transform.position - this.transform.position, Vector3.up);
        // this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, rotateSpeed);
        
        agent.SetDestination(player.transform.position);
        agent.Move(transform.TransformDirection(new Vector3(0, 0, runSpeed*Time.deltaTime)));

        chaseRadiusCheck();
    }

    void returnStart() {
        // targetRotation = Quaternion.LookRotation(initialPosition - this.transform.position, Vector3.up);
        // this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, rotateSpeed);
        // this.transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);

        agent.SetDestination(initialPosition);
        agent.Move(transform.TransformDirection(new Vector3(0, 0, runSpeed * Time.deltaTime)));

        returnBackCheck();
    }

    void playerDistanceCheck() {
        float distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        if (distanceToPlayer < ATTACK_RANGE) {
            animator.SetTrigger("standHit");
        }
        if (distanceToPlayer < DEFEND_RADIUS && distanceToPlayer >= ATTACK_RANGE) {
            state = State.CHASE;
        }
    }
    void chaseRadiusCheck() {
        float distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        float distanceToStart = Vector3.Distance(this.transform.position, this.initialPosition);

        // TODO:
        // 1. debounce 
       if (distanceToPlayer < ATTACK_RANGE) {
            // Hit player
            animator.SetTrigger("hit");
            // run attack method
        }

        // if enemy distance larget than chase raidus, enemy return back
        if (distanceToStart > CHASE_RADIUS) {
            state = State.RETURN;
        }
    }

    void returnBackCheck() {
        float distanceToStart = Vector3.Distance(this.transform.position, initialPosition);
        if (distanceToStart < 0.1f) {
            animator.SetBool("isWalking", false);
            state = State.IDLE;
        }
    }
}
