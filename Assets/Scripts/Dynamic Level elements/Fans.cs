using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Fans : DynamicSurfaces
{
    [SerializeField, Tooltip("How close to the source is the wind at its strongest")]
    private float minDistance = 0.5f; 
    [SerializeField, Tooltip("How far from the source will the wind reach")]
    private float maxDistance = 5.0f;

    [SerializeField, Tooltip("What is the maximum weight per entity can the wind push")]
    private int maxEntityWeight = 1;
    [SerializeField, Tooltip("How much influence will wind have on entities over the max pushable weight threshold. Zero means it will have no effect")]
    private float heavyweightInfluence = .5f;

    private void Start()
    {
        if (TryGetComponent<BoxCollider2D>(out BoxCollider2D col))
        {
            col.offset = forceDirection * (maxDistance / 2);
            col.size = new Vector2(
                forceDirection.x != 0.0f ? Mathf.Abs(forceDirection.x) * maxDistance : col.size.x,
                forceDirection.y != 0.0f ? Mathf.Abs(forceDirection.y) * maxDistance : col.size.y);
        }
       
    }

    protected override float CalculateInfluence(Entity entity, Rigidbody2D rb)
    {
        //Apply no force if the entity weight is greater than the fan can push
        if (entity.weight > maxEntityWeight)
        {
            return 0.0f;
        }

        //Determine the influence that the wind will have based on distance
        float distancePercentage = (Mathf.Clamp(Vector2.Distance(rb.transform.position, transform.position), minDistance, maxDistance) - minDistance) / maxDistance - minDistance;
        float influence = 1.0f - Mathf.Clamp01(distancePercentage);

        
        //Ensure there is always at least some force applied to the full length of the fan
        return Mathf.Max(0.1f, influence * 2);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + (new Vector3(forceDirection.x, forceDirection.y, 0.0f) * maxDistance));
    }
}

