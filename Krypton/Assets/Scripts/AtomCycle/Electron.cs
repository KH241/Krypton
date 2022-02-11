using UnityEngine;

namespace AtomCycle
{
	/*
	* Elecron Class Component 
	*/
	public class Electron : MonoBehaviour
	{

		public Transform centre;
	    public Vector3 axis = Vector3.up;
	    public float radius = 2.0f;
	    public float radiusSpeed = 0.5f;
	    public float rotationSpeed = 40.0f;
	    

		/*
		 * set Initial Spawn Position  
		 */
	    public void Start()
	    {
	        //set initial spawn position 
	        transform.position = (transform.position - centre.position).normalized * radius + centre.position;
	        
	    }

		/*
		 * rotate around center position  
		 */
	    public void Update()
	    {
			transform.RotateAround(centre.position, transform.up, rotationSpeed * Time.deltaTime);
		}
	}
}