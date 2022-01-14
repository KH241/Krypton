using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AtomSO", menuName = "ScriptableObjects/AtomSO", order = 1)]
public class AtomSO : ScriptableObject
{
	public int ID; //Starts at 1 - its the atomic number
	public string Name; //Lets keep it english
	public string Symbol;
	public float Weight;
	public int Neutrons;
	public List<int> Shells;
	public Color32 atomColor;

	public string ToString()
	{
		string output = "";

		output += Name;
		output += "_" + Name; //TODO remove German Name
		output += "_" + Symbol;
		output += "_" + Weight;
		output += "_" + ID;
		output += "_" + Neutrons;

		foreach (int shell in Shells)
		{
			output += "_" + shell;
		}
		
		return output;
	}
}
