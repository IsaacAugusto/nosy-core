using UnityEngine;
using UnityEngine.SceneManagement;

namespace NosyCore.Bootstrap
{
    public static class Bootstrap
    {
        private const string BOOTSTRAP_SCENE_NAME = "Bootstrap";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void LoadBootstrapSceneIfExists()
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string pathToScene = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
                if (sceneName == BOOTSTRAP_SCENE_NAME)
                {
                    var alreadyLoaded = SceneManager.GetSceneByName(sceneName).IsValid();
                    if (alreadyLoaded == false)
                        SceneManager.LoadScene(BOOTSTRAP_SCENE_NAME, LoadSceneMode.Additive);
                    
                    return;
                }
            }
            
            Debug.Log($"Could not find bootstrap scene on build list named: {BOOTSTRAP_SCENE_NAME}. If you don't have a bootstrap scene, ignore this message.");
        }
    }
}