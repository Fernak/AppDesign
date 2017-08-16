package com.epicworkout.fernaak.epicworkout.NavFragments;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.epicworkout.fernaak.epicworkout.R;

public class ProfileFragment extends Fragment
{
    public ProfileFragment(){}
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
        View view = inflater.inflate(R.layout.main_fragment_profile, null);
        return view;
    }
}
