using TMPro;
using UnityEngine;

namespace TaskMode
{
	/**
	 * Task Entry in the Tasklist - UI
	 */
	public class TaskListTask : MonoBehaviour
	{
		public GameObject TaskHelp;
		
		public TMP_Text Text;
		public Task Task;

		public void Initialize(Task task)
		{
			Task = task;
			Text.text += " " + task.Name + " ";
			
			if (task.Done) { Text.fontStyle = FontStyles.Strikethrough; }
		}

		/**
		 * Opens the TaskHelp for the Task
		 */
		public void OnClick()
		{
			GameObject help = Instantiate(TaskHelp, gameObject.GetComponentInParent<TaskListUI>().transform);
			TaskHelpUI ui = help.GetComponent<TaskHelpUI>();
			ui.Initialize(Task);
		}
	}	
}