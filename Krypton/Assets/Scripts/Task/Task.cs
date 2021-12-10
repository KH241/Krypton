public class CreateAtomTask : Task
{
	public int AtomId;
	
	public CreateAtomTask(int atomId)
	{
		this.Done = false;
		this.Name = "Create " + atomId.ToString() + " TODO"; //TODO Atom daten einbinden
	}
}
public class Task
{
	public bool Done;
	public string Name;
}