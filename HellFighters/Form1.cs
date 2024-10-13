using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
using WMPLib;
using System.IO;
namespace HellFighters
{
    public partial class HellFighters : Form
    {
        Random rnd;

        PictureBox[] stars;
        int backgroundSpeed;

        List<PictureBox> playerBullets;
        List<PictureBox> enemyBullets;
        List<PictureBox> enemies;
        List<PictureBox> boosts;

        int playerBulletSpeed;
        int playerSpeed;

        int enemySpeed;
        int enemyBulletSpeed;
        int enemiesPerWave;

        bool playerShielded;

        // AUDIO DECLARE
        WindowsMediaPlayer playerShoot;
        WindowsMediaPlayer backMusic;
        WindowsMediaPlayer explosion;
        WindowsMediaPlayer enemyShoot;

        // BOOST SOUND EFFECTS
        WindowsMediaPlayer overchargePickup;
        WindowsMediaPlayer nukePickup;
        WindowsMediaPlayer expPickup;
        WindowsMediaPlayer shieldPickup;
        WindowsMediaPlayer shieldPoweringDown;
        
        int score;
        int highscore;

        public HellFighters()
        {
            InitializeComponent();    // IDK THIS WAS HERE WHEN I GOT HERE
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            highScoreLoad();   // LOADS HIGHSCORE OR CREATES NEW FILE

            //INITIALIZATION
            backgroundSpeed = 4;
            playerSpeed = 4;
            playerBulletSpeed = 4;
            rnd = new Random();
            stars = new PictureBox[25];
            playerBullets = new List<PictureBox>();
            enemies = new List<PictureBox>();
            enemyBullets = new List<PictureBox>();
            boosts = new List<PictureBox>();
            resetGameVariables();  // RESETS DIFFICULTY, SCORE, PLAYER BUFFS
            
            // INITIALIZATION = AUDIO
            playerShoot = new WindowsMediaPlayer();
            playerShoot.settings.volume = 5;

            enemyShoot = new WindowsMediaPlayer();
            enemyShoot.settings.volume = 5;

            explosion = new WindowsMediaPlayer();

            backMusic = new WindowsMediaPlayer();
            backMusic.URL = "audio\\Waterflame - Electroman Adventures.mp3";
            backMusic.settings.setMode("loop", true);
            backMusic.settings.volume = 5;

            overchargePickup = new WindowsMediaPlayer();
            shieldPoweringDown = new WindowsMediaPlayer();
            shieldPickup = new WindowsMediaPlayer();
            expPickup = new WindowsMediaPlayer();
            nukePickup = new WindowsMediaPlayer();

            for (int i = 0; i < stars.Length; i++)    // ADDS BACKGROUND STARS
            {
                stars[i] = new PictureBox();
                stars[i].BorderStyle = BorderStyle.None;
                stars[i].Location = new Point(rnd.Next(10, 790), rnd.Next(10, 590)); // RANDOMIZES STARTING LOCATIONS OF STARS
                if (i % 2 == 0)             // MAKES 2 DIFFERENT STAR SKINS
                {
                    stars[i].Size = new Size(2, 2);
                    stars[i].BackColor = Color.White;
                }
                else
                {
                    stars[i].Size = new Size(3, 3);
                    stars[i].BackColor = Color.DarkBlue;
                }
                this.Controls.Add(stars[i]);
            }
        }
        private void backgroundCaller_Tick(object sender, EventArgs e)   // MOVES BACKGROUND
        {
            for (int i = 0; i < stars.Length / 2; i++)   // FASTER STARS
            {
                stars[i].SendToBack();
                stars[i].Top += backgroundSpeed;  // MOVES STARS
                if (stars[i].Top >= this.Height)  // UPON REACHING BOTTOM, RETURN TO TOP IN A DIFFERENT PLACE
                {
                    stars[i].Location = new Point(rnd.Next(10, 790), rnd.Next(10, 590));  // IDK WHY
                    stars[i].Top = -stars[i].Height;
                }
            }
            for (int i = stars.Length / 2; i < stars.Length; i++)   // SLOWER STARS
            {
                stars[i].SendToBack();
                stars[i].Top += backgroundSpeed - 2;   // MOVES STARS SLOWER
                if (stars[i].Top >= this.Height)       // UPON REACHING BOTTOM, RETURN TO TOP IN A DIFFERENT PLACE
                {
                    stars[i].Location = new Point(rnd.Next(10, 790), rnd.Next(10, 590));
                    stars[i].Top = -stars[i].Height;
                }
            }
        }
        // KEY BINDINGS
        private void HellFighters_KeyDown(object sender, KeyEventArgs e)
        {
            if (playerFireBulletTimer.Enabled)  // DISABLES MOVEMENT IF GAME PAUSED
            {
                if ((e.KeyCode == Keys.W || e.KeyCode == Keys.Up))   // STARTS TIMERS WHEN RESPECTIVE BUTTONS ARE PRESSED
                    UP.Start();
                if ((e.KeyCode == Keys.S || e.KeyCode == Keys.Down))
                    DOWN.Start();
                if ((e.KeyCode == Keys.A || e.KeyCode == Keys.Left))
                    LEFT.Start();
                if ((e.KeyCode == Keys.D || e.KeyCode == Keys.Right))
                    RIGHT.Start();
                if (e.KeyCode == Keys.P)   // STOPS GAME
                {
                    gameRunning(false);
                    stopMovingTheDamnPlaneFunctionBecauseSpaghettiCode();
                }
            }
            else if (!playerFireBulletTimer.Enabled)  // IF PAUSED
            {
                if (e.KeyCode == Keys.P) // UNPAUSE
                {
                    gameRunning(true);
                }
            }
        }
        private void HellFighters_KeyUp(object sender, KeyEventArgs e) // STOP MOVING WHEN KEY UP
        {
            if ((e.KeyCode == Keys.W || e.KeyCode == Keys.Up))
                UP.Stop();
            if ((e.KeyCode == Keys.S || e.KeyCode == Keys.Down))
                DOWN.Stop();
            if ((e.KeyCode == Keys.A || e.KeyCode == Keys.Left))
                LEFT.Stop();
            if ((e.KeyCode == Keys.D || e.KeyCode == Keys.Right))
                RIGHT.Stop();
        }

