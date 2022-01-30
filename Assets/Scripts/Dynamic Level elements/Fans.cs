using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Fans : MonoBehaviour
{
    [SerializeField, Tooltip("What direction will entities be moved")]
    protected Vector2 forceDirection = Vector2.right;

    [SerializeField]
    protected float windSpeed = 5.0f;

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

    //[SerializeField, Tooltip("How close to the source is the wind at its strongest")]
    //private float minDistance = 0.5f; 
    [SerializeField, Tooltip("How far from the source will the wind reach")]
    private float maxDistance = 5.0f;

    [SerializeField, Tooltip("What is the maximum weight per entity can the wind push")]
    private int maxEntityWeight = 1;


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

   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + (new Vector3(forceDirection.x, forceDirection.y, 0.0f) * maxDistance));
    }

    //Add entities that enter the trigger area to the list
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision" + collision.name);
        if (collision.TryGetComponent<Entity>(out Entity entity) && collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            Debug.Log(collision.gameObject.name);
            if (entity.weight <= maxEntityWeight)
            {
                MovableEntity obj = new MovableEntity(rb, entity);

                if (collision.TryGetComponent<ControllableCharacter>(out ControllableCharacter character))
                {
                    obj.character = character;
                }

                StartCoroutine(MoveEntity(obj));
                //int existingIndex = -1;
                //existingIndex = affectedEntities.FindIndex(x => x.entity == entity);

                //if (existingIndex < 0)
                //{
                //    affectedEntities.Add(obj);
                //}
            }
        }
    }

    IEnumerator MoveEntity(MovableEntity obj)
    {
        Debug.Log("I have acquired " + obj.entity.name);
        obj.rb.simulated = false;
        obj.rb.transform.position = new Vector3(forceDirection.x == 0.0f ? transform.position.x : obj.rb.transform.position.x,
            forceDirection.y == 0.0f ? transform.position.y : obj.rb.transform.position.y
            , obj.rb.transform.position.z);

        Vector3 destination = transform.position + (new Vector3(forceDirection.x, forceDirection.y, 0.0f) * maxDistance);

        float distance = Vector3.Distance(obj.entity.transform.position, destination);

        while(distance > 0.5f)
        {
            obj.entity.transform.position = Vector3.MoveTowards(obj.entity.transform.position, destination, Time.deltaTime * windSpeed);
            distance = Vector3.Distance(obj.entity.transform.position, destination);
            yield return null;
        }

        obj.rb.simulated = true;
    }

    ////Remove entities that exit the trigger area from the list
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent<Entity>(out Entity entity))
    //    {

    //        int existingIndex = -1;
    //        existingIndex = affectedEntities.FindIndex(x => x.entity == entity);

    //        if (existingIndex >= 0)
    //        {
    //            affectedEntities.RemoveAt(existingIndex);
    //        }
    //    }
    //}
}

