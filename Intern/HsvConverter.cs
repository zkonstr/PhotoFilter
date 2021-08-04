using System;
using System.Drawing;

namespace PhotoFilter.Intern
{
    public class HsvConverter
    {
        public static Color ToRgb(float h, float s, float v)
        {
            double r = 0, g = 0, b = 0;

            if (s == 0)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                int i;
                double f, p, q, t;

                if (h == 360)
                    h = 0;
                else
                    h = h / 60;

                i = (int) Math.Truncate(h);
                f = h - i;

                p = v * (1.0 - s);
                q = v * (1.0 - s * f);
                t = v * (1.0 - s * (1.0 - f));

                switch (i)
                {
                    case 0:
                        r = v;
                        g = t;
                        b = p;
                        break;

                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;

                    case 2:
                        r = p;
                        g = v;
                        b = t;
                        break;

                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;

                    case 4:
                        r = t;
                        g = p;
                        b = v;
                        break;

                    default:
                        r = v;
                        g = p;
                        b = q;
                        break;
                }
            }

            return Color.FromArgb((int) (r * 255), (int) (g * 255), (int) (b * 255));
        }
    }
}