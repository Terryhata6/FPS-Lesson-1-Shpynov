
namespace Game
{
	public sealed class Wall : BaseObjectScene, ISelectObj
	{
		public string GetMessage()
		{
			return Name;
		}
	}
}
