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

    public void spawnOxygenAtoms()
    { 
        Debug.Log("Found the oxygen imageTarget");
        OxygenModel = createAtoms(Oxygen, oxygenTarget);

        atomCanvas.SetActive(true);
        string number = Oxygen.ID.ToString();
        string weight = Oxygen.Weight.ToString();
        string neutron = Oxygen.Neutrons.ToString();
        string shells = Oxygen.Shells.Count.ToString();
        string valenceElectrons = Oxygen.Shells[Oxygen.Shells.Count - 1].ToString();

        atomNumber.SetText(number);
        atomSymbol.SetText(Oxygen.Symbol);
        atomName.SetText(Oxygen.Name);
        atomWieght.SetText(weight);
        atomProton.SetText(number);
        atomNeutron.SetText(neutron);
        atomShells.SetText(shells);
        atomElectron.SetText(valenceElectrons);

        atomColor.color = Oxygen.atomColor;
        

}

    public void spawnCarbonAtoms()
    {
        CarbonModel = createAtoms(Carbon, carbonTarget);
        Debug.Log("Found the carbon imageTarget");

        atomCanvas.SetActive(true);
        string number = Carbon.ID.ToString();
        string weight = Carbon.Weight.ToString();
        string neutron = Carbon.Neutrons.ToString();
        string shells = Carbon.Shells.Count.ToString();
        string valenceElectrons = Carbon.Shells[Carbon.Shells.Count - 1].ToString();

        atomNumber.SetText(number);
        atomSymbol.SetText(Carbon.Symbol);
        atomName.SetText(Carbon.Name);
        atomWieght.SetText(weight);
        atomProton.SetText(number);
        atomNeutron.SetText(neutron);
        atomShells.SetText(shells);
        atomElectron.SetText(valenceElectrons);

        atomColor.color = Carbon.atomColor;
    }

    public void spawnHydrogenAtoms()
    {
        Debug.Log("Found the hydrogen imageTarget");
        HydrogenModel = createAtoms(Hydrogen, hydrogenTarget);

        atomCanvas.SetActive(true);
        string number = Hydrogen.ID.ToString();
        string weight = Hydrogen.Weight.ToString();
        string neutron = Hydrogen.Neutrons.ToString();
        string shells = Hydrogen.Shells.Count.ToString();
        string valenceElectrons = Hydrogen.Shells[Hydrogen.Shells.Count - 1].ToString();

        atomNumber.SetText(number);
        atomSymbol.SetText(Hydrogen.Symbol);
        atomName.SetText(Hydrogen.Name);
        atomWieght.SetText(weight);
        atomProton.SetText(number);
        atomNeutron.SetText(neutron);
        atomShells.SetText(shells);
        atomElectron.SetText(valenceElectrons);

        atomColor.color = Hydrogen.atomColor;

    }

    public void spawnSodiumAtom()
    {
        
        //createAtom(sodiumParam, new Vector3(0f, 1f, 0f));
    }

    public void spawnChlorineAtom()
    {
        
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

    public GameObject createAtoms(AtomSO atom, GameObject parent)
    {
        
        atomCenter = parent.transform.position;

        protonCount = atom.ID;
        neutronCount = atom.Neutrons;

        
        int shellCount = atom.Shells.Count;
        List<int> electronList = new List<int>();
            
        int[] electronConfig = atom.Shells.ToArray();

        //Instantiate the Atom 
        GameObject atomModel = Instantiate(atomPrefab, atomCenter, new Quaternion());
        atomModel.transform.parent = parent.transform;

        atomModel.name = assigneName(atom.ID, parent.name);
        
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

        return atomModel;
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

}