        // MOVEMENT TIMERS FOR ALL DIRECTIONS
        private void UP_Tick(object sender, EventArgs e)
        {
            if (playerJet.Top > 10)  // TOP LIMIT
            {
                hitbox1.Top -= playerSpeed;  // MOVE PLAYER HITBOX TOGETHER WITH PLANE
                hitbox2.Top -= playerSpeed;
                playerJet.Top -= playerSpeed;
            }
        }
        private void DOWN_Tick(object sender, EventArgs e)
        {
            if (playerJet.Top < 500)
            {
                hitbox1.Top += playerSpeed;
                hitbox2.Top += playerSpeed;
                playerJet.Top += playerSpeed;
            }
        }
        private void LEFT_Tick(object sender, EventArgs e)
        {
            if (playerJet.Left > 10)
            {
                hitbox1.Left -= playerSpeed;
                hitbox2.Left -= playerSpeed;
                playerJet.Left -= playerSpeed;
            }
                
        }
        private void RIGHT_Tick(object sender, EventArgs e)
        {
            if (playerJet.Left < 720)
            {
                hitbox1.Left += playerSpeed;
                hitbox2.Left += playerSpeed;
                playerJet.Left += playerSpeed;
            }
        }

        // MOVERS, SPAWNERS
        private void playerFireBulletTimer_Tick(object sender, EventArgs e)     //CREATES BULLETS ON PLAYER'S LOCATION     //MAX TICK SPEED HANDLED BY AUDIO = 100
        {
            Image Bullets = Image.FromFile(@"assets\playerBullet.png");
            PictureBox newBullet = new PictureBox();
            newBullet.Size = new Size(8, 24);
            newBullet.Image = Bullets;                                          // IMAGE SETTINGS
            newBullet.SizeMode = PictureBoxSizeMode.Zoom;
            newBullet.BorderStyle = BorderStyle.None;
            newBullet.Location = new Point(playerJet.Location.X + 26, playerJet.Location.Y);

            playerBullets.Add(newBullet);
            this.Controls.Add(newBullet);
            playerShoot.URL = "audio\\Blaster sound effect.mp3";
            playerShoot.controls.play();
        }
        private void BulletFlightTimer_Tick(object sender, EventArgs e) // MOVES ALL PROJECTILES
        {
            for (int i = playerBullets.Count - 1; i >= 0; i--)  // HANDLES PLAYER BULLETS
            {
                PictureBox bullet = playerBullets[i];
                if (bullet.Top > 0)    // MOVE UP UNTIL TOP IS REACHED
                {
                    bullet.Top -= playerBulletSpeed;
                }
                else
                {
                    this.Controls.Remove(bullet);  // REMOVES BULLETS AT THE TOP
                    playerBullets.Remove(bullet);
                }
                collision(bullet, 6);  // CHECKS FOR COLLISION WITH ENEMIES
            }
            for (int j = 0; j < enemyBullets.Count; j++)    // HANDLES ENEMY BULLETS
            {
                PictureBox enemyBullet = enemyBullets[j];
                if (enemyBullet.Top < this.Height)
                {
                    enemyBullet.Top += enemyBulletSpeed;
                }
                else
                {
                    this.Controls.Remove(enemyBullet);
                    enemyBullets.Remove(enemyBullet);
                }
                otherCollisionBecauseIAmLazy();  // CHECKS FOR COLLISION WITH PLAYER
            }
        }
        private void enemyMove_Tick(object sender, EventArgs e) // MANAGES MOVEMENT BY ENEMIES AND BOOSTS 
        {
            enemyMover(enemies, enemySpeed);
            collision(playerJet, 30);  // CHECKS FOR COLLISION BETHWEEN ENEMIES AND PLAYER
            boostMove();
            boostCollision();  // CHECKS FOR COLLISION BETHWEEN BOOSTS AND PLAYER
        }
        private void enemyMover(List<PictureBox> array, int enemyspeed)
        {
            for (int i = 0; i < array.Count; i++)
            {
                array[i].Top += enemyspeed;
                if (array[i].Top > this.Height)
                {
                    this.Controls.Remove(array[i]);
                    enemies.Remove(array[i]);
                }
            }
        }
        private void enemySpawner_Tick(object sender, EventArgs e) // SPAWNS WAVES OF ENEMIES
        {
            Image enemy1 = Image.FromFile(@"assets\enemy1.png");    // THREE DIFFERENT SKIN OPTIONS FOR ENEMIES
            Image enemy2 = Image.FromFile(@"assets\enemy2.png");
            Image enemy3 = Image.FromFile(@"assets\enemy3.png");
            bool[] enemyPositions = new bool[15];  // MAX 15 ENEMIES FIT IN THE SAME WAVE
            for (int i = 0; i < enemiesPerWave; i++)   // FOR EACH WAVE SELECTS POSITIONS FOR ENEMIES 
            {
                int coordinates;
                do
                {
                    coordinates = rnd.Next(0, 15);  // RANDOMLY!!
                }
                while (enemyPositions[coordinates]);
                enemyPositions[coordinates] = true;
            }
            for (int j = 0; j < 15; j++)  // FOR EACH SPOT IN WAVE, SPAWN ENEMY IF SELECTED 
            {
                if (enemyPositions[j])
                {
                    PictureBox enemy = new PictureBox();
                    int skinselect = rnd.Next(1, 4);    // RANDOMIZES SKIN 
                    switch (skinselect)
                    {
                        case 1:
                            enemy.Image = enemy1;
                            break;
                        case 2:
                            enemy.Image = enemy2;
                            break;
                        case 3:
                            enemy.Image = enemy3;
                            break;
                    }
                    enemy.Size = new Size(35, 35);
                    enemy.SizeMode = PictureBoxSizeMode.Zoom;
                    enemy.BorderStyle = BorderStyle.None;
                    enemy.Visible = true;                                   // IMAGE SETTINGS
                    enemy.Location = new Point((j * 50) + 15, -20);
                    enemies.Add(enemy);
                    this.Controls.Add(enemy);
                    enemy.BringToFront();
                }
            }
        }
        private void spawnBoost(int j)  // SPAWN BOOST DROP UPON KILLING AN ENEMY
        {
            int randomness = rnd.Next(1, 101);   // RANDOM CHANCE TO SPAWN BOOST
            if (randomness > 0 && randomness < 12)
            {
                PictureBox boost = new PictureBox();
                if (randomness > 0 && randomness < 3)   // SHIELD 2 %
                {
                    boost.Image = Image.FromFile(@"assets\shield.png");
                    boost.Name = "shield";
                }
                if (randomness > 2 && randomness < 6)
                {
                    boost.Image = Image.FromFile(@"assets\overcharge.png"); // OVERCHARGE 3 %
                    boost.Name = "overcharge";
                }
                if (randomness > 5 && randomness < 8)
                {
                    boost.Image = Image.FromFile(@"assets\nuke.png");  // NUKE 2 %
                    boost.Name = "nuke";
                }
                if (randomness > 7 && randomness < 12)
                {
                    boost.Image = Image.FromFile(@"assets\exp.png");  // EXP 4 %
                    boost.Name = "exp";
                }
                boost.Size = new Size(20, 20);
                boost.SizeMode = PictureBoxSizeMode.Zoom;                 // IMAGE SETTINGS
                boost.BorderStyle = BorderStyle.None;
                boost.Visible = true;
                boost.Location = new Point(enemies[j].Location.X, enemies[j].Location.Y); // SPAWNS THE BOOST ON LOCATION OF THE KILLED ENEMY
                boosts.Add(boost);
                this.Controls.Add(boost);
                boost.BringToFront();
            }
        }
        private void boostMove()  // MOVES BOOSTS UNTIL REACHING BOTTOM OR PICKED UP
        {
            for (int i = 0; i < boosts.Count; i++)
            {
                boosts[i].Top += enemySpeed;
                if (boosts[i].Top > this.Height)
                {
                    this.Controls.Remove(boosts[i]);
                    enemies.Remove(boosts[i]);
                }
            }
        }
        private void enemyFireBulletTimer_Tick(object sender, EventArgs e)  // MAKES ENEMIES FIRE 
        {
            if (enemies.Count > 0)  // IF THERE ARE ANY ENEMIES, DO STUFF
            {
                Image Bullets = Image.FromFile(@"assets\enemyBullet.png");
                PictureBox enemyBullet = new PictureBox();
                enemyBullet.Size = new Size(6, 24);
                enemyBullet.Image = Bullets;                                   // CREATE BULLET, IMAGE SETTINGS
                enemyBullet.SizeMode = PictureBoxSizeMode.Zoom;
                enemyBullet.BorderStyle = BorderStyle.None;
                int enemyFireSelector;
                int attempts = 0;
                do    // ATTEMPTS TO ASSIGN THE BULLET TO AN ENEMY
                {
                    enemyFireSelector = rnd.Next(0, enemies.Count);
                    attempts++;
                    if (attempts > enemies.Count * 2) break;
                }
                while (enemies[enemyFireSelector].Top > this.Height / 3); // ONLY ASSIGN TO ENEMIES IN THE TOP THIRD OF SCREEN

                enemyBullet.Location = new Point(enemies[enemyFireSelector].Location.X + 14, enemies[enemyFireSelector].Location.Y + 10); //PLACES THE BULLET AT ENEMY LOCATION
                enemyBullets.Add(enemyBullet);
                this.Controls.Add(enemyBullet);
                enemyShoot.URL = "audio\\enemy blaster.mp3";
                enemyShoot.controls.play();
            }

        }
        private void stopMovingTheDamnPlaneFunctionBecauseSpaghettiCode()  // STOPS PLANE FROM MOVING WHEN PAUSED/GAME ENDS WHILE PLAYER IS STILL HOLDING MOVEMENT KEYS
        {
            UP.Stop();
            DOWN.Stop();
            LEFT.Stop();
            RIGHT.Stop();
        }

