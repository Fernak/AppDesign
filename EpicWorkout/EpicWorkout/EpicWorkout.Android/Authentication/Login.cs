
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using static Android.Views.View;
using System;
using EpicWorkout.Droid.Activities;

namespace EpicWorkout.Droid.Authentication
{
    [Activity(Label = "Login", MainLauncher = true, Icon = "@drawable/icon")]
    public class Login : AppCompatActivity, IOnCompleteListener
    {
        //-------------------------------------------------------------------------------------------------------//
        //Global Variables
        //-------------------------------------------------------------------------------------------------------//
        #region Globals
        private Button btnLogin;
        private EditText input_email, input_password;
        private TextView btnSignUp, btnForgotPassword;

        LinearLayout activity_main;

        public static FirebaseApp app;
        FirebaseAuth mFirebaseAuth;
        //FirebaseUser mFirebaseUser;
        #endregion
        //-------------------------------------------------------------------------------------------------------//
        //OnCreate Method
        //-------------------------------------------------------------------------------------------------------//
        #region OnCreate
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signin_main);

            InitializeFirebase();
            InitializeScreen();

            FirebaseAuth.GetInstance(app).AuthState += AuthStateChanged;

            btnLogin.Click += BtnLogin_Click;
            btnSignUp.Click += BtnSignUp_Click;
            
            //btnForgotPassword.SetOnClickListener(this);

        }
        #endregion
        //-------------------------------------------------------------------------------------------------------//
        //Built in override methods
        //-------------------------------------------------------------------------------------------------------//
        #region BuiltInOverrides
        protected override void OnStart()
        {
            base.OnStart();

            FirebaseAuth.Instance.AuthState += AuthStateChanged;
        }

        protected override void OnStop()
        {
            base.OnStop();

            FirebaseAuth.Instance.AuthState -= AuthStateChanged;
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------//
        //Event Calls
        //-------------------------------------------------------------------------------------------------------//
        #region EventCalls
        //If the login button is clicked
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (!(input_email.Text.ToString().Equals("") && input_password.Text.ToString().Equals("")))
                mFirebaseAuth.SignInWithEmailAndPassword(input_email.Text, input_password.Text)
                    .AddOnCompleteListener(this);
        }
        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            //Pull up dialog
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            SignUp_Dialog signupDialog = new SignUp_Dialog();
            signupDialog.Show(transaction, "dialog sign up");
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------//
        //Methods
        //-------------------------------------------------------------------------------------------------------//
        #region Methods
        private void InitializeScreen()
        {
            //View
            btnLogin = FindViewById<Button>(Resource.Id.login_btn_login);
            input_email = FindViewById<EditText>(Resource.Id.login_email);
            input_password = FindViewById<EditText>(Resource.Id.login_password);
            btnSignUp = FindViewById<TextView>(Resource.Id.login_btn_sign_up);
            btnForgotPassword = FindViewById<TextView>(Resource.Id.login_btn_forgot_password);
            activity_main = FindViewById<LinearLayout>(Resource.Id.activity_main);
        }
        private void InitializeFirebase()
        {
            var options = new FirebaseOptions.Builder()
                .SetApplicationId("1:762584231980:android:444fd807095eb4c6")
                .SetApiKey("AIzaSyAoq5j-8SM7B9MtfDrAqqZWmtk_D5NHaHc")
                .Build();

            if (app == null)
                app = FirebaseApp.InitializeApp(this, options);
            mFirebaseAuth = FirebaseAuth.GetInstance(app);
        }
        #endregion
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Snackbar snackBar = Snackbar.Make(activity_main, "Logining In ", Snackbar.LengthShort);
                snackBar.Show();
            }
            else
            {
                Snackbar snackBar = Snackbar.Make(activity_main, "Login Failed ", Snackbar.LengthShort);
                snackBar.Show();
            }
        }
        public void AuthStateChanged(object sender, FirebaseAuth.AuthStateEventArgs e)
        {
            var user = e.Auth.CurrentUser;

            if (user != null)
            {
                // User is signed in
                StartActivity(new Android.Content.Intent(this, typeof(MainActivity)));
                Finish();
            }
            else
            {
                StartActivity(new Android.Content.Intent(this, typeof(Login)));
                Finish();
            }
        }

        // ...

    }
}
