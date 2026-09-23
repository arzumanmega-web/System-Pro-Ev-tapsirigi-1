using System.Diagnostics;

namespace System_Pro_Ev_tapsirigi_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool istrue = false;
            bool breaks = true;
            bool threadd1 = true;
            var thread2 = new Thread(() =>
            {
                while (threadd1)
                {
                    var command = Console.ReadKey().Key;
                    Console.Clear();
                    if (command == ConsoleKey.Enter)
                    {
                        istrue = true;
                    }

                    while (istrue)
                    {
                        
                        Console.ReadKey();
                        Console.WriteLine("\n1.Start Process\n2.Kill Process\n0.Exit program");
                        var choice = Console.ReadLine();
                        if (choice == "1")
                        {
                            Console.WriteLine("Enter Start process name.exe or path");
                            var proses = Console.ReadLine();
                            try
                            {
                                Process.Start(proses);
                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine("Wrong process name or path");
                                Console.ReadKey();
                            }
                            
                            istrue = false;
                            breaks = true;
                            break;
                        }
                        else if (choice == "2")
                        {
                            Console.WriteLine("Enter Kill process name");
                            var proses = Console.ReadLine();
                            var pross = Process.GetProcesses();
                            foreach (var item in pross)
                            {
                                if (item.ProcessName.Contains(proses))
                                {
                                    item.Kill();
                                }
                            }
                            istrue = false;
                            breaks = true;
                            break;
                        }
                        else if (choice == "0") { breaks = false; istrue = false; threadd1 = false; }
                        else
                        {
                            Console.WriteLine("Wrong choice...!");
                        }

                    }
                }
            });

            var thread1 = new Thread(() =>
            {
                while (threadd1)
                {

                    while (breaks)
                    {
                        Thread.Sleep(500);
                        Console.Clear();

                        DateTime? GetStartTime(Process p)
                        {
                            try { return p.StartTime; } //error olmasa bu try-i,yeni ki oz tarixini
                            catch { return null; } //error olarsa null qaytaracaq
                        }

                        var proses = (from i in Process.GetProcesses()
                                      let error_time = GetStartTime(i)
                                      where error_time != null
                                      orderby i.StartTime descending
                                      select i).Take(20);
                        Console.WriteLine("-------------------------------------------");
                        foreach (var item in proses)
                        {
                            Console.WriteLine(item.ProcessName);

                        }
                        Console.WriteLine("-------------------------------------------");
                        Console.WriteLine("\nMenu Choice click to enter");
                        if (istrue)
                        {
                            breaks = false;
                        }


                    }
                }
            });
            thread2.Start();
            thread1.Start();
        }
    }
}
