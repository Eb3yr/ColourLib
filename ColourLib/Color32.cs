using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ColourLib
{
    public struct Color32 : IColorB<Color32>
    {
        private byte r;
        private byte g;
        private byte b;
        private byte a;
        public byte R
        {
            get => r;
            set { r = value; }
        }
        public byte G
        {
            get => g;
            set { g = value; }
        }
        public byte B
        {
            get => b;
            set { b = value; }
        }
        public byte A
        {
            get => a;
            set { a = value; }
        }
        public byte this[int i]
        {
            get => i switch
            {
                0 => r,
                1 => g,
                2 => b,
                3 => a,
                _ => throw new IndexOutOfRangeException()
            };
            set
            {
                switch (i)
                {
                    case 0:
                        R = value; break;
                    case 1:
                        G = value; break;
                    case 2:
                        B = value; break;
                    case 3:
                        A = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
    }
}
