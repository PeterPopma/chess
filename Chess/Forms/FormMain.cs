using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess.Forms
{
    public partial class FormMain : Form
    {
        private static System.Windows.Forms.Timer updateScreenTimer;

        public FormMain()
        {
            InitializeComponent();
            SetupTimers();
        }

        private void SetupTimers()
        {
            // Create a timer with a 10 msec interval.
            updateScreenTimer = new System.Windows.Forms.Timer();
            updateScreenTimer.Interval = 10;
            updateScreenTimer.Tick += new EventHandler(OnTimedEventUpdateScreen);
            updateScreenTimer.Start();
        }

        private void OnTimedEventUpdateScreen(object sender, EventArgs eArgs)
        {
            displayMonogame.UpdateFrame();
            displayMonogame.UpdateScreen();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop the timer
            updateScreenTimer.Enabled = false;
        }

        private void displayMonogame_MouseClick(object sender, MouseEventArgs e)
        {
            displayMonogame.OnClick(e.X, e.Y);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            displayMonogame.InitGame();
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            displayMonogame.UndoLastMove();
        }
    }
}
