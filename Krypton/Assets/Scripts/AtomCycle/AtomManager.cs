using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AtomManager : MonoBehaviour
{

    //Atom Prefab needed to spawn Gameobjects
    public GameObject atomPrefab;
   

    private GameObject CarbonModel;
    private GameObject HydrogenModel;
    private GameObject OxygenModel;


    public GameObject oxygenTarget;
    public GameObject carbonTarget;
    public GameObject hydrogenTarget;

    //Center of Atom, 0,0,0 of atom
    private Vector3 atomCenter;

    //Atom Info
    private string elementName;
    private string elementNameDe;
    private string elementSymbol;
    private int protonCount;
    private int neutronCount;
    private string elementWeight;

    //Test Spawn Params
	public AtomSO oxygenData;
	public AtomSO hydrogenData;
    public AtomSO carbonData;
    private string carbonParam = "Carbon_Kohlenstoff_C_12.010_6_6_2_4";
    private string sodiumParam = "Sodium_Natrium_Na_22.989_11_12_2_8_1";
    private string ChlorineParam = "Chlorine_Chlor_Cl_35.453_17_18_2_8_7";

    public AtomSO Oxygen;
    public AtomSO Hydrogen;
    public AtomSO Carbon;

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

    // Start is called before the first frame update
    void Start()
    {

        Physics.IgnoreLayerCollision(0, 8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	/**
     * Opens the Molecule-View - called from the MainMenu-Button "Molecule"
     */
	public void OnMainMenu()
	{
		SceneManager.LoadScene(SceneList.MainMenu);
	}

    



    

  

    public void destroyHydrogen()
    {
        Debug.Log("Destroying Hydrogen");
        if (HydrogenModel != null)
        Destroy(HydrogenModel);

    }

    public void destroyCarbon()
    {
        Debug.Log("Destroying Carbon");
        if (CarbonModel != null)
            Destroy(CarbonModel);
        atomCanvas.SetActive(false);
    }

    public void destroyOxygen()
    {
        Debug.Log("Destroying Oxygen");
        if (OxygenModel != null)
            Destroy(OxygenModel);
        atomCanvas.SetActive(false);

    }

    public void createAtoms(GameObject parent)
    {
        
        AtomSO atom = parent.GetComponent<ImageTarget>().Atom;

        atomCenter = parent.transform.position;

        protonCount = atom.ID;
        neutronCount = atom.Neutrons;

        
        int shellCount = atom.Shells.Count;
        List<int> electronList = new List<int>();
            
        int[] electronConfig = atom.Shells.ToArray();

        //Instantiate the Atom 
        GameObject atomModel = Instantiate(atomPrefab, atomCenter, new Quaternion());
        atomModel.transform.parent = parent.transform;

        // atomModel.name = assigneName(atom.ID, parent.name);
        
            Atom myAtom = atomModel.GetComponent<Atom>();

            if (myAtom != null)
            {
                //Pass Atom values to prefab
                myAtom.atomScale = .5f;
                myAtom.protonCount = protonCount;
                myAtom.neutronCount = neutronCount;
                myAtom.electronConfiguration = electronConfig;

                myAtom.electronShellSpacing = .5f;

                myAtom.elementName = elementName;

                //Create atom objects
                myAtom.createAtom(atomCenter);

                //Seperate layers of nucleus and atom to allow for player interaction
                MoveToLayer(myAtom.transform.GetChild(0), 8);
            }

       // return atomModel;
    }
    
    
    /**
     * Returns the name of the Atom SO
     */
    private string assigneName(int atomicNumber, string parentName)
    {
        switch (atomicNumber)
        {
            case 1:
                return giveNameToHydrogen(parentName);
            case 8:
                return giveNameToOxygen(parentName);
            case 6:
                return giveNameToCarbon(parentName);
            default:
                return "ERROR";
        }
    }

    private string giveNameToHydrogen(string parentName)
    {
        String number = parentName.Substring(19);
        return "hydrogenAtom" + number;
    }
    
    private string giveNameToCarbon(string parentName)
    {
        String number = parentName.Substring(17);
        return "carbonAtom" + number;
    }
    
    private string giveNameToOxygen(string parentName)
    {
        String number = parentName.Substring(17);
        return "oxygenAtom" + number;
    }

    void MoveToLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            MoveToLayer(child, layer);
    }

    public void destroyAtom(GameObject target)
    {
        foreach (Transform child in target.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
