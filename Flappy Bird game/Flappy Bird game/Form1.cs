namespace Flappy_Bird_game
{
    public partial class Form1 : Form
    {
        int pipeSpeed = 8;
        int gravity = 5;
        int score = 0;
        bool spaceKeydown = false;

        // Initial positions of game elements
        Point flappyBirdInitialPos;
        Point pipeTopInitialPos;
        Point pipeBottomInitialPos;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }
        private void InitializeGame()
        {
            // Set initial positions of game elements
            flappyBirdInitialPos = flappyBird.Location;
            pipeTopInitialPos = pipeTop.Location;
            pipeBottomInitialPos = pipeBottom.Location;

            // Start the game timer
            gameTimer.Start();
        }
        private void gameTimerEvent(object sender, EventArgs e)
        {
            // Game logic...
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;

            scoreText.Text = "Score: " + score;

            if (pipeBottom.Left < -150)
            {
                pipeBottom.Left = 800;
                score++;
            }
            if (pipeTop.Left < -180)
            {
                pipeTop.Left = 950;
                score++;
            }

            if (spaceKeydown)
            {
                flappyBird.Top -= gravity;
            }
            else
            {
                flappyBird.Top += gravity;
            }

            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(floor.Bounds) ||
                flappyBird.Top < 0)
            {
                endGame();
            }

            if (score > 5)
            {
                pipeSpeed = 15;
            }

            if (flappyBird.Top < -25)
            {
                endGame();
            }

        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                spaceKeydown = true;
            }
            else if (e.KeyCode == Keys.R) // Check if "R" key is pressed
            {
                if (!gameTimer.Enabled) // Check if the game is over
                {
                    RestartGame(); // Restart the game if it's over and "R" key is pressed
                }
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                spaceKeydown = false;
            }

        }
        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text += " Game over , R to start again";
        }

        private void RestartGame()
        {
            // Reset game elements to their initial positions and states
            flappyBird.Location = flappyBirdInitialPos;
            pipeTop.Location = pipeTopInitialPos;
            pipeBottom.Location = pipeBottomInitialPos;

            // Reset other game elements as needed...
            score = 0;
            pipeSpeed = 8;

            // Start the game timer again
            gameTimer.Start();

            // Update the score display
            scoreText.Text = "Score: " + score;
        }
    }
}
    