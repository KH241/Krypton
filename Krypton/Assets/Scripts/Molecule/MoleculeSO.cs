using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoleculeSO", menuName = "ScriptableObjects/MoleculeSO", order = 1)]
public class MoleculeSO : ScriptableObject
{
	public string ID => CalcHash(); //Starts at 1 - its the atomic number
	public string Name; //Lets keep it english
	public List<AtomSO> Atoms;

	public string CalcHash() //TODO write a proper Hash (instead of a string containing IDs
	{
		string output = "";

		foreach (AtomSO atom in Atoms)
		{
			output += atom.ID;
		}

		return output;
	}
}