using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColourLib
{
    public struct HSVColor : IColor<HSVColor>
    {
        float H;
        float S;
        float V;
        public HSVColor(float H, float S, float V)
        {
            this.H = Math.Clamp(H, 0f, 1f);
            this.S = Math.Clamp(S, 0f, 1f);
            this.V = Math.Clamp(V, 0f, 1f);
        }

        public H Convert<H>()
        {
            return Convert<H>(this);
        }
        public static H Convert<H>(HSVColor HSVColor)
        {
            throw new NotImplementedException();
        }
        public HSVColor Lerp(HSVColor colorTo, float val)
        {
            return Lerp(this, colorTo, val);
        }
        public static HSVColor Lerp(HSVColor colorFrom, HSVColor colorTo, float val)
        {
            throw new NotImplementedException();
        }
    }
}
