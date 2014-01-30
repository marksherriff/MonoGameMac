 #region File Description
//-----------------------------------------------------------------------------
// MonoGameSetupTutorialGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.GamerServices;

#endregion
namespace MonoGameSetupTutorial
{
	/// <summary>
	/// Default Project Template
	/// </summary>
	public class Game1 : Game
	{

		#region Fields
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Texture2D player;
		Rectangle playerLoc;
		Texture2D[] letters = new Texture2D[7];
		Rectangle letterLoc;
		Random rand = new Random();
		int letterNum = 0;
		double time;
		char[] title = new char[11] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
		string newTitle = "Hurry up!!!";
		int timer = 0;
		#endregion

		#region Initialization

		public Game1()  
		{

			graphics = new GraphicsDeviceManager(this);

			Content.RootDirectory = "Content";

			graphics.IsFullScreen = false;
		}

		/// <summary>
		/// Overridden from the base Game.Initialize. Once the GraphicsDevice is setup,
		/// we'll use the viewport to initialize some values.
		/// </summary>
		protected override void Initialize()
		{
			IsMouseVisible = false;
			this.Window.Title = "";
			base.Initialize();
		}


		/// <summary>
		/// Load your graphics content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be use to draw textures.
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

			// TODO: use this.Content to load your game content here eg.
			player = Content.Load<Texture2D>("mario");
			playerLoc = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, player.Width, player.Height);
			letters[0] = Content.Load<Texture2D>("h");
			letters[1] = Content.Load<Texture2D>("e");
			letters[2] = Content.Load<Texture2D>("l");
			letters[3] = Content.Load<Texture2D>("o");
			letters[4] = Content.Load<Texture2D>("w");
			letters[5] = Content.Load<Texture2D>("r");
			letters[6] = Content.Load<Texture2D>("d");
			letterLoc = new Rectangle(this.Window.ClientBounds.Width - letterLoc.Width, this.Window.ClientBounds.Height - letterLoc.Height, letters[0].Width, letters[0].Height);

		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			playerLoc.X = Mouse.GetState().X;
			playerLoc.Y = Mouse.GetState().Y;
			time += gameTime.ElapsedGameTime.TotalSeconds;
			if (time >= 1)
			{
				letterNum = rand.Next(0, 7);
				time = 0;
				letterLoc.X = rand.Next(0, this.Window.ClientBounds.Width -
					letterLoc.Width);
				letterLoc.Y = rand.Next(0, this.Window.ClientBounds.Height -
					letterLoc.Height);
				if (!newTitle.Substring(0, 11).Equals("Hello World"))
				{
					timer++;
				}
			}
			newTitle = newTitle.Substring(0, 11) + " - " + timer + " Seconds";
			this.Window.Title = newTitle;
			if (playerLoc.Intersects(letterLoc) && Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
				switch (letterNum)
				{
				case 0:
					title[0] = 'H';
					break;
				case 1:
					title[1] = 'e';
					break;
				case 2:
					title[2] = 'l';
					title[3] = 'l';
					title[9] = 'l';
					break;
				case 3:
					title[4] = 'o';
					title[7] = 'o';
					break;
				case 4:
					title[6] = 'W';
					break;
				case 5:
					title[8] = 'r';
					break;
				case 6:
					title[10] = 'd';
					break;
				}

				newTitle = "";
				foreach (char letter in title)
				{
				newTitle += letter;
				}
				newTitle += " - " + timer + " Seconds";
				this.Window.Title = newTitle;
			}
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself. 
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			// Clear the backbuffer
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			spriteBatch.Draw(letters[letterNum], letterLoc, Color.White);
			spriteBatch.Draw(player, playerLoc, Color.White);
			spriteBatch.End();
			base.Draw(gameTime);
		}

		#endregion
	}
}
