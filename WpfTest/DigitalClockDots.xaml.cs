using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for DigitalClockDots.xaml
    /// </summary>
    public partial class DigitalClockDots : UserControl
    {
        private Brush renderBrush = null;

        public DigitalClockDots()
        {
            InitializeComponent();

            this.RenderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        public Brush RenderBrush
        {
            get
            {
                return renderBrush;
            }
            set
            {
                renderBrush = value;
                p0.Fill = renderBrush;
                p1.Fill = renderBrush;
            }
        }
    }
}
