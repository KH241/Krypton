using UnityEngine;

public static class SOList
{
	public static AtomSO[] AtomList => atoms;
	private static AtomSO[] atoms;
	
	public static MoleculeSO[] MoleculeList => molecules;
	private static MoleculeSO[] molecules;

	static SOList()
	{
		loadAtoms();
		laodMolecules();
	}

	private static void loadAtoms() { atoms = Resources.LoadAll<AtomSO>("Atoms/"); }
	private static void laodMolecules() { molecules = Resources.LoadAll<MoleculeSO>("Molecules/"); }
}