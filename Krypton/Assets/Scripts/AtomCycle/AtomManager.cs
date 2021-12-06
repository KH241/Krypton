using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomManager : MonoBehaviour
{

    //Atom Prefab needed to spawn Gameobjects
    public GameObject atomPrefab;
    public GameObject atomModel;
    
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
    private string oxygenParam = "Oxygen_Sauerstoff_0_15.999_8_9_2_6";
    private string hydrogenParam = "Hydrogen_Wasserstoff_H_1.007_1_0_1";
    private string carbonParam = "Carbon_Kohlenstoff_C_12.010_6_6_2_4";
    private string sodiumParam = "Sodium_Natrium_Na_22.989_11_12_2_8_1";
    private string ChlorineParam = "Chlorine_Chlor_Cl_35.453_17_18_2_8_7";


    // Start is called before the first frame update
    void Start()
    {

        Physics.IgnoreLayerCollision(0, 8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void spawnOxygenAtoms()
    {
        if(atomModel != null)
        {
            Destroy(atomModel);
        }
        createAtom(oxygenParam, new Vector3(0f,1f,0f));
    }

    public void spawnCarbonAtoms()
    {
        if (atomModel != null)
        {
            Destroy(atomModel);
        }
        createAtom(carbonParam, new Vector3(0f, 1f, 0f));
    }

    public void spawnHydrogenAtoms()
    {
        if (atomModel != null)
        {
            Destroy(atomModel);
        }
        createAtom(hydrogenParam, new Vector3(0f, 1f, 0f));
    }

    public void spawnSodiumAtom()
    {
        if (atomModel != null)
        {
            Destroy(atomModel);
        }
        createAtom(sodiumParam, new Vector3(0f, 1f, 0f));
    }

    public void spawnChlorineAtom()
    {
        if (atomModel != null)
        {
            Destroy(atomModel);
        }
        createAtom(ChlorineParam, new Vector3(0f, 1f, 0f));
    }




    public void createAtom(string atomParam, Vector3 spawn)
    {
        //Process Spawm params
        string[] atomInfo =atomParam.Split('_');
        atomCenter = spawn;

        
        if (atomInfo.Length >= 6)
        {
            elementName = atomInfo[0];
            elementNameDe = atomInfo[1];
            elementSymbol = atomInfo[2];
            elementWeight = atomInfo[3];

            protonCount = int.Parse(atomInfo[4]);
            neutronCount = int.Parse(atomInfo[5]);

            //Iterate through remaining atomInfo and extract Electron Count for each shell. 
            int shellCount = 0;
            List<int> electronList = new List<int>();
            for (int i = 6; i < atomInfo.Length; i++)
            {
                shellCount++;
                electronList.Add(int.Parse(atomInfo[i]));

            }
            int[] electronConfig = electronList.ToArray();

            //Instantiate the Atom 
            atomModel = Instantiate(atomPrefab, atomCenter, new Quaternion());
            
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
                myAtom.createAtom();

                //Seperate layers of nucleus and atom to allow for player interaction
                MoveToLayer(myAtom.transform.GetChild(0), 8);
            }       

        }
    }
    void MoveToLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            MoveToLayer(child, layer);
    }

}
