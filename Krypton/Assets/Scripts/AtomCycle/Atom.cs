using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Atom : MonoBehaviour
{
    //Mats for shells and electrons
    public Material AtomShellMaterial;
    public Material electronMaterial;

    private Vector3 center;

    

    //Size of atom in scene
    public float atomScale = 1f;
    
    //Element Info
    public string elementName;
    public string elementSymbol;
    public int atomicNumber;
    public float atomicWeight;
    

    //Protons
    
    public int protonCount;
    public Color32 protonColor = new Color32(0, 55, 255, 255);

    //Neutrons
    public int neutronCount;
    public Color32 neutronColor = new Color32(255, 28, 28, 255);

    //Electrons
    public int[] electronConfiguration;
    public float electronShellSpacing = .5f;
    public bool spinElectrons = true;
    
    //Physics enabled on Nucleus
    public bool freezeNucleus = false;
    Transform nucleus;

    public void Start()
    {
        
    }

    public void createAtom(Vector3 center)
    {
        this.center = center;
        //Assign Nucleus GameObject
        nucleus = transform.GetChild(0);
        
        //proton and neutrons
        int pc = protonCount;
        int nc = neutronCount;

        generateProton(transform.position);
        pc -= 1;

        //Generate spheres for protons and neutrons in batches
        for (int i = 1; i <= nc + pc; i++)
        {
            int nucleonsToCreate = 10 * i;
            Debug.Log("nucleonsToCreate" + nucleonsToCreate);

            if (nc + pc >= nucleonsToCreate)
            {
                string nucleonsCreated = createNucleons(nucleonsToCreate, atomScale, pc, nc);                
                pc -= int.Parse(nucleonsCreated.Split(',')[0]);
                nc -= int.Parse(nucleonsCreated.Split(',')[1]);
            }
            else
            {
                string nucleonsCreated = createNucleons(pc + nc, atomScale, pc, nc);               
                pc -= int.Parse(nucleonsCreated.Split(',')[0]);
                nc -= int.Parse(nucleonsCreated.Split(',')[1]);
            }
        }

        //Electrons
        GameObject[] shellsGO = generateElectronShells();
        float delta = atomScale / 4;
        float radius = atomScale + delta;
        
        //Shell Loop 
        for (int loop = 0; loop < electronConfiguration.Length; loop++)
        {
            int numberOfElectrons = electronConfiguration[loop];

            for (int electronNumber = 0; electronNumber < numberOfElectrons; electronNumber++)
            {                
                float i = (float)(electronNumber) / numberOfElectrons;
                float angle = i * Mathf.PI * 2;
                float x = transform.position.x + (Mathf.Sin(angle) * ( (loop + 1)));
                float z = transform.position.z + (Mathf.Cos(angle) * ( (loop + 1)));

                Vector3 pos = new Vector3(x , transform.position.y, z);

                generateElectron(pos,  radius, loop + 1, shellsGO[loop]);
                
            }
            delta = delta + electronShellSpacing;
            radius = radius + electronShellSpacing;
        }

        //Deactivate rigidbody and colliders 
        StartCoroutine(freezePos());
    }


    public IEnumerator freezePos()
    {
        yield return new WaitForSeconds(.25f);

        freezeNucleus = true;
        //yield return new WaitForSeconds(.25f);

        
    }

    //Create Nucleon ojects aka protons and neutrons
    string createNucleons(int nPoints, float scale, int protonsToBeCreated, int neutronsToBeCreated)
    {
        int npc = 0;
        int nnc = 0;

        float fPoints = (float)nPoints;

        Vector3[] points = new Vector3[nPoints];

        float inc = 4;

        //offset between inital instantation of Nucleods
        float off = 2 / fPoints;

        //create new spawn points for nucleids
        for (int k = 0; k < nPoints; k++)
        {
            float y = k * off - 1 + (off / 2);
            float r = Mathf.Sqrt(1 - y * y);
            float phi = k * inc;

            points[k] = new Vector3(Mathf.Cos(phi) * r, y, Mathf.Sin(phi) * r);
        }

        //loop through spawn points and generate nuclo
        foreach (Vector3 point in points)
        {
            //Randomly generate proton or neutron until one has been fulfilled
            if (protonsToBeCreated > 0 && neutronsToBeCreated > 0)
            {
                int norp = Random.Range(1, 10);
                if (norp > 5 && protonsToBeCreated > 0)
                {
                    GameObject proton = generateProton(transform.position);
                    proton.transform.position += point * scale;
                    npc += 1;
                    protonsToBeCreated -= 1;
                }
                else if (norp < 6 && neutronsToBeCreated > 0)
                {
                    GameObject neutron = generateNeutron(transform.position);
                    neutron.transform.position += point * scale;
                    nnc += 1;
                    neutronsToBeCreated -= 1;
                }
            }

            //if neutrons fulfilled create remaining protons
            else if (protonsToBeCreated > 0 && neutronsToBeCreated == 0)
            {
                GameObject proton = generateProton(transform.position);
                proton.transform.position += point * scale;
                npc += 1;
                protonsToBeCreated -= 1;
            }

            //if protons fulfilled create remaining neutrons
            else if (protonsToBeCreated == 0 && neutronsToBeCreated > 0)
            {
                GameObject neutron = generateNeutron(transform.position);
                neutron.transform.position += point * scale;
                nnc += 1;
                neutronsToBeCreated -= 1;
            }
        }

        return npc + "," + nnc;
    }

    bool prevSpinElectrons = true;

    public void Update()
    {
        if (spinElectrons)
        {
            foreach (GameObject e in FindObjectsOfType(typeof(GameObject)))
            {
                if (e.name == "Electron")
                {
                    e.GetComponent<Electron>().enabled = true;
                }
            }
        }

        else if (!spinElectrons)
        {
            foreach (GameObject e in FindObjectsOfType(typeof(GameObject)))
            {
                if (e.name == "Electron")
                {
                    e.GetComponent<Electron>().enabled = false;                   
                }
            }
        }
    }

    //Draw Electron Shells
    public void drawCircle(float radius, LineRenderer lr)
    {
        lr.positionCount = 360;
        lr.useWorldSpace = false;
        lr.startWidth = lr.endWidth = atomScale / 16f;
        lr.materials[0] = new Material(Shader.Find("Diffuse"));
        lr.materials[0].SetColor("_Color", new Color32(128, 223, 255, 0));

        float x, y = 0, z, angle = 0;

        for (int i = 0; i < 360; i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lr.SetPosition(i, new Vector3(x, y, z));

            angle += 1;
        }
    }
    
    //Proton gameObject generator
    public GameObject generateProton(Vector3 pos)
    {
        GameObject proton = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        proton.AddComponent<Nucleon>().center = this.center;
        proton.name = "Proton";
        proton.transform.position = pos;
        proton.AddComponent<Rigidbody>().useGravity = false;
        proton.GetComponent<Rigidbody>().mass = 1f;
        proton.GetComponent<Renderer>().material.SetColor("_Color", new Color32(229, 79, 10, 255));
       
        //proton.GetComponent<Nucleon>().center = this.center;

        proton.transform.SetParent(nucleus);
        proton.transform.localScale = new Vector3(atomScale, atomScale, atomScale);

        return proton;
    }


    //Neutron GameObject Generator
    public GameObject generateNeutron(Vector3 pos)
    {
        
        GameObject neutron = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        neutron.AddComponent<Nucleon>().center = this.center;
        neutron.name = "Neutron";
        neutron.transform.position = pos;
        neutron.AddComponent<Rigidbody>().useGravity = false;
        neutron.GetComponent<Rigidbody>().mass = 1f;

        neutron.GetComponent<Renderer>().material.SetColor("_Color", new Color32(51, 46, 46, 255));
        

        neutron.transform.SetParent(nucleus);
        neutron.transform.localScale = new Vector3(atomScale, atomScale, atomScale);


        return neutron;
    }

    //Electorn Shell Generator
    public GameObject[] generateElectronShells()
    {
        List<GameObject> shells = new List<GameObject>();
        
        //Set Distance for initial shell
        float delta = atomScale + atomScale*.25f;
       
        //for each shell in input params draw a circle with offset  inbetween of delta 
        for (int loop = 0; loop < electronConfiguration.Length; loop++)
        {
            //Create shell gameObject and set as child of atom
            GameObject shell = new GameObject();
            shell.name = "Shell " + (loop + 1);
            shell.transform.parent = transform;
            shell.transform.localPosition = nucleus.localPosition;
            LineRenderer lr = shell.AddComponent<LineRenderer>();
            shell.GetComponent<Renderer>().material = AtomShellMaterial;

            drawCircle((delta), lr);
            //Increase shell spacing 
            delta = delta + electronShellSpacing;

            shells.Add(shell);
        }

        return shells.ToArray();
    }

    //Electron Gameobject Generator
    public void generateElectron(Vector3 pos, float r, int shell, GameObject shellGO)
    {
        GameObject electron = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        electron.GetComponent<Renderer>().material = electronMaterial;
        electron.name = "Electron";
        electron.transform.localScale = new Vector3(atomScale / 4, atomScale / 4, atomScale / 4);
        electron.transform.position = pos;
        electron.transform.parent = shellGO.transform;
        Destroy(electron.GetComponent<SphereCollider>());
        electron.GetComponent<Renderer>().material = electronMaterial;
        Electron e = electron.AddComponent<Electron>();
        e.radius = r;
        e.centre = transform;
        e.rotationSpeed = 40 + (10 * shell);

    }


}
