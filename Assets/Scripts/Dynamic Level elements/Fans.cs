using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fans : MonoBehaviour
{
    [SerializeField, Tooltip("What direction will entities be pushed")]
    private Vector2 windDirection = Vector2.right;

    [SerializeField, Tooltip("How much force will be applied")]
    private float windForce = 10.0f;

    [SerializeField, Tooltip("How close to the source is the wind at its strongest")]
    private float minDistance = 0.5f; 
    [SerializeField, Tooltip("How far from the source will the wind reach")]
    private float maxDistance = 5.0f;

    [SerializeField, Tooltip("What is the maximum weight per entity can the wind push")]
    private int maxEntityWeight = 1;
    [SerializeField, Tooltip("How much influence will wind have on entities over the max pushable weight threshold. Zero means it will have no effect")]
    private float heavyweightInfluence = 0.05f;

    private List<Entity> affectedEntities = new List<Entity>();

    private void FixedUpdate()
    {
        foreach (Entity entity in affectedEntities)
        {
            if(entity.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                //Determine the influence that the wind will have based on distance
                float influence =  1.0f - Mathf.Clamp01((Mathf.Clamp(Vector2.Distance(rb.transform.position, transform.position), minDistance, maxDistance) - minDistance) / maxDistance - minDistance);
                
                if (entity.weight > maxEntityWeight)
                {
                    //Factor in the weight - larger values for weight will result in smaller overall influence
                    influence = heavyweightInfluence;
                }

                if(entity.TryGetComponent<ControllableCharacter>(out ControllableCharacter character))
                {
                    character.ApplyForce((windDirection * (windForce * influence)));
                }

                rb.velocity += (windDirection * (windForce * influence));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.TryGetComponent<Entity>(out Entity entity) && !affectedEntities.Contains(entity))
        {
            affectedEntities.Add(entity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out Entity entity) && affectedEntities.Contains(entity))
        {
            affectedEntities.Remove(entity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + (new Vector3(windDirection.x, windDirection.y, 0.0f) * maxDistance));
    }
}

