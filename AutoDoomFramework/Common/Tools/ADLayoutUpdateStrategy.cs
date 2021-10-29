using AvalonDock.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutoDoomFramework.Common.Tools
{
    class ADLayoutUpdateStrategy : ILayoutUpdateStrategy
    {
        public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
        {
            if (((anchorableShown.Content as Border).Tag as string) == "Properties")
            {
                anchorableShown.Content = null;
            }
        }

        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
        {

        }

        public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow, ILayoutContainer destinationContainer)
        {
            if (((anchorableToShow.Content as Border).Tag as string) == "Properties")
            {
                foreach(LayoutAnchorable layoutContent in layout.Descendents().OfType<LayoutAnchorable>())
                {
                    if (layoutContent.ContentId is "Properties")
                    {
                        layoutContent.Content = anchorableToShow.Content;
                    }
                }
            }
            
            return true;
        }

        public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer)
        {
            return false;
        }
    }
}
