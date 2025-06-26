using UnityEngine;
using UnityEngine.AI;

public class DaughterFollower : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isFollowing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isFollowing && player != null)
        {
            agent.SetDestination(player.position);
            float speed = agent.velocity.magnitude;
            animator.SetFloat("InputMagnitude", speed > 0.1f ? 1f : 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = true;
        }
    }
}
