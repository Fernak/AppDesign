using System;
using System.Collections.Generic;

using Android.Content;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using EpicWorkout.Droid.Activities;

namespace EpicWorkout.Droid.Fragments
{
    public class CurrentFragment : Fragment
    {
        public CurrentFragment()
        {
            RetainInstance = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_current, null);
            return view;
        }
    }
}