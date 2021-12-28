using System.Collections.Generic;
using UnityEngine;

public static class TaskListsSaver
{
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
	 */
	public static void SaveTask(TaskList taskList)
	{
		Debug.LogError("Need to implement Task saving"); //TODO implement task saving
	}
}