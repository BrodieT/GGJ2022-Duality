using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


//This class represents an area of the level that can affect the motion of entities
public class DynamicSurfaces : MonoBehaviour
{
    [SerializeField, Tooltip("What direction will entities be moved")]
    protected Vector2 forceDirection = Vector2.right;
    [SerializeField, Tooltip("How much force will be applied")]
    protected float forceAmount = 10.0f;
    protected List<MovableEntity> affectedEntities = new List<MovableEntity>();

    protected struct MovableEntity
    {
        public Rigidbody2D rb;
        public ControllableCharacter character;
        public Entity entity;

        public MovableEntity(Rigidbody2D rigidbody, Entity e, ControllableCharacter c = null)
        {
            rb = rigidbody;
            character = c;
            entity = e;
        }
    }

    private void FixedUpdate()
    {
        //Loop through each entity in the area
        foreach (MovableEntity obj in affectedEntities)
        {
            Vector2 force = CalculateForce(obj.entity, obj.rb);

            if (obj.character)
            {
                obj.character.ApplyForce(force);
            }

            obj.rb.velocity += force;
        }

        Debug.Log(affectedEntities.Count);
    }

    protected virtual Vector2 CalculateForce(Entity entity, Rigidbody2D rb)
    {
        return forceDirection* CalculateInfluence(entity, rb);
    }

    //This function determines how much of the resulting motion force is applied to the entity 
    protected virtual float CalculateInfluence(Entity entity, Rigidbody2D rb)
    {
        return 0.0f;
    }

    //Add entities that enter the trigger area to the list
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out Entity entity) && collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            MovableEntity obj = new MovableEntity(rb, entity);

            if (collision.TryGetComponent<ControllableCharacter>(out ControllableCharacter character))
            {
                obj.character = character;
            }

            int existingIndex = -1;
            existingIndex = affectedEntities.FindIndex(x => x.entity == entity);

            if (existingIndex < 0)
            {
                affectedEntities.Add(obj);
            }
        }
    }

 

    //Remove entities that exit the trigger area from the list
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out Entity entity))
        {

            int existingIndex = -1;
            existingIndex = affectedEntities.FindIndex(x => x.entity == entity);

            if (existingIndex >= 0)
            {
                affectedEntities.RemoveAt(existingIndex);
            }
        }
    }
}


