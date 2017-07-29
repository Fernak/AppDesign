using System;
using Android.App;
using Android.Views;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;

using EpicWorkout.Droid.Ativities;
using EpicWorkout.Droid.Fragments;
using Firebase.Auth;
using EpicWorkout.Droid.Authentication;

namespace EpicWorkout.Droid.Activities
{
    [Activity]
    public class MainActivity : BaseActivity
    {
        //-------------------------------------------------------------------------------------------------------//
        //Global Variables
        //-------------------------------------------------------------------------------------------------------//
        #region Globals
        //instantiate instance of our drawerLayout
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        IMenuItem previousItem;

        FloatingActionButton fab;

        #endregion
        //-------------------------------------------------------------------------------------------------------//
        //OnCreate Method
        //-------------------------------------------------------------------------------------------------------//
        #region OnCreate
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //-----------------------------------------------------
            //ActionBar
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            //Finds the different components from the layout and binds them
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);

            //Click event calls for the main layout
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            fab.Click += Fab_Click;

            if (bundle == null)
            {
                ListItemClicked(0);
                navigationView.SetCheckedItem(Resource.Id.nav_current);
            }
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------//
        //Built in override methods
        //-------------------------------------------------------------------------------------------------------//
        #region BuiltInOverrides
        protected override int LayoutResource
        {
            get{ return Resource.Layout.main; }
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
                case Resource.Id.item_signout:
                    FirebaseAuth.GetInstance(Login.app).SignOut();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        //-------------------------------------------------
        //Creates options menu at top right 
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.overflow_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------//
        //Event Calls
        //-------------------------------------------------------------------------------------------------------//
        #region EventCalls
        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            if (previousItem != null)
                previousItem.SetChecked(false);

            navigationView.SetCheckedItem(e.MenuItem.ItemId);

            previousItem = e.MenuItem;

            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.nav_current:
                    ListItemClicked(0);
                    break;
                case Resource.Id.nav_plans:
                    ListItemClicked(1);
                    break;
                case Resource.Id.nav_schedule:
                    ListItemClicked(2);
                    break;
            }
            drawerLayout.CloseDrawers();
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            View anchor = sender as View;

            Snackbar.Make(anchor, "Create a Workout", Snackbar.LengthLong)
                    .SetAction("Press Here", v =>
                    {
                        //Launch Workout activity
                        //Intent intent = new Intent(fab.Context, typeof(WorkoutBuilderActivity));
                        //StartActivity(intent);
                    })
                    .Show();
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------//
        //Methods
        //-------------------------------------------------------------------------------------------------------//
        #region Methods
        private void ListItemClicked(int position)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case 0:
                    fragment = new CurrentFragment();
                    SupportActionBar.SetTitle(Resource.String.currentString);
                    break;
                case 1:
                    fragment = new PlansFragment();
                    SupportActionBar.SetTitle(Resource.String.plansString);
                    break;
                case 2:
                    fragment = new ScheduleFragment();
                    SupportActionBar.SetTitle(Resource.String.scheduleString);
                    break;
            }
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }
        #endregion
    }
}