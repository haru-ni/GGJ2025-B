namespace GGJ2025
{
    public static class ScoreManager
    {
        public static int Point { get; private set; }
        public static float SizeRate { get; private set; }
        public static string Time { get; private set; }
        public static float TimerBonus { get; private set; }
        public static bool IsClear { get; private set; }
        
        public static void GameClear(int point, float sizeRate, string time, float timerBonus)
        {
            Point = point;
            SizeRate = sizeRate;
            Time = time;
            TimerBonus = timerBonus;
            IsClear = true;
        }
        
        public static void GameOver()
        {
            Point = 0;
            TimerBonus = 0;
            IsClear = false;
        }
        
    }
}