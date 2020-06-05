using System;
using System.Drawing;

namespace Domain {
    static public class Utility {
        static public Color getRainbowColor(int step, int numOfSteps, byte alpha) {
            // This function generates vibrant, "evenly spaced" colours (i.e. no clustering). This is ideal for creating easily distinguishable vibrant markers in Google Maps and other apps.
            // Adam Cole, 2011-Sept-14
            // HSV to RBG adapted from: http://mjijackson.com/2008/02/rgb-to-hsl-and-rgb-to-hsv-color-model-conversion-algorithms-in-javascript
            float r = 0, g = 0, b = 0;
            float h = (float)step / numOfSteps;
            float i = (int)(h * 6f);
            float f = h * 6f - i;
            float q = 1f - f;
            switch ((int)i % 6) {
                case 0: r = 1; g = f; b = 0; break;
                case 1: r = q; g = 1; b = 0; break;
                case 2: r = 0; g = 1; b = f; break;
                case 3: r = 0; g = q; b = 1; break;
                case 4: r = f; g = 0; b = 1; break;
                case 5: r = 1; g = 0; b = q; break;
            }
            Color result = Color.FromArgb(alpha, (int)(r * 255), (int)(g * 255), (int)(b * 255));
            return result;
        }

        static public int GetClampedInt(int min, int value, int max) {
            // geeft een waarde terug die gebaseerd is op 'value' en gegarandeerd in [min,max] ligt
            int result = Math.Min(value, max);
            result = Math.Max(min, result);
            return result;
        }

        static public float GetClampedFloat(float min, float value, float max) {
            // geeft een waarde terug die gebaseerd is op 'value' en gegarandeerd in [min,max] ligt
            float result = Math.Min(value, max);
            result = Math.Max(min, result);
            return result;
        }

        static public float GetMillisFractionOf(int millis, float amount) {
            float result = amount * millis / 1000f;
            return result;
        }

    }
}
