using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovementAI : MonoBehaviour
{
    private Animator animator;
    public GameObject theDestination;
    private NavMeshAgent theAgent;
    private bool isMoving = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        theAgent = GetComponent<NavMeshAgent>();
        theAgent.stoppingDistance = 1.0f;
    }
    public void MoveToLevel1()
    {
        animator.SetBool("IsWalking", true);
        theAgent.SetDestination(theDestination.transform.position);
        isMoving = true;
        if (isMoving && !theAgent.pathPending)
        {
            if (theAgent.remainingDistance <= theAgent.stoppingDistance)
            {
                if (!theAgent.hasPath || theAgent.velocity.sqrMagnitude == 0f)
                {
                    // Arrived at the destination
                    animator.SetBool("IsWalking", false);
                    print("AAAAAAAA");
                    animator.SetBool("ReachedDestination", true); // Transition to waving animation
                    isMoving = false;
                }
            }
        }
        StartCoroutine(CheckArrival());
    }
    void Update()
    {

    }
    private IEnumerator CheckArrival()
    {
        yield return new WaitUntil(() => !theAgent.pathPending);
        while (Vector3.Distance(theAgent.transform.position, theDestination.transform.position) > theAgent.stoppingDistance)
        {
            yield return null;
        }
        animator.SetBool("IsWalking", false);
    }
}