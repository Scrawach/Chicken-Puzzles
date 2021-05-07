using UnityEngine;
using UnityEngine.Events;

namespace UI.HUD
{
    public class Window : MonoBehaviour
    {
        public UnityEvent Opened;
        public UnityEvent Closed;
        
        public void Open()
        {
            gameObject.SetActive(true);
            Opened?.Invoke();
        }

        public void Close()
        {
            gameObject.SetActive(false);
            Closed?.Invoke();
        }
    }
}