using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyatomicDominoes
{
    class Square
    {
        public string top;
        public string bottom;
        public string left;
        public string right;
        public string name;
        rotation rot;
        public Square(string top,
                      string bottom,
                      string left,
                      string right,
                      string name)
        {
            this.top = top;
            this.bottom = bottom;
            this.left = left;
            this.right = right;
            this.name = name;
            this.rot = rotation.up;
        }
        public Square(Square square)
        {
            this.top = square.top;
            this.bottom = square.bottom;
            this.left = square.left;
            this.right = square.right;
            this.name = square.name;
            this.rot = square.rot;
        }
        public void rotate()
        {
            string tempBottom = bottom;
            bottom = right;
            right = top;
            top = left;
            left = tempBottom;
            switch (rot)
            {
                case rotation.up:
                    rot = rotation.right;
                    break;
                case rotation.down:
                    rot = rotation.left;
                    break;
                case rotation.left:
                    rot = rotation.up;
                    break;
                case rotation.right:
                    rot = rotation.down;
                    break;
            }
        }
    }
}
enum rotation
{
    up = 0,
    right = 1,
    down = 2,
    left = 3
}