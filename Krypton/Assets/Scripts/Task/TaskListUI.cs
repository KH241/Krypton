using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskListUI : MonoBehaviour
{
	public TMP_Text Heading;
	public GridLayoutGroup List;
	public GameObject TaskPrefab;

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
		if (!TaskModeManger.Singleton.Active) { return; }
		
		Heading.text = $"{TaskModeManger.Singleton.Tasks.CountDone}/{TaskModeManger.Singleton.Tasks.CountTotal} Tasks Done";

		//Clear the List
		foreach (Transform child in List.transform) { Destroy(child.gameObject); }
		
		foreach (Task task in TaskModeManger.Singleton.Tasks)
		{
			TMP_Text taskEntry = Instantiate(TaskPrefab, List.transform).GetComponent<TMP_Text>();
			
			taskEntry.text += " " + task.Name + " ";
			
			if (task.Done) { taskEntry.fontStyle = FontStyles.Strikethrough; }
		}
	}
}
