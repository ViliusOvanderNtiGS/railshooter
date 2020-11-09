using System;
using System.Numerics;
using Raylib_cs;

namespace railshooter
{
    class Program
    {
        static void Main(string[] args)
        {
            int screenY = 900;
            int screenX = 1400;
            string scene = "intro";
            Raylib.InitWindow(screenX, screenY, "poop B-)");

            bool exitGame = false;

            while (!Raylib.WindowShouldClose() && !exitGame)
            {
                Raylib.BeginDrawing();

                if (scene == "intro")
                {
                    introScreen(screenX, screenY);

                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        scene = "game";
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_Q))
                    {
                        //Raylib.CloseWindow();
                        exitGame = true;
                    }

                }
                else if (scene == "game")
                {
                    Raylib.ClearBackground(Color.GREEN);

                    int mouseX = Raylib.GetMouseX();
                    int mouseY = Raylib.GetMouseY();
                    Texture2D crosshair = Raylib.LoadTexture("crosshair.png");
                    // Raylib.HideCursor();

                    Raylib.DrawTexture(crosshair, mouseX, mouseY, Color.WHITE);
                    Raylib.DrawTextureEx(crosshair, new Vector2(mouseX, mouseY), 0, 0.25f, Color.WHITE);






                }


                Raylib.EndDrawing();
            }

        }
        static void introScreen(int screenX, int screenY)
        {
            Raylib.ClearBackground(Color.BEIGE);
            Raylib.DrawText("INTRO!", 100, 50, 20, Color.WHITE);
            Raylib.DrawText("Space to start", (screenX / 2) - 100, screenY / 4, 50, Color.BLACK);
            Raylib.DrawText("Q to quit", (screenX / 2) - 100, (screenY / 4) + 100, 50, Color.BLACK);
        }



    }
}
