using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoleculeMenu : MonoBehaviour
{
	/**
     * Exits the MoleculeScene - called from the Menu-Button "Exit"
     */
	public void OnExit()
	{
		SceneManager.LoadScene(SceneList.MainMenu);
	}
}
