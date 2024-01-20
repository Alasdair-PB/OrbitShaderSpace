using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;


[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Movement : MonoBehaviour
{
    [SerializeField] private new Camera camera; 
    [SerializeField, Range(0f, 100f)] float maxSpeed = 10f, maxAcceleration = 10f, gravityStrength = 10f, gravityAcceleration = 4f, fallMultiplier = 3f, lowJumpMultiplier = 4f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] private float heightFromGround, rotationSpeed, runningSpeed;
    [SerializeField] private GameObject leftFoot, rightFoot, leftHand, rightHand;

    private IKFootSolverV2 leftFootIKSolver, rightFootIKSolver;
    private IKHandSolver leftHandIKSolver, rightHandIKSolver;
    [SerializeField] private TwoBoneIKConstraint leftHandConstraint, rightHandConstraint, leftFootConstraint, rightFootConstraint; 

    private Rigidbody body;
    private Animator animator;

    private Vector3 velocity, inputAsV3, targetRot;
    private Vector2 playerInput;
    public bool running { get; private set; }
    private bool grounded; 

    private CameraMotionHandler cameraMotionHandler; 

    private Vector3 direction;

    private bool jumpTriggerSet; 


    void Awake()
    {
        leftFootIKSolver = leftFoot.GetComponent<IKFootSolverV2>();
        rightFootIKSolver = rightFoot.GetComponent<IKFootSolverV2>();
        leftHandIKSolver = leftHand.GetComponent<IKHandSolver>();
        rightHandIKSolver = rightHand.GetComponent<IKHandSolver>();

        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cameraMotionHandler = camera.GetComponent<CameraMotionHandler>();
    }

    void Update()
    { 
        //Vector3 quickMathA = leftHand.transform.position - leftHandIKSolver.contactPoint;
        //Vector3 quickMathB = rightHand.transform.position - rightHandIKSolver.contactPoint;
    }

    void FixedUpdate()
    {

        ManageInputs();
        Vector2 lastPlayerInput = playerInput; 
        direction = GetDirection(lastPlayerInput); 
        CheckGrounded();
        ManageAnimator();
        ManageRotation(direction);

        if (grounded)
            if (jumpTriggerSet)
                Jump();
            else
                ManageIKTransform();
        else
            WeightedGravity();

        // If the hands are not colliding with a surface then move forwards
        if (leftHandConstraint.weight == 0 && rightHandConstraint.weight == 0)
        {
            ManageVelocity(direction);
            Decelerate();
        }
        else
            body.velocity = Vector3.zero; 
    }


    private void ManageCombat()
    {
        // Cancel all functions that check for foot position
    }

    private void ManageInputs()
    {
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        if (playerInput.x == 0 && playerInput.y == 0)
            running = false;
        else
        {
            running = true;
            //inputAsV3 = GetDirection();
            inputAsV3 = new Vector3(playerInput.x, 0, playerInput.y);
        }
    }

    private void ManageAnimator()
    { 
        if (jumpTriggerSet)
            animator.SetBool("Jump", true);

        // Cannot use inputAsV3 as the animator will not be updated when speed is 0
        animator.SetFloat("Speed", new Vector3(playerInput.x, 0, playerInput.y).magnitude);
        animator.SetBool("Grounded", grounded);

    }

    private void ManageIKTransform()
    {
        Vector3 targetPos = ManageFootIK();
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * gravityAcceleration); //* angle of incline in speed not up distance
    }


    private void CheckGrounded()
    {
        // If foot wieght is 0 then niether foot is colliding with the ground
        if (leftFootConstraint.weight == 0 && rightFootConstraint.weight == 0)
            grounded = false; 
        else 
            grounded = true; 
    }


    // Adjusts Unity's gravity to make the jump follow principles of animation
    private void WeightedGravity()
    {
        if (body.velocity.y < 0)
        {
            body.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (body.velocity.y > 0) // Should detect if the jump button isn't held to get variable jump height based on holding the button. 
        {
            body.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }


    /// <summary>
    /// Motion is split between rigidbody velocity and Vector3.Movetowards methods. 
    /// Rigid bodies are used for dynamic motion patterns- e.g jumping, running, walking.
    /// While the transform is edited for minor position corrections and rotation edits- 
    ///     such as positioning both feet on the floor using the IK system
    /// </summary>
    private void ManageVelocity(Vector3 direction)
    {
        //Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        Vector3 desiredVelocity = direction * maxSpeed;

        velocity = body.velocity;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        body.velocity = velocity;
        // body.MovePosition(transform.position + desiredVelocity * maxSpeedChange); 
    }

    public void Jump()
    {
        jumpTriggerSet = false; 
        body.AddForce(Vector3.up * jumpSpeed);
    }

    private void ManageRotation(Vector3 direction)
    {
        targetRot = Vector3.RotateTowards(transform.forward, direction, Time.deltaTime * rotationSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(targetRot);
        // Quaternion.Slerp(transform.rotation, QuaternionTarget, Time.deltaTime * rotationSpeed)
    }

    private Vector3 ManageFootIK()
    {
        Vector3 targetPos = transform.position;
        if (body.velocity.y < 0.2f)
        {
            if (!grounded)
                return new Vector3(targetPos.x, targetPos.y - gravityStrength, targetPos.z);

            body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z);
            if (leftFootIKSolver.contactPoint.y > rightFootIKSolver.contactPoint.y)
                targetPos.y = (leftFootIKSolver.contactPoint.y + heightFromGround) / 3 +
                    (rightFootIKSolver.contactPoint.y + heightFromGround) * 2 / 3;
            else
                targetPos.y = (leftFootIKSolver.contactPoint.y + heightFromGround) * 2 / 3 +
                    (rightFootIKSolver.contactPoint.y + heightFromGround) / 3;
        }
        return targetPos;
    }

    // Called by Input System automatically
    private void OnMove(InputValue input)
    {
        playerInput = Vector2.ClampMagnitude(input.Get<Vector2>(), 1f); ;
    }

    private Vector3 GetDirection(Vector2 lastPlayerInput)
    {
        float angle = cameraMotionHandler.GetAngleInRadians(camera.transform.rotation.eulerAngles.y);
        return cameraMotionHandler.SetDirectionByCamera(lastPlayerInput, angle, false);
    }


    public void OnJump()
    {
        jumpTriggerSet = true; 
    }

    private void Decelerate()
    {
        /*
        if (dash == true && !(currentSpeed > dashSpeed))
        {
            currentSpeed += acceleration;
        }
        if (dash == false && currentSpeed > baseSpeed)
        {
            currentSpeed -= deceleration;
        }
        else if (currentSpeed < baseSpeed)
        {
            currentSpeed = baseSpeed;
        }*/
    }


}
