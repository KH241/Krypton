using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nucleon : MonoBehaviour
{
    public GameObject center;
    private bool stop = false;
    private Transform spawn;
    
    void Start()
    {
        //Find center position, GameObject AtomSpawn must be present in scene before instantiation
        center = GameObject.Find("AtomSpawn");
        Debug.Log("Vector3: " + center);
    }

    void Update()
    {
        if (center != null)
        {
            if (gameObject.GetComponent<Rigidbody>() != null)
            {                            
                //Pull GameObject to the center
               gameObject.GetComponent<Rigidbody>().AddForce((center.transform.position - transform.position).normalized * 1000 * Time.smoothDeltaTime);
            }
        }

        //If atom parent stops movement then freeze myself
        if (transform.parent.parent.gameObject.GetComponent<Atom>().freezeNucleus)
        {
            if (!stop)
            {
                Freeze();
                stop = true;
            }
        }
        
    }

    //Turn of all gameobject movement and colliders
    public void Freeze()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject.GetComponent<Rigidbody>());
        Destroy(gameObject.GetComponent<SphereCollider>());
    }

}
