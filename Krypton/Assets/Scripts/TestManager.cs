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

	public Dictionary<TrackableBehaviour, TestAtom>
					trackedImageTargets = new Dictionary<TrackableBehaviour, TestAtom>();

	public Dictionary<MoleculeSO, Dictionary<TrackableBehaviour, TestAtom>> trackedMolecules =
					new Dictionary<MoleculeSO, Dictionary<TrackableBehaviour, TestAtom>>();

	public Dictionary<MoleculeSO, GameObject> trackedMoleculesObjects = new Dictionary<MoleculeSO, GameObject>();

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
			Dictionary<TrackableBehaviour, TestAtom> possibleMolecule = new Dictionary<TrackableBehaviour, TestAtom>();
			Transform transform = gameObject.transform;
			foreach (AtomSO atom in molecule.Atoms)
			{
				bool atomInScene = false;

				foreach (TrackableBehaviour tracked in trackedImageTargets.Keys)
				{
					if (possibleMolecule.ContainsKey(tracked)) { continue; }

					AtomSO data = tracked.gameObject.GetComponent<ImageTarget>().Atom;
					if (data == atom)
					{
						atomInScene = true;
						possibleMolecule[tracked] = tracked.GetComponent<TestAtom>();
						transform = tracked.transform;
						break;
					}
				}

				if (!atomInScene)
				{
					moleculeCanBeCreated = false;
					break;
				}
			}

			if (moleculeCanBeCreated && !trackedMolecules.ContainsKey(molecule))
			{
				trackedMoleculesObjects[molecule] = Instantiate(MoleculeObject, transform);
				trackedMolecules[molecule] = possibleMolecule;
			}

			if (!moleculeCanBeCreated && trackedMolecules.ContainsKey(molecule))
			{
				Destroy(trackedMoleculesObjects[molecule]);
				trackedMoleculesObjects.Remove(molecule);
				trackedMolecules.Remove(molecule);
			}
		}
	}
}