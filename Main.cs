﻿using Microsoft.Xna.Framework;

class FNAGame : Game
{
	//run the game
	static void Main(string[] args)
	{
		using (FNAGame g = new FNAGame())
		{
			g.Run();
		}
	}

	private FNAGame()
	{
		GraphicsDeviceManager gdm = new GraphicsDeviceManager(this);

		// Typically you would load a config here...
		gdm.PreferredBackBufferWidth = 1280;
		gdm.PreferredBackBufferHeight = 720;
		gdm.IsFullScreen = false;
		gdm.SynchronizeWithVerticalRetrace = true;
	}

	protected override void Initialize()
	{
		/* This is a nice place to start up the engine, after
		 * loading configuration stuff in the constructor
		 */
		base.Initialize();
	}

	protected override void LoadContent()
	{
		// Load textures, sounds, and so on in here...
		base.LoadContent();
	}

	protected override void UnloadContent()
	{
		// Clean up after yourself!
		base.UnloadContent();
	}

	protected override void Update(GameTime gameTime)
	{
		// Run game logic in here. Do NOT render anything here!
		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		// Render stuff in here. Do NOT run game logic in here!
		GraphicsDevice.Clear(Color.CornflowerBlue);
		base.Draw(gameTime);
	}
}