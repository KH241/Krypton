using UnityEngine;

/**
 * Looks at Camera
 */
public class LookAtCamera : MonoBehaviour
{
	private Transform target;
    
	void Start()
	{
		target = FindObjectOfType<Camera>().transform;
	}
	
	void Update()
	{
		// Rotate the camera every frame so it keeps looking at the target
		transform.LookAt(target);

		// Same as above, but setting the worldUp parameter to Vector3.left in this example turns the camera on its side
		transform.LookAt(target, Vector3.left);
	}
}
