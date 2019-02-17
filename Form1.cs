using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PilotBrothersSafe
{
    public partial class Form1 : Form
    {
        Game game;
        bool sizeChanged = false;
        public Form1()
        {
            InitializeComponent();
            game = new Game(3);
            StartGame();
        }

        private void StartGame()
        {
            game.Start();
            for (int j = 0; j < 4; j++)
                game.RandomStep();
            Refresh();
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            if(sizeChanged)
            {
                int size = (int)countLever.Value;
                changeSizeSafePanel(size);
                game = new Game(size);
                sizeChanged = false;
            }
            StartGame();
        }


        private void changeSizeSafePanel(int size)
        {
            safePanel.ColumnCount = size;
            safePanel.RowCount = size;

            int width = 100 / size;
            int height = 100 / size;

            safePanel.ColumnStyles.Clear();
            safePanel.RowStyles.Clear();
            safePanel.Controls.Clear();
            int position = 0;
            for (int row = 0; row < size; row++)
            {
                safePanel.RowStyles.Add(new RowStyle(SizeType.Percent, height));
                safePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));
                int fontSize = safePanel.Height;
                for (int col = 0; col < size; col++)
                {
                    var button = new Button();
                    button.Tag = position++;
                    button.Dock = DockStyle.Fill;
                    button.Click += ButtonOnClick;
                    safePanel.Controls.Add(button, col, row);
                   
                    
                }
            }
        }
        public void Refresh()
        {
            foreach (Control control in safePanel.Controls)
            {
                Button button = control as Button;
                int position = Convert.ToInt16(button.Tag);
                if (game.GetOrientation(position) == Orientation.Horizontal)
                    button.Text = "—";
                else
                    button.Text = "|";
                button.Font = new Font("Arial", button.Height, GraphicsUnit.Pixel);
               
            }
        }
        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            var button = (Button)sender;
            int position = Convert.ToInt16(button.Tag);
            game.TurnLevers(position);
            Refresh();
            if (game.CheckLevers())
            {
                MessageBox.Show("Вы Победили!", "Поздравляем");
                StartGame();
             }
        }

        private void countLever_ValueChanged(object sender, EventArgs e)
        {
            sizeChanged = true;
        }
    }
}
