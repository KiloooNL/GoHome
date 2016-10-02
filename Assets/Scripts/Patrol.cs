using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
    public Transform[] patrolPoints;
    public float moveSpeed = 3f;
    private int currentPoint;

	// Use this for initialization
	void Start () {
        transform.position = patrolPoints[0].position; // Set enemy patrol starting position
        currentPoint = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position == patrolPoints[currentPoint].position) // Check position
        {
            currentPoint++; // Keep walking towards patrol point
        }

        // If currentPoint exceeds patrolPoints, reset to first patrolPoint
        if (currentPoint >= patrolPoints.Length)
        {
            currentPoint = 0;
        }

            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
	}
}
