using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMotionHandler : MonoBehaviour
{

    public float smoothSpeed = 0.125f;
    public float playerToCamRadius;

    public float cameraHeight;
    public float rotationSpeed;
    public float verticalRotationSpeed; 
    public float offsetAngle;
    private float rotationX;
    private float rotationY;

    private Vector3 velocity = Vector3.zero;
    public Transform target;

    public Vector2 rightStickVal;
    public bool followPlayer = true;

    private Movement baseMovement;
    public GameObject player;


    public static CameraMotionHandler instance;

    void OnEnable()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    void Awake(){
        baseMovement = player.GetComponent<Movement>();
    }

    void FixedUpdate()
    {
        RotateAroundPlayer();
    }

    void LateUpdate()
    {
        if (followPlayer)
        {
            Vector3 nextPos = new Vector3();
            //RotateAroundPlayer();

            float theta = GetAngleInRadians(transform.rotation.eulerAngles.y );
            float phi = GetAngleInRadians(transform.rotation.eulerAngles.x);

            theta -= Mathf.PI / 2;

            theta = theta % (2 * Mathf.PI);
            phi = phi % (2 * Mathf.PI);
            nextPos = SphericalToCartesian(playerToCamRadius, -phi, -theta);
            nextPos += target.transform.position;
            nextPos.y += cameraHeight; 
            transform.position = nextPos;

        }
    }

    private Vector3 SphericalToCartesian(float p, float phi, float theta)
    {   
        Vector3 nextPos = new Vector3();
        nextPos.x = p * Mathf.Sin(phi) * Mathf.Sin(theta);
        nextPos.z = p * Mathf.Sin(phi) * Mathf.Cos(theta);
        phi += Mathf.PI;
        nextPos.y = p * Mathf.Cos(phi);
        return nextPos;
    }

    /*
    private Vector3 CartesianToSpherical(float x, float y, float z, float phi, float theta)
    {
        float p = Mathf.Sqrt(x ** 2 + y ** 2 + z ** 2);
        float phi = Mathf.ArcCos(z/p);
        float theta = Mathf.ArcTan(y / x);
        return new Vector3(p, phi, theta);

    }*/

    public Vector3 SetDirectionByCamera(Vector2 playerInput, float cameraAngle, bool returnIfZero)
    {
        float angle = 0;
        Vector3 directionByCamera = new Vector3();
        if (playerInput.x != 0 || playerInput.y != 0)
        {
            angle = Mathf.Asin(playerInput.y / Mathf.Sqrt((playerInput.x * playerInput.x) + (playerInput.y * playerInput.y)));
            if (Mathf.Sign(playerInput.x) == -1)
            {
                if (angle == 0)
                    angle += Mathf.PI;
                else if (Mathf.Sign(playerInput.y) == 1)
                {
                    if (angle > ((3 * Mathf.PI) / 4))
                        angle += Mathf.PI / 2;
                    else
                        angle = Mathf.PI - angle;
                }
                else
                {
                    if (angle <= ((5 * Mathf.PI) / 4))
                        angle = Mathf.PI - angle;
                    else
                        angle -= Mathf.PI / 2;
                }
            }
            angle += cameraAngle + Mathf.PI / 2;
        }
        else if (returnIfZero) // Literally only applies for rolls and things- this isn't really needed \(0u0)/
            angle = cameraAngle - 2 * Mathf.PI;
        else
            return directionByCamera;
        float deltaX = Mathf.Cos(angle);
        float deltaZ = Mathf.Sin(angle);
        directionByCamera = new Vector3(deltaX, 0, deltaZ);
        return directionByCamera;
    }



    private void RotateAroundPlayer()
    {
        float angle = GetCameraAngleOpposite();
        rotationY += rightStickVal.x * rotationSpeed * Time.deltaTime;
        //if ((angle <= 0.8f && angle >= 0.05f) || (angle > 0.8f && rightStickVal.y < 0) || (angle < 0.05f && rightStickVal.y > 0))
        //{
        rotationX += rightStickVal.y * verticalRotationSpeed * Time.deltaTime;
        //}

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
    }

    public float GetAngleInRadians(float rotationAxis)
    {
        float angle = rotationAxis + 270;
        angle -= angle * 2 + 180;
        angle *= Mathf.PI / 180;
        return angle; 
    }

    public float GetCameraAngleOpposite()
    {
        float cameraToTargetDis = Mathf.Sqrt(Mathf.Abs(
        (target.transform.position.x - transform.position.x) *
        (target.transform.position.x - transform.position.x) +
        ((target.transform.position.z - transform.position.z) *
        (target.transform.position.z - transform.position.z))));
        cameraToTargetDis = Mathf.Abs((target.transform.position.y - transform.position.y)) /
        Mathf.Abs(cameraToTargetDis);
        float angle;

        if ((cameraToTargetDis < (Mathf.PI/4)) && (cameraToTargetDis > -(Mathf.PI/4)))
        {
            angle = Mathf.Asin(cameraToTargetDis);
            //print(angle);
        }
        else
        {
            angle = 0.9f;
        }
        return angle;

    }

    // If the Camera enters a collison the camers should push forwards or stop the wall from rendering, may need to change perpective settings
    void OnTriggerStay(Collider other)
    {
        // change perpective/or position
    }
    void OnTriggerExit()
    {
        // return to default pos
    }

    // Called by Input System automatically
    private void OnCameraMotion(InputValue input)
    {
        rightStickVal = input.Get<Vector2>();
    }

    
}
