using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class TaskListsSaver
{
	private const string FILEPATH = "Assets/Resources/taskLists.txt";

	public static TaskList[] TaskLists => LoadTaskLists();

	/**
	 * Returns all saved TaskLists from local Storage
	 */
	public static TaskList[] LoadTaskLists()
	{
		StreamReader reader = new StreamReader(FILEPATH);
        string json = reader.ReadToEnd();
        reader.Close();

		
		TaskList[] output = JsonUtility.FromJson<ListWrapper>(json)?.TaskLists;
		if (output == null) { return new TaskList[0]; }

		return output.ToArray();
	}

	/**
	 * Saves given Tasklist into local Storage
	 * The Name of the Task list must be unique
	 */
	public static int SaveTask(TaskList taskList)
	{
		//TODO unique task saving
		int id = TaskLists.Length;
		
		List<TaskList> lists = TaskLists.ToList();
		lists.Add(taskList);

		ListWrapper wrapper = new ListWrapper();
		wrapper.TaskLists = lists.ToArray();
		
		StreamWriter writer = new StreamWriter(FILEPATH, false);
		writer.Write(JsonUtility.ToJson(wrapper, true));
		writer.Close();
		
		return id;
	}
	
	private class ListWrapper
	{
		public TaskList[] TaskLists;
	}
}