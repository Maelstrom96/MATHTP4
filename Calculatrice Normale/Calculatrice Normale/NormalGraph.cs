using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatrice_Normale
{
    class NormalGraph
    {
        private uint gradeHeight = 9;
        private int arrowSize = 8;

        public uint Width { get; set; }
        public uint Height { get; set; }
        public Point Position { get; set; } // TOP-LEFT


        public NormalGraph(Point position_)
        {
            Position = position_;
        }

        private void DrawAxes(Graphics graphics_)
        {
            // Values
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0), 1);

            /**** Draw Axes ****/
            graphics_.DrawLine(pen, new Point(Position.X, Position.Y + (int)Height), new Point(Position.X + (int)Width, Position.Y + (int)Height));
            graphics_.DrawLine(pen, new Point(Position.X + ((int)Width / 2), Position.Y + (int)Height + arrowSize), new Point(Position.X + ((int)Width / 2), Position.Y));

            /**** Draw arrows ****/
            // BOTTOM-RIGHT
            graphics_.DrawLine(pen, new Point(Position.X + (int)Width - arrowSize, Position.Y + (int)Height - arrowSize), new Point(Position.X + (int)Width, Position.Y + (int)Height));
            graphics_.DrawLine(pen, new Point(Position.X + (int)Width - arrowSize, Position.Y + (int)Height + arrowSize), new Point(Position.X + (int)Width, Position.Y + (int)Height));
            // TOP-CENTER
            graphics_.DrawLine(pen, new Point(Position.X + ((int)Width / 2) - arrowSize, Position.Y + arrowSize), new Point(Position.X + ((int)Width / 2), Position.Y));
            graphics_.DrawLine(pen, new Point(Position.X + ((int)Width / 2) + arrowSize, Position.Y + arrowSize), new Point(Position.X + ((int)Width / 2), Position.Y));

            /**** Draw Scales ****/
            // Horrizontal
            int space = 15;
            int axeWidth = (int)Width - space * 2;
            int nbSteps = 16;

            double steps = (double)axeWidth / (double)nbSteps;

            for (int i = 0; i <= nbSteps; i++)
            {
                Point tmpPointTop = new Point(Position.X + space + (int)(steps * i), Position.Y + (int)Height);
                Point tmpPointBottom = new Point(Position.X + space + (int)(steps * i), Position.Y + (int)Height);

                if (i % 2 == 0)
                {
                    tmpPointTop.Y -= (int)gradeHeight;
                    tmpPointBottom.Y += (int)gradeHeight;
                }
                else
                {
                    tmpPointTop.Y -= (int)(gradeHeight / 2);
                    tmpPointBottom.Y += (int)(gradeHeight / 2);
                }

                graphics_.DrawLine(pen, tmpPointTop, tmpPointBottom);
            }
        }

        public void Draw(Graphics graphics_)
        {
            // horizontal line
            DrawAxes(graphics_);
        }
    }
}
