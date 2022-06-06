using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;

namespace Leveling.UI
{
    public class FreeStatPointsText : MonoBehaviour
    {
        [SerializeField] [Required] private TMP_Text _text;
        [SerializeField] [Required] private string _format = "{0:0}";

        private IPlayerStats _playerStats;

        [Preserve]
        public void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        private void OnEnable()
        {
            Refresh();
            _playerStats.FreeStatPointsChanged += OnFreeStatPointsChanged;
        }

        private void OnDisable()
        {
            _playerStats.FreeStatPointsChanged -= OnFreeStatPointsChanged;
        }

        private void OnFreeStatPointsChanged() => Refresh();

        private void Refresh() => _text.SetText(_format, _playerStats.FreeStatPoints);
    }
}