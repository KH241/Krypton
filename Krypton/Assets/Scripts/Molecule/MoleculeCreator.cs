using System;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MoleculeCreator : MonoBehaviour
{
	public GameObject atom1 = null;
	public GameObject atom2 = null;
	public GameObject atom3 = null;
	public GameObject atom4 = null;
	public GameObject atom5 = null;
	public GameObject atom6 = null;
	

	public GameObject waterMolecule;
	public GameObject methanMolecule;
	private GameObject[] _imageTargets;
	private List<GameObject> _trackedImageTargets;
	private AtomManager _atomManager;
	public AtomSO oxygenData;
	public AtomSO hydrogenData;
	public AtomSO carbonData;

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
		_atomManager.hydrogenData = hydrogenData;
		_atomManager.oxygenData = oxygenData;
		_atomManager.carbonData = carbonData;
		
	}

	// Update is called once per frame
	void Update()
	{
		if (moleculeCreated)
		{
			RequirementMetH2O();
			RequirementMetCH4();
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
			atom4 = GameObject.Find("hydrogenAtom3");
			atom5 = GameObject.Find("hydrogenAtom4");
			atom6 = GameObject.Find("carbonAtom1");

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
			
			if (status == Status.NO_POSE && atom4 != null && _imageTargets[i].name.Contains("Hydrogen3"))
			{
				atom4.SetActive(false);
				Destroy(atom4);
			}
			
			if (status == Status.NO_POSE && atom5 != null && _imageTargets[i].name.Contains("Hydrogen4"))
			{
				atom5.SetActive(false);
				Destroy(atom5);
			}
			
			if (status == Status.NO_POSE && atom6 != null && _imageTargets[i].name.Contains("Carbon1"))
			{
				atom6.SetActive(false);
				Destroy(atom6);
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
			
			if (status == Status.TRACKED && atom4 == null && _imageTargets[i].name.Contains("Hydrogen3"))
			{
				SpawnAtom(_imageTargets[i]);
				return;
			}
			
			if (status == Status.TRACKED && atom5 == null && _imageTargets[i].name.Contains("Hydrogen4"))
			{
				SpawnAtom(_imageTargets[i]);
				return;
			}
			
			if (status == Status.TRACKED && atom6 == null && _imageTargets[i].name.Contains("Carbon1"))
			{
				SpawnAtom(_imageTargets[i]);
				return;
			}
		}

		RequirementMetH2O();
		RequirementMetCH4();
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
		else if (imageTargetName.Contains("Carbon"))
		{
			_atomManager.createAtoms(carbonData, imageTargetGameObject);
		}
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
	
	private void RequirementMetCH4()
	{
		bool haveC = false;
		bool haveH1 = false;
		bool haveH2 = false;
		bool haveH3 = false;
		bool haveH4 = false;

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
			if (_trackedImageTargets[i].name.Contains("Carbon1"))
			{
				haveC = true;
			}

			if (_trackedImageTargets[i].name.Contains("Hydrogen1"))
			{
				haveH1 = true;
			}

			if (_trackedImageTargets[i].name.Contains("Hydrogen2"))
			{
				haveH2 = true;
			}
			
			if (_trackedImageTargets[i].name.Contains("Hydrogen3"))
			{
				haveH3 = true;
			}
			
			if (_trackedImageTargets[i].name.Contains("Hydrogen4"))
			{
				haveH4 = true;
			}
		}

		if (haveC && haveH1 && haveH2 && haveH3 && haveH4)
		{
			moleculeCreated = true;
			methanMolecule.SetActive(true);
			DeactivateAllAtoms();
		}
		else
		{
			moleculeCreated = false;
			methanMolecule.SetActive(false);
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
		atom4.SetActive(false);
		Destroy(atom4);
		atom5.SetActive(false);
		Destroy(atom5);
		atom6.SetActive(false);
		Destroy(atom6);
	}
}