namespace CsharpPractice_AeroPlaneChess
{
    internal class Program
    {
        public static int[] Maps = new int[100]; // Map data
        private static int[] PlayerPos = new int[2]; // Create players' positions

        private static void Main(string[] args)
        {
            GameShow();
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
                // If player A and player B have the same positions, draw Angle brackets
                if (PlayerPos[0] == PlayerPos[1] && PlayerPos[0] == i)
                {
                    Console.Write("⭕");
                }
                else if (PlayerPos[0] == i) // When there's only player A in this position
                {
                    
                    Console.Write(" A ");
                }
                else if (PlayerPos[1] == i) // When there's only player B  in this position
                {
                    Console.Write(" B ");
                }
                else
                {
                    switch (Maps[i])
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("□");
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("△");
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("◇"); 
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("☆");
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
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