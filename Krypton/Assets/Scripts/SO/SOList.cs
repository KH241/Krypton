using UnityEngine;

/**
 * Contains Lists of all existing Atom/Molecule Scriptable objects
 */
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

	/**
	 * Loads all available AtomSO's from Resources
	 */
	private static void loadAtoms() { atoms = Resources.LoadAll<AtomSO>("Atoms/"); }
	/**
	 * Loads all available MoleculeSO's from Resources
	 */
	private static void laodMolecules() { molecules = Resources.LoadAll<MoleculeSO>("Molecules/"); }
}