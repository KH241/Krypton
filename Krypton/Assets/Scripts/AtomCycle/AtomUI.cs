using TaskMode;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AtomCycle
{
	public class AtomUI : MonoBehaviour
	{
		private AtomSO trackedAtom;
		public GameObject TaskList;
		public GameObject AtomInfo;
		public Button AtomInfoToggle;
		
		public TMP_Text atomNumber;
		public TMP_Text atomSymbol;
		public TMP_Text atomName;
		public TMP_Text atomWeight;
		public TMP_Text atomProton;
		public TMP_Text atomNeutron;
		public TMP_Text atomElectron;
		public TMP_Text atomShells;
		public Image atomColor;

		// Start is called before the first frame update
		void Start()
		{
			TaskList.SetActive(TaskModeManger.Singleton.Active);
			NoAtomTracked();
		}

		public void ToggleInfo()
		{
			AtomInfo.SetActive(!AtomInfo.activeSelf);
			if (AtomInfo.activeSelf)
			{
				TaskModeManger.Singleton.AtomViewed(trackedAtom);
			}
		}

		public void AtomTracked(AtomSO atom)
		{
			trackedAtom = atom;
			AtomInfoToggle.interactable = true;
			
			AtomInfo.SetActive(false);
			
			string number = atom.ID.ToString();
			string weight = atom.Weight.ToString();
			string neutron = atom.Neutrons.ToString();
			string shells = atom.Shells.Count.ToString();
			string valenceElectrons = atom.Shells[atom.Shells.Count - 1].ToString();

			atomNumber.SetText(number);
			atomSymbol.SetText(atom.Symbol);
			atomName.SetText(atom.Name);
			atomWeight.SetText(weight);
			atomProton.SetText(number);
			atomNeutron.SetText(neutron);
			atomShells.SetText(shells);
			atomElectron.SetText(valenceElectrons);

			atomColor.color = atom.atomColor;
		}

		public void NoAtomTracked()
		{
			trackedAtom = null;
			AtomInfoToggle.interactable = false;
			AtomInfo.SetActive(false);
		}
		
		
		public void OnMainMenu()
		{
			SceneManager.LoadScene(SceneList.MainMenu);
		}
	}
	
}