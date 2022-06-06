using Sirenix.OdinInspector;
using UnityEngine;

namespace Maps
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] [Required] private GameObject _door;
        [SerializeField] [Required] private GameObject _trigger;

        public GameObject Door => _door;

        public GameObject Trigger => _trigger;

        public void Open()
        {
            _door.SetActive(false);
            _trigger.SetActive(false);
        }
    }
}