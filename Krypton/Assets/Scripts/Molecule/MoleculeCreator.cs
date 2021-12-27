using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MoleculeCreator : MonoBehaviour
{
	public GameObject atom1;
	public GameObject atom2;
	public GameObject atom3;

	public GameObject mole;
	private GameObject[] _gameObjects;
	private AtomManager _atomManager;
	public AtomSO oxygenData;
	public AtomSO hydrogenData;
	
	public GameObject atomPrefab;

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
		_gameObjects =  GameObject.FindGameObjectsWithTag("ImageTarget");
		
		//iterate through image targets that are tagged in scene
		//this loop is only for debugging
		for (int i = 0; i < _gameObjects.Length; i++)
		{
			var trackable = _gameObjects[i].GetComponent<TrackableBehaviour>();
			var status = trackable.CurrentStatus;
			Debug.Log(_gameObjects[i].name + status);
		}

		//spawn tracked image targets
		for (int i = 0; i < _gameObjects.Length; i++)
		{
			var trackable = _gameObjects[i].GetComponent<TrackableBehaviour>();
			var status = trackable.CurrentStatus;
			Debug.Log(_gameObjects[i].name + status);
			GameObject atom = GameObject.Find("Atom(Clone)");
			
			if (status == Status.TRACKED && atom == null )
			{
				SpawnAtom(_gameObjects[i]);
			} 
			else if (status == Status.NO_POSE && atom != null)
			{
				atom.SetActive(false);
				Destroy(atom);
			}
		}

		sleep();
		
		///////////////////////////////////////////////
		//       molecule Logic
		///////////////////////////////////////////////
		float distance12 = Vector3.Distance(atom1.transform.position, atom2.transform.position);
		float distance13 = Vector3.Distance(atom1.transform.position, atom3.transform.position);
		// float distance23 = Vector3.Distance(atom2.transform.position, atom3.transform.position);
		float valid1 = distance12 / 10;
		float valid2 = distance13 / 10;
		// float valid3 = distance23 / 10;


		mole.SetActive(false);

		if (distance12 > 7)
		{
			mole.SetActive(false);
		}
		else if (5 > distance12 && 5 > distance13 ) //&& 5 > distance23)
		{
			mole.SetActive(true);
		}

		Debug.Log("distance12: " + distance12);
		Debug.Log("distance13: " + distance13);
		// Debug.Log("distance23" + distance23);
	}

	private void SpawnAtom(GameObject imageTargetGameObject)
	{
		imageTargetGameObject.transform.position = new Vector3(imageTargetGameObject.transform.position.x, -4, imageTargetGameObject.transform.position.z);
		_atomManager.createAtom(hydrogenData.ToString(), imageTargetGameObject.transform.position);
	}

	public IEnumerable<WaitForSeconds> sleep()
	{
		yield return new WaitForSeconds(1);
	}
}