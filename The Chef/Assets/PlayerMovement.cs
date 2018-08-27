using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {

    public const int walkingSpeed = 4;
    public const int runningSpeed = 8;

    public float runningStopBuffer = 1.5f;

    NavMeshAgent agent;
    public bool debugMode = false;
    bool moving;
    Animator animator;
    float mouseDownTimer;
    float touchHoldTimer;


    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("Grounded", true);
	}
	
	// Update is called once per frame
	void Update () {
        //Debug Mode is mouse controlled
        if (debugMode) {
            DebugModeUpdate();
        } else {
            //Walk if the user taps the screen, Run if the user holds touch
            if (Input.touchCount > 0) {
                touchHoldTimer += Time.deltaTime;
                if (touchHoldTimer < 0.2) {
                    agent.speed = walkingSpeed;
                } else {
                    agent.speed = runningSpeed;
                }
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100)) {
                    agent.SetDestination(hit.point);
                }
            } else {
                touchHoldTimer = 0;
                //Stop the agent if they are running with no user input
                if (agent.speed == runningSpeed) {
                    agent.speed = walkingSpeed;
                    Vector3 moveDirection = Vector3.Normalize(agent.destination - agent.transform.position) * runningStopBuffer;
                    agent.SetDestination(agent.transform.position + moveDirection);
                }
            }

            //animation
            if (agent.remainingDistance <= agent.stoppingDistance) {
                moving = false;
            } else {
                moving = true;
            }
            animator.SetBool("Walking", moving);
            animator.SetFloat("Speed", agent.speed);

        }
    }

    void DebugModeUpdate() {
        if (Input.GetMouseButton(0)) {
            mouseDownTimer += Time.deltaTime;
            if (mouseDownTimer < 0.2) {
                agent.speed = walkingSpeed;
            } else {
                agent.speed = runningSpeed;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                agent.SetDestination(hit.point);

            }
        } else {
            mouseDownTimer = 0;
            //Stop the agent if they are running with no user input
            if (agent.speed == runningSpeed) {
                agent.speed = walkingSpeed;
                Vector3 moveDirection = Vector3.Normalize(agent.destination - agent.transform.position) * runningStopBuffer;
                agent.SetDestination(agent.transform.position + moveDirection);
            }
        }

        //animation
        if (agent.remainingDistance <= agent.stoppingDistance) {
            moving = false;
        } else {
            moving = true;
        }
        animator.SetBool("Walking", moving);
        animator.SetFloat("Speed", agent.speed);
    }
}
