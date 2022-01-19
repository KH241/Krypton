using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateTaskUI : MonoBehaviour
{
	public TaskListCreator Creator;

	public TMP_Text TaskName;
	public TMP_Text TaskList;

	public GameObject CreateAtomGrid;
	public GameObject ViewAtomGrid;
	public GameObject CreateMoleculeGrid;
	public GameObject ViewMoleculeGrid;

	public GameObject TaskButtonPrefab;

	private void Start()
	{
		GameObject TaskButton;

		foreach (AtomSO atom in SOList.AtomList)
		{
			TaskButton = Instantiate(TaskButtonPrefab, CreateAtomGrid.transform);
			TaskButton.GetComponent<Button>().onClick.AddListener(delegate { AddCreateAtom(atom); });
			TaskButton.GetComponentInChildren<TMP_Text>().text = "Create " + atom.name;

			TaskButton = Instantiate(TaskButtonPrefab, ViewAtomGrid.transform);
			TaskButton.GetComponent<Button>().onClick.AddListener(delegate { AddViewAtom(atom); });
			TaskButton.GetComponentInChildren<TMP_Text>().text = "Info " + atom.name;
		}

		foreach (MoleculeSO molecule in SOList.MoleculeList)
		{
			TaskButton = Instantiate(TaskButtonPrefab, CreateMoleculeGrid.transform);
			TaskButton.GetComponent<Button>().onClick.AddListener(delegate { AddCreateMolecule(molecule); });
			TaskButton.GetComponentInChildren<TMP_Text>().text = "Create " + molecule.name;

			TaskButton = Instantiate(TaskButtonPrefab, ViewMoleculeGrid.transform);
			TaskButton.GetComponent<Button>().onClick.AddListener(delegate { AddViewMolecule(molecule); });
			TaskButton.GetComponentInChildren<TMP_Text>().text = "Info " + molecule.name;
		}
	}

	#region TaskButtonsToggle

	public void OnCreateAtom()
	{
		CreateAtomGrid.SetActive(true);
		ViewAtomGrid.SetActive(false);
		CreateMoleculeGrid.SetActive(false);
		ViewMoleculeGrid.SetActive(false);
	}

	public void OnViewAtom()
	{
		CreateAtomGrid.SetActive(false);
		ViewAtomGrid.SetActive(true);
		CreateMoleculeGrid.SetActive(false);
		ViewMoleculeGrid.SetActive(false);
	}

	public void OnCreateMolecule()
	{
		CreateAtomGrid.SetActive(false);
		ViewAtomGrid.SetActive(false);
		CreateMoleculeGrid.SetActive(true);
		ViewMoleculeGrid.SetActive(false);
	}

	public void OnViewMolecule()
	{
		CreateAtomGrid.SetActive(false);
		ViewAtomGrid.SetActive(false);
		CreateMoleculeGrid.SetActive(false);
		ViewMoleculeGrid.SetActive(true);
	}

	#endregion

	#region Task Adding

	public void AddCreateAtom(AtomSO atom)
	{
		Creator.AddCreateAtom(atom);
	}

	public void AddViewAtom(AtomSO atom)
	{
		Creator.AddViewAtom(atom);
	}

	public void AddCreateMolecule(MoleculeSO molecule)
	{
		Creator.AddCreateMolecule(molecule);
	}

	public void AddViewMolecule(MoleculeSO molecule)
	{
		Creator.AddViewMolecule(molecule);
	}

	#endregion

	#region Control Buttons

	public void OnSave()
	{
		Creator.Save();
	}

	public void OnSaveStart()
	{
		int id = Creator.Save();
		TaskModeManger.Singleton.StartTaskMode(id);
		SceneManager.LoadScene(SceneList.TaskMenu);
	}

	public void OnCancel()
	{
		SceneManager.LoadScene(SceneList.TaskMenu);
	}

	#endregion

	public void UpdateTaskList()
	{
		TaskName.text = $"{Creator.TaskList.Name}";

		TaskList.text = "";
		foreach (Task task in Creator.TaskList)
		{
			TaskList.text += task.Name + "\n";
		}
	}
}