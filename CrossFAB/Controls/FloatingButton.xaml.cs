using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CrossFAB.Controls
{
    public partial class FloatingButton : Frame
    {

        public static readonly BindableProperty BGColorProperty = BindableProperty.Create(nameof(BGColor), typeof(Color), typeof(FloatingButton), default(Color), Xamarin.Forms.BindingMode.OneWay);
        public Color BGColor
        {
            get
            {
                return (Color)GetValue(BGColorProperty);
            }

            set
            {
                SetValue(BGColorProperty, value);
            }
        }

        public static readonly BindableProperty IconSrcProperty = BindableProperty.Create(nameof(IconSrc), typeof(string), typeof(FloatingButton), default(string), Xamarin.Forms.BindingMode.OneWay);
        public string IconSrc
        {
            get
            {
                return (string)GetValue(IconSrcProperty);
            }

            set
            {
                SetValue(IconSrcProperty, value);
            }
        }
        public static readonly BindableProperty OnClickCommandProperty = BindableProperty.Create(nameof(OnClickCommand), typeof(ICommand), typeof(FloatingButton), null, Xamarin.Forms.BindingMode.OneWay);
        public ICommand OnClickCommand
        {
            get
            {
                return (ICommand)GetValue(OnClickCommandProperty);
            }

            set
            {
                SetValue(OnClickCommandProperty, value);
            }
        }

        TapGestureRecognizer onTap = new TapGestureRecognizer();

        public FloatingButton()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == BGColorProperty.PropertyName)
            {
                BackgroundColor = BGColor;
            }
            else if (propertyName == IconSrcProperty.PropertyName)
            {
                Icon.Source = IconSrc;
            }
            else if (propertyName == OnClickCommandProperty.PropertyName)
            {
                onTap.Command = OnClickCommand;
                if (!GestureRecognizers.Contains(onTap))
                {
                    GestureRecognizers.Add(onTap);
                }
            }

        }
    }
}
