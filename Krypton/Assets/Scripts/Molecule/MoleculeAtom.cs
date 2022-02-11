using UnityEngine;

namespace Molecule
{
	/**
	 * Atom Controller class for creating atoms in the molecule scene
	 */
	public class MoleculeAtom : MonoBehaviour
	{
		public AtomSO Atom;
		public MeshRenderer mesh;
		private bool used = false;

		public bool Used
		{
			get { return used; }
			set
			{
				used = value; 
				OnVisible();
			}
		}
		

		/**
		 * Toggles MeshRenderer
		 */
		private void OnVisible()
		{
			mesh.enabled = !Used;
		}

		/**
		 * Spawns Atom on Imagetarget with color depending on the ID
		 */
		public void Spawn(AtomSO atom)
		{
			Atom = atom;

			switch (atom.ID)
			{
				case 1:
					mesh.material.color = Color.white;
					break;
				case 6:
            		mesh.material.color = Color.black;
            		break;
				case 8:
					mesh.material.color = Color.red;
					break;
				default:
					mesh.material.color = Color.cyan;
					break;
			}
		}
	}
}