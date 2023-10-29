using Game.Scripts._Data.MooCoinData;

namespace Game.Scripts.MooCoins
{
    public static class MooCoinFactory
    {
        public static MooCoin Create(MooCoinData mooCoinData)
        {
            return new MooCoin
            {
                CandyEarned = mooCoinData.CandyEarned
            };
        }
    }
}
