using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace LazyLines_2015
{
    public enum ballColors
    {
        pink = 0,
        green = 1,
        yellow = 2,
        red = 3,
        teal = 4,
        brown = 5,
        blue = 6,
        none = 7
    }

    class ball
    {
        static private Random random;
        public ballColors color; //Main Ball Collor        

        public ball()
        {
            color = (ballColors)random.Next(0,7); 
        }

        public ball(ballColors color)
        {
            this.color = color;
        }

        static ball()
        {
            random = new Random();
        }
              
        public void draw(cell sender)
        {            
            switch (this.color)
            {
                case ballColors.pink:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pink));                    
                    break;
                case ballColors.green:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.green));
                    break;

                case ballColors.yellow:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.yellow));
                    break;

                case ballColors.red:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.red));
                    break;

                case ballColors.teal:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.teal));
                    break;

                case ballColors.brown:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.brown));
                    break;

                case ballColors.blue:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.blue));
                    break;
            }
        }

        public void drawNext(cell sender, ballColors color = ballColors.none)
        {
            ballColors colorForSwitch = this.color;
            if (color != ballColors.none) colorForSwitch = color;
            
            switch (colorForSwitch)
            {
                case ballColors.pink:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pinkNext));
                    break;

                case ballColors.green:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.greenNext));
                    break;

                case ballColors.yellow:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.yellowNext));
                    break;

                case ballColors.red:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.redNext));
                    break;

                case ballColors.teal:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.tealNext));
                    break;

                case ballColors.brown:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.brownNext));
                    break;

                case ballColors.blue:
                    sender.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.blueNext));
                    break;
            }
        }      

        public void remove(cell sender)
        {
            sender.BackgroundImage = null;
        }
    }
}