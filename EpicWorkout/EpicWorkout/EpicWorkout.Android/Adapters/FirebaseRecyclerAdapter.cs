using System;
using Android.Support.V7.Widget;
using Android.Views;
using System.Collections.Generic;
using EpicWorkout.Droid.Fragments.ExerciseItems;

namespace EpicWorkout.Droid.Adapters
{
    public class FirebaseRecyclerAdapter : RecyclerView.Adapter
    {
        private List<ExerciseModel> lstData;
        public FirebaseRecyclerAdapter(List<ExerciseModel> exercises)
        {
            lstData = exercises;
        }
        public override int ItemCount
        {
            get
            {
                return lstData.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
           ExerciseViewHolder viewHolder = holder as ExerciseViewHolder;
            //viewHolder.exerciseImageView.SetImageResource(lstData[position].imageId);
            viewHolder.descriptionTextView.Text = lstData[position].Name;
            viewHolder.nameTextView.Text = lstData[position].Description;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.card_exercise, parent, false);
            return new ExerciseViewHolder(itemView);
        }
    }
}