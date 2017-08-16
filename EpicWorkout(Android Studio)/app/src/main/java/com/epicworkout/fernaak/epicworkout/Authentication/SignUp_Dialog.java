package com.epicworkout.fernaak.epicworkout.Authentication;

import android.app.Activity;
import android.app.DialogFragment;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RelativeLayout;

import com.epicworkout.fernaak.epicworkout.R;

public class SignUp_Dialog extends DialogFragment{
    public SignUp_Dialog(){}

    private Button btnRegister;
    EditText input_username, input_email, input_password;
    RelativeLayout activity_sign_up;

    private OnCompleteListener mListener;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
        View view = inflater.inflate(R.layout.auth_dialog_signup, container, false);

        btnRegister = view.findViewById(R.id.signup_btn_signup);
        input_username = view.findViewById(R.id.signup_username);
        input_email = view.findViewById(R.id.signup_email);
        input_password = view.findViewById(R.id.signup_password);
        activity_sign_up = view.findViewById(R.id.activity_sign_up);

        btnRegister.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                btnRegisterClick();
            }
        });
        return view;
    }
    @Override
    public void onAttach(Activity activity){
        super.onAttach(activity);
        try{
            this.mListener = (OnCompleteListener)activity;
        }catch(ClassCastException e){}
    }

    public interface OnCompleteListener{
        public void onComplete(User user);
    }
    private void btnRegisterClick() {
        if (!(TextUtils.isEmpty(input_username.getText())) && !(TextUtils.isEmpty(input_email.getText())) && !(TextUtils.isEmpty(input_password.getText())))
        {
            User user = new User();

            user.username = input_username.getText().toString();
            user.email = input_email.getText().toString();
            user.password = input_password.getText().toString();

            this.mListener.onComplete(user);
            dismiss();
        }
    }
}
