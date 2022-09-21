namespace CsharpPractice_AeroPlaneChess
{
    internal class Program
    {
        public static int[] Maps = new int[100]; // Map data
        public static int[] PlayerPos = new int[2]; // Create players' p// ositions
        public static string[] PlayerNames = new string[2]; // Save the role names
        public static ConsoleColor colorDefault = ConsoleColor.White;

        private static void Main(string[] args)
        {
            GameShow();

            #region// Get the players' name
            Console.WriteLine("请输入玩家A的姓名");
            PlayerNames[0] = Console.ReadLine();
            while (PlayerNames[0] == "")
            { 
                Console.WriteLine("玩家 A 的姓名不能为空，请重新输入");
                PlayerNames[0] = Console.ReadLine();
            }
            Console.WriteLine("请输入玩家 B 的姓名");
           
            PlayerNames[1] = Console.ReadLine();
            while (PlayerNames[1] == "" || (PlayerNames[1] == PlayerNames[0]))
            { 
                if (PlayerNames[1] == "")
                {
                    Console.WriteLine("玩家的姓名不能为空，请重新输入");
                    PlayerNames[1] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("玩家的姓名不能玩家 A 的相同，请重新输入");
                    PlayerNames[1] = Console.ReadLine();
                }
            }
            #endregion

            InitializeMap();
            DrawMap();
            Console.ReadKey();
        }

        /// <summary>
        /// Mapping data
        /// </summary>
        public static void DrawMap()
        {
            // The first run
            for (int i = 0; i < 30; i++)
            {
                Console.Write(DetermineIcon(i));
            }
            Console.ForegroundColor = colorDefault;
            Console.WriteLine();

            // The first column
            for (int i = 30; i < 35; i++)
            {
                for(int j=0; j < 29; j++)
                {
                    Console.Write("  ");
                }
                Console.Write(DetermineIcon(i));
                Console.ForegroundColor = colorDefault;
                Console.WriteLine();
            }

            // The second row
            for (int i = 64; i >= 35; i--)
            {
                Console.Write(DetermineIcon(i));
            }
            Console.ForegroundColor = colorDefault;
            Console.WriteLine();

            // The second column
            for (int i = 65; i < 70; i++)
            {
                Console.WriteLine(DetermineIcon(i));
            }
            Console.ForegroundColor = colorDefault;

            // The third row
            for (int i = 70; i < 100; i++)
            {
                Console.Write(DetermineIcon(i));
            }
            Console.ForegroundColor = colorDefault;
            Console.WriteLine();

        }
        /// <summary>
        /// 绘制地图格的图标
        /// </summary>
        /// <param name="i">地图格索引</param>
        /// <returns>地图格图标</returns>
        public static string DetermineIcon(int i)
        {
            // If player A and player B have the same positions, draw Angle brackets
            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[0] == i)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // Console.Write("<>");
                return " ^";
            }
            else if (PlayerPos[0] == i) // When there's only player A in this position
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // Console.Write(" A ");
                return " A ";
            }
            else if (PlayerPos[1] == i) // When there's only player B  in this position
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // Console.Write(" B ");
                return " B ";
            }
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        // Console.Write(" □");
                        // break;
                        return " □";
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        // Console.Write(" △");
                        // break;
                        return " △";
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        // Console.Write(" ◇");
                        // break;
                        return " ◇";
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        return " @";
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        // Console.Write(" ☆");
                        // break;
                        return " ☆";
                }
                return Convert.ToString(1);
            }
        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        public static void InitializeMap()
        {
            int[] luckyTurn = { 6, 23, 40, 55, 69, 83 }; // 幸运罗盘
            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 }; // 地雷
            int[] pause = { 9, 27, 60, 93 }; // 暂停
            int[] tumeTunnel = { 20, 25, 45, 63, 72, 88, 90 }; // 时空隧道

            InitializeProps(luckyTurn, 1);
            InitializeProps(landMine, 2);
            InitializeProps(pause, 3);
            InitializeProps(tumeTunnel, 4);

            static void InitializeProps(int[] current_array, int value)
            {
                for (int i = 0; i < current_array.Length; i++)
                {
                    Maps[current_array[i]] = value;
                }
            }
        }

        /// <summary>
        /// 游戏头
        /// </summary>
        public static void GameShow()
        {      
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("******************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("******************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("********AeroPlaneChess********");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("******************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("******************************");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}