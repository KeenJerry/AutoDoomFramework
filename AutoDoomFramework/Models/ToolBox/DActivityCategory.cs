using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Models.ToolBox
{
    class DActivityCategory
    {
        private string categoryName;
        public string CategoryName
        {
            get => categoryName;
            set => categoryName = value;
        }

        private List<DActivityCategory> dActivityCategories = new List<DActivityCategory>();
        public List<DActivityCategory> DActivityCategories
        {
            get => dActivityCategories;
            set => dActivityCategories = value;
        }

        private List<DActivity> dActivities = new List<DActivity>();
        public List<DActivity> DActivities
        {
            get => dActivities;
            set => dActivities = value;
        }

        private List<Object> items = new List<object>();
        public List<Object> Items
        {
            get
            {
                items.Clear();
                items.AddRange(DActivityCategories);
                items.AddRange(DActivities);

                return items;
            }
        }

        public void AddCategory(DActivityCategory category)
        {
            if (!DActivityCategories.Contains(category))
            {
                DActivityCategories.Add(category);
            }
        }

        public void AddActivity(DActivity activity)
        {
            if (!DActivities.Contains(activity))
            {
                DActivities.Add(activity);
            }
        }

        public DActivityCategory(string categoryName)
        {
            CategoryName = categoryName;
        }

        public DActivityCategory(string categoryName, List<DActivity> dActivities, List<DActivityCategory> dActivityCategories)
        {
            CategoryName = categoryName;
            DActivities = dActivities;
            DActivityCategories = dActivityCategories;
        }
    }
}
