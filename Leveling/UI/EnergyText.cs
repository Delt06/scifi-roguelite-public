using Leveling.Energy;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;

namespace Leveling.UI
{
    public class EnergyText : MonoBehaviour
    {
        [SerializeField] [Required] private TMP_Text _text;
        [SerializeField] [Required] private string _format = "{0:0}/{1:0}";

        private IPlayerExperience _playerExperience;

        [Preserve]
        public void Construct(IPlayerExperience playerExperience)
        {
            _playerExperience = playerExperience;
        }

        private void OnEnable()
        {
            Refresh();
            _playerExperience.EnergyChanged += OnEnergyChanged;
        }

        private void OnDisable()
        {
            _playerExperience.EnergyChanged -= OnEnergyChanged;
        }

        private void OnEnergyChanged() => Refresh();

        private void Refresh() =>
            _text.SetText(_format, _playerExperience.Energy, _playerExperience.RequiredEnergyForNextLevel);
    }
}