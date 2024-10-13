namespace HellFighters
{
    partial class HellFighters
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HellFighters));
            backgroundCaller = new System.Windows.Forms.Timer(components);
            playerJet = new PictureBox();
            UP = new System.Windows.Forms.Timer(components);
            DOWN = new System.Windows.Forms.Timer(components);
            LEFT = new System.Windows.Forms.Timer(components);
            RIGHT = new System.Windows.Forms.Timer(components);
            playerFireBulletTimer = new System.Windows.Forms.Timer(components);
            BulletFlightTimer = new System.Windows.Forms.Timer(components);
            enemyMove = new System.Windows.Forms.Timer(components);
            enemySpawner = new System.Windows.Forms.Timer(components);
            enemyFireBulletTimer = new System.Windows.Forms.Timer(components);
            scoreTextLabel = new Label();
            scoreCounter = new Label();
            gameTitle = new Label();
            startGame = new Button();
            gamePaused = new Label();
            gameOver = new Label();
            returnToMenu = new Button();
            finalScore = new Label();
            continueGame = new Button();
            boostNotification = new Label();
            boostInfoTimer = new System.Windows.Forms.Timer(components);
            highScoreLabel = new Label();
            hitbox1 = new PictureBox();
            hitbox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)playerJet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hitbox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hitbox2).BeginInit();
            SuspendLayout();
            // 
            // backgroundCaller
            // 
            backgroundCaller.Enabled = true;
            backgroundCaller.Tick += backgroundCaller_Tick;
            // 
            // playerJet
            // 
            playerJet.BackColor = Color.Transparent;
            playerJet.Image = (Image)resources.GetObject("playerJet.Image");
            playerJet.Location = new Point(365, 400);
            playerJet.Name = "playerJet";
            playerJet.Size = new Size(60, 60);
            playerJet.SizeMode = PictureBoxSizeMode.Zoom;
            playerJet.TabIndex = 0;
            playerJet.TabStop = false;
            // 
            // UP
            // 
            UP.Interval = 5;
            UP.Tick += UP_Tick;
            // 
            // DOWN
            // 
            DOWN.Interval = 5;
            DOWN.Tick += DOWN_Tick;
            // 
            // LEFT
            // 
            LEFT.Interval = 5;
            LEFT.Tick += LEFT_Tick;
            // 
            // RIGHT
            // 
            RIGHT.Interval = 5;
            RIGHT.Tick += RIGHT_Tick;
            // 
            // playerFireBulletTimer
            // 
            playerFireBulletTimer.Interval = 400;
            playerFireBulletTimer.Tick += playerFireBulletTimer_Tick;
            // 
            // BulletFlightTimer
            // 
            BulletFlightTimer.Interval = 15;
            BulletFlightTimer.Tick += BulletFlightTimer_Tick;
            // 
            // enemyMove
            // 
            enemyMove.Tick += enemyMove_Tick;
            // 
            // enemySpawner
            // 
            enemySpawner.Interval = 1800;
            enemySpawner.Tick += enemySpawner_Tick;
            // 
            // enemyFireBulletTimer
            // 
            enemyFireBulletTimer.Interval = 1000;
            enemyFireBulletTimer.Tick += enemyFireBulletTimer_Tick;
            // 
            // scoreTextLabel
            // 
            scoreTextLabel.AutoSize = true;
            scoreTextLabel.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            scoreTextLabel.Location = new Point(732, 5);
            scoreTextLabel.Name = "scoreTextLabel";
            scoreTextLabel.Size = new Size(51, 20);
            scoreTextLabel.TabIndex = 2;
            scoreTextLabel.Text = "SCORE";
            scoreTextLabel.Visible = false;
            // 
            // scoreCounter
            // 
            scoreCounter.AutoSize = true;
            scoreCounter.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            scoreCounter.Location = new Point(754, 25);
            scoreCounter.Name = "scoreCounter";
            scoreCounter.RightToLeft = RightToLeft.Yes;
            scoreCounter.Size = new Size(18, 20);
            scoreCounter.TabIndex = 3;
            scoreCounter.Text = "0";
            scoreCounter.Visible = false;
            scoreCounter.TextChanged += scoreCounter_TextChanged;
            // 
            // gameTitle
            // 
            gameTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gameTitle.AutoSize = true;
            gameTitle.Font = new Font("Impact", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            gameTitle.ForeColor = Color.Firebrick;
            gameTitle.Location = new Point(284, 90);
            gameTitle.Name = "gameTitle";
            gameTitle.Size = new Size(222, 45);
            gameTitle.TabIndex = 4;
            gameTitle.Text = "HELLFIGHTERS";
            // 
            // startGame
            // 
            startGame.BackColor = Color.Transparent;
            startGame.FlatStyle = FlatStyle.Flat;
            startGame.Font = new Font("Impact", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            startGame.ForeColor = Color.Gold;
            startGame.Location = new Point(335, 170);
            startGame.Name = "startGame";
            startGame.Size = new Size(110, 47);
            startGame.TabIndex = 0;
            startGame.TabStop = false;
            startGame.Text = "START";
            startGame.UseVisualStyleBackColor = false;
            startGame.Click += startGame_Click;
            // 
            // gamePaused
            // 
            gamePaused.AutoSize = true;
            gamePaused.Font = new Font("Impact", 36F, FontStyle.Regular, GraphicsUnit.Point, 238);
            gamePaused.Location = new Point(254, 157);
            gamePaused.Name = "gamePaused";
            gamePaused.Size = new Size(282, 60);
            gamePaused.TabIndex = 6;
            gamePaused.Text = "GAME PAUSED";
            gamePaused.Visible = false;
            // 
            // gameOver
            // 
            gameOver.AutoSize = true;
            gameOver.Font = new Font("Impact", 36F, FontStyle.Regular, GraphicsUnit.Point, 238);
            gameOver.ForeColor = Color.Firebrick;
            gameOver.Location = new Point(278, 211);
            gameOver.Name = "gameOver";
            gameOver.Size = new Size(233, 60);
            gameOver.TabIndex = 7;
            gameOver.Text = "GAME OVER";
            gameOver.Visible = false;
            // 
            // returnToMenu
            // 
            returnToMenu.FlatStyle = FlatStyle.Flat;
            returnToMenu.Font = new Font("Impact", 24F, FontStyle.Regular, GraphicsUnit.Point, 238);
            returnToMenu.ForeColor = Color.Gold;
            returnToMenu.Location = new Point(266, 303);
            returnToMenu.Name = "returnToMenu";
            returnToMenu.Size = new Size(257, 47);
            returnToMenu.TabIndex = 0;
            returnToMenu.TabStop = false;
            returnToMenu.Text = "RETURN TO MENU";
            returnToMenu.UseVisualStyleBackColor = true;
            returnToMenu.Visible = false;
            returnToMenu.Click += returnToMenu_Click;
            // 
            // finalScore
            // 
            finalScore.AutoSize = true;
            finalScore.Font = new Font("Impact", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            finalScore.ForeColor = Color.Firebrick;
            finalScore.Location = new Point(320, 263);
            finalScore.Name = "finalScore";
            finalScore.Size = new Size(134, 29);
            finalScore.TabIndex = 9;
            finalScore.Text = "FINAL SCORE:";
            finalScore.Visible = false;
            // 
            // continueGame
            // 
            continueGame.FlatStyle = FlatStyle.Flat;
            continueGame.Font = new Font("Impact", 24F, FontStyle.Regular, GraphicsUnit.Point, 238);
            continueGame.ForeColor = Color.ForestGreen;
            continueGame.Location = new Point(266, 248);
            continueGame.Name = "continueGame";
            continueGame.Size = new Size(257, 49);
            continueGame.TabIndex = 0;
            continueGame.TabStop = false;
            continueGame.Text = "CONTINUE GAME";
            continueGame.UseVisualStyleBackColor = true;
            continueGame.Visible = false;
            continueGame.Click += continueGame_Click;
            // 
            // boostNotification
            // 
            boostNotification.AutoSize = true;
            boostNotification.Font = new Font("Impact", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            boostNotification.ForeColor = Color.ForestGreen;
            boostNotification.Location = new Point(600, 526);
            boostNotification.Name = "boostNotification";
            boostNotification.RightToLeft = RightToLeft.No;
            boostNotification.Size = new Size(63, 26);
            boostNotification.TabIndex = 10;
            boostNotification.Text = "label1";
            boostNotification.TextAlign = ContentAlignment.MiddleCenter;
            boostNotification.Visible = false;
            // 
            // boostInfoTimer
            // 
            boostInfoTimer.Interval = 3000;
            boostInfoTimer.Tick += boostInfoTimer_Tick;
            // 
            // highScoreLabel
            // 
            highScoreLabel.AutoSize = true;
            highScoreLabel.Font = new Font("Impact", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            highScoreLabel.ForeColor = Color.ForestGreen;
            highScoreLabel.Location = new Point(322, 134);
            highScoreLabel.Name = "highScoreLabel";
            highScoreLabel.Size = new Size(103, 23);
            highScoreLabel.TabIndex = 11;
            highScoreLabel.Text = "HIGH SCORE:";
            // 
            // hitbox1
            // 
            hitbox1.BackColor = Color.Transparent;
            hitbox1.Location = new Point(388, 402);
            hitbox1.Name = "hitbox1";
            hitbox1.Size = new Size(13, 54);
            hitbox1.TabIndex = 12;
            hitbox1.TabStop = false;
            hitbox1.Visible = false;
            // 
            // hitbox2
            // 
            hitbox2.BackColor = Color.Transparent;
            hitbox2.Location = new Point(370, 424);
            hitbox2.Name = "hitbox2";
            hitbox2.Size = new Size(50, 32);
            hitbox2.TabIndex = 13;
            hitbox2.TabStop = false;
            hitbox2.Visible = false;
            // 
            // HellFighters
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(784, 561);
            Controls.Add(hitbox2);
            Controls.Add(hitbox1);
            Controls.Add(highScoreLabel);
            Controls.Add(boostNotification);
            Controls.Add(continueGame);
            Controls.Add(finalScore);
            Controls.Add(returnToMenu);
            Controls.Add(gameOver);
            Controls.Add(gamePaused);
            Controls.Add(startGame);
            Controls.Add(gameTitle);
            Controls.Add(scoreCounter);
            Controls.Add(scoreTextLabel);
            Controls.Add(playerJet);
            ForeColor = SystemColors.HotTrack;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HellFighters";
            Text = "HellFighters";
            Load += Form1_Load;
            KeyDown += HellFighters_KeyDown;
            KeyUp += HellFighters_KeyUp;
            ((System.ComponentModel.ISupportInitialize)playerJet).EndInit();
            ((System.ComponentModel.ISupportInitialize)hitbox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)hitbox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer backgroundCaller;
        private PictureBox playerJet;
        private System.Windows.Forms.Timer UP;
        private System.Windows.Forms.Timer DOWN;
        private System.Windows.Forms.Timer LEFT;
        private System.Windows.Forms.Timer RIGHT;
        private System.Windows.Forms.Timer playerFireBulletTimer;
        private System.Windows.Forms.Timer BulletFlightTimer;
        private System.Windows.Forms.Timer enemyMove;
        private System.Windows.Forms.Timer enemySpawner;
        private System.Windows.Forms.Timer enemyFireBulletTimer;
        private Label scoreTextLabel;
        private Label scoreCounter;
        private Label gameTitle;
        private Button startGame;
        private Label gamePaused;
        private Label gameOver;
        private Button returnToMenu;
        private Label finalScore;
        private Button continueGame;
        private Label boostNotification;
        private System.Windows.Forms.Timer boostInfoTimer;
        private Label highScoreLabel;
        private PictureBox hitbox1;
        private PictureBox hitbox2;
    }
}
