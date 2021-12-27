using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskModeManger : DontDestroySingleton<TaskModeManger>
{
	public delegate void TaskModeEvent(bool success, Task[] tasks);
	public event TaskModeEvent TasksChanged;
	public event TaskModeEvent TasksDone;
	
	private bool active = false;
	private bool done { get { return checkIfDone(); } }

	private List<Task> tasks;
	
	
	/**
	 * Exits TaskMode
	 * Invokes the Event TasksDone - if succes is true,all tasks were finished, if not the task-mode was canceled.
	 */
	public void FinishTask()
	{
		if (!active) { return; }
		
		active = false;
		
		TasksDone?.Invoke(done, tasks.ToArray());
	}
	
	private bool checkIfDone()
	{
		foreach (var task in tasks)
		{
			if (!task.Done) { return false; }
		}

		return true;
	}

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
				
				TasksChanged?.Invoke(true, tasks.ToArray());
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
				
				TasksChanged?.Invoke(true, tasks.ToArray());
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
				
				TasksChanged?.Invoke(true, tasks.ToArray());
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
				
				TasksChanged?.Invoke(true, tasks.ToArray());
			}
		}
	}

	#endregion
}
