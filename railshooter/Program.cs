using System;
using System.Numerics;
using Raylib_cs;
using System.Collections.Generic;
using System.Linq;


#region Massa text

/*     Att göra lista

        // Byt ut så att alla enemies är i struct listan
        // få blocken att röra på sig
        // få blocken att röra på sig i olika hastighet
        // få blocken att försvinna när man trycker på dem
        //få blocken att dyka upp från kanten på skärmen
    gör introscreen snyggare
    (det fungerar men man märker knappt det)    // crit system med slump
    kanske fixa fps
*/

/*  Att göra bättre
    1. Metoden vid 254 har nästan samma kåd 3 gångger. 
    2. Gör introscreen snyggare
    3. tydligare critsystem med att det kanske flyger upp en text när man får en crit. 
*/

/*  implementering till andra plattformer
    Det är ett satans simpelt spel som bara behöver en joystick eller D-knappar och en knapp som skjuter.
    Enligt mig så skulle det då kunna fungerar på en nes och uppåt. Eller vilken konsol som helst som har D-knappar eller en joystic + en till knapp
    det skulle kunna fungera på minireäknaren
    5 knappar är allt om behövs.
    optimisering är något som man behöver göra dock för att få skiten att fungera.
    Det kommer dock vara mycket svårare på konsol med joystic eller knappar. så balansering kan behövas.
*/
#endregion

namespace railshooter
{
    #region EnemyStruct

    public class EnemyStruct
    {
        public Rectangle rect;
        public float speed;

        public EnemyStruct(Rectangle rect, float speed)
        {
            this.rect = rect;
            this.speed = speed;
        }
        public void Update()
        {
            this.rect.x += speed;
            Raylib.DrawText(this.rect.x.ToString(), 0, 0, 40, Color.ORANGE);
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {

            #region setup och listor

            int screenY = 900;
            int screenX = 1400;
            string scene = "intro";
            int score = 0;
            int miss = 0;
            Raylib.InitWindow(screenX, screenY, "Carnival Shooter");
            Raylib.SetTargetFPS(60);
            bool exitGame = false;


            // Gör enemies till listan
            Rectangle e1 = new Rectangle(50, 125, 90, 90);
            Rectangle e2 = new Rectangle(screenX, 360, 110, 110);
            Rectangle e3 = new Rectangle(300, 650, 120, 120);

            //Lista till struct
            List<EnemyStruct> enemyList = new List<EnemyStruct>();
            enemyList.Add(new EnemyStruct(e1, 15));

            List<EnemyStruct> enemyList1 = new List<EnemyStruct>();
            enemyList1.Add(new EnemyStruct(e2, -10));

            List<EnemyStruct> enemyList2 = new List<EnemyStruct>();
            enemyList2.Add(new EnemyStruct(e3, 5));

            // Lista till speed som läggs till
            List<int> speed = new List<int>();
            speed.Add(15);
            speed.Add(-10);
            speed.Add(5);
            #endregion


            // main loop
            while (!Raylib.WindowShouldClose() && !exitGame)

            {
                Raylib.BeginDrawing();

                #region intro

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
                #endregion

                #region backgrund och structförsök

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

                    for (int i = 0; i < enemyList1.Count; i++)
                    {
                        EnemyStruct e = enemyList1[i];
                        e.Update();
                        Raylib.DrawRectangle((int)e.rect.x, (int)e.rect.y, (int)e.rect.width, (int)e.rect.height, Color.WHITE);
                    }

                    for (int i = 0; i < enemyList2.Count; i++)
                    {
                        EnemyStruct e = enemyList2[i];
                        e.Update();
                        Raylib.DrawRectangle((int)e.rect.x, (int)e.rect.y, (int)e.rect.width, (int)e.rect.height, Color.WHITE);
                    }
                    #endregion

                    #region enemy spawn

                    //detta ska försöka spawna en till enemy på något sätt tycker jag
                    if (enemyList.Last().rect.x > 80)
                    {
                        enemyList.Add(new EnemyStruct(new Rectangle(-500, 125, 90, 90), 15));
                        speed.Add(15);
                    }

                    if (enemyList1.Last().rect.x < ((screenX / 4) * 3))
                    {
                        enemyList1.Add(new EnemyStruct(new Rectangle((screenX + 220), 360, 110, 110), -15));
                        speed.Add(-10);
                    }

                    if (enemyList2.Last().rect.x > 80)
                    {
                        enemyList2.Add(new EnemyStruct(new Rectangle(-300, 650, 120, 120), 5));
                        speed.Add(5);
                    }
                    #endregion



                    //metoder
                    score = enemy(screenX, screenY, score, miss, enemyList, enemyList1, enemyList2);
                    crossHair();
                    Score(screenX, score);
                    Miss(screenX, screenY, miss); //(används inte)


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
        static int enemy(int screenX, int screenY, int score, int miss, List<EnemyStruct> enemyList, List<EnemyStruct> enemyList1, List<EnemyStruct> enemyList2)
        {

            Random generator = new Random();

            //enemies
            for (int i = 0; i < enemyList.Count; i++)
            {
                Raylib.DrawRectangleRec(enemyList[i].rect, Color.RED);

                Vector2 mousePos = Raylib.GetMousePosition();


                if (Raylib.CheckCollisionPointRec(mousePos, enemyList[i].rect) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    score = score += 10;
                    //slumpad crit system 

                    int crit = generator.Next(0, 101);

                    if (crit >= 90)
                    {
                        score = score += 15;
                    }


                    //Raylib. ta bort rectangeln enemies[den som är klickad på]

                    Rectangle tmp = enemyList[i].rect;
                    tmp.y = -900;
                    enemyList[i].rect = tmp;
                    //enemies.Remove(enemies);
                }


            }

            //enemies1
            for (int i = 0; i < enemyList1.Count; i++)

            {
                Raylib.DrawRectangleRec(enemyList1[i].rect, Color.RED);

                Vector2 mousePos = Raylib.GetMousePosition();


                if (Raylib.CheckCollisionPointRec(mousePos, enemyList1[i].rect) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    score = score += 10;
                    //slumpad crit system 

                    int crit = generator.Next(0, 101);

                    if (crit >= 90)
                    {
                        score = score += 15;
                    }

                    Rectangle tmp = enemyList1[i].rect;
                    tmp.y = -900;
                    enemyList1[i].rect = tmp;
                    //enemies.Remove(enemies);
                }
            }

            //enemies2
            for (int i = 0; i < enemyList2.Count; i++)

            {
                Raylib.DrawRectangleRec(enemyList2[i].rect, Color.RED);

                Vector2 mousePos = Raylib.GetMousePosition();


                if (Raylib.CheckCollisionPointRec(mousePos, enemyList2[i].rect) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    score = score += 10;
                    //slumpad crit system 
                    //det är inte så tydlig kanske men den funkar när man spelar

                    int crit = generator.Next(0, 101);

                    if (crit >= 90)
                    {
                        score = score += 15;
                    }

                    Rectangle tmp = enemyList2[i].rect;
                    tmp.y = -900;
                    enemyList2[i].rect = tmp;
                }
            }

            enemyList.RemoveAll(enemy => enemy.rect.y < 0);
            enemyList1.RemoveAll(enemy => enemy.rect.y < 0);
            enemyList2.RemoveAll(enemy => enemy.rect.y < 0);

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
