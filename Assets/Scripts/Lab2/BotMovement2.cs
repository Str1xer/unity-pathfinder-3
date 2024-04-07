using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class BotMovement2 : MonoBehaviour
{
    [SerializeField]
    public Transform goal2;
    private NavMeshAgent agent;
    private Coroutine walking;
    private readonly float threshold = 5.0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        MoveToLocation(goal2.position);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                // agent.SetDestination(hit.point);
                MoveToLocation(hit.point);
            }
        }
    }

    IEnumerator Walk(Queue<Vector3> path)
    {
        agent.ResetPath();
        while (path.Count > 0)
        {
            Vector3 target = path.Dequeue();
            NavMeshPath navMeshPath = new();

            while (!(agent.CalculatePath(target, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete))
                yield return new WaitForFixedUpdate();

            agent.SetPath(navMeshPath);

            while (agent.remainingDistance > threshold)
                yield return new WaitForFixedUpdate();
        }

        walking = null;
    }

    void MoveToLocation(Vector3 target)
    {
        var owner = agent.navMeshOwner as Component;
        owner.TryGetComponent(out NavArea ownerArea);

 
        int currentArea = ownerArea.areaIndex;
        NavMeshPath path = new();

        if (agent.CalculatePath(target, path) && path.status == NavMeshPathStatus.PathComplete)
            agent.SetPath(path);
        else
        {
            foreach (var area in NavArea.areas.Values)
            {
                Vector3 pointInArea = area.neighbors[0].From;

                NavMesh.CalculatePath(pointInArea, target, NavMesh.AllAreas, path);

                if (path.status == NavMeshPathStatus.PathInvalid)
                    continue;

                var pathToTarget = NavArea.FindPath(currentArea, area.areaIndex);
                if (pathToTarget.Count == 0)
                    continue;

                pathToTarget.Enqueue(target);

                if (walking != null)
                {
                    StopCoroutine(walking);
                    walking = null;
                }

                StartCoroutine(Walk(pathToTarget));
                return;
            }
        }
    }
}
