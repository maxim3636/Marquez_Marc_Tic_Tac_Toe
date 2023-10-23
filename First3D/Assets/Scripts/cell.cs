using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public gameManager gm;
    public GameObject sphere;
    public GameObject cube;
    public int status;
    public bool interactable = true;
    private bool isFigureSet = false; // Variable para rastrear si la figura ya se ha establecido

    void Start()
    {
        sphere.SetActive(false);
        cube.SetActive(false);
        status = 0;
    }

    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (PlayerPrefs.GetInt("AI") == 0 || gm.isCubeTurn)
        {
            OnClick();
        }
    }

    public void OnClick() 
    {
        // Verifica si la figura correspondiente ya se ha establecido y evita cambios
        if (gm.win == false)
        {
            if (!isFigureSet)
            {
                if (gm.isCubeTurn)
                {
                    cube.SetActive(true);
                    sphere.SetActive(false);
                    status = 1;
                }
                else
                {
                        sphere.SetActive(true);
                        cube.SetActive(false);
                        status = 2;
                }

                gm.isCubeTurn = !gm.isCubeTurn; // Alternar turno
                isFigureSet = true; // Marcar la figura como establecida

            }
            gm.check();
        }
    }
}
