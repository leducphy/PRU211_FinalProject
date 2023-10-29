using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] public float scrollSpeed = 2f; 
    private Renderer bgRenderer;
    private float horizontalInput;

    private void Start()
    {
        bgRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // Detect left and right arrow key input
        horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the new offset based on input and scroll speed
        float offset = bgRenderer.material.mainTextureOffset.x + horizontalInput * scrollSpeed * Time.deltaTime;

        // Apply the new offset to the background texture
        bgRenderer.material.mainTextureOffset = new Vector2(offset, 0);
        
    }

}