        // COLLISION HANDLERS
        private void collision(PictureBox entity, int volume) // COLLISIONS PLAYER/ENEMY OR BULLETS/ENEMY
        {
            for (int j = 0; j < enemies.Count; j++)  // FOR EACH ENEMY DO STUFF
                if (entity == playerJet)  // IF WE NEED PLAYER/ENEMY COLLISION HANDLER
                {   // CHECKS IF PLAYER HITBOX COLLIDED WITH VISIBLE ENEMIES
                    if (playerJet.Bounds.IntersectsWith(enemies[j].Bounds))
                        enemies[j].BringToFront();
                    if ((hitbox1.Bounds.IntersectsWith(enemies[j].Bounds) && enemies[j].Visible) || (hitbox2.Bounds.IntersectsWith(enemies[j].Bounds) && enemies[j].Visible))
                    {
                        score = int.Parse(scoreCounter.Text);  // INCREASES SCORE FOR DESTROYED ENEMY
                        score++;
                        scoreCounter.Text = score.ToString();
                        if (playerShielded)  // IF SHIELD IS ACTIVE, REMOVE IT
                        {
                            playerShielded = false;
                            playerJet.Image = Image.FromFile(@"assets\player jet.png");
                            shieldPoweringDown.settings.volume = 15;
                            shieldPoweringDown.URL = "audio\\shieldPoweringDown.mp3";
                            shieldPoweringDown.controls.play();
                        }
                        else   // IF IT ISN'T, GAME OVER
                        {
                            explosion.URL = "audio\\explosion.mp3";
                            playerJet.Visible = false;
                            this.Controls.Remove(playerJet);
                            explosion.settings.volume = 20;
                            explosion.controls.play();
                            if (playerJet.Visible == false)  // LOSE GAME CONDITION
                                GameOver();
                        }
                        enemies[j].Visible = false;
                        this.Controls.Remove(enemies[j]);  // DESTROYS THE ENEMY
                        enemies.Remove(enemies[j]);
                        spawnBoost(j); // GIVES CHANCE TO SPAWN BOOST
                    }
                }
                else if (entity.Bounds.IntersectsWith(enemies[j].Bounds) && enemies[j].Visible)  // FOR BULLET/ENEMY COLLISIONS
                {
                    explosion.URL = "audio\\explosion.mp3";
                    enemies[j].Visible = false;
                    entity.Visible = false;
                    spawnBoost(j);                          // GIVES CHANCE TO SPAWN BOOST
                    this.Controls.Remove(entity);
                    this.Controls.Remove(enemies[j]);
                    enemies.Remove(enemies[j]);                   // REMOVE ENEMY, REMOVE BULLET, REMOVE SANITY
                    playerBullets.Remove(entity);
                    explosion.settings.volume = volume;
                    explosion.controls.play();
                    score = int.Parse(scoreCounter.Text);          // INCREASES SCORE
                    score++;
                    scoreCounter.Text = score.ToString();
                    if (playerJet.Visible == false)    // LOSE GAME CONDITION
                        GameOver();
                }
        }
        private void otherCollisionBecauseIAmLazy()  // HANDLES COLLISIONS PLAYER/ENEMY BULLETS
        {

            for (int j = 0; j < enemyBullets.Count; j++) // FOR EACH ENEMY BULLET
            {
                if (playerJet.Bounds.IntersectsWith(enemyBullets[j].Bounds))  // MAKES THE BULLET STAY VISIBLE UPON ENTERING PLAYER'S PICTUREBOX
                    enemyBullets[j].BringToFront();

                if ((hitbox1.Bounds.IntersectsWith(enemyBullets[j].Bounds) || hitbox2.Bounds.IntersectsWith(enemyBullets[j].Bounds)) && playerJet.Visible)  // CHECKS COLLISION
                {
                    if (playerShielded)     // IF SHIELDED, REMOVE SHIELD
                    {
                        playerShielded = false;
                        playerJet.Image = Image.FromFile(@"assets\player jet.png");
                        shieldPoweringDown.settings.volume = 15;
                        shieldPoweringDown.URL = "audio\\shieldPoweringDown.mp3";
                        shieldPoweringDown.controls.play();
                    }
                    else
                    {
                        explosion.URL = "audio\\explosion.mp3";  // IF NOT SHIELDED, GAME OVER
                        playerJet.Visible = false;
                        this.Controls.Remove(playerJet);
                        explosion.settings.volume = 20;
                        explosion.controls.play();
                        if (playerJet.Visible == false)   // LOSE GAME CONDITION
                            GameOver();
                    }
                    enemyBullets[j].Visible = false;
                    this.Controls.Remove(enemyBullets[j]);  // REMOVE ENEMY BULLET IF IT COLLIDED
                    enemyBullets.Remove(enemyBullets[j]);
                }
            }
        }
        private void boostCollision()  // HANDLES COLLISION BOOST/PLAYER
        {
            for (int j = 0; j < boosts.Count; j++)  // FOR EACH BOOST
            {
                if (playerJet.Bounds.IntersectsWith(boosts[j].Bounds)) // MAKES THE BOOST STAY VISIBLE UPON ENTERING PLAYER'S PICTUREBOX
                    boosts[j].BringToFront();

                if ((hitbox1.Bounds.IntersectsWith(boosts[j].Bounds) || hitbox2.Bounds.IntersectsWith(boosts[j].Bounds)) && playerJet.Visible) // CHECKS COLLISION
                {
                    boosts[j].Visible = false;
                    switch (boosts[j].Name)  // DOES STUFF BASED ON WHICH BOOST WAS COLLECTED
                    {
                        case "overcharge":
                            playerFireBulletTimer.Interval = playerFireBulletTimer.Interval - playerFireBulletTimer.Interval / 10;  // COLLECTED OVERCHARGE
                            overchargePickup.settings.volume = 10;
                            overchargePickup.URL = "audio\\overchargePickup.mp3";
                            overchargePickup.controls.play();
                            boostInfo("FIRE RATE INCREASED");  // SHOWS BOOST PICKUP MESSAGE ON THE RIGHT BOTTOM
                            break;

                        case "shield":
                            playerShielded = true;
                            playerJet.Image = Image.FromFile(@"assets\player jet shielded.png");  // COLLECTED SHIELD
                            shieldPickup.settings.volume = 20;
                            shieldPickup.URL = "audio\\shieldPickup.mp3";
                            shieldPickup.controls.play();
                            boostInfo("      SHIELD ACTIVATED");    // SHOWS BOOST PICKUP MESSAGE ON THE RIGHT BOTTOM
                            break;
                        case "nuke":
                            score = score + enemies.Count / 2;
                            scoreCounter.Text = score.ToString();     // COLLECTED NUKE
                            clearStuff(enemies);
                            nukePickup.settings.volume = 10;
                            nukePickup.URL = "audio\\nukePickup.mp3";
                            nukePickup.controls.play();
                            boostInfo("        NUKE DETONATED");    // SHOWS BOOST PICKUP MESSAGE ON THE RIGHT BOTTOM
                            break;
                        case "exp":
                            score = score + 5;
                            scoreCounter.Text = score.ToString();   // COLLECTED EXP
                            expPickup.settings.volume = 10;
                            expPickup.URL = "audio\\expPickup.mp3";
                            expPickup.controls.play();
                            boostInfo("                      SCORE + 5");        // SHOWS BOOST PICKUP MESSAGE ON THE RIGHT BOTTOM   // VYMYSLETE NEKDO PRO LASKU BOZI NECO LEPSIHO
                            break;
                    }
                    this.Controls.Remove(boosts[j]);  // REMOVES BOOST BOX AFTER COLLECTED
                    boosts.Remove(boosts[j]);
                }
            }
        }

