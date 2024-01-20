using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKFootSolverV2 : MonoBehaviour
{
    private enum RelativeDirection { left, right}
    [SerializeField] private RelativeDirection relativeDirection; 
    [SerializeField] private Animator animator;
    [SerializeField] public LayerMask layerMask;
    [SerializeField] private TwoBoneIKConstraint twoBoneIKConstraint;
    [SerializeField] private string animationStringRef;
    [SerializeField] private GameObject rootObject, player;
    [SerializeField] private float raycastMaxHeight, forwardDistanceMultiplier, raycastHeight, 
        verticalDistanceMultiplier, footSpacing, weightHeight, weightPower;
    [SerializeField] private Movement playerController; 

    private float height;
    private Ray ray;
    private Vector3 rayHeight;

    public Vector3 contactPoint { get; private set; }



    private void Awake()
    {
        transform.position = rootObject.transform.position;
    }

    private void Update()
    {
        //twoBoneIKConstraint.weight = animator.GetFloat(animationStringRef);
        //animator.GetFloat("IKLeftFootWeight"));
        //animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, animator.GetFloat("IKRightFootWeight"));
        rayHeight = player.transform.position;
        rayHeight.y += raycastHeight; 
        ray = new Ray(rayHeight + (player.transform.right * footSpacing), -player.transform.forward);
        //Debug.DrawRay(rayHeight + (player.transform.right * footSpacing), -player.transform.up, Color.red, 10, true);
        if (Physics.Raycast(ray, out RaycastHit info, raycastMaxHeight + 0.2f, layerMask))
        {
            if (info.transform.tag == "Walkable")
            {
                contactPoint = info.point;

                height = Mathf.Abs(rootObject.transform.position.y - info.point.y);

                if (height < weightHeight)
                {
                    twoBoneIKConstraint.weight = Mathf.Clamp(((1 / height) * weightPower), 0.01f, 0.9f);
                } else
                {
                    twoBoneIKConstraint.weight = 0;
                }
                transform.position = info.point - (player.transform.up * forwardDistanceMultiplier) + (Vector3.up * verticalDistanceMultiplier);

                Vector3 crossVector;

                crossVector = Vector3.Cross(transform.right, info.normal);
                transform.rotation = Quaternion.LookRotation(crossVector, info.normal);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);


            }
        } else
        {
            twoBoneIKConstraint.weight = 0;
        }
    }
}
