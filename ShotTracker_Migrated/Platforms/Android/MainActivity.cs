using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Views.InputMethods;
using Android.Views;
using static Android.Views.View;

namespace ShotTracker.Droid
{
    [Activity(Label = "ShotTracker", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : MauiAppCompatActivity, IOnTouchListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            Window.DecorView.ViewTreeObserver.GlobalFocusChange += FocusChanged;
        }

        void FocusChanged(object sender, ViewTreeObserver.GlobalFocusChangeEventArgs e)
        {
            if (e.OldFocus is not null)
            {
                e.OldFocus.SetOnTouchListener(null);
            }

            if (e.NewFocus is not null)
            {
                e.NewFocus.SetOnTouchListener(this);
            }


            if (e.NewFocus is null && e.OldFocus is not null)
            {
                InputMethodManager imm = InputMethodManager.FromContext(this);

                IBinder wt = e.OldFocus.WindowToken;

                if (imm is null || wt is null)
                    return;

                imm.HideSoftInputFromWindow(wt, HideSoftInputFlags.None);
            }
        }

        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            bool dispatch = base.DispatchTouchEvent(ev);

            if (ev.Action == MotionEventActions.Down && CurrentFocus is not null)
            {
                if (!KeepFocus)
                    CurrentFocus.ClearFocus();
                KeepFocus = false;
            }

            return dispatch;
        }

        bool KeepFocus { get; set; }

        bool OnTouch(global::Android.Views.View v, MotionEvent e)
        {

            if (e.Action == MotionEventActions.Down && CurrentFocus == v)
                KeepFocus = true;

            return v.OnTouchEvent(e);
        }

        bool IOnTouchListener.OnTouch(global::Android.Views.View v, MotionEvent e) => OnTouch(v, e);
    }
}