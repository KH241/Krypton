using System.Collections;
using System.Collections.Generic;
using TaskMode;
using UnityEngine;

namespace AtomCycle
{
	/**
	* Controls Game spawning of atoms 
	*/
	public class AtomSpawner : MonoBehaviour
	{

	    private GameObject atomModel;
	    private List<Atom> spawnedAtoms = new List<Atom>();
		public AtomUI UI;
		
		public GameObject atomPrefab;


		/**
		 * Create the atom based off info passed from Scriptable object AtomSO
		 */
	    public void createAtoms( GameObject parent)
	    {

	        AtomSO atom = parent.GetComponent<ImageTarget>().Atom;
	        //Vector3 atomCenter = parent.transform.position;
	        Vector3 atomCenter = parent.transform.GetChild(0).position;

	        int protonCount = atom.ID;
	        int neutronCount = atom.Neutrons;

	        int shellCount = atom.Shells.Count;
	        List<int> electronList = new List<int>();

	        int[] electronConfig = atom.Shells.ToArray();

	        //Instantiate the Atom 
	        atomModel = Instantiate(atomPrefab, atomCenter, new Quaternion());
	        atomModel.transform.parent = parent.transform.GetChild(0);
	        Atom myAtom = atomModel.GetComponent<Atom>();
	        if (myAtom != null)
	        {
	            //Pass Atom values to prefab
	            myAtom.atomScale = .5f;
	            myAtom.protonCount = protonCount;
	            myAtom.neutronCount = neutronCount;
	            myAtom.electronConfiguration = electronConfig;
	            myAtom.electronShellSpacing = .5f;
	            myAtom.elementName = atom.name;
	            //Create atom objects
	            myAtom.createAtom(atomCenter);
	            //Seperate layers of nucleus and atom to allow for player interaction
	            //MoveToLayer(myAtom.transform.GetChild(0), 8);
	        }

			Debug.Log(atom);
	        TaskModeManger.Singleton.AtomCreated(atom);
			
			UI.AtomTracked(atom);
		}

	    /**
		 * Delete all objects of type atom
		 */

	    public void DeleteAtom(GameObject parent)
	    {

	         Atom[] atoms =  FindObjectsOfType<Atom>();

	        foreach(Atom atom in atoms)
	        {
	            Destroy(atom.transform.gameObject);
	        }
			
			UI.NoAtomTracked();
		}
		
	    /**
		 * Spawn Atom with Delay to account for user movement
		 */
	    public IEnumerator TimeDelayedSpawn(GameObject parent)
	    {
	        
	        yield return new WaitForSeconds(1f);
	        createAtoms(parent);
	       // TaskModeManger.Singleton.AtomCreated(parent.GetComponent<ImageTarget>().Atom);
	       // TaskModeManger.Singleton.AtomViewed(parent.GetComponent<ImageTarget>().Atom);

	    }

		/**
	    * Spawn Function
	    */
	    public void spawn(GameObject parent)
	    {
	         this.StartCoroutine(TimeDelayedSpawn(parent));
	    }
	}
}





