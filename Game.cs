using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotBrothersSafe
{

    enum Orientation
    {
        Horizontal,
        Vertical
    }

    class Game
    {
        int size;
        Orientation[,] levers;
        static Random rand = new Random();

        public Game(int size)
        {
            if (size < 2) size = 2;
            if (size > 10) size = 10;
            this.size = size;
            levers = new Orientation[size, size];
        }

        public void Start()
        {
            Orientation startOrientation;
            startOrientation = (rand.Next(0, 1) == 0) ? Orientation.Horizontal : Orientation.Vertical;

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    levers[x, y] = startOrientation;
        }

        public void RandomStep()
        {
            int position = rand.Next(0, size * size - 1);
            TurnLevers(position);
        }

        public Orientation GetOrientation(int position)
        {
            int x, y;
            PositionToCoords(position, out x, out y);
            return levers[x, y];
        }

        private void TurnLever(int x, int y)
        {
            if (levers[x, y] == Orientation.Horizontal) levers[x, y] = Orientation.Vertical;
            else levers[x, y] = Orientation.Horizontal;
        }
        public void TurnLevers(int position)
        {
            int x, y;
            PositionToCoords(position, out x, out y);
            TurnLever(x, y);
            for(int i = 0; i < size; i++)
            {
                if (i == y) continue;
                TurnLever(x, i);
            }
            for (int i = 0; i < size; i++)
            {
                if (i == x) continue;
                TurnLever(i, y);
            }

        }

        public bool CheckLevers()
        {
            Orientation firstLever = levers[0, 0];
            for (int x = 0; x < size; x++)
                for (int y = 1; y < size; y++)
                    if (firstLever != levers[x, y])
                        return false;
            return true;
                    
        }

        private void PositionToCoords(int position, out int x, out int y)
        {
            if (position < 0) position = 0;
            if (position > size * size - 1) position = size * size - 1;
            x = position % size;
            y = position / size;
        }
    }
}
