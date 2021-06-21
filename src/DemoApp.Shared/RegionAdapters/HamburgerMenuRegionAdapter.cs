using DemoApp.Shared.Helper.Extensions;
using MahApps.Metro.Controls;
using Prism.Regions;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;

namespace DemoApp.Shared.RegionAdapters
{
    public class HamburgerMenuRegionAdapter : RegionAdapterBase<HamburgerMenu>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ContentControlRegionAdapter"/>.
        /// </summary>
        /// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param>
        public HamburgerMenuRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        /// <summary>
        /// Adapts a <see cref="ContentControl"/> to an <see cref="IRegion"/>.
        /// </summary>
        /// <param name="region">The new region being used.</param>
        /// <param name="regionTarget">The object to adapt.</param>
        protected override void Adapt(IRegion region, HamburgerMenu regionTarget)
        {
            if (regionTarget == null)
                throw new ArgumentNullException(nameof(regionTarget));

            bool contentIsSet = regionTarget.Content != null;
            contentIsSet = contentIsSet || regionTarget.HasBinding(ContentControl.ContentProperty);

            if (contentIsSet)
                throw new InvalidOperationException("HamburgerMenu Has Content Exception");

            region.ActiveViews.CollectionChanged += delegate
            {
                regionTarget.Content = region.ActiveViews.FirstOrDefault();
            };

            region.Views.CollectionChanged +=
                (sender, e) =>
                {
                    if (e.Action == NotifyCollectionChangedAction.Add && region.ActiveViews.Count() == 0)
                    {
                        region.Activate(e.NewItems[0]);
                    }
                };
        }

        /// <summary>
        /// Creates a new instance of <see cref="SingleActiveRegion"/>.
        /// </summary>
        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
