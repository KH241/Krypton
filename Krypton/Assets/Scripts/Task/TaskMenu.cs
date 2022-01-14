using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TaskMenu : MonoBehaviour
{
	public GameObject Idle;
	public GameObject TaskMode;
	public GameObject TaskListSelection;
	
	public TMP_Dropdown TaskListSelectionDropdown;

	private void Start()
	{
		FillTaskListSelection();
		if (TaskModeManger.Singleton.Active)
		{
			Idle.SetActive(false);
			TaskMode.SetActive(true);
		}
	}

	private void FillTaskListSelection()
	{
		TaskListSelectionDropdown.ClearOptions();
		
		foreach (TaskList taskLists in TaskModeManger.Singleton.AvailableTaskLists)
		{
			TaskListSelectionDropdown.options.Add(new TMP_Dropdown.OptionData(taskLists.Name));
		}
	}
	
	#region Buttons
	/**
     * Opens the Task Selection Dropdown
     */
	public void ShowTaskListSelection()
	{
		TaskListSelection.SetActive(true);
	}

	/**
	 * Closes the Task Selection Dropdown
	 */
	public void CloseTaskListSelection()
	{
		TaskListSelection.SetActive(false);
	}

	/**
	 * Starts the Task Mode - started from the Task Selection Button "Start Task"
	 * It starts the Task Selected in the Dropdown
	 */
	public void StartTaskMode()
	{
		TaskModeManger.Singleton.StartTaskMode(TaskListSelectionDropdown.value);
		
		TaskMode.SetActive(true);
		TaskListSelection.SetActive(false);
		Idle.SetActive(false);
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
		TaskModeManger.Singleton.FinishTaskMode();
		Idle.SetActive(true);
		TaskMode.SetActive(false);
	}

	/**
     * Navigates back to the Main-Menu - called from the TaskMenu-Button "Back"
     */
	public void OnMainMenu()
	{
		SceneManager.LoadScene(SceneList.MainMenu);
	}	
	#endregion
}