        // GAMESTATE FUNCTIONS/ BUTTON CLICKS
        private void startGame_Click(object sender, EventArgs e) // REMOVES MENU BUTTONS/LABELS, RESETS GAME VARIABLES, STARTS TIMERS
        {
            resetGameVariables();
            scoreCounter.Text = score.ToString();
            unaliveButton(startGame, false);
            unaliveLabel(gameTitle, false);
            unaliveLabel(scoreTextLabel, true);
            unaliveLabel(scoreCounter, true);
            unaliveLabel(highScoreLabel, false);
            StartAll();
        }
        private void returnToMenu_Click(object sender, EventArgs e)  // RESETS THE GAME
        {
            unaliveLabel(gameOver, false);
            unaliveLabel(finalScore, false);
            unaliveLabel(gamePaused, false);
            unaliveLabel(gameTitle, true);            // REMOVES ENDGAME/PAUSE LABELS/BUTTONS
            unaliveLabel(scoreCounter, false);
            unaliveLabel(scoreTextLabel, false);
            unaliveButton(startGame, true);
            unaliveButton(continueGame, false);
            unaliveButton(returnToMenu, false);
            clearStuff(enemies);
            clearStuff(playerBullets);              // CLEARS ALL ENTITIES
            clearStuff(enemyBullets);
            clearStuff(boosts);
            this.Controls.Add(playerJet);
            playerJet.Location = new Point(365, 400);
            hitbox1.Location = new Point(388, 402);    // RETURNS PLAYER AND HIS HITBOX TO DEFAULT POSITIONS
            hitbox2.Location = new Point(370, 424);
            playerJet.Visible = true;
            highScoreSave(score);     // IF HIGHSCORE IS REACHED, SAVE IT
            highScoreLoad();            // LOADS THE HIGHSCORE AND SHOWS IT IN TEXTBOX
            resetGameVariables();    // RESETS GAME VARIABLES (DAMN)
            stopMovingTheDamnPlaneFunctionBecauseSpaghettiCode();     //NO COMMENT COMMENT
        }
        private void GameOver()  // LOSE GAME HANDLER
        {
            StopAll();  // STOPS ALL TIMERS
            unaliveLabel(gameOver, true);
            unaliveButton(returnToMenu, true);
            unaliveLabel(finalScore, true);             // SHOWS ENDGAME LABELS AND BUTTONS WHILE REMOVING GAME LABELS
            unaliveLabel(scoreCounter, false);
            unaliveLabel(scoreTextLabel, false);
            finalScore.Text = "FINAL SCORE: " + scoreCounter.Text;  // SHOWS ACHIEVED SCORE
        }
        private void continueGame_Click(object sender, EventArgs e) // RESUMES GAME AFTER PAUSING 
        {
            gameRunning(true);
        }
        private void gameRunning(bool start)  // PAUSES THE GAME IF FALSE, STARTS IT IF TRUE
        {
            if (!start)
            {
                gamePaused.Visible = true;
                continueGame.Visible = true;
                returnToMenu.Visible = true;              // SHOW PAUSE BUTTONS/LABELS
                this.Controls.Add(gamePaused);
                this.Controls.Add(continueGame);
                this.Controls.Add(returnToMenu);
                continueGame.BringToFront();
                returnToMenu.BringToFront();
                gamePaused.BringToFront();
                StopAll();
            }
            else
            {
                gamePaused.Visible = false;
                continueGame.Visible = false;
                returnToMenu.Visible = false;    // HIDES SHOW PAUSE BUTTONS/LABELS
                this.Controls.Remove(gamePaused);
                this.Controls.Remove(continueGame);
                this.Controls.Remove(returnToMenu);
                StartAll();
                this.Focus();   // NEEDED IF BUTTONS ARE PRESSED WITH USE OF ARROW KEYS AND ENTER
            }
        }

