using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*

W.A.P.Jayashanka
prasadjayashanka@gmail.com
http://www.jaynode.com/

*/

namespace Banana_Hunt
{
    class Camera
    {

        public Matrix transform;
        Viewport view;
        Vector2 centre;

        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void update(GameTime gametime, Game1 ship)
        {
            centre = new Vector2(ship.monkeyPosition.X + (ship.monkeyRect.Width / 2) - 640, 0);
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }


    }
}
