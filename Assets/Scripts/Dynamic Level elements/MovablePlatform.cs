using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
      [SerializeField, Tooltip("How fast the platform will move")]
    private float platformSpeed = 3.0f;
    [SerializeField, Tooltip("How much weight is required for this platform to reach the maximum height")]
    private int maxWeightCapacity = 3;
    [SerializeField, Tooltip("What is the maximum height this platform can reach")]
    private float maxHeight = 10.0f;

    private float heightStep = 0.0f;
    private Vector3 startingPos = new Vector3();
    private float targetHeight = 0.0f;
    private float currentHeight = 0.0f;
    private void Start()
    {
        heightStep = maxHeight / maxWeightCapacity;
        startingPos = transform.position;
        currentHeight = startingPos.y;
        targetHeight = startingPos.y;
    }

    private void Update()
    {
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * platformSpeed);
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
    }


    public void UpdateHeight(int weight)
    {
        targetHeight = startingPos.y + Mathf.Clamp((Mathf.Clamp(weight, 0, maxWeightCapacity) * heightStep), 0.0f, maxHeight);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float hStep = maxHeight / maxWeightCapacity;

        for (int i = 0; i <= maxWeightCapacity; i++)
        {
            Vector3 target = transform.position + (transform.up * (hStep * i));
            Gizmos.DrawLine(target + Vector3.right, target + Vector3.left);
        }
    }
}
