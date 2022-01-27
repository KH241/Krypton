using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AtomSO", menuName = "ScriptableObjects/AtomSO", order = 1)]
public class AtomSO : ScriptableObject
{
	public int ID; //Starts at 1 - its the atomic number
	public string Name;
	public string Symbol;
	public float Weight;
	public int Neutrons;
	public List<int> Shells;
	public Color32 atomColor;
}
