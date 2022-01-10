using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
	private void Start()
	{
		
	}

	#region Button Onclick
	/**
     * Opens the Atom-View - called from the MainMenu-Button "Atom"
     */
	public void OnAtom()
	{
		SceneManager.LoadScene(SceneList.AtomView);
	}

	/**
     * Opens the Molecule-View - called from the MainMenu-Button "Molecule"
     */
	public void OnMolecule()
	{
		SceneManager.LoadScene(SceneList.MoleculeView);
	}

	/**
     * Opens the Task-Menu - called from the MainMenu-Button "Task"
     */
	public void OnTask()
	{
		SceneManager.LoadScene(SceneList.TaskMenu);
	}

	/**
     * Opens the Help-Scene - called from the MainMenu-Button "Help"
     */
	public void OnHelp()
	{
		SceneManager.LoadScene(SceneList.Help);
	}
    
    /**
     * Exits the Application - called from the MainMenu-Button "Exit"
     */
    public void OnExit()
    {
        Application.Quit();
    }	
	#endregion
}
