namespace PathfinderRPG.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    // TODO: This class needs the methods checking as it is not clear which relate to UI and code and which relate to only one

    public class ListBox : MonoBehaviour
    {
        public GameObject listItemTemplate;
        public GameObject content;

        public Dropdown.DropdownEvent onValueChanged;

        private List<Dropdown.OptionData> _options = new List<Dropdown.OptionData>();
        private int _selectedIndex = -1;

        /// <summary>
        /// Gets a list containing the options for the ListBox
        /// </summary>
        public List<Dropdown.OptionData> Options
        {
            get { return _options; }
        }

        /// <summary>
        /// Moves an item from one ListBox to another
        /// </summary>
        /// <param name="index">The index value of the item to move</param>
        /// <param name="from">The listbox to move the item from</param>
        /// <param name="to">The ListBox to move the item to</param>
        /// <param name="sort">Whether the <paramref name="to"/> listbox should be sorted after the item is moved</param>
        public static void MoveItem(int index, ListBox from, ListBox to, bool sort)
        {
            Dropdown.OptionData item = from.Options[index];

            to.AddOption(item, true);

            from.RemoveAt(index);

            // TODO: Add sort functionality
        }

        /// <summary>
        /// Adds an option to the ListBox
        /// </summary>
        /// <param name="optionData">The OptionData object to add</param>
        /// <param name="isRemoveable">Whether the object will be flagged as removeable or not</param>
        public void AddOption(Dropdown.OptionData optionData, bool isRemoveable)
        {
            var listItem = Instantiate(listItemTemplate);

            listItem.transform.SetParent(content.transform);
            listItem.transform.localPosition = Vector3.zero;
            listItem.transform.localScale = Vector3.one;

            listItem.GetComponentInChildren<Text>().text = optionData.text;
            listItem.GetComponent<ListItem>().IsRemoveable = isRemoveable;

            listItem.GetComponent<Button>().onClick.AddListener(() => { OnItemSelected(GetIndexOf(listItem)); });

            _options.Add(optionData);
        }

        /// <summary>
        /// Adds a list of options to the ListBox
        /// </summary>
        /// <param name="optionData">A list of OptionData objects to add</param>
        /// <param name="itemsAreRemoveable">Whether the objects will be flagged as removeable or not</param>
        public void AddOptions(List<Dropdown.OptionData> optionData, bool itemsAreRemoveable)
        {
            foreach (Dropdown.OptionData option in optionData)
            {
                AddOption(option, itemsAreRemoveable);
            }
        }

        /// <summary>
        /// Removes all items from the ListBox
        /// </summary>
        public void ClearOptions()
        {
            foreach (Transform child in content.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            _options.Clear();
        }
        
        /// <summary>
        /// Returns the ListItem at the specified index
        /// </summary>
        /// <param name="index">The index of the ListItem to return</param>
        /// <returns>The ListItem at the corresponding index</returns>
        public ListItem GetListItem(int index)
        {
            Transform item = content.transform.GetChild(index);
            ListItem listItem = item.GetComponent<ListItem>();

            return listItem;
        }

        /// <summary>
        /// Removes a ListItem from the ListBox at the specified index
        /// </summary>
        /// <param name="index">The index of the ListItem to remove</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _options.Count) return;

            Transform listItem = content.transform.GetChild(index);

            GameObject.Destroy(listItem.gameObject);

            _options.RemoveAt(index);
        }

        /// <summary>
        /// Removes the selected ListItem from the ListBox
        /// </summary>
        public void RemoveSelected()
        {
            // TODO: This may not remove the corresponding UI item

            RemoveAt(_selectedIndex);
        }

        /// <summary>
        /// Event handler for an item being selected within the ListBox
        /// </summary>
        /// <param name="index">The index of the selected item</param>
        private void OnItemSelected(int index)
        {
            ClearItem(index);
            _selectedIndex = index;
            SetItem(index);

            onValueChanged.Invoke(index);
        }

        /// <summary>
        /// Sets a selected item
        /// </summary>
        /// <param name="index">The index of the item to set</param>
        private void SetItem(int index)
        {
            if (index < 0)
            {
                return;
            }

            // TODO: Could set colours here (ColorBlock)
        }

        /// <summary>
        /// Clears a selected item
        /// </summary>
        /// <param name="index">The index of the item to clear</param>
        private void ClearItem(int index)
        {
            if (index < 0)
            {
                return;
            }

            // TODO: Could set colours here (ColorBlock)
        }

        /// <summary>
        /// Returns the index of the specified <paramref name="gameObject"/>
        /// </summary>
        /// <param name="gameObject">The GameObject to find the index for</param>
        /// <returns>The index of the specified GameObject, or -1 if it is not found in the ListBox</returns>
        private int GetIndexOf(GameObject gameObject)
        {
            for (int i = 0; i < content.transform.childCount; i++)
            {
                Transform childTransform = content.transform.GetChild(i);

                if (childTransform.gameObject == gameObject)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}