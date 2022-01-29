using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelts : DynamicSurfaces
{
    [SerializeField, Tooltip("Entities with a weight greater than this value will be influenced by the conveyor")]
    private int minEntityWeight = 1;
    [SerializeField, Tooltip("")]
    private int lightweightInfluence = 0;



    protected override float CalculateInfluence(Entity entity, Rigidbody2D rb)
    {
        //Unlike fans, conveyors have a consistent influence on all parts of its area
        float influence = 1.0f;

        //If an entity weighs less than the minimum threshold
        //Then use a much smaller influence
        if (entity.weight < minEntityWeight)
        {
            influence = lightweightInfluence;
        }

        return influence;
    }
}
