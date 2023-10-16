using Snack.Properties;
using Snake.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.PropertyGridInternal;

namespace Snake
{
    internal class Game
    {
        public Keys Direction { get; set; }

        public Keys Arrow { get; set; }

        private Timer Frame { get; set; }

        private Label lblPontuacao { get; set; }

        private Panel Pntela { get; set; }

        private int pontos = 0;

        private Food Food;

        private Snake Snake;

        private Bitmap offscreenBitmap;

        private Graphics bitmapGraph;

        private Graphics screenGraph;

        public Game(ref Timer timer, ref Label label, ref Panel panel)
        {
            Pntela = panel;
            Frame = timer;
            lblPontuacao = label;
            offscreenBitmap = new Bitmap(428, 428);
            Snake = new Snake();
            Food = new Food();
            Direction = Keys.Left;
            Arrow = Direction;

        }

        // Função para iniciar o game
        public void StartGame()
        {
            Snake.Reset();
            Food.CreateFood();
            Direction = Keys.Left;
            bitmapGraph = Graphics.FromImage(offscreenBitmap);
            screenGraph = Pntela.CreateGraphics();
            Frame.Enabled = true;

        }

        public void Tick()
        {
            // -- Validação para onde a cobrinha está indo --
            if (((Arrow == Keys.Left) && (Direction != Keys.Right)) ||
            ((Arrow == Keys.Right) && (Direction != Keys.Left)) ||
            ((Arrow == Keys.Up) && (Direction != Keys.Down)) ||
            ((Arrow == Keys.Down) && (Direction != Keys.Up))) 
            {
                Direction = Arrow;
            }
            // -- Validação para onde a cobrinha está indo --

            // -- Direção    onde a cobrinha está indo --
            switch (Direction)
            {
                case Keys.Left:
                    Snake.Left();
                    break;
                case Keys.Right:
                    Snake.Right();
                    break;
                case Keys.Up:
                    Snake.Up();
                    break;
                case Keys.Down:
                    Snake.Down();
                    break;
            }
            // -- Direção    onde a cobrinha está indo --


           

            bitmapGraph.DrawImage(Resources.xadrewz, 0, 0, 428, 428);
            bitmapGraph.DrawImage(Resources.Apple, (Food.Location.X * 15), (Food.Location.Y * 15), 15, 15);
            bool gameOver = false;

            for (int i = 0; i < Snake.Length; i++)
            {
                if (i == 0)
                {
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#000000")), (Snake.Location[i].X * 15), (Snake.Location[i].Y * 15), 15, 15);
                }
                else
                {
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#4F4F4F")), (Snake.Location[i].X * 15), (Snake.Location[i].Y * 15), 15, 15);
                }
                if ((Snake.Location[i] == Snake.Location[0]) && (i > 0))
                {
                    gameOver = true;

                }
            }

            screenGraph.DrawImage(offscreenBitmap,0,0);
            CheckCollision();
            if (gameOver)
            {
                GameOver();
            }

        }

        // Função que identifica se a cobra colidiu com a comida e acumulo de pontos
        public void CheckCollision()
        {
            if (Snake.Location[0] == Food.Location)
            {
                Snake.Eat();
                Food.CreateFood();
                pontos += 500;
                lblPontuacao.Text = "PONTOS: " + pontos;
            }
        }

        // -- Validação GAME OVER --
        public void GameOver()
        {
            Frame.Enabled = false;
            bitmapGraph.Dispose();
            screenGraph.Dispose();
            lblPontuacao.Text = "PONTOS: 0";
            pontos = 0;
            MessageBox.Show("Game Over... Mais sorte na próxima");
        }
        // -- Validação GAME OVER --
    }


}
