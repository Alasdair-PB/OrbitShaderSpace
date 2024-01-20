using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPath : MonoBehaviour
{
    [SerializeField] private float playerToCamRadius;
    [SerializeField] private GameObject target;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float bodyHeight;
    [SerializeField] private float forwardTranslation;
    [SerializeField] private float followSpeed;
    [SerializeField] private float theta;
    [SerializeField] private float phi;


    private float rotationX;
    private float rotationY;


    public Vector2 orbitPath; 



    // Update is called once per frame
    void FixedUpdate()
    {
        //RotateAroundPlayer(orbitPath);
        theta = theta + (orbitPath.x * Time.deltaTime);
        phi = phi + (orbitPath.y * Time.deltaTime);

        Orbit(theta, phi);
    }

    void LateUpdate()
    {
        //Orbit();
    }

    private void RotateAroundPlayer(Vector2 direction)
    {
        float angle = GetAngleOpposite();
        rotationY -= direction.x * rotationSpeed * Time.deltaTime;
        //if ((angle <= 0.8f && angle >= 0.05f) || (angle > 0.8f && rightStickVal.y < 0) || (angle < 0.05f && rightStickVal.y > 0))
        //{
        rotationX += direction.y * rotationSpeed * Time.deltaTime;
        //}

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
    }

    public void Orbit(float theta, float phi)
    {
        Vector3 nextPos = new Vector3();
        //RotateAroundPlayer();

        //float theta = GetAngleInRadians(transform.rotation.eulerAngles.y);
        //float phi = GetAngleInRadians(transform.rotation.eulerAngles.x);

        theta -= Mathf.PI / 2;

        theta = theta % (2 * Mathf.PI);
        phi = phi % (2 * Mathf.PI);
        nextPos = SphericalToCartesian(playerToCamRadius, -phi, -theta);
        nextPos += target.transform.position;
        nextPos.y += bodyHeight;
        nextPos += target.transform.forward * forwardTranslation;
        transform.position = Vector3.MoveTowards(transform.position, nextPos, Time.deltaTime * followSpeed); ;

    }

    public float GetAngleInRadians(float rotationAxis)
    {
        float angle = rotationAxis + 270;
        angle -= angle * 2 + 180;
        angle *= Mathf.PI / 180;
        return angle;
    }

    public float GetAngleOpposite()
    {
        float targetDis = Mathf.Sqrt(Mathf.Abs(
        (target.transform.position.x - transform.position.x) *
        (target.transform.position.x - transform.position.x) +
        ((target.transform.position.z - transform.position.z) *
        (target.transform.position.z - transform.position.z))));
        targetDis = Mathf.Abs((target.transform.position.y - transform.position.y)) /
        Mathf.Abs(targetDis);
        float angle;

        if ((targetDis < (Mathf.PI / 4)) && (targetDis > -(Mathf.PI / 4)))
        {
            angle = Mathf.Asin(targetDis);
            //print(angle);
        }
        else
        {
            angle = 0.9f;
        }
        return angle;

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

}
