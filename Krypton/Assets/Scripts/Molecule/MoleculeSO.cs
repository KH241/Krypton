using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoleculeSO", menuName = "ScriptableObjects/MoleculeSO", order = 1)]
public class MoleculeSO : ScriptableObject
{
	public int ID; // => CalcHash(); 
	public string Name;
	public string Formula;

	public List<AtomSO> Atoms;

	private string CalcHash() //TODO write a proper Hash (instead of a string containing IDs
	{
		string output = "";

		foreach (AtomSO atom in Atoms)
		{
			output += atom.ID;
		}

		return output;
	}

	public override string ToString()
	{
		string atoms = Environment.NewLine + "This molecule consists of:" + Environment.NewLine + Environment.NewLine;
		
		foreach (AtomSO atom in Atoms)
		{
			atoms = atoms + atom.Name + Environment.NewLine;
		}
		return atoms;
	}
}