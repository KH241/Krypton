using System;
using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;
using Vuforia.EditorClasses;

public class TestManager : MonoBehaviour
{
	public MoleculeSO[] Molecules;
	
	public GameObject AtomObject;
	public GameObject MoleculeObject;
	
	public TrackableBehaviour[] ImageTargets;

	public Dictionary<TrackableBehaviour, TestAtom> trackedImageTargets;

	private void Update()
	{
		foreach (TrackableBehaviour target in ImageTargets)
		{
			AtomSO data = target.gameObject.GetComponent<ImageTarget>().Atom;
			switch (target.CurrentStatus)
			{
				case Status.TRACKED:
					if (!trackedImageTargets.ContainsKey(target))
					{
						GameObject atom = Instantiate(AtomObject, target.transform);
						atom.GetComponent<TestAtom>().Spawn(data);
						trackedImageTargets[target] = atom.GetComponent<TestAtom>();
                    }
					
					break;
				default:
					if (trackedImageTargets.ContainsKey(target))
					{
						Destroy(target.gameObject.GetComponent<GameObject>());
						trackedImageTargets.Remove(target);
					}
					break;
			}
		}

		foreach (MoleculeSO molecule in Molecules)
		{
			bool moleculeCanBeCreated = true;
			
			foreach (AtomSO atom in molecule.Atoms)
			{
				bool atomInScene = false;
				
				foreach (TrackableBehaviour tracked in trackedImageTargets.Keys)
                {
                	AtomSO data = tracked.gameObject.GetComponent<ImageTarget>().Atom;
					if (data == atom)
					{
						atomInScene = true;
						break;
					}
				}

				if (!atomInScene)
				{
					moleculeCanBeCreated = false;
					break;
				}
			}

			if (moleculeCanBeCreated)
			{
				Instantiate(MoleculeObject);
			}
		}
	}
}