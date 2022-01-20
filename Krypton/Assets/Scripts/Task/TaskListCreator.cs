using System;
using UnityEngine;

public class TaskListCreator : MonoBehaviour
{
	public CreateTaskUI UI;

	public TaskList TaskList { get; private set; }

	private void Start()
	{
		string name = DateTime.Now.ToString("dd/MM/yy HH:mm:ss");
		TaskList = new TaskList(name);
		UI.UpdateTaskList();
	}

	#region TaskAdder

	public void AddCreateAtom(AtomSO atom)
	{
		CreateAtomTask task;
		task = new CreateAtomTask(atom);
		TaskList.AddTask(task);
		UI.UpdateTaskList();
	}

	public void AddViewAtom(AtomSO atom)
	{
		ViewAtomTask task;
		task = new ViewAtomTask(atom);
		TaskList.AddTask(task);
		UI.UpdateTaskList();
	}

	public void AddCreateMolecule(MoleculeSO molecule)
	{
		CreateMoleculeTask task;
		task = new CreateMoleculeTask(molecule);
		TaskList.AddTask(task);
		UI.UpdateTaskList();
	}

	public void AddViewMolecule(MoleculeSO molecule)
	{
		ViewMoleculeTask task;
		task = new ViewMoleculeTask(molecule);
		TaskList.AddTask(task);
		UI.UpdateTaskList();
	}
	
	#endregion

	public int Save()
	{
		return TaskListsSaver.SaveTask(TaskList);
	}
}