using UnityEngine;

namespace DefaultNamespace
{
	public class TestMolecule : MonoBehaviour
	{
		public MoleculeSO MoleculeData;
		public GameObject H2OGameObject;
		public GameObject CH4GameObject;
		public GameObject CO2GameObject;
		public GameObject C2H2GameObject;
		public GameObject NH3GameObject;
		
		public void Spawn(MoleculeSO molecule)
		{
			switch (molecule.ID)
			{
				case 0:
					Instantiate(H2OGameObject, transform);
					break;
				case 1:
					Instantiate(CH4GameObject, transform);
					break;
				case 2:
					Instantiate(CO2GameObject, transform);
					break;
				case 3:
					Instantiate(C2H2GameObject, transform);
					break;
				case 4:
					Instantiate(NH3GameObject, transform);
					break;
				default:
					Debug.Log("default reached");
					break;
			}
		}
	}
	
}