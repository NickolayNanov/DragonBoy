using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.MainMenu
{
    public static class LevelNavigator
    {
        public static void NavigateToNextLevel()
        {
            var activeScene = SceneManager.GetActiveScene();
            if (activeScene == null) throw new Exception("No active scene");

            Thread.Sleep(1500);

            SceneManager.LoadScene(activeScene.buildIndex + 1);
        }

        public static void ReturnToLevelOne()
        {
            var activeScene = SceneManager.GetActiveScene();
            if (activeScene == null) throw new Exception("No active scene");

            Thread.Sleep(1500);

            SceneManager.LoadScene(activeScene.buildIndex + 1);
        }
        public static void Quit()
        {
            Application.Quit();
        }
    }
}
