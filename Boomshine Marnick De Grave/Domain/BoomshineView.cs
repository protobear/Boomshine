using System.Drawing;
using System.Collections.Generic;

namespace Domain {
    class BoomshineView  {
        public Size Size { get; set; }
        private int _ballCountAtLevelStart; // het aantal ballen dat er was aan het begin van het huidige level
        private bool _isPlaying; // wordt gebruikt om enkel iets te doen in Simulate als we aan het spelen zijn (heeft te maken met initialisatie volgorde in UI, niks van aantrekken)
        private int _numClicks; // aantal kliks dat reeds werd gedaan in het huidige level


        private List<Ball> _balls;


        public BoomshineView() {
            _balls = new List<Ball>();

        }

        public void Simulate(int millis) {
            List<Ball> badBalls = new List<Ball>();
            if (_isPlaying) {

                foreach (Ball b in _balls)
                {
                    b.Simulate(millis,Size);
                    if (b.Status == BallStatus.Regular)
                    {
                        foreach (Ball otherball in _balls)
                        {
                            if (otherball.Status != BallStatus.Exploding && otherball.Status != BallStatus.Imploding)
                                continue;
                            if (b.Overlaps(otherball))
                            {
                                b.Status = BallStatus.Exploding;
                            }
                        }
                    }

                    if(b.Status == BallStatus.Dead)
                    {
                        badBalls.Add(b);
                    }
                }

                foreach (Ball ballThatIsBad in badBalls)
                {
                    _balls.Remove(ballThatIsBad);
                }

                bool noBallsLeft = (_balls.Count == 0);

                // start nieuw level zodra er geen ballen meer over zijn
                if (noBallsLeft) {
                    int nextLevel = _ballCountAtLevelStart - 10;
                    nextLevel += (_numClicks - 1) * 3;
                    nextLevel = Utility.GetClampedInt(1, nextLevel, 100);
                    StartLevel(nextLevel);
                }
            }

        }

        public void DrawAll(Graphics gfx) {
            // toon het aantal ballen aan het begin van deze level in de linkerbovenhoek
            Brush brush = new SolidBrush(Color.LightGray);
            Font font = new Font("Arial", 16);
            gfx.DrawString(_ballCountAtLevelStart.ToString(), font, brush, 0, 0);
            brush.Dispose();
            font.Dispose();
            foreach (Ball teTekenenBallen in _balls)
            {
                teTekenenBallen.Draw(gfx);
            }

        }

        public void ClickAt(int x, int y) {
            // de initiele radius is de maximale radius uit GameConstants
            // de kleur is Color.FromArgb(128,255,0,0)
            // de x en y snelheid is 0
            Ball b = new Ball(x, y, 0, 0, GameConstants.MaxRadius, Color.FromArgb(128, 255, 0, 0));
            b.Status = BallStatus.Exploding;
            _balls.Add(b);

            // onthou dat er geklikt is (nodig om volgende level te bepalen)
            _numClicks++;
        }

        public void StartLevel(int ballCount) {
            _ballCountAtLevelStart = ballCount;
            _balls = new List<Ball>();
            for (int i = 0; i < ballCount; i++)
            {
                _balls.Add(Ball.CreateRandomBall(Size));
            }




            // reset aantal kliks en onthou dat we aan het spelen zijn
            _numClicks = 0;
            _isPlaying = true;
        }

    }
}
