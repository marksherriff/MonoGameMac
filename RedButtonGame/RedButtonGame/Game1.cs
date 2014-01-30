#region Using Statements
using System;
using System.Collections.Generic;
//using System.Windows.Forms.MessageBox;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace RedButtonGame
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;


		//game fields
		int rbx, rby;
		Random r;
		ButtonState lastButtonState;
		private Texture2D buttonTxt;
		private MouseState mouse;

		public Game1()
			: base()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			r = new Random();
			this.IsMouseVisible = true;
		}


		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			ResetButtonLoc();

			base.Initialize();
		}


		//ResetButtonLoc will move the red button elsewhere

		public void ResetButtonLoc()
		{
			int oldx = rbx;
			while (oldx - rbx < 64 && oldx - rbx > -64)
			{
				rbx = 32 + r.Next(GraphicsDevice.Viewport.Width - 64);
			}
			int oldy = rby;
			while (oldy - rby < 64 && oldy - rby > -64)
			{
				rby = 32 + r.Next(GraphicsDevice.Viewport.Height - 64);
			}

		}



		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			buttonTxt = Content.Load<Texture2D>("RedButton");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}


		//distance method because wat
		public double Distance(int x1, int x2, int y1, int y2)
		{
			return (Math.Sqrt(Math.Pow(Math.Abs(x1 - x2), 2) + Math.Pow(Math.Abs(y1 - y2), 2)));
		}



		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			lastButtonState = mouse.LeftButton;
			mouse = Mouse.GetState();
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();



			//Console.WriteLine(Distance(rbx, mouse.X, rby, mouse.Y));
			if (Distance(rbx, mouse.X, rby, mouse.Y) < 32)
			if (mouse.LeftButton == ButtonState.Pressed && lastButtonState != ButtonState.Pressed)
			{
				//win logic
				// MessageBox.Show("You Win!", "WOW SUCH SKILL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Console.WriteLine("WOW SUCH SKILL");
				Exit();
			}
			else
				ResetButtonLoc();
			else if (Distance(rbx, mouse.X, rby, mouse.Y) < 100)
			{
				if (rbx < mouse.X )
					rbx-= 1;
				else
					rbx+= 1;
				if (rby < mouse.Y)
					rby--;
				else
					rby++;

			}
			// TODO: Add your update logic here

			if (rbx < 0 || rbx > GraphicsDevice.Viewport.Width || rby < 0 || rby > GraphicsDevice.Viewport.Height)
				ResetButtonLoc();

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here

			spriteBatch.Begin();

			spriteBatch.Draw(buttonTxt, new Rectangle(rbx - 32, rby - 32, 64, 64), Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
