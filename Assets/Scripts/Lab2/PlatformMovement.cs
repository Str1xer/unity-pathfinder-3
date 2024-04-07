using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlatformMovement : MonoBehaviour
{
    private Vector3 initPosition;
    [SerializeField]
    private bool moving;
    private Vector3 rotationCenter;
    private Vector3 rotationStartPos;
    [SerializeField]
    private float rotationSpeed = 1f;

    private List<NavMeshAgent> AgentsOnPlatform = new List<NavMeshAgent>();

    void Start()
    {
        rotationCenter = transform.position + 10 * Vector3.back;
        rotationStartPos = transform.position;
    }

    void Update()
    {
        if (!moving) return;

        transform.RotateAround(rotationCenter, Vector3.up, Time.deltaTime*rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            AgentsOnPlatform.Add(agent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            AgentsOnPlatform.Remove(agent);
        }
    }


}
