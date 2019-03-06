using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hardcodet.Wpf.TaskbarNotification;

namespace WpfTest
{
  
   // public delegate void No_ClickedEventHandler(object sender);
    /// <summary>
    /// Interaction logic for FancyBalloon.xaml
    /// </summary>
    public partial class FancyBalloon : UserControl
    {
        private bool isClosing = false;

        #region BalloonText dependency property

        /// <summary>
        /// Description
        /// </summary>
        public static readonly DependencyProperty BalloonTextProperty =
            DependencyProperty.Register("BalloonText",
                typeof (string),
                typeof (FancyBalloon),
                new FrameworkPropertyMetadata(""));

        /// <summary>
        /// A property wrapper for the <see cref="BalloonTextProperty"/>
        /// dependency property:<br/>
        /// Description
        /// </summary>
        public string BalloonText
        {
            get { return (string) GetValue(BalloonTextProperty); }
            set { SetValue(BalloonTextProperty, value); }
        }

        #endregion

        public FancyBalloon()
        {
            InitializeComponent();
            SystemSounds.Beep.Play();
            TaskbarIcon.AddBalloonClosingHandler(this, OnBalloonClosing);
        }


        /// <summary>
        /// By subscribing to the <see cref="TaskbarIcon.BalloonClosingEvent"/>
        /// and setting the "Handled" property to true, we suppress the popup
        /// from being closed in order to display the custom fade-out animation.
        /// </summary>
        private void OnBalloonClosing(object sender, RoutedEventArgs e)
        {
            e.Handled = true; //suppresses the popup from being closed immediately
            isClosing = true;
        }


        /// <summary>
        /// Resolves the <see cref="TaskbarIcon"/> that displayed
        /// the balloon and requests a close action.
        /// </summary>
        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //the tray icon assigned this attached property to simplify access
            TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
            taskbarIcon.CloseBalloon();
        }

        /// <summary>
        /// If the users hovers over the balloon, we don't close it.
        /// </summary>
        private void grid_MouseEnter(object sender, MouseEventArgs e)
        {
            //if we're already running the fade-out animation, do not interrupt anymore
            //(makes things too complicated for the sample)
            if (isClosing) return;

            //the tray icon assigned this attached property to simplify access
            TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
            taskbarIcon.ResetBalloonCloseTimer();
        }


        /// <summary>
        /// Closes the popup once the fade-out animation completed.
        /// The animation was triggered in XAML through the attached
        /// BalloonClosing event.
        /// </summary>
        private void OnFadeOutCompleted(object sender, EventArgs e)
        {
            Popup pp = (Popup) Parent;
            pp.IsOpen = false;
        }
        public event EventHandler ExecuteMethod;

        protected virtual void OnExecuteMethod()
        {
            if (ExecuteMethod != null) ExecuteMethod(this, EventArgs.Empty);
        }

        public void ChildButton_Click(object sender, EventArgs e)
        {
            OnExecuteMethod();
        }
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
           
            System.Windows.Application.Current.Windows.OfType<LogTimeAuto_Window>().SingleOrDefault(x => x.IsActive).countdown_Start();
            TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);

            taskbarIcon.CloseBalloon();
        }

      //  public event No_ClickedEventHandler NoClick;
        private void No_Click(object sender, RoutedEventArgs e)
        {
            //LogTimeAuto_Window win = (LogTimeAuto_Window)Window.GetWindow(this);
            //win.stopbtn_Click(sender,e);
            System.Windows.Application.Current.Windows.OfType<LogTimeAuto_Window>().SingleOrDefault(x => x.IsActive).stopbtn_Click(sender, e);
            TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
            taskbarIcon.CloseBalloon();
        }
    }
}