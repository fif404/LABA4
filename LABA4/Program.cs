// Скрипников Сергей 22-ИСП-2\1 17 вариант 
class ZNAK : ICloneable, IComparable<ZNAK>
{
    private string surname;
    private string name;
    private string zodiacSign;
    private int[] birthDate;

    public ZNAK(string surname, string name, string zodiacSign, int[] birthDate)
    {
        this.surname = surname;
        this.name = name;
        this.zodiacSign = zodiacSign;
        this.birthDate = birthDate;
    }

    public string Surname { get { return surname; } }
    public string Name { get { return name; } }
    public string ZodiacSign { get { return zodiacSign; } }
    public int[] BirthDate { get { return birthDate; } }

    public object Clone()
    {
        return new ZNAK(surname, name, 
        zodiacSign, (int[])birthDate.Clone());
    }

    public int CompareTo(ZNAK other)
    {
        return zodiacSign.CompareTo(other.zodiacSign);
    }

    public static IComparer<ZNAK> BirthDateComparer
    {
        get { return new BirthDateComparerClass(); }
    }

    private class BirthDateComparerClass : IComparer<ZNAK>
    {
        public int Compare(ZNAK x, ZNAK y)
        {
            for (int i = 0; i < 3; i++)
            {
                if (x.birthDate[i] < y.birthDate[i])
                    return -1;
                else if (x.birthDate[i] > y.birthDate[i])
                    return 1;
            }
            return 0;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ZNAK[] people = new ZNAK[4];
        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine("Введите данные для человека " + (i + 1));
            Console.Write("Фамилия: ");
            string surname = Console.ReadLine();
            Console.Write("Имя: ");
            string name = Console.ReadLine();
            Console.Write("Знак зодианка: ");
            string sign = Console.ReadLine();
            Console.Write("Дата рождения (ДД/ММ/ГГГГ): ");
            string[] tokens = Console.ReadLine().Split('/');
            int day = int.Parse(tokens[0]);
            int month = int.Parse(tokens[1]);
            int year = int.Parse(tokens[2]);
            int[] date = { day, month, year };
            ZNAK person = new ZNAK(surname, name, sign, date);
            people[i] = person;
        }

        Console.WriteLine();

        Array.Sort(people);

        Console.Write("Введите месяц: ");
        int monthInput = int.Parse(Console.ReadLine());
        bool found = false;
        for (int i = 0; i < 4; i++)
        {
            if (people[i].BirthDate[1] == monthInput)
            {
                found = true;
                Console.WriteLine(people[i].Surname + " " + people[i].Name + " (" + people[i].ZodiacSign + ") "
                + people[i].BirthDate[0] + "/" + people[i].BirthDate[1] + "/" + people[i].BirthDate[2]);
            }
        }
        if (!found)
        {
            Console.WriteLine("Извините, но в этом месяце никто не родился " + monthInput);
        }
    }
}
