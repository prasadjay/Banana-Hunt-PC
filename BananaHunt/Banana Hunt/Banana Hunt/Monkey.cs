using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/*

W.A.P.Jayashanka
prasadjayashanka@gmail.com
http://www.jaynode.com/

*/

namespace Banana_Hunt
{
    class Monkey
    {
        public Texture2D texture;
        public Rectangle monkeyRect;
        public Vector2 monkeyPosition;
        public float monkeySpeed;
        KeyboardState ks;

        public Monkey(Texture2D t, Rectangle r, Vector2 v, float f)
        {
            this.texture = t;
            this.monkeyRect = r;
            this.monkeyPosition = v;
            this.monkeySpeed = f;
        }


        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(texture, monkeyPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

        }

        public void Update()
        {

            monkeyRect = new Rectangle((int)monkeyPosition.X, (int)monkeyPosition.Y, (int)texture.Width, (int)texture.Height);

            ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Right))
                monkeyPosition.X += monkeySpeed;

            if (ks.IsKeyDown(Keys.Left))
                monkeyPosition.X -= monkeySpeed;

            if (ks.IsKeyDown(Keys.Down))
                monkeyPosition.Y += monkeySpeed;

            if (ks.IsKeyDown(Keys.Up))
                monkeyPosition.Y -= monkeySpeed;

            


        }

        public Rectangle getRectangle()
        {
            return new Rectangle((int)monkeyPosition.X, (int)monkeyPosition.Y, (int)texture.Width, (int)texture.Height);
        }

    }
}
