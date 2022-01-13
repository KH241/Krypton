using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAtom : MonoBehaviour
{
	public AtomSO Atom;
	public MeshRenderer mesh;

	public bool Visible
	{
		set { OnVisible(); }
	}

	private void OnVisible()
	{
		
	}

	public void Spawn(AtomSO atom)
	{
		Atom = atom;

		switch (atom.ID)
		{
			case 8:
				mesh.material.color = Color.red;
				break;
			case 6:
				mesh.material.color = Color.black;
				break;
			default:
				mesh.material.color = Color.cyan;
				break;
		}
	}
}
