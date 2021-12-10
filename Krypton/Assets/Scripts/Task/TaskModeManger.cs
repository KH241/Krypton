using System;
using System.Collections;
using System.Collections.Generic;
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
	 * Finishes the Task.
	 * Invokes the Event TasksDone - if succes is true the task was finished, if not the task was canceled.
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
}
