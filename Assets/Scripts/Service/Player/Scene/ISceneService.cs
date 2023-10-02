namespace Service.Player.Scene
{
	public interface IScene
	{

	}

	public interface ISceneService : IGameService
	{
		void Transition(IScene scene);
	}
}