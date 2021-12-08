using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskMenu : MonoBehaviour
{
	/**
     * Starts the Task-Mode - called from the TaskMenu-Button "Start Task"
     */
	public void OnStart()
	{
		//TODO Implement and start Task-Mode
	}
	
	/**
     * Navigates to Task-Creation - called from the TaskMenu-Button "Create Task"
     */
	public void OnCreate()
	{
		SceneManager.LoadScene(SceneList.TaskCreate);
	}
	
	/**
     * Opens the Atom-View - called from the TaskMenu-Button "Atom"
     */
	public void OnAtom()
	{
		SceneManager.LoadScene(SceneList.AtomView);
	}

	/**
     * Opens the Molecule-View - called from the TaskMenu-Button "Molecule"
     */
	public void OnMolecule()
	{
		SceneManager.LoadScene(SceneList.MoleculeView);
	}
	
	/**
     * Exits the Task-Mode - called from the TaskMenu-Buttons "Finish Task"/"Cancel Task"
     */
	public void OnExitTask()
	{
		//TODO Implement and exit Task-Mode
	}

	/**
     * Navigates back to the Main-Menu - called from the TaskMenu-Button "Back"
     */
	public void OnMainMenu()
	{
		SceneManager.LoadScene(SceneList.MainMenu);
	}
}
