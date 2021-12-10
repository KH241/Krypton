using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoleculeMenu : MonoBehaviour
{
	
	/**
     * Opens the Atom-View - called from the Menu-Button "Atom"
     */
	public void OnAtom()
	{
		SceneManager.LoadScene(SceneList.AtomView);
	}
	
	/**
     * Exits the MoleculeScene - called from the Menu-Button "Exit"
     */
	public void OnExit()
	{
		SceneManager.LoadScene(SceneList.MainMenu);
	}
}
