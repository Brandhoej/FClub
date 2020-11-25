namespace FClub.UI.Scene
{
	public enum SceneNavigationDirection
	{
		Up   = 1, Down  = -Up,
		Left = 2, Right = -Left
	}

	public static class ISceneNavigationDirectionExtensions
	{
		public static SceneNavigationDirection Opposite(this SceneNavigationDirection sceneNavigationDirection)
		{
			return (SceneNavigationDirection)(-1 * (int)sceneNavigationDirection);
		}
	}
}
