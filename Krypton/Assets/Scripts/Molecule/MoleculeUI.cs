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

	public Button spawnButton;

	// Start is called before the first frame update
    void Start()
    {
        
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

    public void toggleMoleculeInfo()
    {
	    info.SetActive(!info.activeSelf);
	    if (info.activeSelf)
	    {
		    //TaskModeManger.Singleton.MoleculeViewed(tm.activeMolecule);
	    }
    }

    public void destroyOnClick()
    {
	    tm.DestroyMolecule();
	    toggleView();
    }
    
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
