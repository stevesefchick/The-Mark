using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using The_Mark;
//using SpriteFontPlus;
using System.IO;

class GameMain : Game
{
	//basic stuff
	protected SpriteBatch spriteBatch;
	protected GraphicsDeviceManager gdm;
	public Vector2 backbufferJamz = Vector2.Zero;

	//font info
	public SpriteFont testFont;


	//content
	protected WorldMap worldMap;
	public DataManager dataManager;
	public Camera camera;
	public MouseHandler mouse;

	//controls
	protected Boolean isRightPressed;
	protected Boolean isLeftPressed;
	protected Boolean isUpPressed;
	protected Boolean isDownPressed;
	protected Boolean isPageDownPressed;
	protected Boolean isPageUpPressed;
	protected Boolean isEnterPressed;

	//random stuff
	Random rando = new Random();
	
	

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
		backbufferJamz = new Vector2(gdm.PreferredBackBufferWidth, gdm.PreferredBackBufferHeight);
		gdm.IsFullScreen = false;
		gdm.SynchronizeWithVerticalRetrace = true;

	}

	protected override void Initialize()
	{
		createHelpers();
		dataManager.LoadAllData(this);
		createNewWorld();
		


		base.Initialize();
	}

	void LoadFonts()
    {
		//todo: create new project to load fonts from
		//TODO: add legit new fonts
		testFont = Content.Load<SpriteFont>(@"Fonts/menuFont");
	}

	private void createHelpers()
    {
		dataManager = new DataManager();
		mouse = new MouseHandler(this);
		camera = new Camera();

		LoadFonts();
    }


	//Creates a new world and all relevant items within should the player choose New Game
	void createNewWorld()
    {
		worldMap = new WorldMap(this,rando,dataManager);
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

		//pagedown
		if (kbState.IsKeyDown(Keys.PageDown) == true)
        {
			isPageDownPressed = true;
        }
		else
        {
			isPageDownPressed = false;
        }

		//pageup
		if (kbState.IsKeyDown(Keys.PageUp) == true)
		{
			isPageUpPressed = true;
		}
		else
		{
			isPageUpPressed = false;
		}

		//enter
		if (kbState.IsKeyDown(Keys.Enter) == true)
		{
			isEnterPressed = true;
		}
		else
		{
			isEnterPressed = false;
		}
	}

	protected override void Update(GameTime gameTime)
	{


		getInput();
		camera.Update(isUpPressed, isDownPressed, isLeftPressed, isRightPressed,isPageDownPressed,isPageUpPressed);
		mouse.Update();
		worldMap.Update(this);

		//debug
		checkForEnterPressed();

		base.Update(gameTime);
	}

	private void checkForEnterPressed()
    {
		if (isEnterPressed == true)
		{
			worldMap = null;
			createNewWorld();
		}
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
		worldMap.Draw(spriteBatch,testFont);
		mouse.Draw(spriteBatch,camera.cameraPosition,backbufferJamz);
		spriteBatch.End();

		base.Draw(gameTime);
	}
}