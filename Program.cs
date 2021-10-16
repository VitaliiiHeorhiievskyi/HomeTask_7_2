using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace HomeTask_7_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, (double, double)> products = new Dictionary<string, (double, double)>();

            StreamReader reader = new StreamReader(@"D:\Users\vital\source\repos\HomeTask_7_2\Menu.txt");

            string menu = reader.ReadToEnd();

            string[] lines = menu.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string[] data = new string[] { };

            double weight;

            foreach (var line in lines)
            {
                data = line.Trim('\r').Split();

                if (data.Length != 2)
                    continue;

                if (!double.TryParse(data[1], System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out weight))
                    throw new ArgumentException("Data in file is not correct!!");


                if (products.ContainsKey(data[0]))
                    products[data[0]] = (products[data[0]].Item1 + weight, products[data[0]].Item2);

                else
                    products.Add(data[0], (weight, 0));
            }

            reader = new StreamReader(@"D:\Users\vital\source\repos\HomeTask_7_2\Price.txt");

            string infoAboutPrice = reader.ReadToEnd();

            lines = infoAboutPrice.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            double price = 0;

            foreach (var line in lines)
            {
                data = line.Trim('\r').Split();
                if (data.Length != 2)
                    continue;

                if (!double.TryParse(data[1], System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out price))
                    throw new ArgumentException("Data in file is not correct!!");


                if (products.ContainsKey(data[0]))
                    products[data[0]] = (products[data[0]].Item1, price);

                else
                    products.Add(data[0], (0, price));
            }


            foreach (var product in products)
            {
                Console.WriteLine("Product: " + product.Key);

                Console.WriteLine($"Weight: {product.Value.Item1:0.00}");

                Console.WriteLine($"Price: {(product.Value.Item2 * product.Value.Item1):0.00}\n\n");
            }

        }
    }
}
