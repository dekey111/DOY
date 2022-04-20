using DOY.dataFiles;
using DOY.Pages;
using DOY.Pages.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DOY
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rand = new Random();
        public MainWindow()
        {
            InitializeComponent();
            ConnectHelper.entObj = new DOYEntities1();
            FrameApp.frmObj = FrmMain;
            FrameApp.frmObj.Navigate(new PageChildren());

            var fio = ConnectHelper.entObj.Chief.FirstOrDefault(x => x.ID_Chief == 1);
            txbFIO.Text = fio.FIO;

            //Add();

        }
        private void btnKindergarten_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageKindergarten());
        }

        private void btnParent_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageParents());
        }

        private void btnContract_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageContract());
        }

        private void btnKindergartener_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageKindergartener());
        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageGroup());
        }

        private void btnChild_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageChildren());
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            WindowContact windowContact = new WindowContact();
            windowContact.ShowDialog();
        }
        //Добавление записей
        private void Add()
        {
            //Генерация фамилий
            List<string> Surname = new List<string>
            {
                "Смирнов", "Иванов", "Кузнецов", "Соколов", "Попов", "Лебедев", "Козлов", "Новиков", "Морозов",
                "Петров", "Волков", "Соловьёв", "Васильев", "Зайцев", "Павлов", "Семёнов", "Голубев", "Виноградов",
                "Богданов", "Воробьёв", "Фёдоров", "Михайлов", "Беляев", "Тарасов", "Комаров", "Орлов", "Киселёв",
                "Щербаков", "Сидоров", "Дроздов", "Симонов", "Пестов", "Давыдов", "Григорьев", "Пономарёв", "Логинов",
                "Силин", "Громов", "Фомин", "Лихачёв", "Самойлов", "Архипов", "Прохоров", "Лукин", "Воронов"
            };

            //Генерация мужский имён
            List<string> FirstNameMan = new List<string>
            {
                "Авдей", "Ефрем", "Иван", "Андрей", "Савва", "Виктор", "Григорий", "Евдоким", "Федот",
                "Авксентий", "Пров", "Илья", "Кузьма", "Савелий", "Геннадий", "Денис", "Харитон", "Трофим",
                "Агапит", "Родион", "Артур", "Сергей", "Валентин", "Георгий", "Данакт", "Фотий", "Трифон",
                "Агафон", "Исидор", "Архипп", "Серафим", "Валерий", "Герасим", "Евгений", "Фома", "Тихон",
                "Ермолай", "Игорь", "Афанасий", "Семён", "Варлам", "Герман", "Евграф", "Фирс", "Тарас"
            };

            //Генерация жеских имён
            List<string> FirstNameGirl = new List<string>
            {
                "Жанна", "Алла", "Ермиония", "Инесса", "Валентина", "Серафима", "Глафира", "Яна", "Оксана",
                "Агафья", "Алина", "Зинаида", "Ирина", "Валерия", "Снежана", "Евгения", "Ярослава", "Ульяна",
                "Агния", "Александра", "Злата", "Карина", "Варвара", "София", "Наталья", "Феврония", "Тамара",
                "Ангелина", "Лариса", "Зоя", "Кира", "Кира", "Василиса", "Татьяна", "Нина", "Фаина", "Целестина",
                "Анастасия", "Лилия", "Инга", "Ксения", "Светлана", "Вероника", "Галина", "Ульяна", "Ольга"
            };

            //Генерация Отчества
            List<string> MiddleName = new List<string>
            {
                "Александрович ", "Аркадьевич", "Алексеевич", "Артемович", "Анатольевич", "Бедросович", "Андреевич", "Богданович", "Антонович", "Борисович"
            };

            //Генерация фотографии родителя мужского пола
            List<string> PhotoParentMan = new List<string>
            {
               "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Parent\\Parent1.jpg",
               "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Parent\\Parent3.jpg",
               "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Parent\\Parent5.jpg",
               "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Parent\\Parent6.jpg",
               "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Parent\\Parent10.png"

            };

            //Генерация фотографии родителя женского пола
            List<string> PhotoParentGirl = new List<string>
            {
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Parent\\Parent9.jpg",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Parent\\Parent4.jpg",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Parent\\Parent7.jpg",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Parent\\Parent8.jpg",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Parent\\Parent9.jpg"
            };

            //Генерация фотографии ребёнка мужского пола
            List<string> PhotoChildrenMan = new List<string>
            {
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Children\\Children7.jpg",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Children\\Children8.jpg",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Children\\Children9.jpg",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Children\\Children10.jpg",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Children\\Children11.jpg",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Children\\Children12.jpg"
            };

            //Генерация фотографии ребёнка жеского пола
            List<string> PhotoChildrenGirl = new List<string>
            {
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Children\\Children1.jpg",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Children\\Children2.jpg",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Children\\Children3.png",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Children\\Children4.png",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Children\\Children5.jpg",
                "C:\\Users\\ilias\\OneDrive\\Рабочий стол\\Задания с колледжа\\Диплом\\DOY_v1.3.3 — копия\\DOY\\Resource\\Children\\Children6.jpg"
            };

            for (int i = 0; i < 1; i++)
            {
                int genderParent = rand.Next(1, 3);
                int genderChildren = rand.Next(1, 3);
                Parent parentObj = new Parent();
                Children childrenObj = new Children();
                Contract contractObj = new Contract();
                ChildrenInGroup childrenInGroupObj = new ChildrenInGroup();

                //Генерация суммы контракта 
                int sumContract = rand.Next(1000, 5001);

                //Генерация номеров телефонов
                int num = rand.Next(100, 999);
                int num2 = rand.Next(10, 99);
                int num3 = rand.Next(10, 99);

                //Генерация даты рождения родителя
                int yearParent = rand.Next(1950, 2001);
                int yearChild = rand.Next(2015, 2020);
                int month = rand.Next(1, 13);
                int day = rand.Next(1, 32);

                switch (genderParent)
                {
                    case 1:
                        //Добавление родителя женского рода
                        string fnPG = FirstNameGirl.ElementAt(rand.Next(FirstNameGirl.Count()));
                        string snPG = Surname.ElementAt(rand.Next(Surname.Count()));
                        string mnPG = MiddleName.ElementAt(rand.Next(MiddleName.Count()));
                        DateTime dbPG = Convert.ToDateTime(yearParent + "-" + month + "-" + day);
                        string pPG = "+7" + num + num + num2 + num3;

                        //Проверка повторяющихся полей
                        while (ConnectHelper.entObj.Parent.Where(x => x.FirstName == fnPG && x.Surname == snPG && x.MiddleName == mnPG &&
                        x.DateOfBirth == dbPG && x.Phone == pPG).Count() > 0)
                        {
                            fnPG = FirstNameGirl.ElementAt(rand.Next(FirstNameGirl.Count()));
                            snPG = Surname.ElementAt(rand.Next(Surname.Count()));
                            mnPG = MiddleName.ElementAt(rand.Next(MiddleName.Count()));
                            dbPG = Convert.ToDateTime(yearParent + "-" + month + "-" + day);
                            pPG = "+7" + num + num + num2 + num3;
                        }
                        parentObj.FirstName = fnPG;
                        parentObj.Surname = snPG;
                        parentObj.MiddleName = mnPG;
                        parentObj.DateOfBirth = dbPG;
                        parentObj.Phone = pPG;
                        parentObj.Image = File.ReadAllBytes(PhotoParentGirl.ElementAt(rand.Next(PhotoParentGirl.Count())));
                        break;

                    case 2:
                        //Добавление родителя мужского рода
                        string fnPM = FirstNameMan.ElementAt(rand.Next(FirstNameMan.Count()));
                        string snPM = Surname.ElementAt(rand.Next(Surname.Count()));
                        string mnPM = MiddleName.ElementAt(rand.Next(MiddleName.Count()));
                        DateTime dbPM = Convert.ToDateTime(yearParent + "-" + month + "-" + day);
                        string pPM = "+7" + num + num + num2 + num3;

                        //Проверка повторяющихся полей
                        while (ConnectHelper.entObj.Parent.Where(x => x.FirstName == fnPM && x.Surname == snPM && x.MiddleName == mnPM &&
                        x.DateOfBirth == dbPM && x.Phone == pPM).Count() > 0)
                        {
                            fnPM = FirstNameGirl.ElementAt(rand.Next(FirstNameGirl.Count()));
                            snPM = Surname.ElementAt(rand.Next(Surname.Count()));
                            mnPM = MiddleName.ElementAt(rand.Next(MiddleName.Count()));
                            dbPM = Convert.ToDateTime(yearParent + "-" + month + "-" + day);
                            pPM = "+7" + num + num + num2 + num3;
                        }

                        parentObj.FirstName = fnPM;
                        parentObj.Surname = snPM;
                        parentObj.MiddleName = mnPM;
                        parentObj.DateOfBirth = dbPM;
                        parentObj.Phone = pPM;
                        parentObj.Image = File.ReadAllBytes(PhotoParentMan.ElementAt(rand.Next(PhotoParentMan.Count())));
                        break;
                }

                switch (genderChildren)
                {
                    case 1:
                        //Добавление ребёнка женского пола
                        childrenObj.FirstName = FirstNameGirl.ElementAt(rand.Next(FirstNameGirl.Count()));
                        childrenObj.Surname = parentObj.Surname;
                        childrenObj.MiddleName = parentObj.FirstName;
                        childrenObj.DateOfBirth = Convert.ToDateTime(yearChild + "-" + month + "-" + day);
                        childrenObj.Image = File.ReadAllBytes(PhotoChildrenGirl.ElementAt(rand.Next(PhotoChildrenGirl.Count())));
                        childrenObj.id_Gender = 2;
                        break;
                    case 2:
                        //Добавление ребёнка мужского пола
                        if(ConnectHelper.entObj.Parent.Where(x => x.ID_Parent == parentObj.ID_Parent && parentObj.FirstName.Contains(FirstNameGirl.ToString())).Count() > 0)
                        {
                            childrenObj.FirstName = FirstNameMan.ElementAt(rand.Next(FirstNameMan.Count()));
                            childrenObj.Surname = parentObj.Surname;
                            childrenObj.MiddleName = MiddleName.ElementAt(rand.Next(MiddleName.Count()));
                            childrenObj.DateOfBirth = Convert.ToDateTime(yearChild + "-" + month + "-" + day);
                            childrenObj.Image = File.ReadAllBytes(PhotoChildrenMan.ElementAt(rand.Next(PhotoChildrenMan.Count())));
                            childrenObj.id_Gender = 1;
                        }
                        else
                        {
                            childrenObj.FirstName = FirstNameMan.ElementAt(rand.Next(FirstNameMan.Count()));
                            childrenObj.Surname = parentObj.Surname;
                            childrenObj.MiddleName = parentObj.FirstName;
                            childrenObj.DateOfBirth = Convert.ToDateTime(yearChild + "-" + month + "-" + day);
                            childrenObj.Image = File.ReadAllBytes(PhotoChildrenMan.ElementAt(rand.Next(PhotoChildrenMan.Count())));
                            childrenObj.id_Gender = 1;
                        }

                        break;
                }
                if (FirstNameGirl.Contains(parentObj.FirstName))
                    parentObj.Surname += "а";

                //Генерация группы
                int idGroup = rand.Next(1, 7);

                if (ConnectHelper.entObj.ChildrenInGroup.Where(x => x.id_Group == idGroup).Count() == 25)
                    continue;
                else
                {
                    childrenInGroupObj.id_Children = childrenObj.ID_Children;
                    childrenInGroupObj.id_Group = idGroup;

                    contractObj.id_Children = childrenObj.ID_Children;
                    contractObj.DateContract = DateTime.Today;
                    contractObj.id_Parent = parentObj.ID_Parent;
                    contractObj.Pay = sumContract;
                    contractObj.id_Group = idGroup;
                }

                ConnectHelper.entObj.Parent.Add(parentObj);
                ConnectHelper.entObj.Children.Add(childrenObj);
                ConnectHelper.entObj.ChildrenInGroup.Add(childrenInGroupObj);
                ConnectHelper.entObj.Contract.Add(contractObj);
                ConnectHelper.entObj.SaveChanges();

            }
        }
    }
}
