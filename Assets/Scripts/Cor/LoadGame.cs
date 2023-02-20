using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BlueStellar.Cor
{
    public class LoadGame : MonoBehaviour
    {
        private AsyncOperation loadOperation;

        [SerializeField]
        private Slider progressBar;

        private float currentValue;
        private float targetValue;

        [SerializeField]
        [Range(0, 1)]
        private float progressAnimationMultiplier = 0.25f;

        private bool canLoad;

        private void Start()
        {
            StartCoroutine(IE_Load());
        }

        private void Update()
        {
            if (canLoad)
            {
                targetValue = loadOperation.progress / 0.9f;
                currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplier * Time.deltaTime);
                progressBar.value = currentValue;
                if (Mathf.Approximately(currentValue, 1f))
                {
                    loadOperation.allowSceneActivation = true;
                    progressBar.value = 1.2f;
                    canLoad = false;
                }
            }
        }

        private IEnumerator IE_Load()
        {
            yield return new WaitForSeconds(0.15f);

            progressBar.value = currentValue = targetValue = 0;

            loadOperation = SceneManager.LoadSceneAsync(1);

            loadOperation.allowSceneActivation = false;
            canLoad = true;
        }
    }
}
