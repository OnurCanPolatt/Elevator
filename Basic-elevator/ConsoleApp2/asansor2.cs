// See https://aka.ms/new-console-template for more information
using System;
using System.Globalization;
using System.Net;

internal class Program
{
    private static void Main(string[] args)
    {
        int build = 15;//build has a 10 floor
        List<int> whereYouAre = new List<int>();
        List<int> elevatorFloor = new List<int>();
        List<int> clickFloors = new List<int>();
        List<int> downFLoors = new List<int>();
        List<int> upFLoors = new List<int>();

        Console.WriteLine("Please click the floor goes on ");
        void clickFloor()
        {
            int clicked = Convert.ToInt32(Console.ReadLine());


            if (clicked > build || clicked < 0)
            {
                Console.WriteLine($"Elevator has a {build} floor,you can not click more or less '0'");
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

            elevatorComingFrom();
        }

        void elevatorComingFrom()
        {
            Random random = new Random();
            int randomindex = random.Next(clickFloors.Count);
            elevatorFloor.Add(clickFloors[randomindex]);
            Console.WriteLine($"Elevator stays:{elevatorFloor[elevatorFloor.Count - 1]}.floor");

            elevatorCall();

        }
        int elevatorCall()
        {
            whereYouAre.Clear();
            Random random = new Random();
            int randomWhere = random.Next(0, build);
            whereYouAre.Add(randomWhere);
            Console.WriteLine($"First call:{string.Join(",", whereYouAre)}");



            Console.WriteLine("Elevator Coming:");
            if (elevatorFloor[elevatorFloor.Count - 1] < whereYouAre[whereYouAre.Count - 1])
            {
                for (int i = elevatorFloor[elevatorFloor.Count - 1]; i <= whereYouAre[whereYouAre.Count - 1]; i++)
                {
                    Console.WriteLine(i);
                    elevatorFloor[elevatorFloor.Count - 1] = i;

                }
            }
            else if (elevatorFloor[elevatorFloor.Count - 1] > whereYouAre[whereYouAre.Count - 1])
            {
                for (int i = elevatorFloor[elevatorFloor.Count - 1]; i >= whereYouAre[whereYouAre.Count - 1]; i--)
                {
                    Console.WriteLine(i);
                    elevatorFloor[elevatorFloor.Count - 1] = i;

                }
            }
            else
            {
                Console.WriteLine("You already at your floor.\n You can get in and click wheredoyougo");
                elevatorProcess(whereYouAre, elevatorFloor);
            }
            Console.WriteLine("You can get in.");
            elevatorProcess(whereYouAre, elevatorFloor);
            return elevatorFloor[elevatorFloor.Count - 1];

        }

        (int, int) elevatorProcess(List<int> whereYouAre, List<int> elevatorFloor)
        {
            while (true)
            {
                Console.WriteLine("Where do you want to go?");
                if (!int.TryParse(Console.ReadLine(), out int whereDoYouGo))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }
                if (whereDoYouGo == whereYouAre[whereYouAre.Count - 1])
                {
                    Console.Write($"You are already at your floor.{whereYouAre}.\nAgain click another floor.");
                    continue;
                }

                if (whereDoYouGo < clickFloors[0] || whereDoYouGo > clickFloors[clickFloors.Count - 1])
                {
                    Console.WriteLine("Please enter a valid floor number.");
                    continue;
                }
                if (clickFloors.Contains(whereDoYouGo))
                {
                }
                else
                {
                    clickFloors.Add(whereDoYouGo);
                    Console.WriteLine("Your new click added to list");
                    Console.WriteLine($"New upgraded list:{string.Join(",", clickFloors)}");

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
                return elevatorGoes(downSubtract, upSubtract, whereDoYouGo);
            }

        }


        (int, int) elevatorGoes(int downSubtract, int upSubtract, int whereDoYouGo)
        {
            if (whereDoYouGo == whereYouAre[whereYouAre.Count - 1])
            {
                Console.WriteLine($"You are already at floor {whereYouAre[whereYouAre.Count - 1]}.");

            }

            if (downSubtract > upSubtract)
            {
                return moveUp(whereDoYouGo, whereYouAre);
            }
            else
            {
                return moveDown(whereDoYouGo, whereYouAre);
            }
        }

        (int, int) moveDown(int whereDoYouGo, List<int> whereYouAre)
        {
            bool getOff = true;
            Console.WriteLine("Elevator going down:");
            for (int i = whereYouAre[whereYouAre.Count - 1]; i >= clickFloors[0]; i--)
            {

                if (clickFloors.Contains(i))
                {
                    Console.WriteLine($"Elevator at:{i}.floor");
                    if (i == whereDoYouGo)
                    {
                        getOff = false;
                        Console.WriteLine("You can get off");

                    }
                }
                else
                {
                    Console.WriteLine($"Elevator not waiting this floor:{i}...!");
                }

            }
            Console.WriteLine("Now elevator going up ");
            for (int j = clickFloors[0]; j <= clickFloors[clickFloors.Count - 1]; j++)
            {

                if (clickFloors.Contains(j))
                {
                    Console.WriteLine($"Elevator at:{j}.floor");
                    if (getOff && j == whereDoYouGo)
                    {
                        Console.WriteLine("You can get off");

                    }

                }
                else
                {
                    Console.WriteLine($"Elevator not waiting this floor:{j}...!");
                }
                whereYouAre.Add(j);
                elevatorFloor.Clear();
                elevatorFloor.Add(j);
            }
            Console.WriteLine("---------------------------------------------------");

            Console.WriteLine("Please click the floor goes on ");
            clickFloors.Clear();
            clickFloor();
            return (whereYouAre[whereYouAre.Count - 1], whereDoYouGo);
        }
        (int, int) moveUp(int whereDoYouGo, List<int> whereYouAre)
        {
            bool getOff = true;
            Console.WriteLine("Elevator going up:");
            for (int i = whereYouAre[whereYouAre.Count - 1]; i <= clickFloors[clickFloors.Count - 1]; i++)
            {

                if (clickFloors.Contains(i))
                {
                    Console.WriteLine($"Elevator at:{i}.floor");
                    if (i == whereDoYouGo)
                    {
                        getOff = false;
                        Console.WriteLine("You can get off");

                    }
                }
                else
                {
                    Console.WriteLine("Elevator not waiting this floor...!");
                }
            }
            Console.WriteLine("Now elevator going down ");
            for (int j = clickFloors[clickFloors.Count - 1]; j >= clickFloors[0]; j--)
            {

                if (clickFloors.Contains(j))
                {
                    Console.WriteLine($"Elevator at:{j}.floor");
                    if (getOff && j == whereDoYouGo)
                    {
                        Console.WriteLine("You can get off");

                    }

                }
                else
                {
                    Console.WriteLine($"Elevator not waiting this floor:{j}...!");
                }
                whereYouAre.Add(j);
                elevatorFloor.Clear();
                elevatorFloor.Add(j);
            }
            Console.WriteLine("---------------------------------------------------");

            Console.WriteLine("Please click the floor goes on ");
            clickFloors.Clear();
            clickFloor();
            return (whereYouAre[whereYouAre.Count - 1], whereDoYouGo);
        }

        clickFloor();

    }
}




// asansor tum islemleri tamamladiktan sonra en son bulundugu kattan yukari dogru ciksin
// yukari dogru cikarken veya yukardan assagi iniyorsa bu sirada kullanicidan aldigi gidilecek kat
//kisminda indirsin ve 'yo can get off' yazisini versin.
//bunlari en son nextdown ve next up'tan guncel parametreleri alip yeni fonksiyonlara atayip, yeni fonksiyon
//yazip whredoyougo biligsini alip o fonksiyonlari cagirarak surekli islemleri tekrar ettirebilirsin.

//ya da en son elevatorProcess() cagirdigin kisimda process fonksiyonuna gidip duzenlemeler yapabilirsin.



//ayriyetten bu kodun calismasinda revize etmek istersen soyle bir problem var;
//tuslamalari listeye aldiktan sonra cikilacak kat bilgisini de aliyoruz sonrasinda
//eger ki gidilecek kat o listede yoksa hata aliyoruz almamamiz lazim
//cozumu csudur;
//orda bilgiyi aldiktan sonra listeye o bilgiyi ata ve islemlere devam et 
//fakat listeyi globalde 15 eleman ile sinirladin.Kat bilgisini fonksiyonda alirken
//ayni zamanda l,ste eleman sayisini da 1 arttir ki o kat listede yoksa ekle ve isleme devam edebil.Yarin bunu arastir...


//eklemek istersen en basta kullanicinin bulundugu kati da random alabilirsin.

//104. satirdan sonra ya da karistirmamak icin bunlari yeni fonksiyonlarda ayarla sonra elevatorcalla parametre ata ve cagir
//clickFloors.Add(whereYouAre[whereYouAre.Count - 1]);
//Console.WriteLine($"New list with last call:{string.Join(",", clickFloors)}");