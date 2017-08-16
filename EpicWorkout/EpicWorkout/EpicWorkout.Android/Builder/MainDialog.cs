using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EpicWorkout.Droid.WorkoutBuilder
{
    public class MainDialog : DialogFragment
    {
        public MainDialog()
        { RetainInstance = true; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.dialog_wb_main, container, false);



            return view;
        }
    }
}