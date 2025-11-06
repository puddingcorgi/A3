using UnityEngine;
using UnityEngine.AI;

public class Playermove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private float baseSpeed = 3f; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = baseSpeed; 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    public void SetSpeed(float speedMultiplier)
    {
        agent.speed = baseSpeed * speedMultiplier;
    }



}