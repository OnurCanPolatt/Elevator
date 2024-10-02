// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Net;

internal class Program
{
    private static void Main(string[] args)
    {
        int build = 15;//build has a 10 floor
        int whereYouAre = 6;
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

        int elevatorComingFrom()
        {
            Random random = new Random();
            int randomindex = random.Next(clickFloors.Count);
            elevatorFloor.Add(clickFloors[randomindex]);
            Console.WriteLine($"Elevator stays:{elevatorFloor[0]}.floor");
            elevatorCall();
            return elevatorFloor[0];
        }
        int elevatorCall()

        {
            Console.WriteLine($"Your floor:{whereYouAre}");
            Console.WriteLine("Elevator Coming:");
            if (elevatorFloor[0] < whereYouAre)
            {
                for (int i = elevatorFloor[0]; i <= whereYouAre; i++)
                {
                    Console.WriteLine(i);
                    elevatorFloor[0] = i;

                }
            }
            else if (elevatorFloor[0] > whereYouAre)
            {
                for (int i = elevatorFloor[0]; i >= whereYouAre; i--)
                {
                    Console.WriteLine(i);
                    elevatorFloor[0] = i;

                }
            }
            else
            {
                Console.WriteLine("You already at your floor.\n You can get in and click wheredoyougo");
                elevatorProcess(whereYouAre);
            }
            Console.WriteLine("You can get in.");
            elevatorProcess(whereYouAre);
            return elevatorFloor[0];

        }

        int elevatorProcess(int whereYouAre)
        {
            while (true)
            {
                Console.WriteLine("Where do you want to go?");
                if (!int.TryParse(Console.ReadLine(), out int whereDoYouGo))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }
                if (whereDoYouGo == whereYouAre)
                {
                    Console.Write($"You are already at your floor.{whereYouAre}.\nAgain click another floor.");
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
                int downSubstrack = closeDown.HasValue ? Math.Abs(whereDoYouGo - closeDown.Value) : int.MaxValue;
                int upSubstract = closeUp.HasValue ? Math.Abs(whereDoYouGo - closeUp.Value) : int.MaxValue;

                Console.WriteLine($"downSubstrack value: {downSubstrack}");
                Console.WriteLine($"upSubstract value: {upSubstract}");

                return elevatorGoes(whereDoYouGo, downSubstrack, upSubstract);

            }
        }

        int elevatorGoes(int targetFloor, int downSubstrack, int upSubstract)
        {

            if (downSubstrack >= upSubstract)
            {

                return moveUp(targetFloor);

            }

            else if (downSubstrack < upSubstract)
            {
                return moveDown(targetFloor);
            }
            
            return targetFloor;
        }
        int moveDown(int targetFloor)
        {
            Console.WriteLine("Elevator going down:");
            for (int i = whereYouAre; i >= targetFloor; i--)
            {
                if (clickFloors.Contains(i))
                {
                    Console.WriteLine($"Elevator at:{i}.floor");
                    if (i == targetFloor)
                    {
                        Console.WriteLine("You can get off");
                        whereYouAre = i;
                    }
                }
            }
            return nextDown(whereYouAre);

        }
        int moveUp(int targetFloor)
        {
            Console.WriteLine("Elevator going up:");
            for (int i = whereYouAre; i <= targetFloor; i++)
            {
                if (clickFloors.Contains(i))
                {
                    Console.WriteLine($"Elevator at:{i}.floor");
                    if (i == targetFloor)
                    {
                        Console.WriteLine("You can get off");
                        whereYouAre = i;
                    }
                }
            }

            return nextUp(whereYouAre);          
        }
        int nextUp(int whereYouAre)
        {
            Console.WriteLine("Elevator continue to going up");
            for (int i = whereYouAre; i <= clickFloors.Count; i++)
            {
                Console.WriteLine($"Elevator at:{i}.floor");
                whereYouAre = i;

            }
            Console.WriteLine("Now,elevator going from last floor to initial floor:");
            for (int j = clickFloors.Count - 1; j >= 0; j--)
            {

                Console.WriteLine($"Elevator at:{j}.floor");
                whereYouAre = j;

            }
            return elevatorProcess(whereYouAre);
        }
        int nextDown(int whereYouAre)
        {
            Console.WriteLine("Elevator continue to going down");
            for (int i = whereYouAre; i >= clickFloors[0]; i--)
            {
                Console.WriteLine($"Elevator at:{i}.floor");
                whereYouAre = i;

            }
            
            Console.WriteLine("Now,elevator going from initial floor to last floor:");
            for (int j = clickFloors[0]; j <= clickFloors.Count; j++)
            {

                Console.WriteLine($"Elevator at:{j}.floor");
                whereYouAre = j;
            }
            return elevatorProcess(whereYouAre);
        }
        
        clickFloor();
    }


}

// asansor tüm işlemleri tamamladıktan sonra en son bulunduğu kattan yukarı doğru çıksın
// yukarı doğru çıkarken veya yukardan aşşağı iniyorsa bu sırada kullanıcıdan aldığı gidilecek kat
//kısmında indirsin ve 'yo can get off' yazısını versin.
//bunları en son nextdown ve next up'tan güncel parametreleri alıp yeni fonksiyonlara atayıp, yeni fonksiyon
//yazıp whredoyougo biligsini alıp o fonksiyonları çağırarak sürekli işlemleri tekrar ettirebilirsin.

//ya da en son elevatorProcess() çağırdığın kısımda process fonksiyonuna gidip düzenlemeler yapabilirsin.



//ayrıyetten bu kodun çalışmasında revize etmek istersen şöyle bir problem var;
//tuşlamaları listeye aldıktan sonra çıkılacak kat bilgisini de alıyoruz sonrasında
//eğer ki gidilecek kat o listede yoksa hata alıyoruz almamamız lazım
//çözümü şudur;
//orda bilgiyi aldıktan sonra listeye o bilgiyi ata ve işlemlere devam et 
//fakat listeyi globalde 15 eleman ile sınırladın.Kat bilgisini fonksiyonda alırken
//aynı zamanda l,ste eleman sayısını da 1 arttır ki o kat listede yoksa ekle ve işleme devam edebil.Yarın bunu araştır...


//eklemek istersen en başta kullanıcının bulunduğu katı da random alabilirsin.