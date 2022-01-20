using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class TaskList : IEnumerable<Task>
{
	public List<Task> tasks = new List<Task>();
	public int CountTotal => tasks.Count;
	public int CountDone => getCountDone();
	public bool Done => CountDone == CountTotal;
	public string Name;
	
	public TaskList(string name) { Name = name; }
	public TaskList(string name, List<Task> tasks) : this(name, tasks.ToArray()) { }
	public TaskList(string name, Task[] tasks)
	{
		Name = name;
		foreach (Task task in tasks) { this.tasks.Add(task); }
	}

	private int getCountDone()
	{
		int output = 0;
		
		foreach (Task task in tasks)
		{
			if (task.Done) { output++; }
		}

		return output;
	}

	public IEnumerator<Task> GetEnumerator()
	{
		return tasks.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void AddTask(Task task) { tasks.Add(task); }
	public void RemoveTask(Task task) { tasks.Remove(task); }
}