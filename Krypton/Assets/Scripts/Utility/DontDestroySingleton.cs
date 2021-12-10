using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySingleton<T> : MonoBehaviour where T : Component
{
	public static T Singleton => singleton;
	private static T singleton;
	void Awake()
	{
		if (singleton != null && singleton != this)
		{
			Destroy(gameObject);
			return;
		}

		singleton = this as T;
		DontDestroyOnLoad(this);
	}
}
