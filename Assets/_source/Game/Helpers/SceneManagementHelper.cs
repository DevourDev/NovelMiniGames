using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Helpers
{

    public class SceneManagementHelper : MonoBehaviour
    {
        private bool _busy;


        public void LoadScene(int index)
        {
            if (_busy)
                return;

            var state = SceneManager.LoadSceneAsync(index);
            state.completed += (x) => _busy = false;
            state.allowSceneActivation = true;
        }

        public void ReloadScene()
        {
            if (_busy)
                return;

            var state = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            state.completed += (x) => _busy = false;
            state.allowSceneActivation = true;
        }
    }
}
