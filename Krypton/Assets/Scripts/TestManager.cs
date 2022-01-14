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

	//All imageTargets that can be tracked
	private TrackableBehaviour[] ImageTargets;

	//All tracked imagetargets (updates each frame)
	private Dictionary<TrackableBehaviour, TestAtom> trackedImageTargets;

	//All Molecules in the Scene (also contains a list of atom-imageTargets it uses)
	private Dictionary<MoleculeSO, Dictionary<TrackableBehaviour, TestAtom>> trackedMolecules;

	//All References
	private Dictionary<MoleculeSO, TestMolecule> trackedMoleculesObjects;

	private void Start()
	{
		trackedImageTargets = new Dictionary<TrackableBehaviour, TestAtom>();
		trackedMolecules = new Dictionary<MoleculeSO, Dictionary<TrackableBehaviour, TestAtom>>();
		trackedMoleculesObjects = new Dictionary<MoleculeSO, TestMolecule>();

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
		//Iterate over all Molecules that can be created
		foreach (MoleculeSO molecule in Molecules)
		{
			//If the Molecule already exists
			if (trackedMolecules.ContainsKey(molecule))
			{
				//Loop over all imagetargets it is using and check if they are still tracked
				foreach (var target in trackedMolecules[molecule])
				{
					//If the target is not tracked anymore, destoy the molecule
					if (!trackedImageTargets.ContainsKey(target.Key)) { DestroyMolecule(molecule); }
				}
				
				//If the targets forming the molecule are too far away from each other + the molecule hasnt been destroyed yet
				if (trackedMolecules.ContainsKey(molecule))
				{
					if (!AllTargetsInsideRange(trackedMolecules[molecule]))
					{
						DestroyMolecule(molecule);
					}					
				}
			}
			
			bool moleculeCanBeCreated = true;
			
			//Contains a list of Imagetargets that could form the Molecule
			Dictionary<TrackableBehaviour, TestAtom> possibleMolecule = new Dictionary<TrackableBehaviour, TestAtom>();

			//Iterate over all Atoms needed to form Molecule
			foreach (AtomSO atom in molecule.Atoms)
			{
				
				bool atomInScene = false;
				
				//Iterate over all tracked Imagetargets to check if the atom needed is in the scene
				foreach (TrackableBehaviour tracked in trackedImageTargets.Keys)
				{
					//If the Atom is already used to form the/a molecule
					if (possibleMolecule.ContainsKey(tracked)) { continue; }
					if (trackedImageTargets[tracked].Used) { Debug.Log("used");continue; }

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
			
			if (moleculeCanBeCreated && AllTargetsInsideRange(possibleMolecule))
			{
				Debug.Log(possibleMolecule.First());
				//Create the molecule + save references
				GameObject moleculeObject = Instantiate(MoleculeObject, possibleMolecule.First().Value.transform);
				trackedMoleculesObjects[molecule] = moleculeObject.GetComponent<TestMolecule>();
				// trackedMoleculesObjects[molecule].Spawn(molecule);
				moleculeObject.GetComponent<TestMolecule>().Spawn(molecule);
				trackedMolecules[molecule] = possibleMolecule;
				
				//Hide all atoms used to display molecule
				foreach (var atom in possibleMolecule) { atom.Value.Used = true; }
			}
		}
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
				if (Vector3.Distance(position1, position2) > MoleculeRange) { return false; }
			}
		}

		return true;
	}

	private void DestroyMolecule(MoleculeSO molecule)
	{
		//Show all atoms formerly used to create molecule (that are still in the scene)		
		foreach (var atom in trackedMolecules[molecule])
		{
			if (trackedImageTargets.ContainsKey(atom.Key))
			{
				atom.Value.Used = false;
			}
		}

		//Destroy the Molecule + remove all references to it
		Destroy(trackedMoleculesObjects[molecule]);
		trackedMoleculesObjects.Remove(molecule);
		trackedMolecules.Remove(molecule);
	}
}