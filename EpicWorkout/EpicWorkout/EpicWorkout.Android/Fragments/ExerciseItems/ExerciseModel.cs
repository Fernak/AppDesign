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

namespace EpicWorkout.Droid.Fragments.ExerciseItems
{
    public class ExerciseModel
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String Area { get; set; }
    }
}