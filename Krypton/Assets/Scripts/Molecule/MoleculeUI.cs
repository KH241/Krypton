using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoleculeUI : MonoBehaviour
{
	public TMP_Text mainText;
	public TMP_Text subText;

	public GameObject idle;
	public GameObject view;
	public GameObject info;
	public TestManager tm;

	public GameObject TaskList;

	public Button spawnButton;

	// Start is called before the first frame update
    void Start()
	{
		TaskList.SetActive(TaskModeManger.Singleton.Active);
	}

    // Update is called once per frame
    void Update()
    {
	    if (tm.activeMolecule == null && view.activeSelf)
	    {
		    toggleView();
	    }
	    
	    if (tm.activeMolecule != null && idle.activeSelf)
	    {
		    toggleView();
	    }

	    spawnButton.interactable = tm.PossibleMolecules.Length > 0;
	    
    }

    /**
     * show information about the created molecule
     */
    public void toggleMoleculeInfo()
    {
	    info.SetActive(!info.activeSelf);
	    if (info.activeSelf)
	    {
		    mainText.text = tm.PossibleMolecules[0].Name;
		    subText.text = tm.PossibleMolecules[0].ToString();
		    TaskModeManger.Singleton.MoleculeViewed(tm.activeMolecule);
	    }
    }

    /**
     * destroy the molecule and return to Idle mode
     */
    public void destroyOnClick()
    {
	    tm.DestroyMolecule();
	    toggleView();
    }
    
    /**
     * create the molecule. This can only be clicked when it is possible to create a molecule
     */
    public void spawnMole()
    {
	    tm.SpawnMolecule(tm.PossibleMolecules[0]);
	    toggleView();
	    
    }

    private void toggleView()
    {
	    idle.SetActive(!idle.activeSelf);
	    view.SetActive(!view.activeSelf);
    }
    
}
