// See https://aka.ms/new-console-template for more information
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Net;

internal class Program
{
    private static void Main(string[] args)
    {
        int build = 15;
        List<int> clickFloors = new List<int>();


        List<int> elevatorFloor = new List<int>();//Asansorun çağırıldığı işlem kısmında içersinde başlangıc konumu ata
        List<int> oddNumbers = new List<int>();
        List<int> evenNumbers = new List<int>();


        int clickFloor()
        {
            Console.WriteLine("Other people clicks");
            while (true)
            {
                string? input = Console.ReadLine();
                int select;
                if ((!int.TryParse(input, out select)) || select < 0 || select > build)
                {
                    Console.WriteLine("This is not a valid number");
                    continue;


                }
                else
                {
                    clickFloors.Add(select);

                    if (clickFloors.Count < 14)
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("-------------------------------------------");
                        Console.WriteLine("People clicked where they want to go.\nYou are the last person who will click ");
                        int lastclick;
                        while (true)
                        {


                            string? newinput = Console.ReadLine();


                            if ((int.TryParse(newinput, out lastclick)) && lastclick >= 0 && lastclick <= build)
                            {
                                clickFloors.Add(lastclick);
                                Console.WriteLine("-------------------------------------------");
                                Console.WriteLine("Clicked list:" + string.Join(",", clickFloors));
                                change();
                                delete();
                                Console.WriteLine("Last clicked list:" + string.Join(",", clickFloors));
                                Console.WriteLine("-------------------------------------------");
                                oddOrEven(lastclick);
                                Console.WriteLine("-------------------------------------------");
                                return lastclick;

                            }
                            else
                            {

                                Console.WriteLine("This is not a valid number. Please enter a number between 0 and " + build);
                            }

                        }
                    }
                }

            }

        }
        void change()
        {
            for (int i = 0; i < clickFloors.Count; i++)
            {
                for (int j = i + 1; j < clickFloors.Count - 1; j++)
                {
                    if (clickFloors[i] > clickFloors[j])

                    {
                        int temp = clickFloors[i];
                        clickFloors[i] = clickFloors[j];
                        clickFloors[j] = temp;

                    }

                }

            }
            Console.WriteLine("Sorted floors:" + string.Join(",", clickFloors));
        }
        void delete()
        {
            List<int> delete = new List<int>();
            for (int i = 0; i < clickFloors.Count; i++)
            {
                bool control = true;
                for (int j = 0; j < delete.Count; j++)
                {
                    if (clickFloors[i] == delete[j])
                    {
                        control = false;
                        break;

                    }
                }
                if (control)
                {
                    delete.Add(clickFloors[i]);

                }
            }
            clickFloors = delete;
            Console.WriteLine("Deleted floors:" + string.Join(",", clickFloors));
        }
        void oddOrEven(int lastclick)
        {
            oddNumbers.Clear();
            evenNumbers.Clear();
            Console.WriteLine("Odd and even florrs are detach ");
            for (int i = 0; i < clickFloors.Count; i++)
            {
                if (clickFloors[i] % 2 == 0)
                {
                    evenNumbers.Add(clickFloors[i]);

                }
                else
                {
                    oddNumbers.Add(clickFloors[i]);
                }
            }
            Console.WriteLine($"Odd numbers: {string.Join(",", oddNumbers)}");
            Console.WriteLine($"Even numbers: {string.Join(",", evenNumbers)}");
            elevatorChoose(lastclick);
        }
        void elevatorChoose(int lastclick)
        {
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("[1].elevator is just going odd floors\n[2].elevator is just going even floors\n[3].elevator is going every floor");
            Console.WriteLine("Select the elevator: [1],[2],[3]");

            string? selectElevator = Console.ReadLine();
            int select;
            if ((!int.TryParse(selectElevator, out select)) || select < 1 || select > 3)
            {
                Console.WriteLine("Please choose the valid elevator!!");
                elevatorChoose(lastclick);//geçerli sayı girmediyse hop yallah geri elevatorChoose();'a

            }
            else if (select == 1 && lastclick % 2 == 0)
            {
                Console.WriteLine("You can not call [1].\nBecause your floor is 'EVEN'.\nCall [2] or [3] ");
                elevatorChoose(lastclick);

            }
            else if (select == 2 && lastclick % 2 != 0)
            {
                Console.WriteLine("You can not call [2].\nBecause your floor is 'ODD'.\nCall [1] or [3] ");
                elevatorChoose(lastclick);

            }
            else
            {//seçtiği asansore gönder işlemler başlasın                
                elevatorStarsRandomFloor(lastclick, select);

            }

        }
        void elevatorStarsRandomFloor(int lastclick, int select)
        {
            if (select == 1)
            {
                Random random = new Random();
                elevatorFloor.Add(oddNumbers[random.Next(0, oddNumbers.Count)]); Console.WriteLine("-------------------------------------------");

                Console.WriteLine($"Elevator is waiting: {string.Join(",", elevatorFloor)}");
                Console.WriteLine($"You are waiting: {lastclick}");
                if ((lastclick == elevatorFloor[0]) && (lastclick % 2 != 0 && elevatorFloor[0] % 2 != 0))//asansor ve son çağıran aynı kattaysa napcak ? direkt gideceği yeri seçtirelim devam etsin 
                {
                    Console.WriteLine("You are at the same floor with elevator and this floor is 'ODD'");
                    elevatorGoesUp1(lastclick);//;BURAYA EL AT
                }
                else
                {
                    elevatorComingFromElevator1ForTakingYou(lastclick);
                }

            }
            if (select == 2)
            {
                Random random = new Random();
                elevatorFloor.Add(evenNumbers[random.Next(0, evenNumbers.Count)]);
                Console.WriteLine("-------------------------------------------");

                Console.WriteLine($"Elevator is waiting: {string.Join(",", elevatorFloor)}");
                Console.WriteLine($"You are waiting: {lastclick}");
                Console.WriteLine("-------------------------------------------");

                if ((lastclick == elevatorFloor[0]) && (lastclick % 2 != 0 && elevatorFloor[0] % 2 != 0))//asansor ve son çağıran aynı kattaysa napcak ? direkt gideceği yeri seçtirelim devam etsin 
                {
                    Console.WriteLine("You are at the same floor with elevator and this floor is 'ODD'");
                    elevatorGoesUp2(lastclick);



                }
                else
                {
                    elevatorComingFromElevator2ForTakingYou(lastclick);
                }
            }
            if (select == 3)
            {
                Random random = new Random();
                elevatorFloor.Add(clickFloors[random.Next(0, clickFloors.Count)]); Console.WriteLine("-------------------------------------------");


                Console.WriteLine($"Elevator is waiting: {string.Join(",", elevatorFloor)}");
                Console.WriteLine($"You are waiting: {lastclick}");
                if ((lastclick == elevatorFloor[0]) && (lastclick % 2 != 0 && elevatorFloor[0] % 2 != 0))//asansor ve son çağıran aynı kattaysa napcak ? direkt gideceği yeri seçtirelim devam etsin 
                {
                    Console.WriteLine("You are at the same floor with elevator and this floor is 'ODD'");
                    elevatorGoesUp3(lastclick);
                }
                else
                {
                    elevatorComingFromElevator3ForTakingYou(lastclick);
                }
            }


        }
        void elevatorComingFromElevator1ForTakingYou(int lastclick)
        {
            if (lastclick < elevatorFloor[0])
            {
                for (int i = elevatorFloor[0]; i >= oddNumbers[0]; i -= 2)
                {
                    if (oddNumbers.Contains(i) && i == lastclick)
                    {
                        Console.WriteLine($"You can get in{i}");
                        oddNumbers.Remove(i);  // i, listenin elemanı
                    }
                    else if (oddNumbers.Contains(i) && i != lastclick)
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                    }
                    else
                    {
                        Console.WriteLine($"Elevator passing: {i}. floor");
                    }
                }
                elevatorGoesUp1(lastclick);
            }


