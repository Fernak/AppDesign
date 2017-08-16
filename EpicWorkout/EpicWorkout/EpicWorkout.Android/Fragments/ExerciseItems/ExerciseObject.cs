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
using Java.IO;

namespace EpicWorkout.Droid.Fragments.ExerciseItems
{
    public class ExerciseObject
    {
        private String description;
        private String name;
        private String area;
        private String mPhotoUrl;

        public ExerciseObject() { }

        public ExerciseObject(String name, String description, String area)
        {
            this.name = name;
            this.description = description;
            this.area = area;
        }

        public String getDescription() { return description; }

        public String getName() { return name; }

        public String getArea() { return area; }

        public String getPhotoUrl()
        {
            return mPhotoUrl;
        }

    }
}