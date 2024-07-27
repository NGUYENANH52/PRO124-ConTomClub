using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float scrollSpeed;

    private Renderer Renderer;
    private Vector2 saveOffset;

    private void Start()
    {
        Renderer = gameObject.GetComponent<Renderer>();
    }
    private void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed,1);
        Vector2 offset = new Vector2(x,0);
        Renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
