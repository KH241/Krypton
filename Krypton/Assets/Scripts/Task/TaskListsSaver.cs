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

		foreach (AtomSO atom in SOList.AtomList)
		{
			TaskList temp = new TaskList("Task " + atom.Name);
            temp.AddTask(new CreateAtomTask(atom));
			temp.AddTask(new ViewAtomTask(atom));
    
            output.Add(temp);
		}
		
		foreach (MoleculeSO molecule in SOList.MoleculeList)
		{
			TaskList temp = new TaskList("Task " + molecule.Name);
			temp.AddTask(new CreateMoleculeTask(molecule));
			temp.AddTask(new ViewMoleculeTask(molecule));
    
			output.Add(temp);
		}
		
		return output.ToArray();
	}

	/**
	 * Saves given Tasklist into local Storage
	 * The Name of the Task list must be unique
	 */
	public static bool SaveTask(TaskList taskList)
	{
		foreach (TaskList list in LoadTaskLists())
		{
			if (list.Name == taskList.Name) { return false; }
		}
		
		Debug.LogError("Need to implement Task saving"); //TODO implement task saving
		
		return true;
	}
}