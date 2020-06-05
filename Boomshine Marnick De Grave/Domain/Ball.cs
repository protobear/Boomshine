using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;


namespace Domain {
    class Ball {
        private BallStatus _status;

        public BallStatus Status
        {
            get { return _status;} set { _status = value; }

        }
        public float _centerX;
        public float _centerY;
        public float _radius;
        public Color _color;
        public float _blastRadius;
        public float _speedX;
        public float _speedY;

        public Ball(float centerX, float centerY, float speedX, float speedY, float initialRadius, Color c) {

            _centerX = centerX;
            _centerY = centerY;
            _speedX = speedX;
            _speedY = speedY;
            _blastRadius = GameConstants.BlastRadiusMultiplier * initialRadius;
            _color = c;
            _radius = initialRadius;
        }

        public BallStatus GetStatus() {
            return _status;
        }

        public void Simulate(int millis, Size size)
        {
            
            float rightSide = size.Width;
            float leftSide = 0;

            if (_status == BallStatus.Regular)
            {
                _centerY = _centerY + Utility.GetMillisFractionOf(millis, _speedY);
                _centerX = _centerX + Utility.GetMillisFractionOf(millis, _speedX);

                if (_centerX - _radius <= leftSide)
                {
                    _speedX = Math.Abs(_speedX);
                }

                if (_centerY - _radius <= leftSide)
                {
                    _speedY = Math.Abs(_speedY);

                }

                if (_centerX + _radius >= rightSide)
                {
                    _speedX = Math.Abs(_speedX) * -1;
                }

                if (_centerY + _radius >= rightSide)
                {
                    _speedY = Math.Abs(_speedY) * -1;
                }

            }

            if (_status == BallStatus.Exploding)
            {
                _radius = _radius + Utility.GetMillisFractionOf(millis, GameConstants.ExplodeSpeed);
                if (_radius >= _blastRadius)
                {
                    _status = BallStatus.Imploding;
                }
            }

            if (_status == BallStatus.Imploding)
            {
                _radius = _radius + Utility.GetMillisFractionOf(millis, GameConstants.ImplodeSpeed);
                if (_radius < GameConstants.MinRadius)
                {
                    _status = BallStatus.Dead;
                }
            }

        }

        public void Explode() {
            _status = BallStatus.Exploding;
        }

        public void Draw(Graphics gfx) {

            if (_status == BallStatus.Exploding || _status == BallStatus.Imploding)
            {
                Brush brush = new SolidBrush(_color);
                gfx.FillEllipse(brush, _centerX - _radius, _centerY - _radius, _radius * 2, _radius * 2);
            }
            if (_status != BallStatus.Dead)
            {
                Pen pen = new Pen(Color.Black);
                gfx.DrawEllipse(pen, _centerX-_radius, _centerY-_radius, _radius * 2, _radius * 2);
            }


        }

        public bool Overlaps(Ball other) {
                //double x1 = this._centerX;
                //double x2 = other._centerX;
                //double y1 = this._centerY;
                //double y2 = other._centerY;
                double sumRadius = this._radius + other._radius;
                //double distancePoints = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
                double distancePoints = Math.Sqrt(Math.Pow(this._centerX - other._centerX, 2) +
                                                  Math.Pow(this._centerY - other._centerY, 2));
                return (distancePoints <= sumRadius);
        }

        public static Ball CreateRandomBall(Size size) {
            Ball result = null;
            Random random = new Random();
            int step = random.Next(0, 256);
            float speedX = random.Next(GameConstants.MinSpeed, GameConstants.MaxSpeed);
            float speedY = random.Next(GameConstants.MinSpeed, GameConstants.MaxSpeed);
            int ranSpeed = random.Next(1, 100);
            if (ranSpeed % 2 == 0)
            {
                speedX = speedX * -1;
            }
            int radius = random.Next(GameConstants.MinRadius, GameConstants.MaxRadius);
            float centerX = random.Next(radius, size.Width-radius);
            float centerY = random.Next(radius, size.Height-radius);
            Color c = Utility.getRainbowColor(step, 255, 128);


            result = new Ball(centerX,centerY,speedX, speedY,radius, c);

            return result;
        }
    }
}
