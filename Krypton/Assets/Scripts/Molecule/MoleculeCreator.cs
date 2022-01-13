using System;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MoleculeCreator : MonoBehaviour
{
	public GameObject atom1 = null;
	public GameObject atom2 = null;
	public GameObject atom3 = null;

	public GameObject waterMolecule;
	private GameObject[] _imageTargets;
	private List<GameObject> _trackedImageTargets;
	private AtomManager _atomManager;
	public AtomSO oxygenData;
	public AtomSO hydrogenData;

	public GameObject atomPrefab;

	private GameObject oxygenAtom;
	private GameObject oxygenAtom1;
	private GameObject oxygenAtom2;
	private GameObject hydrogenAtom;
	private GameObject hydrogenAtom1;
	private GameObject hydrogenAtom2;
	private GameObject carbonAtom;
	private GameObject carbonAtom1;
	private GameObject carbonAtom2;

	private bool moleculeCreated = false;

	// Start is called before the first frame update
	void Start()
	{
		_atomManager = gameObject.AddComponent<AtomManager>();
		_atomManager.atomPrefab = atomPrefab;
		_atomManager.Hydrogen = hydrogenData;
		_atomManager.Oxygen = oxygenData;
	}

	// Update is called once per frame
	void Update()
	{
		if (moleculeCreated)
		{
			RequirementMetH2O();
			return;
		}

		//get all image target of that are tagged in scene
		_imageTargets = GameObject.FindGameObjectsWithTag("ImageTarget");

		//iterate through image targets that are tagged in scene
		//spawn tracked image targets
		for (int i = 0; i < _imageTargets.Length; i++)
		{
			var trackable = _imageTargets[i].GetComponent<TrackableBehaviour>();
			var status = trackable.CurrentStatus;
			Debug.Log(_imageTargets[i].name + status);

			atom1 = GameObject.Find("hydrogenAtom1");
			atom2 = GameObject.Find("oxygenAtom");
			atom3 = GameObject.Find("hydrogenAtom2");

			//deactivate atoms
			if (status == Status.NO_POSE && atom1 != null && _imageTargets[i].name.Contains("Hydrogen1"))
			{
				atom1.SetActive(false);
				Destroy(atom1);
			}

			if (status == Status.NO_POSE && atom3 != null && _imageTargets[i].name.Contains("Hydrogen2"))
			{
				atom3.SetActive(false);
				Destroy(atom3);
			}

			if (status == Status.NO_POSE && atom2 != null && _imageTargets[i].name.Contains("Oxygen"))
			{
				atom2.SetActive(false);
				Destroy(atom2);
			}

			//activate atoms
			if (status == Status.TRACKED && atom1 == null && _imageTargets[i].name.Contains("Hydrogen1"))
			{
				SpawnAtom(_imageTargets[i]);
				return;
			}

			if (status == Status.TRACKED && atom3 == null && _imageTargets[i].name.Contains("Hydrogen2"))
			{
				SpawnAtom(_imageTargets[i]);
				return;
			}

			if (status == Status.TRACKED && atom2 == null && _imageTargets[i].name.Contains("Oxygen"))
			{
				SpawnAtom(_imageTargets[i]);
				return;
			}
		}


		////////////////////////////////////////////
		//       molecule Logic	                  //
		////////////////////////////////////////////


		/**
		 * check if water can be created
		 */
		RequirementMetH2O();
	}

	private void SpawnAtom(GameObject imageTargetGameObject)
	{
		String imageTargetName = imageTargetGameObject.name;

		if (imageTargetName.Contains("Oxygen"))
		{
			_atomManager.createAtoms(oxygenData, imageTargetGameObject);
		}
		else if (imageTargetName.Contains("Hydrogen"))
		{
			_atomManager.createAtoms(hydrogenData, imageTargetGameObject);
		}
	}

	private void AssignAtom(GameObject atom)
	{
		String atomName = atom.name;

		switch (atomName)
		{
			case string a when a.Contains("Oxygen"):
				AssignOxygenAtom(atom);
				break;
			case string a when a.Contains("Hydrogen"):
				AssignHydrogenAtom(atom);
				break;
			case string a when a.Contains("Carbon"):
				AssignCarbonAtom(atom);
				break;
		}
	}

	private void AssignCarbonAtom(GameObject atom)
	{
		if (carbonAtom != null)
		{
			carbonAtom = atom;
		}
		else if (carbonAtom1 != null)
		{
			carbonAtom1 = atom;
		}
		else if (carbonAtom2 != null)
		{
			carbonAtom2 = atom;
		}
	}

	private void AssignHydrogenAtom(GameObject atom)
	{
		if (hydrogenAtom != null)
		{
			hydrogenAtom = atom;
		}
		else if (hydrogenAtom1 != null)
		{
			hydrogenAtom1 = atom;
		}
		else if (hydrogenAtom2 != null)
		{
			hydrogenAtom2 = atom;
		}
	}

	private void AssignOxygenAtom(GameObject atom)
	{
		if (oxygenAtom != null)
		{
			oxygenAtom = atom;
		}
		else if (oxygenAtom1 != null)
		{
			oxygenAtom1 = atom;
		}
		else if (oxygenAtom2 != null)
		{
			oxygenAtom2 = atom;
		}
	}

	public IEnumerable<WaitForSeconds> sleep()
	{
		yield return new WaitForSeconds(1);
	}

	private void RequirementMetH2O()
	{
		bool haveO = false;
		bool haveH1 = false;
		bool haveH2 = false;

		_imageTargets = GameObject.FindGameObjectsWithTag("ImageTarget");
		_trackedImageTargets = new List<GameObject>();

		for (int i = 0; i < _imageTargets.Length; i++)
		{
			var trackable = _imageTargets[i].GetComponent<TrackableBehaviour>();
			var status = trackable.CurrentStatus;
			Debug.Log(_imageTargets[i].name + status);

			if (status == Status.TRACKED)
			{
				_trackedImageTargets.Add(_imageTargets[i]);
			}
		}

		for (int i = 0; i < _trackedImageTargets.Count; i++)
		{
			if (_trackedImageTargets[i].name.Contains("Oxygen"))
			{
				haveO = true;
			}

			if (_trackedImageTargets[i].name.Contains("Hydrogen1"))
			{
				haveH1 = true;
			}

			if (_trackedImageTargets[i].name.Contains("Hydrogen2"))
			{
				haveH2 = true;
			}
		}

		if (haveO && haveH1 && haveH2)
		{
			moleculeCreated = true;
			waterMolecule.SetActive(true);
			DeactivateAllAtoms();
		}
		else
		{
			moleculeCreated = false;
			waterMolecule.SetActive(false);
		}
	}

	private void DeactivateAllAtoms()
	{
		atom1.SetActive(false);
		Destroy(atom1);
		atom2.SetActive(false);
		Destroy(atom2);
		atom3.SetActive(false);
		Destroy(atom3);
	}
}