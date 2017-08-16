package com.epicworkout.fernaak.epicworkout.Authentication;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v7.app.AppCompatActivity;
import android.text.TextUtils;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.epicworkout.fernaak.epicworkout.MainActivity;
import com.epicworkout.fernaak.epicworkout.R;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.FirebaseApp;
import com.google.firebase.auth.AuthResult;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.auth.UserProfileChangeRequest;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

import java.util.HashMap;
import java.util.Map;

public class Login extends AppCompatActivity implements SignUp_Dialog.OnCompleteListener {
    //-------------------------------------------------------------------------------------------------------//
    //Global Variables
    //-------------------------------------------------------------------------------------------------------//
    private static final String TAG = "EmailPassword";
    private Button btnLogin;
    private EditText input_email, input_password;
    private TextView btnSignUp, btnForgotPassword;

    android.app.FragmentManager mFragmentManager = getFragmentManager();
    //RelativeLayout activity_main;

    public static FirebaseApp app;
    FirebaseAuth mFirebaseAuth;
    FirebaseUser mFirebaseUser;
    FirebaseDatabase mDatabase;
    FirebaseAuth.AuthStateListener mAuthListener;

    private final String mFirebaseURL = "https://epicworkout-8c711.firebaseio.com/";

    //-------------------------------------------------------------------------------------------------------//
    //OnCreate Method
    //-------------------------------------------------------------------------------------------------------//
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.auth_signin_main);

        InitializeFirebase();
        InitializeScreen();

        mAuthListener = new FirebaseAuth.AuthStateListener() {
            @Override
            public void onAuthStateChanged(@NonNull FirebaseAuth firebaseAuth) {
                if(firebaseAuth.getCurrentUser() != null)
                {
                    Intent intent = new Intent(Login.this, MainActivity.class);
                    startActivity(intent);
                }
            }
        };



        btnLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (!(TextUtils.isEmpty(input_email.getText()) && TextUtils.isEmpty(input_password.getText()))) {
                    signIn(input_email.getText().toString(), input_password.getText().toString());
                }

            }
        });
        btnSignUp.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                signUp();
            }
        });
        btnForgotPassword.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                forgotPassword();
            }
        });

    }

    //-------------------------------------------------------------------------------------------------------//
    //Built in override methods
    //-------------------------------------------------------------------------------------------------------//
    @Override
    protected void onStart() {
        super.onStart();

        mAuthListener = new FirebaseAuth.AuthStateListener() {
            @Override
            public void onAuthStateChanged(@NonNull FirebaseAuth firebaseAuth) {
                if(firebaseAuth.getCurrentUser() != null)
                {
                    Intent intent = new Intent(Login.this, MainActivity.class);
                    startActivity(intent);
                }
            }
        };
    }

    @Override
    protected void onStop() {
        super.onStop();

        if(mAuthListener != null)
            mFirebaseAuth.removeAuthStateListener(mAuthListener);
    }
    @Override
    protected void onPause(){
        super.onPause();
        if (mAuthListener != null)
            mFirebaseAuth.removeAuthStateListener(mAuthListener);
    }
    @Override
    protected void onResume(){
        super.onResume();
        mFirebaseAuth.addAuthStateListener(mAuthListener);
    }

    @Override
    public void onComplete(final User user) {
        mFirebaseAuth = FirebaseAuth.getInstance();
        mFirebaseAuth.createUserWithEmailAndPassword(user.getEmail().toString(), user.getPassword().toString())
                .addOnCompleteListener(this, new OnCompleteListener<AuthResult>() {
            @Override
            public void onComplete(@NonNull Task<AuthResult> task) {
                if (task.isSuccessful()) {
                    createNewUser(user);
                } else {
                    Toast.makeText(Login.this, "Signup has failed.",
                            Toast.LENGTH_SHORT).show();
                    signUp();
                }
            }
        });
    }

    //-------------------------------------------------------------------------------------------------------//
    //Methods
    //-------------------------------------------------------------------------------------------------------//
    private void InitializeScreen() {
        //View
        btnLogin = (Button) findViewById(R.id.login_btn_login);
        input_email = (EditText) findViewById(R.id.login_email);
        input_password = (EditText) findViewById(R.id.login_password);
        btnSignUp = (TextView) findViewById(R.id.login_btn_sign_up);
        btnForgotPassword = (TextView) findViewById(R.id.login_btn_forgot_password);
        //activity_main = (RelativeLayout) findViewById(R.id.activity_main);
    }

    private void InitializeFirebase() {
        mFirebaseAuth = FirebaseAuth.getInstance();
        mDatabase = FirebaseDatabase.getInstance(mFirebaseURL);

    }

    public void signIn(String email, String password) {
        Log.d(TAG, "signIn:" + email);
        mFirebaseAuth.signInWithEmailAndPassword(email, password)
                .addOnCompleteListener(this, new OnCompleteListener<AuthResult>() {
                    @Override
                    public void onComplete(@NonNull Task<AuthResult> task) {
                        if (task.isSuccessful()) {
                            // Sign in success, update UI with the signed-in user's information
                            Log.d(TAG, "signInWithEmail:success");
                            FirebaseUser user = mFirebaseAuth.getCurrentUser();
                            updateUI(user);
                        } else {
                            // If sign in fails, display a message to the user.
                            Log.w(TAG, "signInWithEmail:failure", task.getException());
                            Toast.makeText(Login.this, "Authentication failed.",
                                    Toast.LENGTH_SHORT).show();
                            updateUI(null);
                        }

                    }
                });
    }

    public void signUp() {
        Log.d(TAG, "create Account");

        SignUp_Dialog signup_dialog = new SignUp_Dialog();
        //signup_dialog.setTargetFragment(this, 0);
        signup_dialog.show(mFragmentManager, "signup dialog");
    }

    public void forgotPassword() {
        Log.d(TAG, "forgot password");
        //Pull up dialog
        Forgot_Password_Dialog forgot_pass = new Forgot_Password_Dialog();
        forgot_pass.show(mFragmentManager, "forgot password dialog");
    }

    public void updateUI(FirebaseUser user) {
        if (user != null) {
            Intent intent = new Intent(Login.this, MainActivity.class);
            startActivity(intent);
        } else {

        }
    }
    private void createNewUser(User currentUser) {
        mFirebaseUser = FirebaseAuth.getInstance().getCurrentUser();
        UserProfileChangeRequest profileUpdates = new UserProfileChangeRequest.Builder()
                .setDisplayName(currentUser.getUsername())
                .build();

        mFirebaseUser.updateProfile(profileUpdates);
        DatabaseReference usersRef = mDatabase.getReference("users");

        Map<String, User> users = new HashMap<String, User>();
        users.put(mFirebaseAuth.getCurrentUser().getUid(), currentUser);
        usersRef.setValue(users);
        updateUI(FirebaseAuth.getInstance().getCurrentUser());
    }
}


