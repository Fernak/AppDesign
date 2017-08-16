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
import android.widget.Toast;

import com.epicworkout.fernaak.epicworkout.R;
import com.google.firebase.auth.FirebaseAuth;

public class Forgot_Password_Dialog extends DialogFragment {

    public Forgot_Password_Dialog(){}

    private Button btnSend;
    EditText input_email;

    FirebaseAuth mFirebaseAuth;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.auth_dialog_signup, container, false);

        btnSend = view.findViewById(R.id.forgot_password_btn);
        input_email = view.findViewById(R.id.forgot_password_input_email);

        btnSend.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                btnSendClick();
            }
        });
        return view;
    }
    private void btnSendClick() {
        if (!(TextUtils.isEmpty(input_email.getText())))
            mFirebaseAuth.sendPasswordResetEmail(input_email.getText().toString());
    }
}