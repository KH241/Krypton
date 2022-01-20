using System;

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
}