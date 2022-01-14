using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAtom : MonoBehaviour
{
	public AtomSO Atom;
	public MeshRenderer mesh;

	public bool Used
	{
		get { return used; }
		set
		{
			used = value; 
			OnVisible();
		}
	}
	private bool used = false;

	private void OnVisible()
	{
		mesh.enabled = !Used;
	}

	public void Spawn(AtomSO atom)
	{
		Atom = atom;

		switch (atom.ID)
		{
			case 1:
				mesh.material.color = Color.white;
				break;
			case 6:
            	mesh.material.color = Color.black;
            	break;
			case 8:
				mesh.material.color = Color.red;
				break;
			default:
				mesh.material.color = Color.cyan;
				break;
		}
	}
}
