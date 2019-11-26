using System;
using System.Collections.Generic;
using System.Text;

namespace BluetoothTest.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        BT
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
