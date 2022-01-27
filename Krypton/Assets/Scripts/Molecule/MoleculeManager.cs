using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;
using TaskMode;
#pragma warning disable 618

namespace Molecule
{
	public class MoleculeManager : MonoBehaviour
	{
		[Range(0, 10)] public float MoleculeRange;
		public MoleculeSO[] Molecules;

		public GameObject AtomObject;
		public GameObject MoleculeObject;

		public MoleculeSO activeMolecule;

		//All imageTargets that can be tracked
		private TrackableBehaviour[] ImageTargets;

		//All tracked imagetargets (updates each frame)
		private Dictionary<TrackableBehaviour, MoleculeAtom> trackedImageTargets;

		//All Molecules in the Scene (also contains a list of atom-imageTargets it uses)
		private Dictionary<TrackableBehaviour, MoleculeAtom> usedAtoms;

		//All References
		private MoleculeObject activeMoleculeObject;

		public MoleculeSO[] PossibleMolecules => possibleMolecules.Keys.ToArray();
		private Dictionary<MoleculeSO, Dictionary<TrackableBehaviour, MoleculeAtom>> possibleMolecules;

		private void Start()
		{
			trackedImageTargets = new Dictionary<TrackableBehaviour, MoleculeAtom>();
			usedAtoms = new Dictionary<TrackableBehaviour, MoleculeAtom>();
			possibleMolecules = new Dictionary<MoleculeSO, Dictionary<TrackableBehaviour, MoleculeAtom>>();

			ImageTargets = gameObject.GetComponentsInChildren<TrackableBehaviour>();
		}

		private void Update()
		{
			UpdateImageTargets();

			UpdateMolecules();
		}
		
		/*
		 * Checks the scene for active imageTargets and spawns atoms ontop
		 */
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
							atom.GetComponent<MoleculeAtom>().Spawn(target.gameObject.GetComponent<ImageTarget>().Atom);

							//Save the reference to the Atom + that the target is tracked
							trackedImageTargets[target] = atom.GetComponent<MoleculeAtom>();
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

		/*
		 * Molecule Logic (called each frame)
		 * Creates molecules when possible and destroys them when requirements are not met anymore
		 */
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
				possibleMolecules = new Dictionary<MoleculeSO, Dictionary<TrackableBehaviour, MoleculeAtom>>();
				//Iterate over all Molecules that can be created
				foreach (MoleculeSO molecule in Molecules)
				{
					if (possibleMolecules.ContainsKey(molecule))
					{
						continue;
					}
					bool moleculeCanBeCreated = true;

					//Contains a list of Imagetargets that could form the Molecule
					Dictionary<TrackableBehaviour, MoleculeAtom> possibleMolecule =
						new Dictionary<TrackableBehaviour, MoleculeAtom>();

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
								possibleMolecule[tracked] = tracked.GetComponentInChildren<MoleculeAtom>();
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

		/*
		 * Spawns the molecule given (if possible) and hides all the atoms used to build it
		 */
		public void SpawnMolecule(MoleculeSO molecule)
		{
			if (!possibleMolecules.ContainsKey(molecule)) { return; }
			
			//Create the molecule + save references
			GameObject moleculeObject = Instantiate(MoleculeObject, possibleMolecules[molecule].First().Value.transform);
			activeMoleculeObject = moleculeObject.GetComponent<MoleculeObject>();
			moleculeObject.GetComponent<MoleculeObject>().Spawn(molecule);
			usedAtoms = possibleMolecules[molecule];
			activeMolecule = molecule;

			//Hide all atoms used to display molecule
			foreach (var atom in possibleMolecules[molecule])
			{
				atom.Value.Used = true;
			}
			
			TaskModeManger.Singleton.MoleculeCreated(molecule);
		}

		/*
		 * Returns true, if all targets are inside the Range (Range is a param of MoleculeManager
		 */
		private bool AllTargetsInsideRange(Dictionary<TrackableBehaviour, MoleculeAtom> targets)
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

		/*
		 * Destroys the Active Molecule
		 */
		public void DestroyMolecule()
		{
			//Show all atoms formerly used to create molecule (that are still in the scene)		
			foreach (var atom in usedAtoms)
			{
				if (trackedImageTargets.ContainsKey(atom.Key)) { atom.Value.Used = false; }
			}

			usedAtoms = new Dictionary<TrackableBehaviour, MoleculeAtom>();
			Destroy(activeMoleculeObject.gameObject);
			activeMoleculeObject = null;
			activeMolecule = null;
		}
	}	
}