            else if (lastclick > elevatorFloor[0])
            {
                for (int i = elevatorFloor[0]; i <= oddNumbers.Count-1; i += 2)
                {
                    if (oddNumbers.Contains(i) && i == lastclick)
                    {
                        Console.WriteLine($"You can get in{i}");
                        oddNumbers.Remove(i);  // i, listenin elemanı
                    }
                    else if (oddNumbers.Contains(i) && i != lastclick)
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                    }
                    else
                    {
                        Console.WriteLine($"Elevator passing: {i}. floor");
                    }
                }
                elevatorGoesDown1(lastclick);
            }
            
        }
        void elevatorComingFromElevator2ForTakingYou(int lastclick)
        {
            if (lastclick < elevatorFloor[0])
            {
                for (int i = elevatorFloor[0]; i >= evenNumbers[0]; i -= 2)
                {
                    if (evenNumbers.Contains(i) && i == lastclick)
                    {
                        Console.WriteLine($"You can get in{i}");
                        evenNumbers.Remove(i);  // i, listenin elemanı
                    }
                    else if (evenNumbers.Contains(i) && i != lastclick)
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                    }
                    else
                    {
                        Console.WriteLine($"Elevator passing: {i}. floor");
                    }
                }
                elevatorGoesUp2(lastclick);
            }

            else if (lastclick > elevatorFloor[0])
            {
                for (int i = elevatorFloor[0]; i <= evenNumbers.Count-1; i += 2)
                {
                    if (evenNumbers.Contains(i) && i == lastclick)
                    {
                        Console.WriteLine($"You can get in{i}");
                        evenNumbers.Remove(i);  // i, listenin elemanı
                    }
                    else if (evenNumbers.Contains(i) && i != lastclick)
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                    }
                    else
                    {
                        Console.WriteLine($"Elevator passing: {i}. floor");
                    }
                }

               elevatorGoesDown2(lastclick);
            }
            
        }
        void elevatorComingFromElevator3ForTakingYou(int lastclick)
        {
            if (lastclick < elevatorFloor[0])
            {
                for (int i = elevatorFloor[0]; i >= clickFloors[0]; i -= 2)
                {
                    if (clickFloors.Contains(i) && i == lastclick)
                    {
                        Console.WriteLine($"You can get in{i}");
                        clickFloors.Remove(i);  // i, listenin elemanı
                    }
                    else if (clickFloors.Contains(i) && i != lastclick)
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                    }
                    else
                    {
                        Console.WriteLine($"Elevator passing: {i}. floor");
                    }
                }
                elevatorGoesUp3(lastclick);
            }

            else if (lastclick > elevatorFloor[0])
            {
                for (int i = elevatorFloor[0]; i <= clickFloors.Count-1; i += 2)
                {
                    if (clickFloors.Contains(i) && i == lastclick)
                    {
                        Console.WriteLine($"You can get in{i}");
                        clickFloors.Remove(i);  // i, listenin elemanı
                    }
                    else if (clickFloors.Contains(i) && i != lastclick)
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                    }
                    else
                    {
                        Console.WriteLine($"Elevator passing: {i}. floor");
                    }
                }
                elevatorGoesDown3(lastclick);
            }
            

        }
        void elevatorGoesUp1(int lastclick)
        {

            Console.WriteLine("Which floor do you want to go ?");
            string? selectFloor = Console.ReadLine();
            int select;
            if ((!int.TryParse(selectFloor, out select)) || select % 2 == 0 || select > 15)
            {
                Console.WriteLine("Please choose the valid floor!!\nBecause this elevator just going 'ODD' floors");
                elevatorGoesUp1(lastclick);//geçerli sayı girmediyse hop yallah geri elevatorChoose();'a

            }
            else {
                for (int i = oddNumbers[0]; i <= oddNumbers.Count- 1;i++)
                {
                    if (oddNumbers.Contains(i) && i == lastclick)
                    {
                        Console.WriteLine($"You can get in{i}");
                        oddNumbers.Remove(i);  // i, listenin elemanı
                    }
                    else if (oddNumbers.Contains(i) && i != lastclick)
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                    }
                    else
                    {
                        Console.WriteLine($"Elevator passing: {i}. floor");
                    }
                }

            }
            clear();
        }
        void elevatorGoesDown1(int lastclick)
        {
            Console.WriteLine("Which floor do you want to go ?");
            string? selectFloor = Console.ReadLine();
            int select;
            if ((!int.TryParse(selectFloor, out select)) || select % 2 == 0 || select > 15)
            {
                Console.WriteLine("Please choose the valid floor!!\nBecause this elevator just going 'ODD' floors");
                elevatorGoesUp1(lastclick);//geçerli sayı girmediyse hop yallah geri elevatorChoose();'a

            }
            else
            {
                for (int i = oddNumbers.Count-1; i >= oddNumbers[0];i--)
                {
                    if (oddNumbers.Contains(i) && i == lastclick)
                    {
                        Console.WriteLine($"You can get in{i}");
                        oddNumbers.Remove(i);  // i, listenin elemanı
                    }
                    else if (oddNumbers.Contains(i) && i != lastclick)
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                    }
                    else
                    {
                        Console.WriteLine($"Elevator passing: {i}. floor");
                    }
                }

            }
            clear();
        }


        void elevatorGoesUp2(int lastclick)
        {

            Console.WriteLine("Which floor do you want to go ?");
            string? selectFloor = Console.ReadLine();
            int select;
            if ((!int.TryParse(selectFloor, out select)) || select % 2 != 0 || select > 15)
            {
                Console.WriteLine("Please choose the valid floor!!\nBecause this elevator just going 'EVEN' floors");
                elevatorGoesUp2(lastclick);//geçerli sayı girmediyse hop yallah geri elevatorChoose();'a

            }
            else
            {
                for (int i = evenNumbers[0]; i <= evenNumbers.Count - 1; i++)
                {
                    if (evenNumbers.Contains(i) && i == lastclick)
                    {
                        Console.WriteLine($"You can get in{i}");
                        evenNumbers.Remove(i);  // i, listenin elemanı
                    }
                    else if (evenNumbers.Contains(i) && i != lastclick)
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                    }
                    else
                    {
                        Console.WriteLine($"Elevator passing: {i}. floor");
                    }
                }

            }
            clear();
        }
        void elevatorGoesDown2(int lastclick)
        {
            Console.WriteLine("Which floor do you want to go ?");
            string? selectFloor = Console.ReadLine();
            int select;
            if ((!int.TryParse(selectFloor, out select)) || select % 2 != 0 || select > 15)
            {
                Console.WriteLine("Please choose the valid floor!!\nBecause this elevator just going 'ODD' floors");
                elevatorGoesUp1(lastclick);//geçerli sayı girmediyse hop yallah geri elevatorChoose();'a

            }
            else
            {
                for (int i = evenNumbers.Count - 1; i >= evenNumbers[0]; i--)
                {
                    if (evenNumbers.Contains(i) && i == lastclick)
                    {
                        Console.WriteLine($"You can get in{i}");
                        evenNumbers.Remove(i);  // i, listenin elemanı
                    }
                    else if (evenNumbers.Contains(i) && i != lastclick)
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                    }
                    else
                    {
                        Console.WriteLine($"Elevator passing: {i}. floor");
                    }
                }

            }
            clear();
        }


        void elevatorGoesUp3(int lastclick)
        {

            Console.WriteLine("Which floor do you want to go ?");
            string? selectFloor = Console.ReadLine();
            int select;
            if ((!int.TryParse(selectFloor, out select)) || select > 15)
            {
                Console.WriteLine("Please choose the valid floor!!\nBecause this elevator working '0 to 15'");
                elevatorGoesUp2(lastclick);//geçerli sayı girmediyse hop yallah geri elevatorChoose();'a

            }
            else
            {
                for (int i = clickFloors[0]; i <= clickFloors.Count - 1; i++)
                {
                    if (clickFloors.Contains(i) && i == lastclick)
                    {
                        Console.WriteLine($"You can get in{i}");
                        clickFloors.Remove(i);  // i, listenin elemanı
                    }
                    else if (clickFloors.Contains(i) && i != lastclick)
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                    }
                    else
                    {
                        Console.WriteLine($"Elevator passing: {i}. floor");
                    }
                }

            }
            clear();
        }
        void elevatorGoesDown3(int lastclick)
        {
            Console.WriteLine("Which floor do you want to go ?");
            string? selectFloor = Console.ReadLine();
            int select;
            if ((!int.TryParse(selectFloor, out select)) || select % 2 != 0 || select > 15)
            {
                Console.WriteLine("Please choose the valid floor!!\nBecause this elevator just working '0 to 15'");
                elevatorGoesUp1(lastclick);//geçerli sayı girmediyse hop yallah geri elevatorChoose();'a

            }
            else
            {
                for (int i = clickFloors.Count - 1; i >= clickFloors[0]; i--)
                {
                    if (clickFloors.Contains(i) && i == lastclick)
                    {
                        Console.WriteLine($"You can get in{i}");
                        clickFloors.Remove(i);  // i, listenin elemanı
                    }
                    else if (clickFloors.Contains(i) && i != lastclick)
                    {
                        Console.WriteLine($"Elevator at: {i}. floor");
                    }
                    else
                    {
                        Console.WriteLine($"Elevator passing: {i}. floor");
                    }
                }

            }
            clear();
        }




        void clear()
        {
            Console.WriteLine("-------------------------------------------");
            clickFloors.Clear();
            elevatorFloor.Clear();
            oddNumbers.Clear();
            evenNumbers.Clear();
            clickFloor();

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