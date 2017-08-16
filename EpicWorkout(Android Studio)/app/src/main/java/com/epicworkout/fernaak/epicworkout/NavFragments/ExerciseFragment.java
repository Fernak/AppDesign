package com.epicworkout.fernaak.epicworkout.NavFragments;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.GridLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.epicworkout.fernaak.epicworkout.ExerciseItems.ExerciseObject;
import com.epicworkout.fernaak.epicworkout.ExerciseItems.ExercisePage;
import com.epicworkout.fernaak.epicworkout.ExerciseItems.ExerciseViewHolder;
import com.epicworkout.fernaak.epicworkout.R;
import com.firebase.ui.database.FirebaseRecyclerAdapter;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

public class ExerciseFragment extends Fragment
{
    private RecyclerView mRecyclerView;
    private GridLayoutManager mLayoutManager;

    private FirebaseRecyclerAdapter<ExerciseObject, ExerciseViewHolder> mRecyclerViewAdapter;
    private FirebaseDatabase mDatabase;
    private DatabaseReference mDatabaseReference;

    private OnExerciseClickListener mExerciseClickListener;

    public ExerciseFragment(){}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
        View view = inflater.inflate(R.layout.main_fragment_exercise, null);

        mDatabase = FirebaseDatabase.getInstance();
        mDatabaseReference = mDatabase.getReference().child("exercises");

        mRecyclerView = view.findViewById(R.id.recycler_view);
        mLayoutManager = new GridLayoutManager(getContext(), 2);
        mRecyclerView.setLayoutManager(mLayoutManager);

        attachRecyclerViewAdapter();

        return view;
    }
    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            this.mExerciseClickListener = (OnExerciseClickListener) activity;
        } catch (ClassCastException e) {
        }
    }
    private void attachRecyclerViewAdapter(){
        mRecyclerViewAdapter = new FirebaseRecyclerAdapter<ExerciseObject, ExerciseViewHolder>
                (ExerciseObject.class, R.layout.exercise_cards, ExerciseViewHolder.class, mDatabaseReference) {
            @Override
            protected void populateViewHolder(ExerciseViewHolder viewHolder, final ExerciseObject model, int position) {
                viewHolder.setName(model.getName());
                viewHolder.setDescription(model.getDescription());
                viewHolder.setExerciseImageView();

                viewHolder.itemView.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        Intent intent = new Intent(getContext(), ExercisePage.class);
                        intent.putExtra("Exercise Object", model);
                        startActivity(intent);
                    }
                });
            }
        };
        mRecyclerView.setAdapter(mRecyclerViewAdapter);
    }
    public interface OnExerciseClickListener {
        public void onComplete(View v, ExerciseObject object);
    }
}
