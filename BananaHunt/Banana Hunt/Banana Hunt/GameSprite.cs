using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class GameSprite
    {
        public Texture2D texture;
        public Rectangle Rect;
        public Vector2 Position;
        public float Speed;

        public GameSprite(Texture2D t, Rectangle r, Vector2 v, float f)
        {
            this.texture = t;
            this.Rect = r;
            this.Position = v;
            this.Speed = f;
        }


        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(texture, Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

        }

        public void DrawObject(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(texture, Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 10);

        }


        public void Update()
        {
            Rect = new Rectangle((int)Position.X, (int)Position.Y, (int)texture.Width, (int)texture.Height);
        }

        public Rectangle getRectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)texture.Width, (int)texture.Height);
        }
    }
}
