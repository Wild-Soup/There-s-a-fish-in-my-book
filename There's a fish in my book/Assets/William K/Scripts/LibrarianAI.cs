using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
public class LibrarianAI : MonoBehaviour
{
    public GameObject idlePos;
    public GameObject player;
    public float anger;
    public float maxAnger;
    public float minAnger;
    public float angerDegeradtion;
    public float speed;
    public float runningSpeed;
    public float huntTime;
    public float minWalkDistance;
    public LayerMask obstacleMask;
    public SphereCollider walkBound;

    public float maxViewRange;
    public float minViewRange;

    public GameObject gameOverScreen;

    private NavMeshAgent agent;
    public bool isInHunt;
    public bool sawHiding;
    public bool gotPlayer;

    public Image angerMeter;
    public TextMeshProUGUI angerTxt;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        angerTxt.text = $"Librarian Anger: {anger}";
        angerMeter.fillAmount = anger / maxAnger;
        StartCoroutine(AngerMeter());
    }

    // Update is called once per frame
    void Update()
    {
        if (isInHunt)
        {
            Vector3 pos = transform.InverseTransformPoint(player.transform.position);
            if (pos.normalized.z > 0.5f && pos.magnitude < maxViewRange || sawHiding)
            {
                if (!player.GetComponent<PlayerManager>().isHiding || sawHiding)
                {
                    agent.speed = runningSpeed;

                    agent.SetDestination(player.transform.position);
                }


                if (player.GetComponent<PlayerManager>().enteringHideout)
                {
                    sawHiding = true;
                }
            }
            else
            {
                agent.speed = speed;

                if (transform.position == agent.destination || agent.velocity.magnitude == 0 && !gotPlayer)
                {
                    agent.SetDestination(GetRandomAreaPos(transform.position));
                }
                sawHiding = false;
            }

            

        }

    }

    public void IncreaseAnger(float amount)
    {
        anger = Mathf.Clamp(anger + amount, 0, maxAnger);
        angerTxt.text = $"Librarian Anger: {anger}";
        angerMeter.fillAmount = anger / maxAnger;

        if (anger >= maxAnger)
        {
            HuntPlayer();
        }
    }

    private void HuntPlayer()
    {
        isInHunt = true;
        agent.SetDestination(player.transform.position);
    }

    private void EndHunt()
    {
        isInHunt = false;
        agent.SetDestination(idlePos.transform.position);
        sawHiding = false;
    }

    private Vector3 GetRandomAreaPos(Vector3 pos)
    {

        float xPos = Random.Range(walkBound.bounds.min.x, walkBound.bounds.max.x);
        float zPos = Random.Range(walkBound.bounds.min.z, walkBound.bounds.max.z);

        return new Vector3(xPos, pos.y, zPos);

    }

    public IEnumerator AngerMeter()
    {
        while (true)
        {
            if (anger > 0)
            {
                anger = Mathf.Clamp(anger - angerDegeradtion, 0, maxAnger);
                angerTxt.text = $"Librarian Anger: {anger}";
                angerMeter.fillAmount = anger / maxAnger;
            }
            else if(anger == 0)
            {
                EndHunt();
            }

            yield return new WaitForSeconds(10);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameOverScreen.SetActive(true);
            other.GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed = 0;
            other.GetComponent<ActionBasedContinuousTurnProvider>().turnSpeed = 0;
        }
    }
}
