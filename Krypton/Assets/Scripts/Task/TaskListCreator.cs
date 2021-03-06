using System;
using UnityEngine;

namespace TaskMode
{
	/**
	 * Handles the Task List Creation
	 */
	public class TaskListCreator : MonoBehaviour
	{
		public CreateTaskUI UI;

		public TaskList TaskList { get; private set; }

		private void Start()
		{
			string name = DateTime.Now.ToString("dd/MM/yy HH:mm:ss");
			TaskList = new TaskList(name);
			UI.UpdateTaskList();
		}

		#region TaskAdder

		/**
		 * Adds a Create - Atom - Task
		 */
		public void AddCreateAtom(AtomSO atom)
		{
			Task task = new Task(TaskType.CreateAtom, atom);
			TaskList.AddTask(task);
			UI.UpdateTaskList();
		}

		/**
		 * Adds a View - Atom - Task
		 */
		public void AddViewAtom(AtomSO atom)
		{
			Task task = new Task(TaskType.ViewAtom, atom);
			TaskList.AddTask(task);
			UI.UpdateTaskList();
		}

		/**
		 * Adds a Create - Molecule - Task
		 */
		public void AddCreateMolecule(MoleculeSO molecule)
		{
			Task task = new Task(TaskType.CreateMolecule, molecule);
			TaskList.AddTask(task);
			UI.UpdateTaskList();
		}

		/**
		 * Adds a View - Molecule - Task
		 */
		public void AddViewMolecule(MoleculeSO molecule)
		{
			Task task = new Task(TaskType.ViewMolecule, molecule);
			TaskList.AddTask(task);
			UI.UpdateTaskList();
		}
		
		#endregion

		/**
		 * @return The Id of the created tasklist inside TaskListsSaver.TaskLists
		 */
		public int Save() { return TaskListsSaver.SaveTask(TaskList); }
	}	
}