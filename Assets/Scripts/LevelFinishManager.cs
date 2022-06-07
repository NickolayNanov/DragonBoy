using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public static class LevelFinishManager
    {
        private static readonly Dictionary<string, float> horizontalLevelFinishLocation = new Dictionary<string, float>
        {
            { "Level1", 41.92f },
            { "Level2", 45.58f },
            { "Level3", 87.5f },
            { "Level4", 88f },
        };

        private static Scene activeLevel;

        public static void Init()
            => activeLevel = SceneManager.GetActiveScene();


        public static bool HasPlayerFinished(float playerHorizontalLocation) 
            => horizontalLevelFinishLocation[activeLevel.name] <= playerHorizontalLocation;
    }
}
