using System;
using System.Numerics;
using Raylib_cs;
using System.Collections.Generic;
using System.Linq;

/* 
    Att göra lista

        // få blocken att röra på sig
        // få blocken att röra på sig i olika hastighet
    få blocken att försvinna när man trycker på dem
    få blocken att dyka upp från kanten på skärmen
    gör introscreen snyggare
    crit system med slump
    kanske fixa fps om det går eller inte
*/

namespace railshooter
{

    public class EnemyStruct
    {
        public Rectangle rect;
        public float speed;
        public int row;

        public EnemyStruct(Rectangle rect, float speed, int row)
        {
            this.rect = rect;
            this.speed = speed;
            this.row = row;
        }
        public void Update()
        {
            this.rect.x += speed;
            Raylib.DrawText(this.rect.x.ToString(), 0, 0, 40, Color.ORANGE);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            int screenY = 900;
            int screenX = 1400;
            string scene = "intro";
            int score = 0;
            int miss = 0;
            Raylib.InitWindow(screenX, screenY, "Hej");
            Raylib.SetTargetFPS(60);
            bool exitGame = false;


            // Gör lista till enemies
            List<Rectangle> enemies = new List<Rectangle>();
            Rectangle e1 = new Rectangle(50, 125, 90, 90);

            List<Rectangle> enemies1 = new List<Rectangle>();
            Rectangle e2 = new Rectangle(200, 360, 110, 110);

            List<Rectangle> enemies2 = new List<Rectangle>();
            Rectangle e3 = new Rectangle(300, 650, 120, 120);

            enemies.Add(e1);
            enemies1.Add(e2);
            enemies2.Add(e3);

            // Lista till speed som läggs till
            List<int> speed = new List<int>();
            speed.Add(15);
            speed.Add(-10);
            speed.Add(5);


            //Lista till struct
            List<EnemyStruct> enemyList = new List<EnemyStruct>();
            enemyList.Add(new EnemyStruct(e1, 15, 1));

            List<EnemyStruct> enemyList1 = new List<EnemyStruct>();
            enemyList1.Add(new EnemyStruct(e2, -10, 2));

            List<EnemyStruct> enemyList2 = new List<EnemyStruct>();
            enemyList2.Add(new EnemyStruct(e3, 5, 3));



            // main loop
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


                    // försök till att använda structurer



                    for (int i = 0; i < enemyList.Count; i++)
                    {
                        EnemyStruct e = enemyList[i];
                        e.Update();
                        Raylib.DrawRectangle((int)e.rect.x, (int)e.rect.y, (int)e.rect.width, (int)e.rect.height, Color.WHITE);
                    }






                    /*

                                        for (int i = 0; i < enemies.Count; i++)
                                        {
                                            //enemies rörelser
                                            Rectangle tmp = enemies[i];
                                            tmp.x += speed[i];
                                            enemies[i] = tmp;
                                        }

                                        for (int i = 0; i < enemies1.Count; i++)
                                        {
                                            //enemies rörelser
                                            Rectangle tmp = enemies1[i];
                                            tmp.x += speed[i];
                                            enemies1[i] = tmp;
                                        }

                                        for (int i = 0; i < enemies2.Count; i++)
                                        {
                                            //enemies rörelser
                                            Rectangle tmp = enemies2[i];
                                            tmp.x += speed[i];
                                            enemies2[i] = tmp;
                                        }

                    */


                    //detta ska försöka spawna en till enemy på något sätt tycker jag
                    if (enemyList.Last().rect.x > 80)
                    {
                        enemyList.Add(new EnemyStruct(new Rectangle(-500, 125, 90, 90), 15, 1));
                        speed.Add(15);
                    }

                    if (enemyList1.Last().rect.x > 80)
                    {
                        enemyList1.Add(new EnemyStruct(new Rectangle((screenX - 220), 360, 110, 110), -15, 2));
                        speed.Add(-10);
                    }

                    if (enemyList2.Last().rect.x > 80)
                    {
                        enemyList2.Add(new EnemyStruct(new Rectangle(-300, 650, 120, 120), 5, 3));
                        speed.Add(5);
                    }



                    /*
                                        // kan bheöva göra en list per rad av enemy så gör det senare lata fan
                                        if (enemies1.Last().x < screenX - 170)

                                        {
                                            enemies1.Add(new Rectangle(screenX + 100, 360, 90, 90));
                                            speed.Add(-10);
                                        }

                                        if (enemies2.Last().x > 80 && enemies2.Count < 4)
                                        {
                                            enemies2.Add(new Rectangle(300, 650, 120, 120));
                                            speed.Add(-10);
                                        }
                    */



                    score = enemy(screenX, screenY, score, enemies, enemies1, enemies2, miss);
                    crossHair();
                    Score(screenX, score);
                    Miss(screenX, screenY, miss);


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
        static int enemy(int screenX, int screenY, int score, List<Rectangle> enemies, List<Rectangle> enemies1, List<Rectangle> enemies2, int miss)
        {
            // int e1PosX = 50;


            Random generator = new Random();

            //enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                Raylib.DrawRectangleRec(enemies[i], Color.RED);

                Vector2 mousePos = Raylib.GetMousePosition();


                if (Raylib.CheckCollisionPointRec(mousePos, enemies[i]) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    score = score += 10;
                    //slumpad crit system 

                    int crit = generator.Next(0, 101);

                    if (crit >= 90)
                    {
                        score = score += 15;
                    }


                    //Raylib. ta bort rectangeln enemies[den som är klickad på]

                    Rectangle tmp = enemies[i];
                    tmp.y = -900;
                    enemies[i] = tmp;
                    //enemies.Remove(enemies);
                }

                //lägga till i miss
                /*
                if (enemies[i].x => screenX){
                    miss++;
                }
                */
            }

            //enemies1
            for (int i = 0; i < enemies1.Count; i++)

            {
                Raylib.DrawRectangleRec(enemies1[i], Color.RED);

                Vector2 mousePos = Raylib.GetMousePosition();


                if (Raylib.CheckCollisionPointRec(mousePos, enemies1[i]) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    score = score += 10;
                    //slumpad crit system 

                    int crit = generator.Next(0, 101);

                    if (crit >= 90)
                    {
                        score = score += 15;
                    }

                    Rectangle tmp = enemies1[i];
                    tmp.y = -900;
                    enemies1[i] = tmp;
                    //enemies.Remove(enemies);
                }
            }

            //enemies2
            for (int i = 0; i < enemies2.Count; i++)

            {
                Raylib.DrawRectangleRec(enemies2[i], Color.RED);

                Vector2 mousePos = Raylib.GetMousePosition();


                if (Raylib.CheckCollisionPointRec(mousePos, enemies2[i]) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    score = score += 10;
                    //slumpad crit system 
                    //det är inte så tydlig kanske men den funkar när man spelar

                    int crit = generator.Next(0, 101);

                    if (crit >= 90)
                    {
                        score = score += 15;
                    }

                    Rectangle tmp = enemies2[i];
                    tmp.y = -900;
                    enemies2[i] = tmp;
                }
            }

            enemies.RemoveAll(enemy => enemy.y < 0);
            enemies1.RemoveAll(enemy => enemy.y < 0);
            enemies2.RemoveAll(enemy => enemy.y < 0);

            return score;
            //return miss;
        }
        static void Score(int screenX, int score)
        {
            //lägg till ett crit system med en random generator om nummer är >10 = crit = x2 score + crit text som ploppar upp

            Raylib.DrawText("Score " + score, (screenX / 2) - 100, 50, 50, Color.BLACK);

        }

        static void Miss(int screenX, int screenY, int miss)
        {
            Raylib.DrawText("Miss " + miss, (screenX / 2) - 100, screenY - 150, 50, Color.BLACK);

        }

    }
}
