using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Transform goal;

    void Start()
    {
        // ��������� ���������� ������
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // ������� ����� ����������
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
