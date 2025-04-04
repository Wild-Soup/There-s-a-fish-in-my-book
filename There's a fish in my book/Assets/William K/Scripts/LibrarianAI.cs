using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Rendering;
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

    public XRRayInteractor ray;

    public AudioSource huntMusic;
    public AudioSource normalMusic;

    public float walkRange;
    public float maxViewRange;

    private NavMeshAgent agent;
    public bool isInHunt;
    public bool sawHiding;
    public bool gotPlayer;

    public Image angerMeter;
    public TextMeshProUGUI angerTxt;

    [Header("AngerVignette")]
    public Volume angerVignette;

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

                if (agent.velocity.magnitude <= 0.2f && !gotPlayer)
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
        angerVignette.weight = anger / maxAnger;

        if (anger >= maxAnger && !isInHunt)
        {
            HuntPlayer();
        }
        else if (isInHunt)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    private void HuntPlayer()
    {
        isInHunt = true;
        normalMusic.Stop();
        huntMusic.Play();
        agent.SetDestination(player.transform.position);
    }

    private void EndHunt()
    {
        isInHunt = false;
        huntMusic.Stop();
        normalMusic.Play();
        agent.SetDestination(idlePos.transform.position);
        sawHiding = false;
    }

    private Vector3 GetRandomAreaPos(Vector3 pos)
    {
        float minX = transform.position.x - walkRange;
        float maxX = transform.position.x + walkRange;
        float minZ = transform.position.z - walkRange;
        float maxZ = transform.position.z + walkRange;

        float xPos = Random.Range(minX, maxX);
        float zPos = Random.Range(minZ,maxZ);

        return new Vector3(xPos, pos.y, zPos);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(walkRange, 2, walkRange));
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
                angerVignette.weight = anger / maxAnger;
            }
            else if(anger == 0 && isInHunt)
            {
                EndHunt();
            }

            yield return new WaitForSeconds(5);
        }
    }

    public void ResetAnger()
    {
        anger = 0;
        angerTxt.text = $"Librarian Anger: {anger}";
        angerMeter.fillAmount = anger / maxAnger;
        angerVignette.weight = anger / maxAnger;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.gameOverPanel.SetActive(true);
            other.gameObject.GetComponentInChildren<ActionBasedContinuousMoveProvider>().moveSpeed = 0;
            other.gameObject.GetComponentInChildren<ActionBasedSnapTurnProvider>().turnAmount = 0;
            ray.maxRaycastDistance = 50;
            ray.sphereCastRadius = 32;
        }
    }
}
