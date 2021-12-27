public class CreateAtomTask : Task
{
	public AtomSO Atom;
	
	public CreateAtomTask(AtomSO atom)
	{
		this.Done = false;
		this.Name = "Create " + atom.Name;
		this.Atom = atom;
	}
}

public class ViewAtomTask : Task
{
	public AtomSO Atom;
	
	public ViewAtomTask(AtomSO atom)
	{
		this.Done = false;
		this.Name = "Look at the Info sheet for " + atom.Name;
		this.Atom = atom;
	}
}

public class CreateMoleculeTask : Task
{
	public MoleculeSO Molecule;
	
	public CreateMoleculeTask(MoleculeSO molecule)
	{
		this.Done = false;
		this.Name = "Create " + molecule.Name;
		this.Molecule = molecule;
	}
}

public class ViewMoleculeTask : Task
{
	public MoleculeSO Molecule;
	
	public ViewMoleculeTask(MoleculeSO molecule)
	{ 
		this.Done = false;
		this.Name = "Look at the Info sheet for " + molecule.Name;
		this.Molecule = molecule;
	}
}

public class Task
{
	public bool Done;
	public string Name;
}