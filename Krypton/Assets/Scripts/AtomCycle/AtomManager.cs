using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomManager : MonoBehaviour
{

    //Atom Prefab needed to spawn Gameobjects
    public GameObject atomPrefab;
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


    // Start is called before the first frame update
    void Start()
    {

        Physics.IgnoreLayerCollision(0, 8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void spawnAtoms()
    {
        createAtom(oxygenParam, new Vector3(0f,1f,0f));
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
            atomPrefab = Instantiate(atomPrefab, atomCenter, new Quaternion());
            
            Atom myAtom = atomPrefab.GetComponent<Atom>();

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
