using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] private NavMeshAgent meshAgent;
    [SerializeField] Transform headPos;
    [SerializeField] int viewConeAngle;
    [SerializeField, Range(1, 100)] int roamDistance;
    [SerializeField, Range(0, 10)] float roamTimer;

    private EnemyBase baseScript;
    
    // NavMesh variables
    private bool playerInRange;
    private bool destinationChosen;
    private int playerFaceSpeed;
    private Vector3 playerDir;
    private Vector3 startingPos;
    private float angleToPlayer;
    private float stoppingDistanceOriginal;

    private Vector3 playerPos => PlayerBase.Instance.transform.position;

    private void Start()
    {
        baseScript = GetComponent<EnemyBase>();
        startingPos = transform.position;
        stoppingDistanceOriginal = meshAgent.stoppingDistance;
    }

    private void Update()
    {
        if ((playerInRange && !CanSeePlayer()) || (meshAgent.destination != playerPos))
        {
            StartCoroutine(Roam());
        } 
    }

    IEnumerator Roam()
    {
        if (!destinationChosen && meshAgent.remainingDistance < 0.05f)
        {
            destinationChosen = true;
            meshAgent.stoppingDistance = 0;

            yield return new WaitForSeconds(roamTimer);

            Vector3 randomPos = Random.insideUnitSphere * roamDistance;
            randomPos += startingPos;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPos, out hit, roamDistance, 1);
            meshAgent.SetDestination(hit.position);

            destinationChosen = false;
        }
    }

    bool CanSeePlayer()
    {
        meshAgent.stoppingDistance = stoppingDistanceOriginal;

        playerDir = PlayerBase.Instance.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

        Debug.DrawRay(headPos.position, playerDir);
        Debug.Log(angleToPlayer);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewConeAngle)
            {
                meshAgent.SetDestination(playerPos);
                if (meshAgent.remainingDistance <= meshAgent.stoppingDistance)
                    FacePlayer();

                return true;
            }
        }

        meshAgent.stoppingDistance = 0;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void FacePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }


}
