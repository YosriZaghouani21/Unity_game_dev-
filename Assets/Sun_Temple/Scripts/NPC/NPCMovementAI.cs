using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class NPCMovementAI : MonoBehaviour
{
    private Animator animator;
    public GameObject theDestination;
    public GameObject newDestination; 
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
        Debug.Log("Moving to Level 1");

        animator.SetBool("IsWaving", false);
        animator.SetBool("IsWalking", true);
        
        theAgent.SetDestination(theDestination.transform.position);
        isMoving = true;
        StartCoroutine(CheckArrival());
    }

    public void MoveToNewDestination()
    {
        Debug.Log("Moving to New Destination");

        animator.SetBool("IsWaving", false);
        animator.SetBool("IsWalking", true);

        Vector3 newDestination = new Vector3(-10.096f, 16.61f, -149.212f);
        theAgent.SetDestination(newDestination);
        isMoving = true;
        StartCoroutine(CheckArrival());
    }

    void Update()
    {
        // Other update logic...
    }

    private IEnumerator CheckArrival()
    {
        yield return new WaitUntil(() => !theAgent.pathPending);

        while (Vector3.Distance(theAgent.transform.position, theAgent.destination) > theAgent.stoppingDistance)
        {
            yield return null;
        }

        Debug.Log("Arrived at a Destination");
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsWaving", true); // Transition to waving animation
    }


}
