using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LibrarianAI : MonoBehaviour
{
    public GameObject idlePos;
    public GameObject player;
    public float anger;
    public float maxAnger;
    public float minAnger;
    public float speed;
    public float runningSpeed;
    public float huntTime;
    public float minWalkDistance;
    public LayerMask obstacleMask;
    public SphereCollider walkBound;

    private NavMeshAgent agent;
    private bool isInHunt;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInHunt || anger > maxAnger)
        {
            if (!Physics.Linecast(transform.position, player.transform.position,obstacleMask))
            {
                agent.speed = runningSpeed;

                agent.SetDestination(player.transform.position);
                CancelInvoke();
            }
            else if(Physics.Linecast(transform.position, player.transform.position,obstacleMask))
            {
                agent.speed = speed;

                if(transform.position == agent.destination || agent.velocity.magnitude == 0)
                {
                    agent.SetDestination(GetRandomAreaPos(transform.position));
                }

                Invoke("EndHunt", huntTime);
            }
        }
    }

    public void IncreaseAnger(float amount)
    {
        anger += amount;
        if(anger >= maxAnger)
        {
            HuntPlayer();
        }
    }

    private void HuntPlayer()
    {
        isInHunt = true;
        agent.SetDestination(player.transform.position);
        Invoke("EndHunt", huntTime);
    }

    private void EndHunt()
    {
        isInHunt = false;
        agent.SetDestination(idlePos.transform.position);
    }

    private Vector3 GetRandomAreaPos(Vector3 pos)
    {

        float xPos = Random.Range(walkBound.bounds.min.x, walkBound.bounds.max.x);
        float zPos = Random.Range(walkBound.bounds.min.z, walkBound.bounds.max.z);

        return new Vector3(xPos, pos.y, zPos);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, player.transform.position);
    }

}
