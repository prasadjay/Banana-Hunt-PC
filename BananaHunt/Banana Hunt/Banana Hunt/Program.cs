using System;

/*

W.A.P.Jayashanka
prasadjayashanka@gmail.com
http://www.jaynode.com/

*/

namespace Banana_Hunt
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

