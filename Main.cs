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

	//effects
	Effect dayNightEffect;

	//content
	protected WorldMap worldMap;
	public DataManager dataManager;
	public Camera camera;
	public MouseHandler mouse;
	protected TimeManager time;

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
		worldFont = Content.Load<SpriteFont>(@"Fonts/PaperJohnny");
	}

	private void createHelpers()
    {
		dataManager = new DataManager();
		mouse = new MouseHandler(this);
		camera = new Camera();
		time = new TimeManager();

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
		//8 - mouse
		//9 - ui

		spriteBatch.Begin(SpriteSortMode.FrontToBack,
			BlendState.NonPremultiplied,
			SamplerState.PointClamp,
			null,
			null,
			dayNightEffect,
			camera.get_transformation(gdm));
		worldMap.Draw(spriteBatch,worldFont);
		mouse.Draw(spriteBatch,camera.cameraPosition,backbufferJamz, worldFont);
		spriteBatch.End();


		//UI
		spriteBatch.Begin();
		time.Draw(spriteBatch, worldFont);
		spriteBatch.End();


		base.Draw(gameTime);
	}
}