using System.Collections;
using Hero;
using UnityEngine;

namespace Scene
{
    public class StartInitChicken : MonoBehaviour
    {
        [SerializeField] private Chicken _initChicken;
        [SerializeField] private float _pauseBeforeGame;
        
        private void Start()
        {
            _initChicken.gameObject.SetActive(false);
            StartCoroutine(Pause(_pauseBeforeGame));
        }

        private IEnumerator Pause(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            _initChicken.GetComponent<RiseEffect>().Play();
            _initChicken.gameObject.SetActive(true);
        }
    }
}
