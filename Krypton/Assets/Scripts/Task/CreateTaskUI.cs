using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TaskMode
{
	/**
	 * Handles the Task Creation UI
	 */
	public class CreateTaskUI : MonoBehaviour
	{
		public TaskListCreator Creator;

		public TMP_Text TaskName;
		public GridLayoutGroup TaskList;

		public GameObject TaskPrefab;

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

		/**
		 * Shows all available Create - Atom - Tasks
		 */
		public void OnCreateAtom()
		{
			CreateAtomGrid.SetActive(true);
			ViewAtomGrid.SetActive(false);
			CreateMoleculeGrid.SetActive(false);
			ViewMoleculeGrid.SetActive(false);
		}

		/**
		 * Shows all available View - Atom - Tasks
		 */
		public void OnViewAtom()
		{
			CreateAtomGrid.SetActive(false);
			ViewAtomGrid.SetActive(true);
			CreateMoleculeGrid.SetActive(false);
			ViewMoleculeGrid.SetActive(false);
		}

		/**
		 * Shows all available Create - Molecule - Tasks
		 */
		public void OnCreateMolecule()
		{
			CreateAtomGrid.SetActive(false);
			ViewAtomGrid.SetActive(false);
			CreateMoleculeGrid.SetActive(true);
			ViewMoleculeGrid.SetActive(false);
		}

		/**
		 * Shows all available View - Molecule - Tasks
		 */
		public void OnViewMolecule()
		{
			CreateAtomGrid.SetActive(false);
			ViewAtomGrid.SetActive(false);
			CreateMoleculeGrid.SetActive(false);
			ViewMoleculeGrid.SetActive(true);
		}

		#endregion

		#region Task Adding

		//TODO remove option that is already in task list
		/**
		 * Adds Create - Atom -Task to List
		 */
		public void AddCreateAtom(AtomSO atom)
		{
			Creator.AddCreateAtom(atom);
		}

		/**
		 * Adds View - Atom -Task to List
		 */
		public void AddViewAtom(AtomSO atom)
		{
			Creator.AddViewAtom(atom);
		}

		/**
		 * Adds Create - Molecule -Task to List
		 */
		public void AddCreateMolecule(MoleculeSO molecule)
		{
			Creator.AddCreateMolecule(molecule);
		}

		/**
		 * Adds View - Molecule -Task to List
		 */
		public void AddViewMolecule(MoleculeSO molecule)
		{
			Creator.AddViewMolecule(molecule);
		}

		#endregion

		#region Control Buttons

		/**
		 * Saves the task  List
		 */
		public void OnSave()
		{
			Creator.Save();
		}

		/*
		 * Saves the TaskList + Starts the TaskMode + Navigates back to TaskMenu Scene
		 */
		public void OnSaveStart()
		{
			int id = Creator.Save();
			TaskModeManger.Singleton.StartTaskMode(id);
			SceneManager.LoadScene(SceneList.TaskMenu);
		}

		/*
		 * Navigates back to TaskMenu Scene
		 */
		public void OnCancel()
		{
			SceneManager.LoadScene(SceneList.TaskMenu);
		}

		#endregion

		/**
		 * Updates the Task List - UI with current selection of tasks
		 */
		public void UpdateTaskList()
		{
			TaskName.text = $"{Creator.TaskList.Name}";

			//Clear the List
			foreach (Transform child in TaskList.transform)
			{
				Destroy(child.gameObject);
			}

			//(Re-)fill the List
			foreach (Task task in Creator.TaskList)
			{
				TMP_Text taskEntry = Instantiate(TaskPrefab, TaskList.transform).GetComponent<TMP_Text>();

				taskEntry.text += " " + task.Name + " ";
			}
		}
	}
}