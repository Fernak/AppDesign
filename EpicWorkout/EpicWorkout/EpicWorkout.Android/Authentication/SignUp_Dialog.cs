using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Database;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;
using System.Collections.Generic;

namespace EpicWorkout.Droid.Authentication
{
    public class SignUp_Dialog : DialogFragment, IOnCompleteListener
    {
        public SignUp_Dialog()
        { RetainInstance = true; }

        private Button btnRegister;
        EditText input_username, input_email, input_password;
        RelativeLayout activity_sign_up;

        FirebaseAuth mAuth;
        FirebaseDatabase mDatabase;

        private const String mFirebaseURL = "https://epicworkout-8c711.firebaseio.com/";

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.dialog_signup, container, false);

            //InitFirebase
            mAuth = FirebaseAuth.GetInstance(Login.app);
            mDatabase = FirebaseDatabase.GetInstance(mFirebaseURL);

            //View
            btnRegister = view.FindViewById<Button>(Resource.Id.signup_btn_signup);
            input_username = view.FindViewById<EditText>(Resource.Id.signup_username);
            input_email = view.FindViewById<EditText>(Resource.Id.signup_email);
            input_password = view.FindViewById<EditText>(Resource.Id.signup_password);
            activity_sign_up = view.FindViewById<RelativeLayout>(Resource.Id.activity_sign_up);

            btnRegister.Click += BtnSignup_Click;
            return view;
        }

        private void BtnSignup_Click(object sender, EventArgs e)
        {
            if (input_username.Text == "")
                Toast.MakeText(Context, "Input Username", ToastLength.Short);
            else if (input_email.Text == "")
                Toast.MakeText(Context, "Input Email", ToastLength.Short);
            else if (input_password.Text == "")
                Toast.MakeText(Context, "Input Password", ToastLength.Short);
            else
                mAuth.CreateUserWithEmailAndPassword(input_email.Text, input_password.Text)
                    .AddOnCompleteListener(this);
        }
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful == true)
            {
                CreateNewUser(FirebaseAuth.Instance.CurrentUser);
                Snackbar snackBar = Snackbar.Make(activity_sign_up, "Register successfully", Snackbar.LengthShort);
                snackBar.Show();
            }
            else
            {
                Snackbar snackBar = Snackbar.Make(activity_sign_up, "Register Failed ", Snackbar.LengthShort);
                snackBar.Show();
            }
        }
        public async void CreateNewUser(FirebaseUser currentUser)
        {
            User user = new User();
            user.uid = currentUser.Uid;
            user.name = input_username.Text;
            user.email = input_email.Text;

            DatabaseReference usersRef = mDatabase.GetReference("users");
            usersRef.Child(user.uid).Child(user.name).SetValue(user.email);

            //Use built in methods to set username and later picture
            var mUser = FirebaseAuth.Instance.CurrentUser;
            var profileUpdates = new UserProfileChangeRequest.Builder()
                .SetDisplayName(user.name)
                .Build();
            try
            {
                await mUser.UpdateProfileAsync(profileUpdates);
            }
            catch (Exception ex) { }
        }
    }
}
