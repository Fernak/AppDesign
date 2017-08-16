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
using Android.Support.V7.Widget;

namespace EpicWorkout.Droid.Fragments.ExerciseItems
{
    public class ExerciseViewHolder : RecyclerView.ViewHolder
    {
        public ImageView exerciseImageView { get; set; }
        public TextView descriptionTextView { get; set; }
        public TextView nameTextView { get; set; }
        public View itemView { get; set; }

        public ExerciseViewHolder(View itemView) : base(itemView)
        {
            exerciseImageView = itemView.FindViewById<ImageView>(Resource.Id.thumbnail);
            descriptionTextView = itemView.FindViewById<TextView>(Resource.Id.description);
            nameTextView = itemView.FindViewById<TextView>(Resource.Id.name);
            this.itemView = itemView;
        }

        public void setName(String name)
        {
            nameTextView.Text = name;
        }
        public void setDescription(String description)
        {
            descriptionTextView.Text = description;
        }
    }
}