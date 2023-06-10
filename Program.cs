// See https://aka.ms/new-console-template for more information
using System;
using System.Timers;
namespace CrudTemplate
{
    public class Program
    {
        public static void Main()
        {
           /* System.Timers.Timer  aTimer = new System.Timers.Timer(2000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;*/

            Console.WriteLine("Donner le nom du projet {0}",DateTime.Now.ToString());
            
                /*var startTimeSpan = TimeSpan.Zero;
                var periodTimeSpan = TimeSpan.FromMinutes(1);
                var timer = new System.Threading.Timer((e) =>
                {
                    Task.Run(async () => {
                        try
                    {
                        Console.WriteLine("timer en fonction");

                    }
                    catch (Exception ex)
                    {

                    }
                    });

                }, null, startTimeSpan, periodTimeSpan);*/
            
            string Project = Console.ReadLine();
            Console.WriteLine("Donner le nom de l'entité");
            string Entity = Console.ReadLine();
            List<string> champs=new List<string>();
            List<string> types = new List<string>();
            string champ = "";
            string type = "";
            while (champ != "f")
            {
                Item item= new Item();
                Console.WriteLine("Donner le nom du champ:(Taper f pour terminer)");
                champ = Console.ReadLine();

                Console.WriteLine("son Type");
                type = Console.ReadLine();

                if (champ != "f" &&  type!="r")
                {
                    
                    champs.Add(champ);
                    types.Add(type);
                }
                
            }
            Crud.CreatController(Entity, Project);
            Crud.CreateBllEntity(Entity, Project);
            Crud.CreateDalEntity(Entity, champs, Project,types);
            Crud.CreateEntity(Entity, champs, Project,types);
            Console.WriteLine("Crud crée avec succes");

            
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }
        }
    }