using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObstacles : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 8.0f;
    float direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, movementSpeed*direction)*Time.deltaTime;
        if (transform.position.z < 18) direction *= -1;
        if (transform.position.z > 82) direction *= -1;
    }
}
