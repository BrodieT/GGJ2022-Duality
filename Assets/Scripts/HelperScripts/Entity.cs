using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField, Tooltip("How heavy this entity is. This will determine how influenced it can be by external forces such as hazards.")]
    public int weight = 1;

    //1 = normal weight, influenced by hazards and can be pushed by characters
    //2 = heavy weight, less affected by hazards and cannot be pushed by characters
    //3 = mjolnir, cannot be moved by external forces - ideal for powered platforms

    //Things that can move entities will have a weight threshold
    //E.g. A threshold of 2 means they can move entities with a weight of 2 or lower
}
