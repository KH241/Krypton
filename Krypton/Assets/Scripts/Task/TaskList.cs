using System.Collections.Generic;

public class TaskList
{
	public Task[] Tasks => tasks.ToArray();
	private List<Task> tasks;
	public int CountTotal => tasks.Count;
	public int CountDone => getCountDone();
	public bool Done => CountDone == CountTotal;

	private int getCountDone()
	{
		int output = 0;
		
		foreach (Task task in tasks)
		{
			if (task.Done) { output++; }
		}

		return output;
	}
}