using System;
using System.Collections.Generic;

using Android.Content;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using Firebase.Database;
using EpicWorkout.Droid.Fragments.ExerciseItems;
using EpicWorkout.Droid.Adapters;
using System.Linq;

namespace EpicWorkout.Droid.Fragments
{
    public class ExerciseFragment : Fragment
    {
        FirebaseDatabase mDatabase;

        RecyclerView mRecyclerView;
        GridLayoutManager mGridLayoutManager;
        RecyclerView.Adapter mAdapter;

        private List<ExerciseModel> mExercises = new List<ExerciseModel>();

        private const String mFirebaseURL = "https://epicworkout-8c711.firebaseio.com/";

        public ExerciseFragment()
        {
            RetainInstance = true;
            InitilizeFirebase();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_exercise, null);

            mDatabase = FirebaseDatabase.GetInstance(mFirebaseURL);
            DatabaseReference exerciseRef = mDatabase.GetReference("exercises");
            exerciseRef.AddValueEventListener(new MyValueEventListener());

            mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.exercise_recycler_view);
            mGridLayoutManager = new GridLayoutManager(Context, 2);
            mRecyclerView.SetLayoutManager(mGridLayoutManager);

            mAdapter = new FirebaseRecyclerAdapter(mExercises);
            mRecyclerView.SetAdapter(mAdapter);

            return view;
        }

        public void InitilizeFirebase()
        {
            mDatabase = FirebaseDatabase.GetInstance(mFirebaseURL);
            DatabaseReference exerciseRef = mDatabase.GetReference("exercises");
            exerciseRef.AddValueEventListener(new MyValueEventListener());
        }
    }
    public class MyValueEventListener : Java.Lang.Object, Firebase.Database.IValueEventListener
    {
        public void OnCancelled(DatabaseError error)
        {
            throw new NotImplementedException();
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            var mExercise = new List<ExerciseModel>();

            var obj = snapshot.Children;

            foreach(DataSnapshot s in obj.ToEnumerable())
            {
                ExerciseModel exObj = new ExerciseModel();

                exObj.Name = s.Child("").Child("name")?.GetValue(true)?.ToString();
                exObj.Description = s.Child("").Child("description")?.GetValue(true)?.ToString();
                exObj.Area = s.Child("").Child("area")?.GetValue(true)?.ToString();

                mExercise.Add(exObj);
            }
        }
    }
}