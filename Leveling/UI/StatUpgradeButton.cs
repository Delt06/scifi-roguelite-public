using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace Leveling.UI
{
    public class StatUpgradeButton : MonoBehaviour
    {
        [SerializeField] private Stat _stat;
        [SerializeField] private Button _button;

        private IPlayerStats _playerStats;

        [Preserve]
        public void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            _playerStats.TryUpgrade(_stat);
        }
    }
}