using System;
using System.Collections;
using System.Collections.Generic;

namespace ToDo_List_Assignment
{

    class Message
    {
        string name,title,message;
        int priority;
        public Message()
        {
            name = title = message = "";
            priority = 0;
        }
        public Message(string name,string title,string message,int priority)
        {
            this.name = name;
            this.title = title;
            this.message = message;
            this.priority = priority;
        }

        public string Name { get => name; set => name = value; }
        public string Title { get => title; set => title = value; }
        public string Msg { get => message; set => message = value; }
        public int Priority { get => priority; set => priority = value; }


    }

    class Task
    {
        int state ;
        DateTime dt;
        Message message;

        public int State { get => state; set => state = value; }
        public DateTime Dt { get => dt; set => dt = value; }
        public Task()
        {
            state = 1;
            message = new Message();
        }
        public Task(DateTime d)
        {
            state = 1;
            dt = d;
            message = new Message();
        }
        public Task(Message m, DateTime d)
        {
            state = 1;
            dt = d;
            message = m;
        }

        public void setName()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter name:");
            Console.ForegroundColor = ConsoleColor.White;
            message.Name = Console.ReadLine();
        }

        public void setTitle()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter Title:");
            Console.ForegroundColor = ConsoleColor.White;
            message.Title = Console.ReadLine();
        }

        public void setMsg()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter Description:");
            Console.ForegroundColor = ConsoleColor.White;
            message.Msg = Console.ReadLine();
        }
        public void setPriority()
        {
            int p;
            bool valid = false;
            do
            {
                valid = true;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Do you want to set Priority to Task?");
                Console.WriteLine("Enter 0 - 1 - 2 - 3 (3 is Max priority)");
                Console.ForegroundColor = ConsoleColor.White;
                try { p = Convert.ToInt32(Console.ReadLine()); }
                catch {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Priority..!");
                    Console.ForegroundColor = ConsoleColor.White; 
                    valid = false;
                    continue;
                }
                if (p < 0 || p > 3)
                {
                    valid = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Priority..!");
                    Console.ForegroundColor = ConsoleColor.White;
                }   
                else
                    message.Priority = p;
            } while (!valid);
            Console.WriteLine();
        }

        public void getMessage()
        {
            Console.WriteLine("          - Title: {0}", message.Title);
            Console.WriteLine("          - Name: {0}", message.Name);
            Console.WriteLine("          - Description: {0}", message.Msg);
            Console.WriteLine("          - Task Priority: {0}", message.Priority);
        }


        public bool SearchName(string name)
        { 
              if (name == message.Name)
                    return true;
            return false;
        }

    }

    interface list
    {
        void createNew();
        void view(int id);
        void view();
        void edit();
        void deleteMsg();

    }

    class MyList:list
    {
        int id;
        string listName;
        List<Task> lt;
        

        public MyList()
        {
            id = 0;
            listName = "";
            lt = new List<Task>();
        }
        public MyList(string nm)
        {
            id = 0;
            listName = nm;
            lt = new List<Task>();
        }

        public MyList(string nm,Task t)
        {
            id = 0;
            listName = nm;
            lt = new List<Task>();
            lt.Add(t);
            Message m2 = new Message("xyz", "Create Repo", "structure all content and push the code", 3);
            lt.Add(new Task(m2, new DateTime(2022,10,10)));
        }

        public int getTaskCount() { return lt.Count; }
        public void createNew()
        {
            Task t = new Task();
            bool v = false;
            DateTime a = DateTime.Now;
            do
            {
                v = true;
                bool valid = false;
                do
                {
                    valid = true;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Enter date: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    try { DateTime.TryParse(Console.ReadLine(), out a); }
                    catch { 
                        valid = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong Date Format..!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                } while (!valid);

                if (a > DateTime.Now)
                {
                    t.Dt = a;
                    v = true;
                }
                    
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Date..! Date should be greater than today!");
                    Console.ForegroundColor = ConsoleColor.White;
                    v = false;
                }
            } while (!v);
            t.setName();
            t.setTitle();
            t.setMsg();
            t.setPriority();
            lt.Add(t);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Successfully Added the Task");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void deleteMsg()
        {
            if (lt.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("List is Empty..!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter the Name of Task that need to Delete: ");
            Console.ForegroundColor = ConsoleColor.White;
            bool flg = false;
            string nm = Console.ReadLine();
            foreach (var i in lt)
            {
                if (i.SearchName(nm))
                {
                    flg = true;
                    lt.Remove(i);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Task {0} deleted successfully..!", nm);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
            }
            if (!flg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter valid name..!");
                Console.ForegroundColor = ConsoleColor.White;
            }
                
        }

        

        public void edit()
        {
            if (lt.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("List is Empty..!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter the Name of Task that need to Edit: ");
            Console.ForegroundColor = ConsoleColor.White;
            bool flg = false;
            string nm = Console.ReadLine();
            foreach (var i in lt)
            {
                
                if (i.SearchName(nm))
                {
                    do
                    {
                        int opt;
                        Console.WriteLine();
                        Console.WriteLine("          ------------------------ ");
                        Console.WriteLine("         |      1.Name            |");
                        Console.WriteLine("         |      2.Title           |");
                        Console.WriteLine("         |      3.Description     |");
                        Console.WriteLine("         |      4.Priority        |");
                        Console.WriteLine("         |      5.Task State      |");
                        Console.WriteLine("         |      6.Exit            |");
                        Console.WriteLine("          ------------------------ ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Select field that you want to Edit");
                        Console.ForegroundColor = ConsoleColor.White;

                        try { opt = Convert.ToInt32(Console.ReadLine()); }
                        catch {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Enter In Proper Format!");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue; }
                        if (opt == 6)
                        {
                            flg = true;
                            break;
                        }
                            
                        if (opt < 0 || opt > 6) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Enter Valid option!");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue; }
                            
                        switch(opt)
                        {
                            case 1:
                                i.setName();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Name has been modified Successfully..!");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case 2:
                                i.setTitle();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Title has been modified Successfully..!");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case 3:
                                i.setMsg();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Msg description has been modified Successfully..!");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case 4:
                                i.setPriority();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Priority has been modified Successfully..!");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case 5:
                                bool valid = false;
                                do
                                {
                                    valid = true;
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("Task Completed? (Y/N)");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string state = "N";
                                    try { state = Console.ReadLine(); }
                                    catch {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid option!             Provide Y/N");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        valid = false; continue; }
                                    if (state == "Y" && DateTime.Now < i.Dt)
                                    {
                                        i.State = 2; flg = true;
                                        break;
                                    }
                                    if (state == "Y" && DateTime.Now > i.Dt)
                                    {
                                        i.State = 3; flg = true;
                                        break;
                                    }
                                    else if (state == "N" && DateTime.Now > i.Dt)
                                    {
                                        i.State = 0;
                                        flg = true;
                                        break;
                                    }
                                    else if (state == "N" && DateTime.Now < i.Dt)
                                    {
                                        i.State = 1;
                                        flg = true;
                                        break;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid option!         Provide Y/N");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        valid = false; }
                                } while (!valid);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Task Status has been modified Successfully..!");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                        }

                    } while (true) ;
                    break;    
                } 
            }
            if (!flg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name Not Found...!       Enter a valid Name..!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Task with Name: {0} Modified Successfully..!", nm);
                Console.ForegroundColor = ConsoleColor.White;
            }
                

        }

        public void view(int para)
        {
            if (lt.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("List is Empty..!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            string[] st = { "Incomplete", "Pending", "Complete","Late" };
            
            if (para > 0 && para <= lt.Count)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" __________________________________________________");
                Console.WriteLine("|           List name: {0}", listName);
                Console.WriteLine("|           List id: {0}", id);
                Console.WriteLine("|__________________________________________________");
                Console.WriteLine();
                Console.WriteLine("        Tasks are Given Below: ");
                lt[para - 1].getMessage();
                Console.WriteLine("          - State of the task: {0}", st[lt[para - 1].State]);
                Console.WriteLine("          - Expected Date to be Completed: {0}", lt[para - 1].Dt);
                Console.WriteLine("     _____________________________________________________________________");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID");
                Console.ForegroundColor = ConsoleColor.White;
            }
                
        }

        public void view()
        {
            if (lt.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("List is Empty..!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            //lt.Sort((l1, l2) => l1.State.CompareTo(l2.State));
            string[] st = { "Incomplete", "Pending", "Complete","Late" };
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine("|           List name: {0}", listName);
            Console.WriteLine("|           List id: {0}", id);
            Console.WriteLine("|__________________________________________________");
            Console.WriteLine();
            Console.WriteLine("        Tasks are Given Below: ");
            foreach (var item in lt)
            {
                
                item.getMessage();
                Console.WriteLine("          - State of the task: {0}", st[item.State]);
                Console.WriteLine("          - Expected Date to be Completed: {0}", item.Dt);
                Console.WriteLine("     _____________________________________________________________________");
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void viewByOrder()
        {
            if (lt.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("List is Empty..!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            string[] st = { "Incomplete", "Pending", "Complete" , "Late" };
            lt.Sort((a, b) => a.Dt.CompareTo(b.Dt));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine("|           List name: {0}", listName);
            Console.WriteLine("|           List id: {0}", id);
            Console.WriteLine("|__________________________________________________");
            Console.WriteLine();
            Console.WriteLine("        Tasks ordered by Date are Given Below: ");
            foreach (var item in lt)
            {
                item.getMessage();
                Console.WriteLine("          - State of the task: {0}", st[item.State]);
                Console.WriteLine("          - Expected Date to be Completed: {0}", item.Dt);
                Console.WriteLine("     _____________________________________________________________________");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is Todo List Assignment Program");

            //Adding some tasks
            Message m1= new Message("Ap","Xerox","5 Copies each for A,B",2);

            //create a new List
            MyList l = new MyList("My First List",new Task(m1, new DateTime(2021,12,10)));

            while (true)
            {
                int opt;
                Console.WriteLine();
                Console.WriteLine("          ************************* ");
                Console.WriteLine("         |    1.Create A New Task  |");
                Console.WriteLine("         |    2.View by ID         |");
                Console.WriteLine("         |    3.View All           |");
                Console.WriteLine("         |    4.View By Order      |");
                Console.WriteLine("         |    5.Edit A Task        |");
                Console.WriteLine("         |    6.Delete a Task      |");
                Console.WriteLine("         |    7.Exit               |");
                Console.WriteLine("          ************************* ");
                bool valid = false;
                
                do
                {
                    Console.WriteLine("Enter option: ");
                    valid = true;
                    try
                    {
                        opt = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        valid = false;
                        opt = 7;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter in Proper Format..!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();          
                    }
                } while (!valid);

                if (opt == 7)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Have a Good Day..!");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                    

                switch(opt)
                {
                    case 1:
                        l.createNew();
                        break;
                    case 2:
                        if (l.getTaskCount() == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("List Is Empty.!");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                        Console.WriteLine("Enter the Index");
                        int id=0;
                        try { id = Convert.ToInt32(Console.ReadLine()); }
                        catch {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Enter Index as a Integer");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                            
                            
                        l.view(id);
                        break;
                    case 3:
                        l.view();
                        break;
                    case 4:
                        l.viewByOrder();
                        break;
                    case 5:
                        l.edit();
                        break;
                    case 6:
                        l.deleteMsg();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter Valid Option..!");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                }
            }
        }
    }
}
