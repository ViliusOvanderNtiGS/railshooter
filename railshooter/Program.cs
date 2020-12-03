using System;
using System.Numerics;
using Raylib_cs;
using System.Collections.Generic;

/* 
    Att göra lista

    få blocken att röra på sig
    få blocken att röra på sig i olika hastighet
        // få blocken att försvinna när man trycker på dem
    få blocken att dyka upp slumpat från kanten på skärmen
    gör introscreen snyggare
    kanske fixa fps om det går
*/

namespace railshooter
{
    class Program
    {
        static void Main(string[] args)
        {

            int screenY = 900;
            int screenX = 1400;
            string scene = "intro";
            int score = 0;
            Raylib.InitWindow(screenX, screenY, "Hej");
            Raylib.SetTargetFPS(60);

            bool exitGame = false;

            // försök till movement
            float x1 = 50;

            List<Rectangle> enemies = new List<Rectangle>();

            Rectangle e1 = new Rectangle((int)x1, 125, 90, 90);
            Rectangle e2 = new Rectangle(200, 360, 110, 110);
            Rectangle e3 = new Rectangle(300, 650, 120, 120);

            enemies.Add(e1);
            enemies.Add(e2);
            enemies.Add(e3);




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

                    Texture2D bg = Raylib.LoadTexture("bg_vatten1.png");
                    Raylib.DrawTexture(bg, 0, 0, Color.WHITE);


                    //space är för tillfället försök till movement


                    for (int i = 0; i < enemies.Count; i++)
                    {
                        //enemies rörelser
                        Rectangle tmp = enemies[i];
                        tmp.x += 1;
                        enemies[i] = tmp;
                    }



                    //enemies rörelser
                    Rectangle tmp = enemies[0];
                    tmp.x += 1;
                    enemies[0] = tmp;

                    //andra enemies
                    Rectangle tmp = enemies[1];
                    tmp.x += 2;
                    enemies[1] = tmp;

                    //tredje enemies
                    Rectangle tmp = enemies[2];
                    tmp.x += 3;
                    enemies[2] = tmp;




                    score = enemy(screenX, screenY, score, x1, enemies);
                    crossHair();
                    Score(screenX, score);


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
            Vector2 pos1 = new Vector2(screenX / 2, screenY / 2);
            Vector2 pos2 = new Vector2(screenX / 2 - 100, screenY / 2 + 100);

            Raylib.DrawTextEx(fonten, "INTRO", pos, 20, 0, Color.WHITE);
            Raylib.DrawTextEx(fonten, "Space to start", pos1, 50, 0, Color.WHITE);
            Raylib.DrawTextEx(fonten, "Q to quit", pos2, 60, 0, Color.WHITE);
        }

        static void crossHair()
        {

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
        static int enemy(int screenX, int screenY, int score, float x1, List<Rectangle> enemies)
        {
            // int e1PosX = 50;
            int e1PosY = 125;


            //------------------------------------------------------------------------//

            for (int i = 0; i < enemies.Count; i++)
            {
                Raylib.DrawRectangleRec(enemies[i], Color.RED);

                Vector2 mousePos = Raylib.GetMousePosition();




                if (Raylib.CheckCollisionPointRec(mousePos, enemies[i]) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    score = score += 10;
                    //Raylib. ta bort rectangeln enemies[den som är klickad på]

                    Rectangle tmp = enemies[i];
                    tmp.y = -900;
                    enemies[i] = tmp;
                    //enemies.Remove(enemies);
                }
            }

            enemies.RemoveAll(enemy => enemy.y < 0);

            return score;
        }
        static void Score(int screenX, int score)
        {
            //lägg till ett crit system med en random generator om nummer är >10 = crit = x2 score + crit text som ploppar upp

            Raylib.DrawText("Score " + score, (screenX / 2) - 100, 50, 50, Color.BLACK);
        }






    }
}
