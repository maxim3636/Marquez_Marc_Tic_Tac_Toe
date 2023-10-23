using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Toggle aiToggle;
    // Start is called before the first frame update
    void Start()
    {
        int flag = PlayerPrefs.GetInt("AI", 0);
        if (flag == 0)
        {
            aiToggle.isOn = false;
        }
        else 
        { 
            aiToggle.isOn = true;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Debug.Log("Start button");
        SceneManager.LoadScene(1);
    }

    public void aiToggleChange(bool flag)
    {
        Debug.Log("toggle button" + aiToggle.isOn);
        if (aiToggle.isOn)
        { 
        PlayerPrefs.SetInt("AI", 1);
        }
        else
        {
            PlayerPrefs.SetInt("AI", 0);
        }
    }
    
}
