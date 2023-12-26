using Определение_плотности_вероятности;

namespace Определение_плотности_вероятности
{
    public class HelpMethods
    {
        #region Метод считывающий и передающий в список данные файла CHELUST.DAT
        public static List<S> GetObjectsFromDATFile()
        {
            List<S> objects = new();
            string[] substrings = File.ReadAllText(@"C:\VS projects\Универ\ИИ\Определение плотности вероятности\Определение плотности вероятности\CHELUST.DAT").Split('\r');
            bool IsCorrect = false;
            for(int i = 0; i < substrings.Length; i++)
            {
                string[] substr = substrings[i].Split('\t');
                double[] values = new double[substr.Length];

                for(int j = 0; j < values.Length; j++)
                {
                    substr[j] = substr[j].Replace('.', ',');
                    if (double.TryParse(substr[j], out double _))
                    {
                        values[j] = double.Parse(substr[j]);
                        IsCorrect = true;
                    }
                    else IsCorrect = false;
                }
                if (IsCorrect)
                    objects.Add(new S(values));
            }

            return objects;
        }
        #endregion

        #region Метод возвращающий евклидово расстояние между двумя объектами в n-мерном пространстве
        public static double EuclideanDistance(double[] d1, double[] d2)
        {
            if (d1.Length == d2.Length)
            {
                double result = 0;

                for (int i = 0; i < d1.Length; i++)
                {
                    result += Math.Pow(d1[i] - d2[i], 2);
                }
                return Math.Sqrt(result);
            }
            else return -1;
        }
        #endregion

        #region Метод возвращающий расстояние между двумя объектами по метрике Чебышева
        public static double ChebishevDistance(double[] d1, double[] d2)
        {
            if (d1.Length == d2.Length)
            {
                double result = 0;

                for (int i = 0; i < d1.Length; i++)
                {
                    result += Math.Abs(d1[i] - d2[i]);
                }
                return result;
            }
            else return -1;
        }
        #endregion

        #region Метод сохраняющий в экземпляре объекта отношения между объектами и заданной точкой в n-мерном пространстве по обеим метрикам
        public static void RelationShip(List<S> objects, S setPoint)
        {
            foreach(var o in objects)
            {
                o.EuclideanRelToGivenObject = EuclideanDistance(o.Signs, setPoint.Signs);
                o.ChebishevRelToGivenObject = ChebishevDistance(o.Signs, setPoint.Signs);
            }
        }
        #endregion
    }
}