        // SAVE SPACE FUNCTIONS
        private void clearStuff(List<PictureBox> entity) // CLEARS THE SELECTED ENTITY TYPE
        {
            for (int i = entity.Count - 1; i >= 0; i--)
            {
                entity[i].Visible = false;
                this.Controls.Remove(entity[i]);
                entity.Remove(entity[i]);
            }
        }
        private void StopAll() // STOPS ALL RELEVANT TIMERS
        {
            enemySpawner.Stop();
            enemyMove.Stop();
            enemyFireBulletTimer.Stop();
            playerFireBulletTimer.Stop();
            BulletFlightTimer.Stop();
            enemyFireBulletTimer.Stop();
        }
        private void StartAll() // STARTS ALL RELEVANT TIMERS
        {
            enemySpawner.Start();
            enemyMove.Start();
            enemyFireBulletTimer.Start();
            playerFireBulletTimer.Start();
            BulletFlightTimer.Start();
            enemyFireBulletTimer.Start();
        }
        private void unaliveLabel(System.Windows.Forms.Label labelName, bool reverseFunction)  // REMOVES/ADDS LABEL
        {
            if (!reverseFunction)  // REMOVE LABEL
            {
                this.Controls.Remove(labelName);
                labelName.Visible = false;
            }
            else
            {
                this.Controls.Add(labelName);  // ADD LABEL
                labelName.Visible = true;
                labelName.BringToFront();
            }
        }
        private void unaliveButton(System.Windows.Forms.Button buttonName, bool reverseFunction)  // REMOVES/ADDS BUTTON
        {
            if (!reverseFunction)   // REMOVE 
            {
                this.Controls.Remove(buttonName);
                buttonName.Visible = false;
            }
            else    //ADD
            {
                this.Controls.Add(buttonName);
                buttonName.Visible = true;
                buttonName.BringToFront();
            }
        }
        // OTHER FUNCTIONS
        private void highScoreSave(int score) // SAVE SCORE IF HIGHER THAN CURRENT HIGHSCORE
        {
            int highScore;
            using (StreamReader reader = new StreamReader("Hellfighters_Highscore.txt"))  // READ CURRENT HIGHSCORE
            {
                highScore = int.Parse(reader.ReadLine());
            }
            if (score > highScore)  // IF CURRENT SCORE IS HIGHER, REPLACE OLD SCORE (NETUŠÍM CO DÌLÁM)
            {
                File.Delete("Hellfighters_Highscore.txt");
                using (FileStream idk = File.Create("Hellfighters_Highscore.txt"))
                { }
                using (StreamWriter writer = new StreamWriter("Hellfighters_Highscore.txt"))
                {
                    writer.WriteLine(score.ToString());
                }
            }
        }
        private void highScoreLoad()  // LOADS HIGH SCORE
        {
            if (!File.Exists("Hellfighters_Highscore.txt"))  // CREATE HIGHSCORE FILE WITH VALUE 0 IF IT DOESN'T EXIST
            {
                using (FileStream idk = File.Create("Hellfighters_Highscore.txt"))
                { }
                using (StreamWriter writer = new StreamWriter("Hellfighters_Highscore.txt"))
                {
                    writer.WriteLine("0");
                }
                unaliveLabel(highScoreLabel, false);  // REMOVES THE HIGHSCORE LABEL IF THERE ISN'T ONE
            }
            else
            {
                using (StreamReader reader = new StreamReader("Hellfighters_Highscore.txt"))  // LOADS HIGHSCORE INTO IT'S LABEL
                {
                    unaliveLabel(highScoreLabel, true);
                    highscore = int.Parse(reader.ReadLine());
                    highScoreLabel.Text = "HIGH SCORE: " + highscore;
                }
            }
        }
        private void scoreCounter_TextChanged(object sender, EventArgs e)  // INCREASES DIFFICULTY UPON REACHING CERTAIN SCORES
        {
            if (int.Parse(scoreCounter.Text) % 100 == 0 && score != 0)  // EACH 100 SCORE INCREASE ENEMY BULLET SPEED, MOVE SPEED, ENEMIES FIRE FASTER
            {
                enemyBulletSpeed++;
                if (int.Parse(scoreCounter.Text) % 200 == 0 && enemiesPerWave < 7) // EACH 200 SCORE INCREASE THE NUMBER OF ENEMIES IN EACH WAVE UP TO 6
                    enemiesPerWave++;

                if (enemyMove.Interval > 70)                         // EACH 100 SCORE INCREASE THE SPEED OF ENEMIES
                    enemyMove.Interval = enemyMove.Interval - 10;
                else if (enemyMove.Interval > 40)
                    enemyMove.Interval = enemyMove.Interval - 7;    // INCREASE IN SPEED GRADUALLY LESS SIGNIFICANT (ANO DANE, ŠLO BY TO JINAK,STEJNÌ TÌ MÁM RÁD)
                else if (enemyMove.Interval > 30)
                    enemyMove.Interval = enemyMove.Interval - 5;


                if (enemyFireBulletTimer.Interval > 600)
                    enemyFireBulletTimer.Interval = enemyFireBulletTimer.Interval - 80;  // ENEMIES FIRE MORE OFTEN, GRADUALLY LESS SIGNIFICANT

                else if (enemyFireBulletTimer.Interval > 400)
                    enemyFireBulletTimer.Interval = enemyFireBulletTimer.Interval - 50;

                else if (enemyFireBulletTimer.Interval > 200)
                    enemyFireBulletTimer.Interval = enemyFireBulletTimer.Interval - 25;
            }
        }
        private void resetGameVariables()  // RESETS DIFFICULTY, SCORE, BOOST EFFECTS
        {
            score = 0;
            enemyBulletSpeed = 3;
            enemySpeed = 4;
            enemiesPerWave = 4;
            playerShielded = false;
            playerFireBulletTimer.Interval = 400;
            playerJet.Image = Image.FromFile(@"assets\player jet.png");
        }
        private void boostInfo(string message)   // SHOWS BOOST INFO AFTER COLLECTING
        {
            boostNotification.Text = message;
            boostNotification.Visible = true;
            playerJet.BringToFront();
            boostInfoTimer.Start();   // STARTS TIMER WHICH REMOVES THE INFO AFTER 3 SECONDS
        }
        private void boostInfoTimer_Tick(object sender, EventArgs e)  // REMOVE BOOST INFO AFTER 3 SECS
        {
            boostNotification.Visible = false;
            boostInfoTimer.Stop();
        }

    }
}

// BUGS
// 0 INCIDENTS TODAY, CONGRATS (DAN NÁM TO ZKAZÍ)
