using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Generic.Functions
{
    public static class ListShuffler
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            for (var i = list.Count - 1; i > 0; i--)
            {
                var randomIndex = Random.Range(0, i + 1);
                (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
            }
        }
    }
}
