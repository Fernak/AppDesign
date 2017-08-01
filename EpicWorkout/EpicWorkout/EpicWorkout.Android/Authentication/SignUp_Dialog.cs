using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Database;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;

namespace EpicWorkout.Droid.Authentication
{
    public class SignUp_Dialog : DialogFragment, IOnCompleteListener
    {
        public SignUp_Dialog()
        { RetainInstance = true; }

        private Button btnRegister;
        EditText input_username, input_email, input_password;
        LinearLayout activity_sign_up;

        FirebaseAuth auth;
        //FirebaseDatabase mDatatbase;

        private const String mFirebaseURL = "https://epicworkout-8c711.firebaseio.com/";

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.dialog_signup, container, false);

            //InitFirebase
            auth = FirebaseAuth.GetInstance(Login.app);

            //View
            btnRegister = view.FindViewById<Button>(Resource.Id.signup_btn_signup);
            input_username = view.FindViewById<EditText>(Resource.Id.signup_username);
            input_email = view.FindViewById<EditText>(Resource.Id.signup_email);
            input_password = view.FindViewById<EditText>(Resource.Id.signup_password);
            activity_sign_up = view.FindViewById<LinearLayout>(Resource.Id.activity_sign_up);
            
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
                auth.CreateUserWithEmailAndPassword(input_email.Text, input_password.Text)
                    .AddOnCompleteListener(this);
        }
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful == true)
            {
                var user = FirebaseAuth.Instance.CurrentUser;
                CreateNewUser(user);
                Snackbar snackBar = Snackbar.Make(activity_sign_up, "Register successfully", Snackbar.LengthShort);
                snackBar.Show();
            }
            else
            {
                Snackbar snackBar = Snackbar.Make(activity_sign_up, "Register Failed ", Snackbar.LengthShort);
                snackBar.Show();
            }
        }

        private void CreateNewUser(FirebaseUser user)
        {
            User newUser = new User();
            newUser.uid = user.Uid;
            newUser.name = input_username.Text;
            newUser.email = user.Email;


            //var firebase = new FirebaseClient(mFirebaseURL);
            //var item = await firebase.Child("users").PostAsync(newUser);



        }
    }
    public class User
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }
}