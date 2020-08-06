using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using The_Mark;

class GameMain : Game
{
	//basic stuff
	protected SpriteBatch spriteBatch;

	//content
	protected WorldMap worldMap;
	public DataManager dataManager;
	protected Camera camera;

	//run the game
	static void Main(string[] args)
	{
		using (GameMain g = new GameMain())
		{
			g.Run();
		}
	}

	protected GameMain()
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
		createNewWorld();
		createHelpers();


		dataManager.LoadAllData(this);

		base.Initialize();
	}

	private void createHelpers()
    {
		dataManager = new DataManager();
		camera = new Camera();
    }


	//Creates a new world and all relevant items within should the player choose New Game
	void createNewWorld()
    {
		worldMap = new WorldMap(this);
    }

	protected override void LoadContent()
	{
		spriteBatch = new SpriteBatch(GraphicsDevice);

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


		spriteBatch.Begin();
		worldMap.Draw(gameTime, spriteBatch);
		spriteBatch.End();


		base.Draw(gameTime);
	}
}