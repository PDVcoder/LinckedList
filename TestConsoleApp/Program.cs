using System;
using LinkedList;

namespace TestConsoleApp
{
    class Program
    {
        private static int CountOfSix { get; set; }

        static void Main(string[] args)
        {
            CountOfSix = 0;
            int[] array = { 1, 2, 3, 3, 4, 5, 6, 6, 6, 8, 10, 10 };
            var linkedList = new LinkedList<int>(array);

            linkedList.OnAdd += addedItem;
            linkedList.OnRemove += RemovedItem;

            Console.WriteLine($"Added elements to collection: " + linkedList.ToString());

            //linkedList.Add(1);
            //linkedList.Add(2);
            //linkedList.Add(3);
            //linkedList.Add(3);
            //linkedList.Add(4);
            //linkedList.Add(5);
            //linkedList.Add(6);
            //linkedList.Add(7);
            //linkedList.Add(6);
            //linkedList.Add(6);
            //linkedList.Add(6);
            //linkedList.Add(8);
            //linkedList.Add(10);
            //linkedList.Add(10);

            foreach (var item in linkedList)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n");

            Console.WriteLine($"Removing items: ");

            linkedList.Remove(1);
            linkedList.Remove(3);
            linkedList.Remove(10);

            foreach (var item in linkedList)
            {
                Console.Write(item.GetHashCode().ToString() + " ");
            }
            Console.WriteLine("\n");

            Console.WriteLine($"Reversed list: ");

            var reversedList = linkedList.Reverse();

            foreach (var item in reversedList)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n");

            Console.Write($"Work with indexes: list[2] = {reversedList[2].ToString()}, list[0] = {reversedList[0].ToString()}, list[7] =  {reversedList[7].ToString()}\n");

            reversedList[5] = 1000;

            foreach (var item in reversedList)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n");

            Console.WriteLine($"Copy to array: ");

            int[] arr = new int[15];
            arr[0] = 100;
            arr[1] = 200;
            arr[2] = 300;

            reversedList.CopyTo(arr, 4);

            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n");

            Console.WriteLine($"Count of added six: {CountOfSix}");

            Console.ReadLine();
        }

        private static void RemovedItem(object sender, int e)
        {
            Console.WriteLine($"Item {e} was removed");
        }

        private static void addedItem(object sender, int e)
        {
            if (e == 6) CountOfSix++;
        }
    }
}