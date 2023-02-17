using System;

namespace FlappyBird
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameFlappyBird game = new GameFlappyBird())
            {
                game.Run();
            }
        }
    }

}

