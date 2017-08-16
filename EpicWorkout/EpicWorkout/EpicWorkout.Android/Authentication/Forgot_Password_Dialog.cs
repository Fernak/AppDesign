using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Tasks;
using Firebase.Auth;

namespace EpicWorkout.Droid.Authentication
{
    [Activity(Label = "Forgot_Password_Dialog")]
    public class Forgot_Password_Dialog : DialogFragment, IOnCompleteListener
    {
        public Forgot_Password_Dialog()
        { RetainInstance = true; }

        private Button btnSend;
        EditText input_email;

        FirebaseAuth mAuth;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.dialog_forgot_password, container, false);

            //InitFirebase
            mAuth = FirebaseAuth.GetInstance(Login.app);

            //View
            btnSend = view.FindViewById<Button>(Resource.Id.forgot_password_btn);
            input_email = view.FindViewById<EditText>(Resource.Id.forgot_password_email);

            btnSend.Click += BtnSignup_Click;
            return view;
        }

        private void BtnSignup_Click(object sender, EventArgs e)
        {
            if (input_email.Text == "")
                Toast.MakeText(Context, "Input Email", ToastLength.Short);
            else
                mAuth.SendPasswordResetEmail(input_email.Text)
                .AddOnCompleteListener(this);
        }
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful == false)
            {
                Toast.MakeText(Context, "Reset password failed", ToastLength.Short);
            }
            else
            {
                Toast.MakeText(Context, "Reset password link sent to email : " + input_email.Text, ToastLength.Short);
            }
        }
    }
}