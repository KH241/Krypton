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
	public event TaskModeEvent TasksDone;
	
	private bool active = false;

	public TaskList[] AvailableTaskLists => TaskListsSaver.LoadTaskLists();

	#region Start + Finish Task Mode
	
	public void StartTaskMode(int id)
	{
		tasks = AvailableTaskLists[id];
		active = true;
	}	
	
	public void FinishTaskMode()
	{
		if (!active) { return; }
		
		active = false;
		
		TasksDone?.Invoke(tasks.Done);
	}
	
	#endregion

	#region Task Completing
	
	/**
	 * Call if Atom was created
	 * If atom created matches a task - the task is considered "done"
	 */
	public void AtomCreated(AtomSO atom)
	{
		foreach (CreateAtomTask task in tasks.OfType<CreateAtomTask>())
		{
			if (task.Atom == atom)
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
		foreach (ViewAtomTask task in tasks.OfType<ViewAtomTask>())
		{
			if (task.Atom == atom)
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
		foreach (CreateMoleculeTask task in tasks.OfType<CreateMoleculeTask>())
		{
			if (task.Molecule == molecule)
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
		foreach (ViewMoleculeTask task in tasks.OfType<ViewMoleculeTask>())
		{
			if (task.Molecule == molecule)
			{
				task.Done = true;
				
				TasksChanged?.Invoke();
			}
		}
	}

	#endregion
}