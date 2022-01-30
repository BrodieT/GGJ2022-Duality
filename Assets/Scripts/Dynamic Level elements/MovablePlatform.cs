using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
      [SerializeField, Tooltip("How fast the platform will move")]
    private float platformSpeed = 3.0f;
    [SerializeField, Tooltip("The height levels for each weight unit applied to the pressure pad relative to the starting position. Each entry represents a level that can be reached")]
    private List<float> heightIncrements = new List<float>();

    private Vector3 startingPos = new Vector3();
    private float targetHeight = 0.0f;
    private float currentHeight = 0.0f;
    private void Start()
    {
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
        int index = Mathf.Min(weight, heightIncrements.Count) - 1;

        if (index < 0)
            targetHeight = startingPos.y;
        else
            targetHeight = startingPos.y + heightIncrements[index];
        //targetHeight = startingPos.y + Mathf.Clamp((Mathf.Clamp(weight, 0, maxWeightCapacity) * heightStep), 0.0f, maxHeight);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //float hStep = maxHeight / maxWeightCapacity;

        //for (int i = 0; i <= maxWeightCapacity; i++)
        //{
        //    Vector3 target = transform.position + (transform.up * (hStep * i));
        //    Gizmos.DrawLine(target + Vector3.right, target + Vector3.left);
        //}

        foreach (float item in heightIncrements)
        {
            Vector3 target = transform.position + (transform.up * (item));
            Gizmos.DrawLine(target + Vector3.right, target + Vector3.left);
        }
    }
}
