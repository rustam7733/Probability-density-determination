using Определение_плотности_вероятности;

class Program
{
    static void Main()
    {
        int k = 0;
        bool isCorrectValue = false;
        Console.Write("Укажите значение k => ");

        while(!isCorrectValue)
        {
            int kValue = Convert.ToInt32(Console.ReadLine());
            if (kValue != 0 && kValue != 1)
                { k = kValue; isCorrectValue = true; }

            else
                Console.Write("Некорректное значение 'k'. \n\nУкажите значение k => ");
        }

        Console.WriteLine();

        List<S> objects = HelpMethods.GetObjectsFromDATFile();

        int c = 1;

        foreach (S obj in objects)
        {
            Console.Write(c + ") ");
            for (int i = 0; i < obj.Signs.Length; i++)
                Console.Write(obj.Signs[i] + " ");

            c++;
            Console.WriteLine();
        }

        Console.Write("\nВыберите объект по порядковому номеру ");
        int index = Convert.ToInt32(Console.ReadLine());
        S setPoint = objects[index - 1];

        HelpMethods.RelationShip(objects, setPoint);

        foreach (S obj in objects)
        {
            Console.Write(c + ") ");
            for (int i = 0; i < obj.Signs.Length; i++)
                Console.Write(obj.Signs[i] + " ");

            c++;
            Console.Write("    => " + obj.EuclideanRelToGivenObject);
            Console.Write("    => " + obj.ChebishevRelToGivenObject);
            Console.WriteLine();
        }

        List<S> eucNearestObjects = (from o in objects orderby o.EuclideanRelToGivenObject select o).ToList();
        Console.WriteLine("\n\n\nОтношения объектов к заданному по метрике Евклида\n");
        foreach (var o in eucNearestObjects)
            Console.WriteLine("euc {0}", o.EuclideanRelToGivenObject);

        Console.WriteLine("\n\nРазрывы по метрике Евклида\n");
        double maxDis = double.MinValue;
        double minDis = double.MaxValue;

        for (int i = 1; i < eucNearestObjects.Count; i++)
        {
            if (eucNearestObjects[i].EuclideanRelToGivenObject - eucNearestObjects[i - 1].EuclideanRelToGivenObject > maxDis)
                maxDis = eucNearestObjects[i].EuclideanRelToGivenObject - eucNearestObjects[i - 1].EuclideanRelToGivenObject;

            if (eucNearestObjects[i].EuclideanRelToGivenObject - eucNearestObjects[i - 1].EuclideanRelToGivenObject < minDis)
                minDis = eucNearestObjects[i].EuclideanRelToGivenObject - eucNearestObjects[i - 1].EuclideanRelToGivenObject;

            Console.WriteLine("" +
                "\tРазрыв от k={0}, до k={1} составляет: {2}", i, i + 1,
                eucNearestObjects[i].EuclideanRelToGivenObject - eucNearestObjects[i - 1].EuclideanRelToGivenObject);
        }
        Console.WriteLine("\n\tМаксимальный разрыв по метрике Евклида: {0}" +
            "\n\tМинимальный разрыв по метрике Евклида: {1}", maxDis, minDis);


        List<S> chebNearestObjects = (from o in objects orderby o.ChebishevRelToGivenObject select o).ToList();
        Console.WriteLine("\n\nОтношения объектов к заданному по метрике Чебышева");

        foreach(var o in chebNearestObjects)
        {
            Console.WriteLine("cheb {0}", o.ChebishevRelToGivenObject);
        }

        Console.WriteLine("\n\nРазрывы по метрике Чебышева\n");
        for (int i = 1; i < chebNearestObjects.Count; i++)
        {
            if (chebNearestObjects[i].ChebishevRelToGivenObject - chebNearestObjects[i - 1].ChebishevRelToGivenObject > maxDis)
                maxDis = chebNearestObjects[i].ChebishevRelToGivenObject - chebNearestObjects[i - 1].ChebishevRelToGivenObject;

            if (chebNearestObjects[i].ChebishevRelToGivenObject - chebNearestObjects[i - 1].ChebishevRelToGivenObject < minDis)
                minDis = chebNearestObjects[i].ChebishevRelToGivenObject - chebNearestObjects[i - 1].ChebishevRelToGivenObject;

            Console.WriteLine("" +
                "\tРазрыв от k={0}, до k={1} составляет: {2}", i, i + 1,
                chebNearestObjects[i].ChebishevRelToGivenObject - chebNearestObjects[i - 1].ChebishevRelToGivenObject);
        }

        Console.WriteLine("\n\tМаксимальный разрыв по метрике Чебышева: {0}" +
            "\n\tМинимальный разрыв по метрике Чебышева: {1}", maxDis, minDis);

        Console.WriteLine("\nПлотность k-объектов по метрике Евклида составляет: " + 1 / eucNearestObjects[k - 1].EuclideanRelToGivenObject);

        Console.WriteLine("Плотность k-объектов по метрике Чебышева составляет: " + 1 / chebNearestObjects[k - 1].ChebishevRelToGivenObject);
    }
}