using Microsoft.Xna.Framework;
using The_Mark;

class GameMain : Game
{
	protected WorldMap worldMap;

	//run the game
	static void Main(string[] args)
	{
		using (GameMain g = new GameMain())
		{
			g.Run();
		}
	}

	private GameMain()
	{
		GraphicsDeviceManager gdm = new GraphicsDeviceManager(this);

		// All content loaded will be in a "Content" folder
		Content.RootDirectory = "Content";

		//load configs here
		gdm.PreferredBackBufferWidth = 1280;
		gdm.PreferredBackBufferHeight = 720;
		gdm.IsFullScreen = false;
		gdm.SynchronizeWithVerticalRetrace = true;
	}

	protected override void Initialize()
	{
		base.Initialize();
	}

	protected override void LoadContent()
	{
		base.LoadContent();
	}

	protected override void UnloadContent()
	{
		base.UnloadContent();
	}

	protected override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(Color.CornflowerBlue);
		base.Draw(gameTime);
	}
}