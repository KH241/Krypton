using System;

/* Task apparently needs to be one class instead of inherited,
 because unity's JsonUtility cant deal with inheritance (and its too late to change the library for json parsing)
[Serializable]
public class CreateAtomTask : Task
{
	public AtomSO Atom;

	public CreateAtomTask(AtomSO atom)
	{
		Done = false;
        Name = "Create " + atom.Name;
        Atom = atom;
	}
}

[Serializable]
public class ViewAtomTask : Task
{
	public AtomSO Atom;
	
	public ViewAtomTask(AtomSO atom) 
	{ 
		Done = false;
        Name = "Look at the Info sheet for " + atom.Name;
        Atom = atom; 
	}
}

[Serializable]
public class CreateMoleculeTask : Task
{
	public MoleculeSO Molecule;

	public CreateMoleculeTask(MoleculeSO molecule)
	{
		Done = false;
        Name = "Create " + molecule.Name;
        Molecule = molecule;
	}
}

[Serializable]
public class ViewMoleculeTask : Task
{
	public MoleculeSO Molecule;

	public ViewMoleculeTask(MoleculeSO molecule)
	{
		Done = false;
        Name = "Look at the Info sheet for " + molecule.Name;
        Molecule = molecule;
	}
}


[Serializable]
public class Task
{
	public bool Done;
	public string Name;
}*/

[Serializable]
public class Task
{
	//This can be done very differently
	//TODO do it differently
	public TaskType Type;
	public MoleculeSO Molecule;
	public AtomSO Atom;
	
	public bool Done;
	public string Name;

	public Task(TaskType type, AtomSO atom)
	{
		Type = type;
		Atom = atom;
		Done = false;
		switch (Type)
		{
			case TaskType.CreateAtom:
				Name = "Create " + atom.Name;
				break;
			case TaskType.ViewAtom:
				Name = "Look at the Info sheet for " + atom.Name;
				break;
			default:
				throw new DataMisalignedException("Please select a valid atom task type (you put in a AtomSO)");
		}
	}
	
	public Task(TaskType type, MoleculeSO molecule)
	{
		Type = type;
		Molecule = molecule;
		Done = false;
		switch (Type)
		{
			case TaskType.CreateMolecule:
				Name = "Create " + molecule.Name;
				break;
			case TaskType.ViewMolecule:
				Name = "Look at the Info sheet for " + molecule.Name;
				break;
			default:
				throw new DataMisalignedException("Please select a valid molecule task type (you put in a MoleculeSO)");
		}
	}
}

public enum TaskType
{
	CreateAtom,
	ViewAtom,
	CreateMolecule,
	ViewMolecule
}