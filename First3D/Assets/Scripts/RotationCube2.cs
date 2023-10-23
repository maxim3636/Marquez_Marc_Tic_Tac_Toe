using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior2 : MonoBehaviour
{
    public float rotationSpeed;
    private bool rotate = false;
    private float crono = 0;

    private List<Color> colors = new List<Color> { Color.blue, Color.red, Color.green, Color.yellow, Color.white };
    private int currentColorIndex = 0;
    private Renderer cubeRenderer;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.color = colors[currentColorIndex];
    }

    void Update()
    {
        crono = crono + Time.deltaTime;

        if (crono >= 2)
        {
            rotate = true;
        }

        if (crono >= 6)
        {
            rotate = false;
            crono = 0;
            ChangeColor();
        }

        if (rotate)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.Rotate(0, 0, 0);
        }
    }

    void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % colors.Count;
        cubeRenderer.material.color = colors[currentColorIndex];
    }
}

