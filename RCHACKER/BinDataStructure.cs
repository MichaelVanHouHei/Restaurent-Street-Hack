using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCHACKER
{
    public class Level
    {
        public int id { get; set; }
        public int coinReward { get; set; }
        public string employees { get; set; }
        public string gardenPlots { get; set; }
        public int maxPopularity { get; set; }
        public string numDishes { get; set; }
        public int points { get; set; }
        public string roomSizeX { get; set; }
        public string roomSizeY { get; set; }
        public List<int> itemAwards { get; set; }
    }

    public class LevelBin
    {
        public List<Level> levels { get; set; }
    }
    public class Item
    {
        public int cash { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string hash { get; set; }
        public int cost { get; set; }
        public int smcost { get; set; }
        public int level { get; set; }
        public string className { get; set; }
        public string iconName { get; set; }
        public string type { get; set; }
        public bool? invisible { get; set; }
        public int? sizeX { get; set; }
        public int? sizeY { get; set; }
        public bool? isLimited { get; set; }
        public int? unlockLevel { get; set; }
        public string description { get; set; }
        public string effectText { get; set; }
        public int? muldelay { get; set; }
        public int? maxUsage { get; set; }
        public int? profit { get; set; }
        public string functions { get; set; }
        public int? bonusCook { get; set; }
        public string plantClassName { get; set; }
        public int? ingredientId { get; set; }
        public int? numPlants { get; set; }
        public bool? isNew { get; set; }
        public int? areaNum { get; set; }
        public string coverName { get; set; }
    }

    public class Group
    {
        public string groupName { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string buttonName { get; set; }
        public List<Item> items { get; set; }
    }

    public class BinaryStruture
    {
        public List<Group> groups { get; set; }
    }

    public class CustomItem
    {
        public int gid { get; set; }
        public string groupName { get; set; }
        public bool isSelected { get; set; }=false;
        public string name { get; set; }
        public int cost { get; set; }
        public int smcost { get; set; }
        public int level { get; set; }

    }
}
