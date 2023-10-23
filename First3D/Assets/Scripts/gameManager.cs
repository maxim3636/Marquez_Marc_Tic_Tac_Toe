using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public bool isCubeTurn = true;
    public Cell[] cells;
    public bool win;
    public bool ai;
    public TextMeshProUGUI label;
    public Button button;
    public Button backbutton;
    public AudioClip clipWin;
    public AudioClip clipDraw;
    private float timer = 0.0f;
    private float timeToPlay = 2.5f;
    private bool gameOver = false; // Variable para controlar el estado del juego

    private void Start()
    {
        if (isCubeTurn && PlayerPrefs.GetInt("AI") == 0)
        {
            label.text = "Torn dels cubs!";
        }
        else if (isCubeTurn && PlayerPrefs.GetInt("AI") == 1)
        {
            label.text = "Torn del Jugador!\n"+
                         "(Cubs)";
        }
    }
    
    public void check()
    {
        if (win)
        {
            // Manejar la victoria
            HandleWin(isCubeTurn ? 2 : 1); // Determinar el ganador y manejar la victoria
            gameOver = true; // El juego ha terminado
        }
        else
        {
            if (isCubeTurn)
            {
                if (PlayerPrefs.GetInt("AI") == 0)
                {
                    label.text = "Torn dels cubs!";
                }
                if (PlayerPrefs.GetInt("AI") == 1)
                {
                    label.text = "Torn del Jugador";
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("AI") == 0)
                {
                    label.text = "Torn de les esferes!";
                }
                if (PlayerPrefs.GetInt("AI") == 1)
                {
                    label.text = "Torn de la IA!";
                }
            }
        }

        // Comprobación de victoria horizontal, vertical y diagonal
        if (CheckForWin(1))
        {
            HandleWin(1); // Función para manejar la victoria del jugador
            gameOver = true; // El juego ha terminado
        }
        else if (CheckForWin(2))
        {
            HandleWin(2); // Función para manejar la victoria de la IA
            gameOver = true; // El juego ha terminado
        }
        else
        {
            bool isBoardFull = true;
            foreach (Cell cell in cells)
            {
                if (cell.status == 0)
                {
                    isBoardFull = false;
                    break;
                }
            }

            if (isBoardFull && !win)
            {
                Debug.Log("El joc a acabat en empat!");
                label.text = "El joc a acabat en empat!";
                GetComponent<AudioSource>().PlayOneShot(clipDraw);
                gameOver = true; // El juego ha terminado en empate
            }
        }

        if (gameOver)
        {
            button.gameObject.SetActive(true);
            backbutton.gameObject.SetActive(true);
        }
    }

    void HandleWin(int player)
    {
        win = true;
        if (player == 1)
        {
            Debug.Log("El guanyador es el jugador dels cubs!");
            label.text = "Victoria dels cubs!";
        }
        else
        {
            Debug.Log("El guanyador es el jugador de les esferes!");
            label.text = "Victoria de les esferes!";
        }
        GetComponent<AudioSource>().PlayOneShot(clipWin);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
        Debug.Log("botó restart");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("AI") == 1 && !isCubeTurn)
        {
            timer += Time.deltaTime;
        }

        if (gameOver) // No ejecutar nada más si el juego ha terminado
            return;

        if (timer > timeToPlay)
        {
            if (PlayerPrefs.GetInt("AI") == 1 && !isCubeTurn)
            {
                ai = false;

                // Busca movimientos que ganarían el juego en la siguiente jugada
                for (int i = 0; i < 9; i++)
                {
                    if (cells[i].status == 0)
                    {
                        cells[i].status = 2; // Simula que la IA juega en esta casilla
                        if (CheckForWin(2)) // Comprueba si la IA ganaría en esta casilla
                        {
                            cells[i].OnClick(); // Realiza el movimiento
                            ai = true;
                            timer = 0.0f;
                            break;
                        }

                        cells[i].status = 0; // Deshacer la simulación
                    }
                }

                // Si la IA no va a ganar en la siguiente jugada, busca movimientos para bloquear al jugador
                if (!ai)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (cells[i].status == 0)
                        {
                            cells[i].status = 1; // Simula que el jugador juega en esta casilla
                            if (CheckForWin(1)) // Comprueba si el jugador podría ganar en esta casilla
                            {
                                cells[i].OnClick(); // Bloquea al jugador
                                ai = true;
                                break;
                            }

                            cells[i].status = 0; // Deshacer la simulación
                        }
                    }
                }

                // Si la IA no bloquea al jugador ni gana en la siguiente jugada, intenta ocupar el centro para ganar ventaja
                if (!ai && cells[4].status == 0)
                {
                    cells[4].OnClick(); // Intenta ocupar el centro
                    ai = true;
                }

                // Si la IA no puede ocupar el centro, juega aleatoriamente
                if (!ai)
                {
                    List<int> emptyCells = new List<int>();
                    for (int i = 0; i < 9; i++)
                    {
                        if (cells[i].status == 0)
                        {
                            emptyCells.Add(i);
                        }
                    }

                    if (emptyCells.Count > 0)
                    {
                        int randomIndex = Random.Range(0, emptyCells.Count);
                        int randomCell = emptyCells[randomIndex];
                        cells[randomCell].OnClick();
                    }
                }
            }
            timer = 0.0f;
        }
    }

    bool CheckForWin(int player)
    {
        for (int i = 0; i < 3; i++)
        {
            // Comprobar filas
            if (cells[i].status == player && cells[i + 3].status == player && cells[i + 6].status == player)
            {
                return true;
            }

            // Comprobar columnas
            if (cells[i * 3].status == player && cells[i * 3 + 1].status == player && cells[i * 3 + 2].status == player)
            {
                return true;
            }
        }

        // Comprobar diagonales
        if (cells[0].status == player && cells[4].status == player && cells[8].status == player)
        {
            return true;
        }

        if (cells[2].status == player && cells[4].status == player && cells[6].status == player)
        {
            return true;
        }

        return false; // No hay victoria en esta configuración
    }
}
