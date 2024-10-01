// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Net;

internal class Program
{
    private static void Main(string[] args)
    {
        int build = 15;//build has a 10 floor
        int whereYouAre = 6;
        int elevatorFloor = 3;
        List<int> clickFloors = new List<int>();
        List<int> downFLoors = new List<int>();
        List<int> upFLoors = new List<int>();

        Console.WriteLine("Please click the floor goes on ");
        void clickFloor()
        {
            int clicked = Convert.ToInt32(Console.ReadLine());


            if (clicked > build)
            {
                Console.WriteLine($"Elevator has a 10 floor,you can not click more");
                clickFloor();
            }

            else if (clicked < 0)
            {
                Console.WriteLine($"You can not go to,negative floor.");
                clickFloor();
            }
            else
            {
                clickFloors.Add(clicked);
                if (clickFloors.Count < build)
                {
                    clickFloor();
                }
                else
                {
                    change();
                }
            }

        }

        void change()
        {
            for (int j = 0; j < clickFloors.Count - 1; j++)
            {
                for (int i = 0; i < clickFloors.Count - 1; i++)
                {
                    if (clickFloors[i] > clickFloors[i + 1])
                    {
                        int temp = clickFloors[i];
                        clickFloors[i] = clickFloors[i + 1];
                        clickFloors[i + 1] = temp;
                    }
                }
            }

            Console.WriteLine($"New clickFloors revise version: {string.Join(",", clickFloors)}");
            delete();
        }
        void delete()
        {
            List<int> cleanedList = new List<int>();
            for (int j = 0; j < clickFloors.Count; j++)
            {
                bool exist = false;
                for (int i = 0; i < cleanedList.Count; i++)
                {
                    if (cleanedList[i] == clickFloors[j])
                    {
                        exist = true;
                        break;
                    }

                }
                if (!exist)
                {
                    cleanedList.Add(clickFloors[j]);

                }
            }
            clickFloors = cleanedList;
            Console.WriteLine($"Refresh list: {string.Join(",", clickFloors)}");

            elevatorCall();
        }
        void elevatorCall()

        {
            Console.WriteLine("Elevator Coming:");
            if (elevatorFloor < whereYouAre)
            {
                for (int i = elevatorFloor; i <= whereYouAre; i++)
                {
                    Console.WriteLine(i);
                }
            }
            else if (elevatorFloor > whereYouAre)
            {
                for (int i = elevatorFloor; i >= whereYouAre; i--)
                {
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine("You can get in.");
            elevatorProcess();
        }

        int elevatorProcess()
        {
            while (true)
            {
                Console.WriteLine("Where do you want to go?");
                if (!int.TryParse(Console.ReadLine(), out int whereDoYouGo))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                if (whereDoYouGo < clickFloors[0] || whereDoYouGo > clickFloors[clickFloors.Count - 1])
                {
                    Console.WriteLine("Please enter a valid floor number.");
                    continue;
                }

                int index = clickFloors.IndexOf(whereDoYouGo);
                if (index == -1)
                {
                    Console.WriteLine("Floor not found.");
                    continue;
                }

                int? closeDown = (index > 0) ? (int?)clickFloors[index - 1] : null;
                int? closeUp = (index < clickFloors.Count - 1) ? (int?)clickFloors[index + 1] : null;
                int downSubtract = closeDown.HasValue ? Math.Abs(whereDoYouGo - closeDown.Value) : int.MaxValue;
                int upSubtract = closeUp.HasValue ? Math.Abs(whereDoYouGo - closeUp.Value) : int.MaxValue;

                Console.WriteLine($"downSubtract value: {downSubtract}");
                Console.WriteLine($"upSubtract value: {upSubtract}");

                return elevatorGoes(whereDoYouGo, downSubtract, upSubtract);
            }
        }

        int elevatorGoes(int targetFloor, int downSubtract, int upSubtract)
        {
            if (downSubtract <= upSubtract)
            {
                Console.WriteLine("Elevator going down");
                for (int i = whereYouAre; i >= clickFloors[0]; i--)
                {
                    if (clickFloors.Contains(i))
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                        if (i == targetFloor)
                        {
                            whereYouAre = i;
                            return i;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Elevator going up");
                for (int i = whereYouAre; i <= clickFloors[clickFloors.Count - 1]; i++)
                {
                    if (clickFloors.Contains(i))
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                        if (i == targetFloor)
                        {
                            whereYouAre = i;
                            return i;
                        }
                    }
                }
            }

            // Eğer hedef kata ulaşılamazsa
            return -1;
        }


        clickFloor();
    }

}
