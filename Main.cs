//GAME NAME IDEAS
//The Mark --> taken
//Deathpunk --> a band
//Dead Legends --> larping?


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
	public SpriteFont worldFont;
	public SpriteFont bigWorldFont;
	public SpriteFont textFont;

	//effects
	Effect dayNightEffect;

	//UI Element positions
	Vector2 timeUIPosition;

	//content
	protected WorldMap worldMap;
	protected PlayerHandler playerHandler;
	protected TimeManager time;

	//helpers
	public DataManager dataManager;
	public UI_Helper uiHelper;
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
	protected Boolean isSpacePressed;

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

	private void GetUIElementPositions()
    {
		timeUIPosition = new Vector2(backbufferJamz.X - 475, 50);

    }

	protected override void Initialize()
	{
		//Create necessary helpers
		createHelpers();
		//generate UI Element Positions
		GetUIElementPositions();
		//Load all data
		dataManager.LoadAllData(this);
		//Set up the world
		createNewWorld();
		//Set up player character
		playerHandler.CreateTheMark(worldMap, rando);
		//Start the timer
		time.StartTimer();


		base.Initialize();
	}

	void LoadFonts()
    {
		//todo: create new project to load fonts from
		worldFont = Content.Load<SpriteFont>(@"Fonts/PaperJohnny");
		bigWorldFont = Content.Load<SpriteFont>(@"Fonts/PaperJohnnyBig");
		textFont = Content.Load<SpriteFont>(@"Fonts/Pixellari");
	}

	private void createHelpers()
    {
		LoadFonts();

		dataManager = new DataManager();
		mouse = new MouseHandler(this);
		camera = new Camera();
		time = new TimeManager();
		playerHandler = new PlayerHandler();
		uiHelper = new UI_Helper(this,bigWorldFont,textFont);

    }


	//Creates a new world and all relevant items within should the player choose New Game
	void createNewWorld()
    {
		worldMap = new WorldMap(this,rando,dataManager);
    }

	protected override void LoadContent()
	{
		spriteBatch = new SpriteBatch(GraphicsDevice);
		dayNightEffect = Content.Load<Effect>("Shaders/MarkEffect");

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

		//space
		if (kbState.IsKeyDown(Keys.Space) == true)
		{
			isSpacePressed = true;
		}
		else
		{
			isSpacePressed = false;
		}
	}

	void getShaderColors()
    {
		Vector3 influences = time.returnColorInfluencesBasedOnTime();
		dayNightEffect.Parameters["redinfluence"].SetValue(influences.X);
		dayNightEffect.Parameters["greeninfluence"].SetValue(influences.Y);
		dayNightEffect.Parameters["blueinfluence"].SetValue(influences.Z);

	}

	protected override void Update(GameTime gameTime)
	{
		getInput();
		getShaderColors();
		camera.Update(isUpPressed, isDownPressed, isLeftPressed, isRightPressed,isPageDownPressed,isPageUpPressed);
		mouse.Update(camera.cameraPosition,backbufferJamz,worldFont);
		worldMap.Update(this,rando);
		uiHelper.Update(mouse,playerHandler, returnPositionCameraOffset(Vector2.Zero));

		//debug
		checkForEnterPressed();
		checkForSpacePressed();

		base.Update(gameTime);
	}

	private void checkForSpacePressed()
    {
		if (isSpacePressed==true)
        {
			time.timeTick(5);
        }
    }
	private void checkForEnterPressed()
    {
		if (isEnterPressed == true)
		{
			worldMap = null;
			createNewWorld();
		}
	}

	Vector2 returnPositionCameraOffset(Vector2 position)
    {
		return position - backbufferJamz / 2 + camera.cameraPosition;



	}

	protected override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(Color.CornflowerBlue);

		//0 - background
		//1 - grass
		//1.5 - textures
		//2 - water
		//2.5 - roads
		//3 - places and doodads
		//6 - clouds
		//7 - text
		//8 - ui
		//9 - mouse

		//world
		spriteBatch.Begin(SpriteSortMode.FrontToBack,
			BlendState.NonPremultiplied,
			SamplerState.PointClamp,
			null,
			null,
			dayNightEffect,
			camera.get_transformation(gdm));
		worldMap.Draw(spriteBatch,worldFont);
		spriteBatch.End();


		//mouse and ui
		spriteBatch.Begin(SpriteSortMode.FrontToBack,
	BlendState.NonPremultiplied,
	SamplerState.PointClamp,
	null,
	null,
	null,
	camera.get_transformation(gdm));
		mouse.Draw(spriteBatch, camera.cameraPosition, backbufferJamz, worldFont);
		time.Draw(spriteBatch, worldFont, returnPositionCameraOffset(timeUIPosition));
		uiHelper.Draw(spriteBatch, returnPositionCameraOffset(Vector2.Zero));
		spriteBatch.End();

		base.Draw(gameTime);
	}
}