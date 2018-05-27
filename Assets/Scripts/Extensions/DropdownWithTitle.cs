namespace PathfinderRPG.Extensions.UI
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [RequireComponent(typeof(Dropdown))]
    public class DropdownWithTitle : MonoBehaviour, ISelectHandler
    {
        [SerializeField]
        private Dropdown.OptionData _optionData;

        private bool wasNeverSelected = true;

        private Dropdown dropdown;

        /// <summary>
        /// Event handler for the OnSelect event
        /// </summary>
        /// <param name="eventData">The event data</param>
        public void OnSelect(BaseEventData eventData)
        {
            if (wasNeverSelected)
            {
                RemoveTitle();
            }

            wasNeverSelected = false;
        }

        /// <summary>
        /// Event handler for the OnEnable event
        /// </summary>
        private void OnEnable()
        {
            dropdown = gameObject.GetComponent<Dropdown>();

            dropdown.options.Insert(dropdown.value, _optionData);
            dropdown.RefreshShownValue();
        }

        /// <summary>
        /// Event handler for the OnDisable event
        /// </summary>
        private void OnDisable()
        {
            wasNeverSelected = true;
            RemoveTitle();
        }

        /// <summary>
        /// Removes the title option from the dropdown
        /// </summary>
        private void RemoveTitle()
        {
            dropdown.options.RemoveAt(dropdown.value);
            dropdown.onValueChanged.Invoke(dropdown.value);
            dropdown.RefreshShownValue();
        }
    }
}