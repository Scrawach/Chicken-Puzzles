using UnityEngine;
using Random = UnityEngine.Random;

namespace Hero
{
    public class RandomCrestColor : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Color[] _availableColors;

        private Color _selectedColor;
        
        public void Generate()
        {
            _selectedColor = ChooseRandomColor();
            _meshRenderer.materials[1].color = _selectedColor;
        }

        private Color ChooseRandomColor() => 
            _availableColors[Random.Range(0, _availableColors.Length)];
    }
}
