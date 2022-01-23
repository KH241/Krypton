using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

	public void OnClick()
	{
		Debug.Log("test");
		GameObject help = Instantiate(TaskHelp, gameObject.GetComponentInParent<TaskListUI>().transform);
		TaskHelpUI ui = help.GetComponent<TaskHelpUI>();
		ui.Initialize(Task);
	}
}