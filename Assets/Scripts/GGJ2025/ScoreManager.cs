namespace GGJ2025
{
    public static class ScoreManager
    {
        public static int Point { get; private set; }
        public static float TimerBonus { get; private set; }
        public static bool IsClear { get; private set; }
        
        public static void GameClear(int point, float timerBonus)
        {
            Point = point;
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