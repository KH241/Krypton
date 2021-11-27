using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    /**
     * Exits the Application - called from the MainMenu-Button "Exit"
     */
    public void OnExit()
    {
        Application.Quit();
    }
}
