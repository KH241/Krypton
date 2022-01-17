using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateTaskUI : MonoBehaviour
{
	public TMP_Text TaskName;
	public TMP_Text TaskList;
	public GameObject TaskButtonGrid;

	public void AddCreateAtom(AtomSO atom)
	{
		Debug.Log($"Create {atom.name}");
	}

	public void AddViewAtom(AtomSO atom)
	{
		Debug.Log($"View {atom.name}");
	}

	public void AddCreateMolecule(MoleculeSO molecule)
	{
		Debug.Log($"Create {molecule.name}");
	}

	public void AddViewMolecule(MoleculeSO molecule)
	{
		Debug.Log($"View {molecule.name}");
	}
}
