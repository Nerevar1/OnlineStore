using System;
using System.Collections.Generic;

namespace StepikTask
{
    public class Users
    {
        public List<User> ListUsers = new List<User>();
        public void AddUser(User NewUser)
        {
            ListUsers.Add(NewUser);
        }
        public void ShowUsers()
        {
            foreach(User user in ListUsers )
            {
                Console.WriteLine(user.Name);
            }
        }
        public bool CheckUserAllow(string login,string pass,ref User curUser)
        {
            foreach (User user in ListUsers)
            {
                if(user.Name==login&&user.Pass==pass)
                {
                    Console.WriteLine("Доступ разрешён");
                    curUser = user;
                    return true;

                }
            }
            Console.WriteLine("Введён неверный логин или пароль");
            return false;
        }
    }
    public class User
    {
        public string Name;
        public string Pass;
        public List<Order> MyListOrder = new List<Order>();
        public bool IsAdnim = false;
        public User(string name,string pass)
        {
            Name = name;
            Pass = pass;
        }

        public void ShowMyOrder()
        {
            foreach (Order ord in MyListOrder)
            {
                ord.ShowProducts(ord);
            }
        }
    }
    public class Product
    {
        public string Name;
        public decimal Price;
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void Print()
        {
            Console.WriteLine($"{Name} {Price}");
        }
    }

    public class Order
    {
        public List<Product> Products = new List<Product>();
        public decimal FullPrice;
        public Order(List<Product> products)
        {

            foreach (var product in products)
            {
                Products.Add(new Product(product.Name, product.Price)); 
                FullPrice += product.Price;
            }
        }
        public void ShowProducts(Order order)
        {
            Console.WriteLine("Заказ №");
            foreach (Product product in order.Products)
            {
                Console.WriteLine($"{product.Name} {product.Price}");
            }
        }
    }

    public class Store
    {
        public List<Product> Products;

        public List<Product> Basket;

        public List<Order> Orders;

        public Store()
        {
            Products = new List<Product>
            {
                new Product("Хлеб", 25),
                new Product("Молоко", 100),
                new Product("Печенье", 50),
                new Product("Масло", 250),
                new Product("Йогурт", 300),
                new Product("Сок", 80)
            };

            Basket = new List<Product>();
            Orders = new List<Order>();
        }

        public void AddProductToCatalog(string name, int price)
        {
            Products.Add(new Product(name, price));
        }

        public void ShowCatalog()
        {
            Console.WriteLine("Каталог продуктов");
            ShowProducts(Products);
        }

        public void ShowBasket()
        {
            Console.WriteLine("Содержимое корзины");
            ShowProducts(Basket);
        }

        public void AddToBasket(int numberProduct)
        {
            Basket.Add(Products[numberProduct - 1]);
            Console.WriteLine($"Продукт {Products[numberProduct - 1].Name} успешно добавлен в корзину.");
            Console.WriteLine($"В корзине {Basket.Count} продуктов.");
        }

        public void ShowProducts(List<Product> products)
        {
            int number = 1;
            foreach (Product product in products)
            {
                Console.Write(number + ". ");
                product.Print();
                number++;
            }
        }

        public void CreateOrder(User curUser)
        {
            Order order = new Order(Basket);
            Orders.Add(order);
            curUser.MyListOrder.Add(order);
            Basket.Clear();
        }
    }

    class Program
    {
        static void Main()
        {
            Users ListUsers = new Users();
            Store onlineStore = new Store();
            User currentUser= null;
            int numberAction;

            User Den = new User("Denis","12345");
            User Jul = new User("Julie", "54321");

            ListUsers.AddUser(Den);
            Den.IsAdnim = true;
            ListUsers.AddUser(Jul);

            bool AllowAccess;

            do
            {
                Console.WriteLine("Здравствуйте. Введите логин:");
                string login = Console.ReadLine();
                Console.WriteLine("Введите пароль:");
                string pass = Console.ReadLine();
                AllowAccess = ListUsers.CheckUserAllow(login,pass,ref currentUser);
                    

            } while (AllowAccess==false);



            Console.WriteLine("Здравствуйте.");
            do
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Показать каталог продуктов?");
                Console.WriteLine("2. Посмотреть карзину?");
                Console.WriteLine("3. Добавить продукт в карзину?");
                Console.WriteLine("4. Хотите оформить заказ?");
                Console.WriteLine("5. Посмотреть мои заказы?");
                Console.WriteLine("6. Если хотите выйти наберите 0.");
                if(currentUser.IsAdnim==true)
                {
                    Console.WriteLine("7. Добавить продукт в каталог?");
                }
                Console.WriteLine("Выберите номер действия, которое хотите совершить.");
                numberAction = Convert.ToInt32(Console.ReadLine());

                switch (numberAction)
                {
                    case 1:
                        {
                            onlineStore.ShowCatalog();
                            break;
                        }
                    case 2:
                        {
                            onlineStore.ShowBasket();
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Напишите номер продукта, которого нужно добавить в корзину");
                            int productNumber = Convert.ToInt32(Console.ReadLine());
                            onlineStore.AddToBasket(productNumber);
                            break;
                        }
                    case 4:
                        {
                            onlineStore.CreateOrder(currentUser);
                            break;
                        }
                    case 5:
                        {
                            currentUser.ShowMyOrder();
                            break;
                        }
                    case 7:
                        {
                            if(currentUser.IsAdnim!=true)
                            {
                                goto default;
                            }
                            else
                            {
                                Console.WriteLine("Введите название");
                                string name = Console.ReadLine();
                                Console.WriteLine("Введите цену");
                                int price = int.Parse(Console.ReadLine());
                                onlineStore.AddProductToCatalog(name,price);
                            }
                            break;
                        }
                    default:
                        Console.WriteLine("Выберите номер действия из списка");
                        break;
                }
            } while (IsExit(numberAction));

        }

        static bool IsExit(int answer)
        {
            return answer != 0;
        }
    }
}
