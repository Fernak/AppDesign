package com.epicworkout.fernaak.epicworkout.NavFragments;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.GridLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.epicworkout.fernaak.epicworkout.R;
import com.epicworkout.fernaak.epicworkout.WorkoutPlansItems.WorkoutObject;
import com.epicworkout.fernaak.epicworkout.WorkoutPlansItems.WorkoutPage;
import com.epicworkout.fernaak.epicworkout.WorkoutPlansItems.WorkoutViewHolder;
import com.firebase.ui.database.FirebaseRecyclerAdapter;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

public class PlansFragment extends Fragment
{
    private RecyclerView mRecyclerView;
    private GridLayoutManager mLayoutManager;

    private FirebaseRecyclerAdapter<WorkoutObject, WorkoutViewHolder> mRecyclerViewAdapter;
    private FirebaseDatabase mDatabase;
    private DatabaseReference mDatabaseReference;

    public PlansFragment(){}
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
        View view = inflater.inflate(R.layout.main_fragment_exercise, null);

        mDatabase = FirebaseDatabase.getInstance();
        mDatabaseReference = mDatabase.getReference().child("workouts");

        mRecyclerView = view.findViewById(R.id.recycler_view);
        mLayoutManager = new GridLayoutManager(getContext(), 2);
        mRecyclerView.setLayoutManager(mLayoutManager);

        attachRecyclerViewAdapter();

        return view;
    }
    private void attachRecyclerViewAdapter(){
        mRecyclerViewAdapter = new FirebaseRecyclerAdapter<WorkoutObject, WorkoutViewHolder>
                (WorkoutObject.class, R.layout.workout_cards, WorkoutViewHolder.class, mDatabaseReference) {
            @Override
            protected void populateViewHolder(WorkoutViewHolder viewHolder, final WorkoutObject model, int position) {
                viewHolder.setName(model.getName());
                viewHolder.setDescription(model.getDescription());

                viewHolder.itemView.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        Intent intent = new Intent(v.getContext(), WorkoutPage.class);
                        intent.putExtra("Plans Object", model);
                        v.getContext().startActivity(intent);
                    }
                });
            }
        };
        mRecyclerView.setAdapter(mRecyclerViewAdapter);
    }
}
