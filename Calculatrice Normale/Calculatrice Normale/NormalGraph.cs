using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Calculatrice_Normale
{
    class NormalGraph
    {
        private uint gradeHeight = 9;
        private int arrowSize = 8;
        private int space = 15;
        private Point[] function;

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
            //graphics_.DrawLine(pen, new Point(Position.X + ((int)Width / 2), Position.Y + (int)Height + arrowSize), new Point(Position.X + ((int)Width / 2), Position.Y));

            /**** Draw arrows ****/
            // BOTTOM-RIGHT
            graphics_.DrawLine(pen, new Point(Position.X + (int)Width - arrowSize, Position.Y + (int)Height - arrowSize), new Point(Position.X + (int)Width, Position.Y + (int)Height));
            graphics_.DrawLine(pen, new Point(Position.X + (int)Width - arrowSize, Position.Y + (int)Height + arrowSize), new Point(Position.X + (int)Width, Position.Y + (int)Height));
            // TOP-CENTER
            //graphics_.DrawLine(pen, new Point(Position.X + ((int)Width / 2) - arrowSize, Position.Y + arrowSize), new Point(Position.X + ((int)Width / 2), Position.Y));
            //graphics_.DrawLine(pen, new Point(Position.X + ((int)Width / 2) + arrowSize, Position.Y + arrowSize), new Point(Position.X + ((int)Width / 2), Position.Y));

            /**** Draw Scales ****/
            // Horrizontal
            int axeWidth = (int)Width - space * 2;
            int nbSteps = 16;
            int index = -4;
            Font font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            double steps = (double)axeWidth / (double)nbSteps;

            for (int i = 0; i <= nbSteps; i++)
            {
                Point tmpPointTop = new Point(Position.X + space + (int)(steps * i), Position.Y + (int)Height);
                Point tmpPointBottom = new Point(Position.X + space + (int)(steps * i), Position.Y + (int)Height);

                if (i % 2 == 0)
                {
                    tmpPointTop.Y -= (int)gradeHeight;
                    tmpPointBottom.Y += (int)gradeHeight;

                    // TEXT
                    Rectangle rect1 = new Rectangle(tmpPointBottom.X - 10, tmpPointBottom.Y + 10, 20, 20);
                    graphics_.DrawString(index.ToString(), font, Brushes.Black, rect1, stringFormat);
                    index++;
                }
                else
                {
                    tmpPointTop.Y -= (int)(gradeHeight / 2);
                    tmpPointBottom.Y += (int)(gradeHeight / 2);
                }

                graphics_.DrawLine(pen, tmpPointTop, tmpPointBottom);
            }

            /**** Draw symbol ****/

            Rectangle rect = new Rectangle(Position.X - 15, Position.Y + (int)Height + 17, 20, 20);
            graphics_.DrawString("z", font, Brushes.Black, rect, stringFormat);
        }

        public void DrawFunction(Graphics graphics_)
        {
            int axeWidth = (int)Width - space * 2;
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0), 1);
            Chart chart = new Chart();
            GraphicsPath  path = new GraphicsPath();

            if (function == null)
            {
                function = new Point[axeWidth];

                for (int i = 0; i < axeWidth; i++)
                {
                    double z = -4 + (i / ((double)axeWidth / (double)8));

                    double val = 0.5 - MathNet.Numerics.Distributions.Normal.PDF(0,1,z);
                    double pointHeight = ((val * (double)Height) / .5) + Position.Y;
                    function[i] = new Point(Position.X + i + space, (int)pointHeight);
                }
            }

            graphics_.DrawLines(pen, function);
        }

        public void Reset()
        {
            function = null;
        }

        public void Draw(Graphics graphics_)
        {
            graphics_.SmoothingMode = SmoothingMode.AntiAlias;


            // horizontal line
            DrawAxes(graphics_);
            DrawFunction(graphics_);
        }
    }
}
