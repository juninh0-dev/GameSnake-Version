using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Food
    {

        public Point Location { get; private set; }

        // Função que faz a comida nascer aleatóriamente pelo mapa
        public void CreateFood()
        {
            Random rnd = new Random();
            Location = new Point(rnd.Next(0,27), rnd.Next(0, 27));
        }
    }
}
