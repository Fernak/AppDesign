using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.OS;

using SupportFragment = Android.Support.V4.App.Fragment;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;

using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using System.Collections.Generic;
using Android.Content.PM;

namespace EpicWorkout.Droid
{
	[Activity (Label = "EpicWorkout.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        //-------------------------------------------------------------------------------------------------------//
        //Global Variables
        //-------------------------------------------------------------------------------------------------------//
        #region Globals
        //instantiate instance of our drawerLayout
        private DrawerLayout drawerLayout;

        //Onload we set screen to profile frag
        private Profile_Fragment profileFrag;
        private Plans_Fragment plansFrag;
        private Schedule_Fragment scheduleFrag;
        private Current_Fragment currentFrag;
        //Store the current fragment 
        private SupportFragment mCurrentFragment;

        private Stack<SupportFragment> mStackFragments;
        private Stack<int> stackFragmentTitles;
        FloatingActionButton fab;
        private int basePageTitle;
        #endregion
        //-------------------------------------------------------------------------------------------------------//
        //OnCreate Method
        //-------------------------------------------------------------------------------------------------------//
        #region OnCreate
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.main);

            //Sets up the toolbar at top
            SupportToolbar toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            //-----------------------------------------------------
            //ActionBar
            SupportActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            //Finds the different components from the layout and binds them
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            View navView = navigationView.GetHeaderView(0);

            //Setting up the navigation menu (the left drawer) and getting event from it
            if (navigationView != null)
                SetUpDrawerContent(navigationView);
       
            //Initilize all the main fragements 
            FragmentInitilization();

            //Click event calls for the main layout
            fab.Click += Fab_Click;
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------//
        //Built in override methods
        //-------------------------------------------------------------------------------------------------------//
        #region BuiltInOverrides
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
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
        //-------------------------------------------------
        //Goes to previous fragment when back arrow pressed
        public override void OnBackPressed()
        {
            if (SupportFragmentManager.BackStackEntryCount > 0)
            {
                SupportFragmentManager.PopBackStack();
                mCurrentFragment = mStackFragments.Pop();
                if (stackFragmentTitles.Count != 0)
                {
                    stackFragmentTitles.Pop();
                    SupportActionBar.SetTitle(stackFragmentTitles.Pop());
                }
                else
                    SupportActionBar.SetTitle(basePageTitle);
            }
            else
                base.OnBackPressed();
            drawerLayout.CloseDrawers();
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------//
        //Event Calls
        //-------------------------------------------------------------------------------------------------------//
        #region EventCalls
        //If the user clicks on the profile image
        private void UserImage_Click(object sender, EventArgs e)
        {
            ListItemClicked(3);
            drawerLayout.CloseDrawers();
        }
        //If the user selects on of the items in the drawer 
        private void SetUpDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);

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
                //Close drawer when item clicked
                drawerLayout.CloseDrawers();
            };
        }
        private void ListItemClicked(int position)
        {
            switch (position)
            {
                case 0:
                    ShowFragment(currentFrag);
                    stackFragmentTitles.Push(Resource.String.currentString);
                    SupportActionBar.SetTitle(Resource.String.currentString);
                    break;
                case 1:
                    ShowFragment(plansFrag);
                    stackFragmentTitles.Push(Resource.String.plansString);
                    SupportActionBar.SetTitle(Resource.String.plansString);
                    break;
                case 2:
                    ShowFragment(scheduleFrag);
                    stackFragmentTitles.Push(Resource.String.scheduleString);
                    SupportActionBar.SetTitle(Resource.String.scheduleString);
                    break;
                case 3:
                    ShowFragment(profileFrag);
                    stackFragmentTitles.Push(Resource.String.profileString);
                    SupportActionBar.SetTitle(Resource.String.profileString);
                    break;
            }
        }
        private void ShowFragment(SupportFragment fragment)
        {
            if (fragment.IsVisible)
            {
                return;
            }

            var trans = SupportFragmentManager.BeginTransaction();
            //If we want to add any animations
            //trans.SetCustomAnimations(Resource.Animation.slide_in, Resource.Animation.slide_out, 
            //Resource.Animation.slide_in, Resource.Animation.slide_out);

            fragment.View.BringToFront();
            mCurrentFragment.View.BringToFront();

            trans.Hide(mCurrentFragment);
            trans.Show(fragment);

            trans.AddToBackStack(null);
            mStackFragments.Push(mCurrentFragment);
            trans.Commit();

            mCurrentFragment = fragment;
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
        //FragmentManagement
        //-------------------------------------------------------------------------------------------------------//
        #region FragmentManagement
        private void FragmentInitilization()
        {

            drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
            basePageTitle = Resource.String.currentString;

            //-------------------------------------------------
            //Initializing fragments
            profileFrag = new Profile_Fragment();
            plansFrag = new Plans_Fragment();
            scheduleFrag = new Schedule_Fragment();
            currentFrag = new Current_Fragment();

            //-------------------------------------------------
            //Initializating stacks
            mStackFragments = new Stack<SupportFragment>();
            stackFragmentTitles = new Stack<int>();

            //-------------------------------------------------
            //Setting fragments to transictions to content_frame in layout
            var trans = SupportFragmentManager.BeginTransaction();

            trans.Add(Resource.Id.content_frame, profileFrag, "Profile_Fragment");
            trans.Hide(profileFrag);

            trans.Add(Resource.Id.content_frame, scheduleFrag, "Schedule_Fragment");
            trans.Hide(scheduleFrag);

            trans.Add(Resource.Id.content_frame, plansFrag, "Plans_Fragment");
            trans.Hide(plansFrag);

            trans.Add(Resource.Id.content_frame, currentFrag, "Current_Fragment");
            trans.Commit();

            //Getting current fragment seen
            mCurrentFragment = currentFrag;
            //-------------------------------------------------
            //Setting / saving string resource to title
            stackFragmentTitles.Push(basePageTitle);
            SupportActionBar.SetTitle(basePageTitle);
        }
        #endregion
    }
}