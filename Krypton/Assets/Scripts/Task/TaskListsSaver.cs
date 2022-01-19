using System.Collections.Generic;
using UnityEngine;

public static class TaskListsSaver
{
	public static TaskList[] TaskLists => list.ToArray();
	private static List<TaskList> list = new List<TaskList>();
	
	/**
	 * Returns all saved TaskLists from local Storage
	 */
	public static TaskList[] LoadTaskLists()
	{
		List<TaskList> output = new List<TaskList>();

		//TODO implement task loading

		return output.ToArray();
	}

	/**
	 * Saves given Tasklist into local Storage
	 * The Name of the Task list must be unique
	 */
	public static int SaveTask(TaskList taskList)
	{
		int id = TaskLists.Length;
		
		/*foreach (TaskList list in TaskLists)
		{
			if (list.Name == taskList.Name) { return -1; }
		}*/
		
		list.Add(taskList);
		
		Debug.LogError("Need to implement Task saving"); //TODO implement task saving
		
		return id;
	}
}