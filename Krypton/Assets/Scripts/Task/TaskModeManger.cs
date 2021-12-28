using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TaskModeManger : DontDestroySingleton<TaskModeManger>
{
	public delegate void TaskModeEvent(bool success=true);
	public event TaskModeEvent TasksChanged;
	public event TaskModeEvent TasksDone;
	
	private bool active = false;

	public Task[] Tasks => tasksList.Tasks;
	private TaskList tasksList;

	private TaskList[] availableTaskLists => TaskListsSaver.LoadTaskLists();

	/**
	 * Exits TaskMode
	 * Invokes the Event TasksDone - if succes is true,all tasks were finished, if not the task-mode was canceled.
	 */
	public void FinishTask()
	{
		if (!active) { return; }
		
		active = false;
		
		TasksDone?.Invoke(tasksList.Done);
	}

	#region Task Completing
	
	/**
	 * Call if Atom was created
	 * If atom created matches a task - the task is considered "done"
	 */
	public void AtomCreated(AtomSO atom)
	{
		foreach (CreateAtomTask task in tasksList.Tasks.OfType<CreateAtomTask>())
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
		foreach (ViewAtomTask task in tasksList.Tasks.OfType<ViewAtomTask>())
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
		foreach (CreateMoleculeTask task in tasksList.Tasks.OfType<CreateMoleculeTask>())
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
		foreach (ViewMoleculeTask task in tasksList.Tasks.OfType<ViewMoleculeTask>())
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