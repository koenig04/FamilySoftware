using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StatisticProcess.DiagramRelative.DiagramDataClassifier
{
    class DiagramDataClassifierFactory
    {
        private static DiagramDataClassifierBase _curveClassifier = new DiagramDataClassifierForCurve();
        private static DiagramDataClassifierBase _pieClassifier = new DiagramDataClassifierForPie();

        public static DiagramDataClassifierBase CreateClassifier(StaticType classifierType)
        {
            switch (classifierType)
            {
                case StaticType.Time:
                    return _curveClassifier;
                case StaticType.Sort:
                    return _pieClassifier;
                default:
                    return null;
            }
        }
    }
}
