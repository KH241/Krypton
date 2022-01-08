using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;

public class MoleculeCreator : MonoBehaviour
{
	// public GameObject atom1;
	// public GameObject atom2;
	// public GameObject atom3;

	public GameObject mole;
	private GameObject[] _imageTargets;
	private GameObject[] _trackedImageTargets = new GameObject[100];
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

	// Start is called before the first frame update
	void Start()
	{
		_atomManager = gameObject.AddComponent<AtomManager>();
		_atomManager.atomPrefab = atomPrefab;
		_atomManager.hydrogenData = hydrogenData;
		_atomManager.oxygenData = oxygenData;
	}

	// Update is called once per frame
	void Update()
	{
		//get all image target of that are tagged in scene
		_imageTargets = GameObject.FindGameObjectsWithTag("ImageTarget");
		GameObject atom1 = null;
		GameObject atom2 = null;
		GameObject atom3 = null;

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

		// if (status == Status.TRACKED)
		// {
		// 	_trackedImageTargets[i] = _imageTargets[i];
		// }


		////////////////////////////////////////////
		//       molecule Logic	                  //
		////////////////////////////////////////////


		GameObject[] atoms = new GameObject[_trackedImageTargets.Length];

		for (int i = 0; i < _trackedImageTargets.Length; i++)
		{
			atoms[i] = _trackedImageTargets[i].transform.GetChild(0).gameObject;
		}

		foreach (GameObject atom in atoms)
		{
			AssignAtom(atom);
		}
		
		
		// checkIfH20CanBeCreated();
		RequirementMetH2O();


		float distance12 = Vector3.Distance(atom1.transform.position, atom2.transform.position);
		// float distance13 = Vector3.Distance(atom1.transform.position, atom3.transform.position);
		// float distance23 = Vector3.Distance(atom2.transform.position, atom3.transform.position);
		float valid1 = distance12 / 10;
		// float valid2 = distance13 / 10;
		// float valid3 = distance23 / 10;


		mole.SetActive(false);

		if (distance12 > 7)
		{
			mole.SetActive(false);
		}
		else if (5 > distance12) // && 5 > distance13 ) //&& 5 > distance23)
		{
			mole.SetActive(true);
		}

		Debug.Log("distance12: " + distance12);
		// Debug.Log("distance13: " + distance13);
		// Debug.Log("distance23" + distance23);
	}

	private void SpawnAtom(GameObject imageTargetGameObject)
	{
		String imageTargetName = imageTargetGameObject.name;
		// Vector3 transformPosition = imageTargetGameObject.transform.position;
		// imageTargetGameObject.transform.position = new Vector3(imageTargetGameObject.transform.position.x, -4,
		// 	imageTargetGameObject.transform.position.z);

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
		if (oxygenAtom != null && hydrogenAtom != null && hydrogenAtom1 != null)
		{
			IEnumerable<GameObject> atoms = Resources.FindObjectsOfTypeAll<GameObject>()
				.Where(obj => obj.name == "Name");

			foreach (GameObject atom in atoms)
			{
				atom.SetActive(false);
				Destroy(atom);
			}

			mole.SetActive(true);
		}
	}
}