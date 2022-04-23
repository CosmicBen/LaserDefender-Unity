using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float backgroundScrollSpeed = 0.5f;
    private Material myMaterial = null;
    private Vector2 offset;

    private void Awake()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        offset = new Vector2(0.0f, backgroundScrollSpeed);
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
