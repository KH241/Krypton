using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace TaskMode
{
	/**
	 * Saves + Loads Tasklist from and to local Storage
	 */
	public static class TaskListsSaver
	{
		private static string FILEPATH = Application.persistentDataPath + "taskLists.json";
		
		public static TaskList[] TaskLists => LoadTaskLists();

		/**
		 * Returns all saved TaskLists from local Storage
		 */
		public static TaskList[] LoadTaskLists()
		{
			//Create File if it doesnt exist
			if (!File.Exists(FILEPATH)) { File.Create(FILEPATH).Close(); }
			
			StreamReader reader = new StreamReader(FILEPATH);
	        string json = reader.ReadToEnd();
	        reader.Close();

			
			TaskList[] output = JsonUtility.FromJson<ListWrapper>(json)?.TaskLists;
			if (output == null) { return new TaskList[0]; }

			return output.ToArray();
		}

		/**
		 * Saves given Tasklist into local Storage
		 * @param taskList needs to be unique, otherwise the old tasklist will be overriden
		 * @return The Id of the Task inside TaskLists
		 */
		public static int SaveTask(TaskList taskList)
		{
			int id;
			
			List<TaskList> lists = TaskLists.ToList();
			
			//Override the old Tasklist, if the name is already in TaskLists
			for (int i=0;i < lists.Count; i++)
			{
				if (lists[i].Name == taskList.Name) { lists.RemoveAt(i); }
			}
			
			id = lists.Count;
			
			//Add the new Tasklist
			lists.Add(taskList);

			//Put the list inside a wrapper, so it can be serialized by JSONUtility
			ListWrapper wrapper = new ListWrapper();
			wrapper.TaskLists = lists.ToArray();
			
			//Create File if it doesnt exist
			if (!File.Exists(FILEPATH)) { File.Create(FILEPATH).Close(); }
			
			
			//Write to file
			StreamWriter writer = new StreamWriter(FILEPATH, false);
			writer.Write(JsonUtility.ToJson(wrapper, true));
			writer.Close();
			
			return id;
		}
		
		//TasklistWrapper (for JSONUtility)
		private class ListWrapper { public TaskList[] TaskLists; }
	}	
}