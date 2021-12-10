using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoleculeMenu : MonoBehaviour
{
	
	/**
     * Opens the Atom-View - called from the MainMenu-Button "Atom"
     */
	public void OnAtom()
	{
		SceneManager.LoadScene(SceneList.AtomView);
	}
	
	/**
     * Exits the Application - called from the MainMenu-Button "Exit"
     */
	public void OnExit()
	{
		SceneManager.LoadScene(SceneList.MainMenu);
	}
}
