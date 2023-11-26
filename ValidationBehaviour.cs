using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arghiroiu_Raluca_Lab7
{
    class ValidationBehaviour : Behavior<Editor>
    {
        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            ((Editor)sender).BackgroundColor = string.IsNullOrEmpty(args.NewTextValue) ?
                Color.FromRgba("#AA4A44") :
                Color.FromRgba("#FFFFFF");
        }

        protected override void OnAttachedTo(Editor entry)
        {
            // This line subscribes the OnEntryTextChanged method to the TextChanged event
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Editor entry)
        {
            // This line unsubscribes the OnEntryTextChanged method from the TextChanged event
            // When the behavior is detached, it stops listening to the TextChanged event
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
