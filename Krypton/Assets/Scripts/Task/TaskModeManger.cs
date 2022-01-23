using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TaskModeManger : DontDestroySingleton<TaskModeManger>
{
	public TaskList Tasks => tasks;
 	private TaskList tasks;
	
	public delegate void TaskModeEvent(bool success=true);
	public event TaskModeEvent TasksChanged;

	public bool Active { get; private set; } = false;

	#region Start + Finish Task Mode
	
	public void StartTaskMode(int id)
	{
		tasks = TaskListsSaver.TaskLists[id];
		Active = true;
	}	
	
	public void FinishTaskMode()
	{
		if (!Active) { return; }
		
		Active = false;
	}
	
	#endregion

	#region Task Completing
	
	/**
	 * Call if Atom was created
	 * If atom created matches a task - the task is considered "done"
	 */
	public void AtomCreated(AtomSO atom)
	{
		if (!Active) { return; }
		foreach (Task task in tasks)
		{
			if (task.Type == TaskType.CreateAtom && task.Atom == atom)
			{
				task.Done = true;
				
				TasksChanged?.Invoke();
			}
		}
	}
	
	/**
	 * Call if Atom Description was viewed
	 * If atom description viewed matches a task - the task is considered "done"
	 */
	public void AtomViewed(AtomSO atom)
	{
		if (!Active) { return; }
		foreach (Task task in tasks)
		{
			if (task.Type == TaskType.ViewAtom && task.Atom == atom)
			{
				task.Done = true;
				
				TasksChanged?.Invoke();
			}
		}
	}
	
	/**
	 * Call if Molecule was created
	 * If Molecule created matches a task - the task is considered "done"
	 */
	public void MoleculeCreated(MoleculeSO molecule)
	{
		if (!Active) { return; }
		foreach (Task task in tasks)
		{
			if (task.Type == TaskType.CreateMolecule && task.Molecule == molecule)
			{
				task.Done = true;
				
				TasksChanged?.Invoke();
			}
		}
	}
	
	/**
	 * Call if Molecule Description was viewed
	 * If molecule description viewed matches a task - the task is considered "done"
	 */
	public void MoleculeViewed(MoleculeSO molecule)
	{
		if (!Active) { return; }
		foreach (Task task in tasks)
		{
			if (task.Type == TaskType.ViewMolecule && task.Molecule == molecule)
			{
				task.Done = true;
				TasksChanged?.Invoke();
			}
		}
	}

	#endregion
}