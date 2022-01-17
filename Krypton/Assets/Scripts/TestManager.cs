using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Vuforia;

public class TestManager : MonoBehaviour
{
	[Range(0, 10)] public float MoleculeRange;
	public MoleculeSO[] Molecules;

	public GameObject AtomObject;
	public GameObject MoleculeObject;

	public MoleculeSO activeMolecule;

	//All imageTargets that can be tracked
	private TrackableBehaviour[] ImageTargets;

	//All tracked imagetargets (updates each frame)
	private Dictionary<TrackableBehaviour, TestAtom> trackedImageTargets;

	//All Molecules in the Scene (also contains a list of atom-imageTargets it uses)
	private Dictionary<TrackableBehaviour, TestAtom> usedAtoms;

	//All References
	private TestMolecule activeMoleculeObject;

	public MoleculeSO[] PossibleMolecules => possibleMolecules.Keys.ToArray();
	private Dictionary<MoleculeSO, Dictionary<TrackableBehaviour, TestAtom>> possibleMolecules;

	private void Start()
	{
		trackedImageTargets = new Dictionary<TrackableBehaviour, TestAtom>();
		usedAtoms = new Dictionary<TrackableBehaviour, TestAtom>();
		possibleMolecules = new Dictionary<MoleculeSO, Dictionary<TrackableBehaviour, TestAtom>>();

		ImageTargets = gameObject.GetComponentsInChildren<TrackableBehaviour>();
	}

	private void Update()
	{
		UpdateImageTargets();

		UpdateMolecules();
	}

	private void UpdateImageTargets()
	{
		//Loop trough all imagetargets and check if they are tracked
		foreach (TrackableBehaviour target in ImageTargets)
		{
			switch (target.CurrentStatus)
			{
				case Status.TRACKED:
					//If it not already tracked (aka it is the first frame it is tracked)
					if (!trackedImageTargets.ContainsKey(target))
					{
						//Spawn the Atom over the Imagetarget
						GameObject atom = Instantiate(AtomObject, target.transform);
						atom.GetComponent<TestAtom>().Spawn(target.gameObject.GetComponent<ImageTarget>().Atom);

						//Save the reference to the Atom + that the target is tracked
						trackedImageTargets[target] = atom.GetComponent<TestAtom>();
					}

					break;

				//If it is not tracked
				default:
					//If it was tracked in the last frame
					if (trackedImageTargets.ContainsKey(target))
					{
						//Destroy the Atom-Object + Remove the target from the list of tracked targets
						Destroy(trackedImageTargets[target].gameObject);
						trackedImageTargets.Remove(target);
					}

					break;
			}
		}
	}

	private void UpdateMolecules()
	{
		
		if (activeMolecule != null)
		{
			//Loop over all imagetargets it is using and check if they are still tracked
			foreach (var target in usedAtoms)
			{
				//If the target is not tracked anymore, destoy the molecule
				if (!trackedImageTargets.ContainsKey(target.Key))
				{
					DestroyMolecule();
				}
			}

			//If the targets forming the molecule are too far away from each other + the molecule hasnt been destroyed yet
			if (activeMolecule != null)
			{
				if (!AllTargetsInsideRange(usedAtoms))
				{
					DestroyMolecule();
				}
			}
		}
		else
		{
			possibleMolecules = new Dictionary<MoleculeSO, Dictionary<TrackableBehaviour, TestAtom>>();
			//Iterate over all Molecules that can be created
			foreach (MoleculeSO molecule in Molecules)
			{
				if (possibleMolecules.ContainsKey(molecule))
				{
					continue;
				}
				bool moleculeCanBeCreated = true;

				//Contains a list of Imagetargets that could form the Molecule
				Dictionary<TrackableBehaviour, TestAtom> possibleMolecule =
					new Dictionary<TrackableBehaviour, TestAtom>();

				//Iterate over all Atoms needed to form Molecule
				foreach (AtomSO atom in molecule.Atoms)
				{
					bool atomInScene = false;

					//Iterate over all tracked Imagetargets to check if the atom needed is in the scene
					foreach (TrackableBehaviour tracked in trackedImageTargets.Keys)
					{
						//If the Atom is already used to form the/a molecule
						if (possibleMolecule.ContainsKey(tracked))
						{
							continue;
						}

						if (trackedImageTargets[tracked].Used)
						{
							continue;
						}

						//If the tracked Imagetargets is the atom needed
						AtomSO data = tracked.gameObject.GetComponentInChildren<ImageTarget>().Atom;
						if (data == atom)
						{
							atomInScene = true;

							//Add Atom to list of Imagetargets that (could) form the molecule
							possibleMolecule[tracked] = tracked.GetComponentInChildren<TestAtom>();
							break;
						}
					}

					if (!atomInScene)
					{
						moleculeCanBeCreated = false;
						break;
					}
				}

				if (moleculeCanBeCreated && AllTargetsInsideRange(possibleMolecule) && possibleMolecule.Count > 0)
				{
					possibleMolecules[molecule] = possibleMolecule;
				}
			}
		}
	}

	public void SpawnMolecule(MoleculeSO molecule)
	{
		if (!possibleMolecules.ContainsKey(molecule))
		{
			return;
		}
		
		//Create the molecule + save references
		GameObject moleculeObject = Instantiate(MoleculeObject, possibleMolecules[molecule].First().Value.transform);
		activeMoleculeObject = moleculeObject.GetComponent<TestMolecule>();
		moleculeObject.GetComponent<TestMolecule>().Spawn(molecule);
		usedAtoms = possibleMolecules[molecule];
		activeMolecule = molecule;

		//Hide all atoms used to display molecule
		foreach (var atom in possibleMolecules[molecule])
		{
			atom.Value.Used = true;
		}
		
		TaskModeManger.Singleton.MoleculeCreated(molecule);
	}

	private bool AllTargetsInsideRange(Dictionary<TrackableBehaviour, TestAtom> targets)
	{
		List<Vector3> positions = new List<Vector3>();

		foreach (var target in targets)
		{
			positions.Add(target.Value.gameObject.transform.position);
		}

		foreach (var position1 in positions)
		{
			foreach (var position2 in positions)
			{
				if (Vector3.Distance(position1, position2) > MoleculeRange)
				{
					return false;
				}
			}
		}

		return true;
	}

	public void DestroyMolecule()
	{
		//Show all atoms formerly used to create molecule (that are still in the scene)		
		foreach (var atom in usedAtoms)
		{
			if (trackedImageTargets.ContainsKey(atom.Key))
			{
				atom.Value.Used = false;
			}
		}

		usedAtoms = new Dictionary<TrackableBehaviour, TestAtom>();
		Destroy(activeMoleculeObject.gameObject);
		activeMoleculeObject = null;
		activeMolecule = null;
	}
	
	
}