using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CrossFAB.Controls
{
    public partial class FloatingMenu : AbsoluteLayout
    {

        bool isRevealed = false;
        bool raised = false;

        public static readonly BindableProperty BGColorProperty = BindableProperty.Create(nameof(BGColor), typeof(Color), typeof(FloatingMenu), default(Color), Xamarin.Forms.BindingMode.OneWay);
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

        public static readonly BindableProperty OpenIconProperty = BindableProperty.Create(nameof(OpenIcon), typeof(string), typeof(FloatingMenu), default(string), Xamarin.Forms.BindingMode.OneWay);
        public string OpenIcon
        {
            get
            {
                return (string)GetValue(OpenIconProperty);
            }

            set
            {
                SetValue(OpenIconProperty, value);
            }
        }

        public static readonly BindableProperty CloseIconProperty = BindableProperty.Create(nameof(CloseIcon), typeof(string), typeof(FloatingMenu), default(string), Xamarin.Forms.BindingMode.OneWay);
        public string CloseIcon
        {
            get
            {
                return (string)GetValue(CloseIconProperty);
            }

            set
            {
                SetValue(CloseIconProperty, value);
            }
        }

        public static readonly BindableProperty AnimationTimeProperty = BindableProperty.Create(nameof(AnimationTime), typeof(int), typeof(FloatingMenu), 250, Xamarin.Forms.BindingMode.OneWay);
        public int AnimationTime
        {
            get
            {
                return (int)GetValue(AnimationTimeProperty);
            }

            set
            {
                SetValue(AnimationTimeProperty, value);
            }
        }

        public static readonly BindableProperty ExtraCommandProperty = BindableProperty.Create(nameof(ExtraCommand), typeof(ICommand), typeof(FloatingButton), null, Xamarin.Forms.BindingMode.OneWay);
        public ICommand ExtraCommand
        {
            get
            {
                return (ICommand)GetValue(ExtraCommandProperty);
            }

            set
            {
                SetValue(ExtraCommandProperty, value);
            }
        }

        public FloatingMenu()
        {
            InitializeComponent();

            ChildAdded += ArrangeChildren;

            MainButton.OnClickCommand = new Command(() =>
            {
                if (isRevealed)
                {
                    System.Diagnostics.Debug.WriteLine("Collapsing");
                    Collapse(AnimationTime);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Expanding");
                    Expand(AnimationTime);
                }
                isRevealed = !isRevealed;
                if (isRevealed)
                    MainButton.IconSrc = CloseIcon;
                else
                    MainButton.IconSrc = OpenIcon;
                if (ExtraCommand != null)
                    ExtraCommand.Execute(null);
            });
        }

        void ArrangeChildren(object sender, EventArgs evt)
        {
            System.Diagnostics.Debug.WriteLine("Arranging " + Children.Count);

            for (int i = 1; i < Children.Count; i++)
            {
                Children[i].Scale = 0.7;
                SetLayoutBounds(Children[i], new Rectangle(0, -60 * i, 60, 60));
            }

            Collapse(1);
        }

        public async void Collapse(int time)
        {
            int raisInd = raised ? 1 : 0;
            for (int i = 1 - raisInd; i < Children.Count - raisInd; i++)
            {
                Children[i].TranslateTo(0, 60 * (i + raisInd), (uint)time);
            }
            await Task.Delay(time);
            for (int i = 1 - raisInd; i < Children.Count - raisInd; i++)
            {
                Children[i].IsVisible = false;
            }
        }

        public void Expand(int time)
        {
            RaiseChild(MainButton);
            raised = true;
            for (int i = 0; i < Children.Count - 1; i++)
            {
                Children[i].IsVisible = true;
                Children[i].TranslateTo(0, 0, (uint)time);
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == BGColorProperty.PropertyName)
            {
                MainButton.BGColor = BGColor;
            }
            else if (propertyName == OpenIconProperty.PropertyName)
            {
                if (!isRevealed)
                    MainButton.IconSrc = OpenIcon;
            }
            else if (propertyName == CloseIconProperty.PropertyName)
            {
                if (CloseIcon == default(string))
                    CloseIcon = OpenIcon;
                if (isRevealed)
                    MainButton.IconSrc = CloseIcon;
            }



        }
    }
}
