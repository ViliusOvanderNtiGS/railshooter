using System;
using Raylib_cs;

namespace railshooter
{
    class Program
    {
        static void Main(string[] args)
        {
            int screenY = 800;
            int screenX = 1200;
            string scene = "intro";
            Raylib.InitWindow(screenX, screenY, "poop B-)");

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.BEIGE);



                if (scene == "intro")
                {
                    Raylib.ClearBackground(Color.BEIGE);
                    Raylib.DrawText("INTRO!!!!!", 100, 50, 20, Color.WHITE);
                    Raylib.DrawText("Shoot B-)", (screenX / 2) - 100, screenY / 4, 50, Color.BLACK);


                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        scene = "game";
                    }

                }
                else if (scene == "game")
                {

                }


                Raylib.EndDrawing();
            }

        }
    }
}
