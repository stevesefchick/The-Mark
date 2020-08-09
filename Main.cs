using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using The_Mark;

class GameMain : Game
{
	//basic stuff
	protected SpriteBatch spriteBatch;
	protected GraphicsDeviceManager gdm;


	//content
	protected WorldMap worldMap;
	public DataManager dataManager;
	protected Camera camera;

	//controls
	protected Boolean isRightPressed;
	protected Boolean isLeftPressed;
	protected Boolean isUpPressed;
	protected Boolean isDownPressed;
	

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
		gdm = new GraphicsDeviceManager(this);

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

	protected void getInput()
    {
		KeyboardState kbState = Keyboard.GetState();

		//right
		if (kbState.IsKeyDown(Keys.Right)==true)
        {
			isRightPressed = true;
        }
		else
        {
			isRightPressed = false;
        }

		//left
		if (kbState.IsKeyDown(Keys.Left) == true)
		{
			isLeftPressed = true;
		}
		else
		{
			isLeftPressed = false;
		}

		//up
		if (kbState.IsKeyDown(Keys.Up) == true)
		{
			isUpPressed = true;
		}
		else
		{
			isUpPressed = false;
		}

		//down
		if (kbState.IsKeyDown(Keys.Down) == true)
		{
			isDownPressed = true;
		}
		else
		{
			isDownPressed = false;
		}

	}

	protected override void Update(GameTime gameTime)
	{
		getInput();
		camera.Update(isUpPressed, isDownPressed, isLeftPressed, isRightPressed);
		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(Color.CornflowerBlue);


		spriteBatch.Begin(SpriteSortMode.BackToFront,
			BlendState.AlphaBlend,
			null,
			null,
			null,
			null,
			camera.get_transformation(gdm));
		worldMap.Draw(spriteBatch);
		spriteBatch.End();


		base.Draw(gameTime);
	}
}