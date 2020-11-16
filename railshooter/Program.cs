using System;
using System.Numerics;
using Raylib_cs;
using System.Collections.Generic;

namespace railshooter
{
    class Program
    {
        static void Main(string[] args)
        {

            

            int screenY = 900;
            int screenX = 1400;
            string scene = "intro";
            Raylib.InitWindow(screenX, screenY, ":-)");

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

                    crossHair();






                }


                Raylib.EndDrawing();
            }

        }
        static void introScreen(int screenX, int screenY)
        {
            Raylib.ClearBackground(Color.BEIGE);
            Font fonten = Raylib.LoadFont("Orbitron-Black.ttf");
            //Raylib.DrawText("INTRO!", 100, 50, 20, Color.WHITE);
            //Raylib.DrawText("Space to start", (screenX / 2) - 100, screenY / 4, 50, Color.BLACK);
            //Raylib.DrawText("Q to quit", (screenX / 2) - 100, (screenY / 4) + 100, 50, Color.BLACK);
            Vector2 pos = new Vector2(100, 50);
            Vector2 pos1 = new Vector2(screenX/2,screenY/2);
            Vector2 pos2 = new Vector2(screenX/2 - 100,screenY/2 + 100);

            Raylib.DrawTextEx(fonten, "INTRO", pos, 20, 0, Color.WHITE);
            Raylib.DrawTextEx(fonten, "Space to start", pos1, 50, 0, Color.WHITE);
            Raylib.DrawTextEx(fonten, "Q to quit", pos2, 60, 0, Color.WHITE);
        }

        static void crossHair(){

            // Crosshair 
                    int mouseX = Raylib.GetMouseX();
                    int mouseY = Raylib.GetMouseY();
                    Texture2D crosshair = Raylib.LoadTexture("crosshair.png");
                    Raylib.HideCursor();

                    Raylib.DrawTexture(crosshair, mouseX - 64, mouseY - 64, Color.WHITE);
                    //Raylib.DrawTextureEx(crosshair, new Vector2(mouseX, mouseY), 0, 0.20f, Color.WHITE);
            // Pistol

            // meningen är att rita en pistol från botten mitten av skärmen som pipan följer musen
            // så bara ena änden av pistolen följer mauseX fast inte heal vägen


        }

        



    }
}
