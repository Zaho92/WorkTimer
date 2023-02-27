using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkTimer.Controls
{
    [TemplatePart(Name = "PART_Title", Type = typeof(TextBlock))]
    public partial class TitledTextBox : TextBox
    {
        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(TitledTextBox),
          new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, OnTitlePropertyChanged));

        private static void OnTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TitledTextBox)d;
            control.TitleChanged();
        }

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        private void TitleChanged()
        {
            if (PART_Title is not null)
            {
                PART_Title.Text = Title;
                PART_Title.Visibility = String.IsNullOrWhiteSpace(Title) ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public override void OnApplyTemplate()
        {
            PART_Title = GetTemplateChild("PART_Title") as TextBlock;
            TitleChanged();
            base.OnApplyTemplate();
        }

        private TextBlock PART_Title;

        public TitledTextBox()
        {
            InitializeComponent();
        }
    }
}