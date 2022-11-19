using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public Animator playerAnimator;
    public bool canMove;
    public Vector3 currentClickedPoint;

    public float animationDuration;
    public AnimationClip plantClip;

    public float GetCurrentAnimationDuration()
    {
        animationDuration = plantClip.length;

        return animationDuration / 3;
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        DeactivateWalkAnimation();
    }

    public void MovePlayerToPoint(Vector3 position)
    {
        if (canMove)
        {
            _navMeshAgent.SetDestination(position);
            ActivateWalkAnimation();
        }
        else
        {
            return;
        }
    }

    public void ActivateWalkAnimation()
    {
        playerAnimator.SetBool("Walk",true);
    }

    public void DeactivateWalkAnimation()
    {
        if (transform.position.x == currentClickedPoint.x && transform.position.z == currentClickedPoint.z)
        {
            playerAnimator.SetBool("Walk", false);
        }
    }
}
