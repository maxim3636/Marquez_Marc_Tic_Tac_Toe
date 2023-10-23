using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    public float rotationSpeed;
    private bool rotate = false;
    private float crono = 0;

    private List<Color> colors = new List<Color> { Color.red, Color.blue, Color.yellow, Color.green, Color.white, new Color(0.5f, 0, 0.5f) }; //violeta
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
        }

        if (rotate)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.Rotate(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % colors.Count;
        cubeRenderer.material.color = colors[currentColorIndex];
    }
}
