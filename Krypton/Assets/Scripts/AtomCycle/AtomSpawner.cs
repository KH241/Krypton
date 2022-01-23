using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AtomSpawner : MonoBehaviour
{

    private GameObject atomModel;
    private List<Atom> spawnedAtoms = new List<Atom>();
    public GameObject atomCanvas;
    public TextMeshProUGUI atomNumber;
    public TextMeshProUGUI atomSymbol;
    public TextMeshProUGUI atomName;
    public TextMeshProUGUI atomWieght;
    public TextMeshProUGUI atomProton;
    public TextMeshProUGUI atomNeutron;
    public TextMeshProUGUI atomElectron;
    public TextMeshProUGUI atomShells;
    public Image atomColor;


    public GameObject atomPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /**
	 * Create the atom based off info passed from Scriptable object AtomSO
	 * 
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

        TaskModeManger.Singleton.AtomCreated(atom);
        // updateAtomInfo(atom);



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
            
            if(atomCanvas != null)
            {
            atomCanvas.SetActive(false);
            }
    }


    /**
	 * Spawn Atom with Delay to account for user movement
	 */
    public IEnumerator TimeDelayedSpawn(GameObject parent)
    {
        
        yield return new WaitForSeconds(1f);
        createAtoms(parent);
        updateAtomInfo(parent.GetComponent<ImageTarget>().Atom);
       // TaskModeManger.Singleton.AtomCreated(parent.GetComponent<ImageTarget>().Atom);
       // TaskModeManger.Singleton.AtomViewed(parent.GetComponent<ImageTarget>().Atom);

    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(SceneList.MainMenu);
    }


    /**
    * Spawn Function
    */
    public void spawn(GameObject parent)
    {
         this.StartCoroutine(TimeDelayedSpawn(parent));
    }


 /**
 * Activate and Update the info for atom ui canvas. 
 */
    public void updateAtomInfo(AtomSO atom)
    {
        if (atomCanvas != null)
        {
            Debug.Log("Activating canvas");
            atomCanvas.SetActive(true);
            string number = atom.ID.ToString();
            string weight = atom.Weight.ToString();
            string neutron = atom.Neutrons.ToString();
            string shells = atom.Shells.Count.ToString();
            string valenceElectrons = atom.Shells[atom.Shells.Count - 1].ToString();

            atomNumber.SetText(number);
            atomSymbol.SetText(atom.Symbol);
            atomName.SetText(atom.Name);
            atomWieght.SetText(weight);
            atomProton.SetText(number);
            atomNeutron.SetText(neutron);
            atomShells.SetText(shells);
            atomElectron.SetText(valenceElectrons);

            atomColor.color = atom.atomColor;

        }
        TaskModeManger.Singleton.AtomViewed(atom);

    }
}





