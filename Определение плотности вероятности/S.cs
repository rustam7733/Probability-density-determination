namespace Определение_плотности_вероятности
{
    public class S
    {
        public double[] Signs { get; set; }

        public double EuclideanRelToGivenObject { get; set; }
        public double ChebishevRelToGivenObject { get; set; }

        public S(double[] signs) => Signs = signs;
    }
}
