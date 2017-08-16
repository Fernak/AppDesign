package com.epicworkout.fernaak.epicworkout;

import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.ActionBar;
import android.view.View;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ImageView;
import android.widget.TextView;

import com.epicworkout.fernaak.epicworkout.Authentication.Login;
import com.epicworkout.fernaak.epicworkout.BuilderItems.WorkoutBuilder_Dialog;
import com.epicworkout.fernaak.epicworkout.ExerciseItems.ExerciseObject;
import com.epicworkout.fernaak.epicworkout.ExerciseItems.ExercisePage;
import com.epicworkout.fernaak.epicworkout.NavFragments.CurrentFragment;
import com.epicworkout.fernaak.epicworkout.NavFragments.ExerciseFragment;
import com.epicworkout.fernaak.epicworkout.NavFragments.PlansFragment;
import com.epicworkout.fernaak.epicworkout.NavFragments.ProfileFragment;
import com.epicworkout.fernaak.epicworkout.NavFragments.ScheduleFragment;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;

import de.hdodenhof.circleimageview.CircleImageView;

public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener, ExerciseFragment.OnExerciseClickListener{

    ImageView mBackground;
    CircleImageView mUserImage;
    TextView mUsername;
    ActionBar mActionBar;
    Toolbar mToolbar;
    FloatingActionButton mFab;
    DrawerLayout mNavDrawer;
    NavigationView mNavigationView;

    FirebaseAuth mFirebaseAuth;
    FirebaseUser mFirebaseUser;

    android.app.FragmentManager mFragmentManager = getFragmentManager();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main_activity);

        initilizeScreen();
        mFirebaseAuth = FirebaseAuth.getInstance();
        mFirebaseUser = mFirebaseAuth.getCurrentUser();
        getUserInfo(mFirebaseUser);

        mActionBar = getSupportActionBar();
        mToolbar.setTitle("Current Workout");
        setSupportActionBar(mToolbar);
        getSupportActionBar().setDefaultDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setHomeButtonEnabled(true);
        //The on click event for the fab
        mFab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG).setAction("Action", null).show();
                WorkoutBuilder_Dialog workoutBuilder_dialog = new WorkoutBuilder_Dialog();
                workoutBuilder_dialog.show(mFragmentManager, "forgot password dialog");
            }
        });


        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, mNavDrawer, mToolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        mNavDrawer.setDrawerListener(toggle);
        toggle.syncState();

        mNavigationView.setNavigationItemSelectedListener(this);

        if(savedInstanceState == null)
        {
            ListItemClicked(1);
            mNavigationView.setCheckedItem(R.id.nav_current);
            mToolbar.setTitle("Current Workout");
            mNavDrawer.closeDrawers();
        }
    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main_menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId())
        {
            case R.id.action_settings:
                return true;
            case R.id.signout:
                FirebaseAuth.getInstance().signOut();
                Intent intent =  new Intent(MainActivity.this, Login.class);
                startActivity(intent);
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_profile) {
            ListItemClicked(0);
        } else if (id == R.id.nav_current) {
            ListItemClicked(1);
        } else if (id == R.id.nav_plans) {
            ListItemClicked(2);
        } else if (id == R.id.nav_exercises) {
            ListItemClicked(3);
        } else if (id == R.id.nav_schedule) {
            ListItemClicked(4);
        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }
    @Override
    public void onComplete(View v, ExerciseObject model){
        Intent intent = new Intent(MainActivity.this, ExercisePage.class);
        //Intent intent = new Intent(v.getContext(), ExercisePage.class);
        //intent.putExtra("Exercise Object", model);
        //v.getContext().startActivity(intent);
        startActivity(intent);
    }
    private void ListItemClicked(int position)
    {
        android.support.v4.app.Fragment fragment = null;
        switch (position)
        {
            case 0:
                fragment = new ProfileFragment();
                mToolbar.setTitle("Profile");
                break;
            case 1:
                fragment = new CurrentFragment();
                mToolbar.setTitle("Current Workout");
                break;
            case 2:
                fragment = new PlansFragment();
                mToolbar.setTitle("Workout Plans");
                break;
            case 3:
                fragment = new ExerciseFragment();
                mToolbar.setTitle("Exercises");
                break;
            case 4:
                fragment = new ScheduleFragment();
                mToolbar.setTitle("Schedule");
                break;
        }
        getSupportFragmentManager().beginTransaction()
                .replace(R.id.content_frame, fragment)
                .commit();
    }
    private void initilizeScreen() {
        mToolbar = (Toolbar) findViewById(R.id.toolbar);
        mFab = (FloatingActionButton) findViewById(R.id.fab);
        mNavDrawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        mNavigationView = (NavigationView) findViewById(R.id.nav_view);

        View header = mNavigationView.getHeaderView(0);

        mBackground = header.findViewById(R.id.imgHeader);
        mUserImage = header.findViewById(R.id.avatar);
        mUsername = header.findViewById(R.id.nav_username);
    }
    private void getUserInfo(FirebaseUser mFirebaseUser) {
        String name = mFirebaseUser.getDisplayName();
        mUsername.setText(name);
    }
}
