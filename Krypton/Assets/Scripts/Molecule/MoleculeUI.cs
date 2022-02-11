using TaskMode;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Molecule
{
	/**
	* Class Controls UI elements for Molecule Scene 
	*/
	public class MoleculeUI : MonoBehaviour
	{
		public TMP_Text mainText;
		public TMP_Text subText;

		public GameObject idle;
		public GameObject view;
		public GameObject info;
		public MoleculeManager manager;

		public GameObject TaskList;

		public Button spawnButton;

	    void Start() { TaskList.SetActive(TaskModeManger.Singleton.Active); }

	    void Update()
	    {
		    if (manager.activeMolecule == null && view.activeSelf) { toggleView(); }
		    
		    if (manager.activeMolecule != null && idle.activeSelf) { toggleView(); }

		    spawnButton.interactable = manager.PossibleMolecules.Length > 0;
		}
 
		/**
		 * Toggles visibility of the Info sheet for the Molecule
		 */
	    public void toggleMoleculeInfo()
	    {
		    info.SetActive(!info.activeSelf);
		    if (info.activeSelf)
		    {
			    mainText.text = manager.PossibleMolecules[0].Name;
			    subText.text = manager.PossibleMolecules[0].ToString();
			    TaskModeManger.Singleton.MoleculeViewed(manager.activeMolecule);
		    }
	    }

		/**
		 * Destroys the active Molecule + returns to idle mode
		 */
	    public void destroyOnClick()
	    {
		    manager.DestroyMolecule();
		    toggleView();
	    }
	    
		/**
		 * Spawns possible Molecule 
		 */
	    public void spawnMole()
	    {
		    manager.SpawnMolecule(manager.PossibleMolecules[0]);
		    toggleView();
		    
	    }

		/**
		 * Toggles the view between IDLE and VIEW 
		 */
	    private void toggleView()
	    {
		    idle.SetActive(!idle.activeSelf);
		    view.SetActive(!view.activeSelf);
	    }
	    
		/**
	     * Exits the MoleculeScene - called from the Menu-Button "Exit"
	     */
		public void OnExit()
		{
			SceneManager.LoadScene(SceneList.MainMenu);
		}
	}
}