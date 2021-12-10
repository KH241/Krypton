using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeCreator : MonoBehaviour
{
    public GameObject atom1;
    public GameObject atom2;
    public GameObject atom3;

    public GameObject mole;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance12 = Vector3.Distance(atom1.transform.position, atom2.transform.position);
        float distance13 = Vector3.Distance(atom1.transform.position, atom3.transform.position);
        float distance23 = Vector3.Distance(atom2.transform.position, atom3.transform.position);
        float valid1 = distance12 / 10;
        float valid2 = distance13 / 10;
        float valid3 = distance23 / 10;
        
        
        mole.SetActive(false);
        
        if (distance12 > 1) {
			
            mole.SetActive(false);
        }
        else if (0.2 > distance12 && 0.2 > distance13 && 0.2 > distance23)
        {
            mole.SetActive(true);
        }
     
        Debug.Log (distance12);
        Debug.Log (distance13);
        Debug.Log (distance23);
    }
}
