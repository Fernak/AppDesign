package com.epicworkout.fernaak.epicworkout.BuilderItems;

import android.app.DialogFragment;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import com.epicworkout.fernaak.epicworkout.R;

public class WorkoutBuilder_Dialog extends DialogFragment{

    private Button btnBYAG, btnRandom, btnCustom, btnSplit;

    public WorkoutBuilder_Dialog(){}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.builder_main_dialog, container, false);

        btnBYAG = view.findViewById(R.id.btn_bayg);
        btnRandom = view.findViewById(R.id.btn_random);
        btnCustom = view.findViewById(R.id.btn_custom);
        btnSplit = view.findViewById(R.id.btn_Split);

        btnBYAG.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getContext(), Build_Your_Own_Workout_Main.class);
                startActivity(intent);
            }
        });
        btnRandom.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getContext(), Build_Your_Own_Workout_Main.class);
                startActivity(intent);
            }
        });
        btnCustom.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

            }
        });
        btnSplit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

            }
        });

        return view;
    }
}
