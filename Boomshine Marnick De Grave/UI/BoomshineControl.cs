using System;
using System.Windows.Forms;
using System.Drawing;
using Domain;
using System.Drawing.Drawing2D;

namespace UI {
    class BoomshineControl : Control {

        public BoomshineView View { get; set; }

        private Timer Timer { get; set; }

        private long _previousTicks;

        private bool _started;

        public BoomshineControl(BoomshineView view) {
            // stel in dat deze control zichzelf zal hertekenen igv resize
            this.ResizeRedraw = true;

            // teken eerst op een buffer om flikkering te vermijden
            this.DoubleBuffered = true;

            // stel de achtergrondkleur in op lichtgrijs
            this.BackColor = Color.White;

            // stel het diagram in dat deze control zal visualiseren
            this.View = view;

            // prepareer een timer die elke 10 milliseconden afloopt
            this.Timer = new Timer();
            this.Timer.Interval = 10;
            this.Timer.Tick += new EventHandler(TimerAlarm);
        }

        private void TimerAlarm(Object myObject, EventArgs myEventArgs) {
            // bereken hoeveel milliseconden het geleden is sinds
            // het vorige timer alarm
            long ticks = DateTime.Now.Ticks;
            int millis = (int)((ticks - this._previousTicks) / TimeSpan.TicksPerMillisecond);
            this._previousTicks = ticks;

            // meld aan de View hoeveel milliseconden er verstreken zijn
            this.View.Simulate(millis);

            // zorg dat OnPaint (zo snel mogelijk) wordt opgeroepen
            // zodat we de sprites kunnen laten hertekenen
            // (we gaan er immers van uit dat dit nodig zal zijn,
            // er zal er toch minstens eentje verschoven zijn)
            Refresh();
        }

        protected override void OnVisibleChanged(EventArgs e) {
            base.OnVisibleChanged(e);
            if (this.Visible) {
                // deze control is zichtbaar geworden
                this._previousTicks = DateTime.Now.Ticks;
                this.Timer.Start();
            } else {
                // deze control werd verborgen (niet zo relevant voor ons)
                this.Timer.Stop();
            }
        }

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);

            // de grootte van het venster is veranderd, meld dit aan de view
            this.View.Size = this.Size;

            // indien nog niet gestart, start nu
            if (!_started) {
                Start();
                _started = true;
            }
        }


        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);
            MouseClickedAt(e.X, e.Y);
        }

        // DEZE METHOD WORDT OPGEROEPEN TELKENS DE CONTROL MOET HERTEKEND WORDEN
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            Graphics gfx = e.Graphics;

            // zorg dat er mooi kan getekend worden (anti-aliasing e.d.)
            gfx.PixelOffsetMode = PixelOffsetMode.Half;
            gfx.SmoothingMode = SmoothingMode.HighQuality;

            // de breedte en hoogte van deze control kun je
            // achterhalen met this.Width en this.Height

            // HIERONDER PLAATS JE JE EIGEN CODE OM TE TEKENEN


            // geef de View de opdracht om alles te hertekenen
            this.View.DrawAll(gfx);
        }



        private void MouseClickedAt(int x, int y) {
            this.View.ClickAt(x, y);
        }

        private void Start() {
            this.View.StartLevel(100);
        }
    }
}
