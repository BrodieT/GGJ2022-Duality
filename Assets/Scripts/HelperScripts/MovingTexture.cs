using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTexture : MonoBehaviour
{
    [SerializeField]
    private Renderer rend = default;

    [SerializeField]
    private float speed = default;

    private float offset = default;

    // Update is called once per frame
    void FixedUpdate()
    {
        offset = Time.time * speed;
        rend.materials[0].SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
