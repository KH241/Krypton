using TMPro;
using UnityEngine;

public class TaskHelpUI : MonoBehaviour
{
	public TMP_Text Text;

	public void Initialize(Task task)
	{
		Debug.Log("got here");
		switch (task.Type)
		{
			case TaskType.CreateAtom:
				Debug.Log("?");
				Text.text = $"Drag the Imagetarget with the Symbol for {task.Atom.Name} ({task.Atom.Symbol}) into the camera.";
				break;
			case TaskType.ViewAtom:
				Text.text = $"Create {task.Atom.name} and then click the button called \"Atom Info\".";
				break;
			case TaskType.CreateMolecule:
				Text.text = $"Drag the Imagetargets for the atoms {task.Molecule.name} is build of into the camera. " +
							$"You can find the atoms + amount in the formula: {task.Molecule.Formula}.";
				break;
			case TaskType.ViewMolecule:
				Text.text = $"Create {task.Molecule.name} and then click the button called \"Atom Info\".";
				break;
		}
	}

	public void Close() { Destroy(gameObject); }
}