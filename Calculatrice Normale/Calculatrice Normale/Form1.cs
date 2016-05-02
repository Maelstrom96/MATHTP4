﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculatrice_Normale
{
    public partial class Form1 : Form
    {
        NormalGraph graph;

        public Form1()
        {
            InitializeComponent();
            graph = new NormalGraph(new Point(50, 50));
            graph.Width = 350;
            graph.Height = 150;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            graph.Draw(e.Graphics);
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            graph.Height = (uint)this.Height - 100;
            graph.Width = (uint)this.Width - 100;
            Refresh();
        }
    }
}