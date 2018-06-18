namespace PathfinderRPG.UI
{
    using UnityEngine;

    public class ListItem : MonoBehaviour
    {
        private bool _isRemoveable;

        /// <summary>
        /// Gets or sets whether the list item is removeable from a list or not
        /// </summary>
        public bool IsRemoveable
        {
            get { return _isRemoveable; }
            set { _isRemoveable = value; }
        }
    }
}
