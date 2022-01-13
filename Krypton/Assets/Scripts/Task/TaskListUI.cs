using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskListUI : MonoBehaviour
{
	public TMP_Text Heading;
	public TMP_Text List;

	private void Awake()
	{
		TaskModeManger.Singleton.TasksChanged += UpdateList;
	}

	private void OnDestroy()
	{
		TaskModeManger.Singleton.TasksChanged -= UpdateList;
	}

	private void OnEnable()
	{
		UpdateList();
	}

	/*
	 * Writes the whole TaskList from TaskModeManager.Singleton.Tasks
	 */
	private void UpdateList(bool success=true)
	{
		Heading.text = $"{TaskModeManger.Singleton.Tasks.CountDone}/{TaskModeManger.Singleton.Tasks.CountTotal} Tasks Done";

		List.text = "";
		foreach (Task task in TaskModeManger.Singleton.Tasks)
		{
			List.text += task.Name + "\n";
		}
	}
}
