using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FamilyAsset.Pages.Statistic
{
    public class StatisticColorSet
    {
        private static List<Color> _colorSet = new List<Color>()
        {
            Color.FromRgb(96,157,202),
            Color.FromRgb(255,150,65),
            Color.FromRgb(56,194,93),
            Color.FromRgb(255,91,78),
            Color.FromRgb(184,135,195),
            Color.FromRgb(182,115,101),
            Color.FromRgb(254,144,194),
            Color.FromRgb(164,160,155),
            Color.FromRgb(210,204,90)
        };

        public static List<Color> GetSeperateColors(int count)
        {
            List<Color> res = new List<Color>();
            for (int i = 0; i < count; i++)
            {
                res.Add(_colorSet[i]);
            }
            return res;
        }
    }
}
