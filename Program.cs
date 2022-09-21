namespace CsharpPractice_AeroPlaneChess
{
    internal class Program
    {
        public static int[] Maps = new int[100]; // Map data
        public static int[] PlayerPos = new int[2]; // Create players' positions
        public static string[] PlayerNames = new string[2]; // Save the role names
        public static bool[] PauseFlag = new bool[2]; // Indicates whether the player needs to pause
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
            Console.Clear();
            GameShow();
            Console.WriteLine("{0}的士兵用 A 表示", PlayerNames[0]);
            Console.WriteLine("{0}的士兵用 B 表示", PlayerNames[1]);

            InitializeMap();
            DrawMap();

            // When no player is at the end, the game continues
            while (PlayerPos[0] < 99 && PlayerPos[1] < 99)
            {
                // Judging if player A needs to pause a turn
                if (PauseFlag[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    PauseFlag[0] = false;
                }

                // Judging the victory condition of player A
                if (PlayerPos[0] >= 99)
                {
                    Console.WriteLine("玩家 {0} 胜利！\n", PlayerNames[0]);
                    Console.WriteLine("按任意键结束游戏.......");
                    break;
                }

                // Judging if player B needs to pause a turn
                if (PauseFlag[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    PauseFlag[1] = false;
                }

                // Judging the victory condition of player B
                if (PlayerPos[1] >= 99)
                {
                    Console.WriteLine("玩家 {0} 胜利！\n", PlayerNames[1]);
                    Console.WriteLine("按任意键结束游戏.......");
                    break;
                }
            }
            Console.ReadKey(true);
        }

        /// <summary>
        /// Mapping data
        /// </summary>
        public static void DrawMap()
        {
            // Legend
            Console.WriteLine("□：地图格子   △：幸运罗盘   ◇：地雷   @：暂停   ☆：时空隧道");

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
                for (int j = 0; j < 29; j++)
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
                return " A";
            }
            else if (PlayerPos[1] == i) // When there's only player B  in this position
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // Console.Write(" B ");
                return " B";
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

        /// <summary>
        /// 开始游戏
        /// </summary>
        public static void PlayGame(int playerNum)
        {
            // Random roll of the dice
            Random rand = new Random();
            int rNum = rand.Next(1, 7);

            Console.WriteLine("{0} 按任意键开始掷骰子", PlayerNames[playerNum]);
            Console.ReadKey(true);
            Console.WriteLine("{0} 掷出了 {1}", PlayerNames[playerNum], rNum);
            PlayerPos[playerNum] += rNum;
            JudgingPos();
            Console.ReadKey(true);
            Console.WriteLine("{0} 行动完了", PlayerNames[playerNum]);
            Console.ReadKey(true);

            // Judging player behavior
            if (PlayerPos[playerNum] == PlayerPos[1 - playerNum])
            {
                Console.WriteLine("玩家 {0} 踩到了玩家 {1}，玩家 {1} 退 6 格", PlayerNames[playerNum], PlayerNames[1 - playerNum]);
                PlayerPos[1 - playerNum] -= 6;
                JudgingPos();
                Console.ReadKey(true);
            }
            else
            {
                switch (Maps[PlayerPos[playerNum]])
                {
                    case 0:
                        Console.WriteLine("玩家 {0} 踩到了方块，安全，按任意键继续", PlayerNames[playerNum]);
                        Console.ReadKey(true);
                        break;

                    case 1:
                        Console.WriteLine("玩家 {0} 踩到了幸运轮盘，请选择\n1-> 交换位置\n2-> 轰炸对方，使对方退 6 格", PlayerNames[playerNum]);
                        string luckyTuneInput = Console.ReadLine();
                        while (true)
                        {
                            if (luckyTuneInput == "1")
                            {
                                Console.WriteLine("玩家 {0} 选择和玩家 {1} 交换位置", PlayerNames[playerNum], PlayerNames[1 - playerNum]);
                                int temp = PlayerPos[playerNum];
                                PlayerPos[playerNum] = PlayerPos[1 - playerNum];
                                PlayerPos[1 - playerNum] = temp;
                                Console.WriteLine("交换完成！按任意键继续");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (luckyTuneInput == "2")
                            {
                                Console.WriteLine("玩家 {0} 选择轰炸玩家 {1}，玩家 {1} 退 6 格", PlayerNames[playerNum], PlayerNames[1 - playerNum]);
                                Console.ReadKey(true);
                                PlayerPos[1 - playerNum] -= 6;
                                JudgingPos();
                                Console.WriteLine("玩家 {0} 退了 6 格", PlayerNames[1 - playerNum]);
                                Console.ReadKey(true);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("只能输入 1 或 2");
                                luckyTuneInput = Console.ReadLine();
                            }
                        }
                        break;

                    case 2:
                        Console.WriteLine("玩家 {0} 踩到了地雷，退 6 格", PlayerNames[playerNum]);
                        PlayerPos[playerNum] -= 6;
                        JudgingPos();
                        Console.ReadKey(true);
                        break;

                    case 3:
                        Console.WriteLine("玩家 {0} 踩到了暂停，暂停一回合", PlayerNames[playerNum]);
                        PauseFlag[playerNum] = true;
                        Console.ReadKey(true);
                        break;

                    case 4:
                        Console.WriteLine("玩家 {0} 踩到了时空隧道，前进 10 格", PlayerNames[playerNum]);
                        PlayerPos[playerNum] += 10;
                        JudgingPos();
                        Console.ReadKey(true);
                        break;
                }
            }

            JudgingPos();
            Console.Clear();
            DrawMap();
        }

        /// <summary>
        /// 当玩家坐标发生改变则调用
        /// </summary>
        public static void JudgingPos()
        {
            // Avoid players' positions going out of bounds
            if (PlayerPos[0] < 0)
            {
                PlayerPos[0] = 0;
            }
            if (PlayerPos[1] < 0)
            {
                PlayerPos[1] = 0;
            }
            if (PlayerPos[0] > 99)
            {
                PlayerPos[0] = 99;
            }
            if (PlayerPos[1] > 99)
            {
                PlayerPos[1] = 99;
            }
        }
    }
}