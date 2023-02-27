using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WorkTimer.Model;

namespace WorkTimer.Controls
{
    [TemplatePart(Name = "PART_Title", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_Button", Type = typeof(Button))]
    public partial class PathPicker : TextBox
    {
        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(PathPicker),
          new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, OnTitlePropertyChanged));

        private static void OnTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (PathPicker)d;
            control.TitleChanged();
        }

        private void TitleChanged()
        {
            if (PART_Title is not null)
            {
                PART_Title.Text = Title;
                PART_Title.Visibility = String.IsNullOrWhiteSpace(Title) ? Visibility.Collapsed : Visibility.Visible;
            }
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

        private TextBlock PART_Title;
        private Button PART_Button;

        public override void OnApplyTemplate()
        {
            PART_Title = GetTemplateChild("PART_Title") as TextBlock;
            TitleChanged();
            PART_Button = GetTemplateChild("PART_Button") as Button;
            if (PART_Button is not null)
            {
                PART_Button.Click += PART_Button_Click;
            }
            base.OnApplyTemplate();
        }

        private void PART_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "Bitte wählen Sie einen Ordner aus";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Text = dialog.SelectedPath;
            }
        }

        public PathPicker()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(this, OnPaste);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (IsInvalidInput(Clipboard.GetText()))
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (IsInvalidInput(e.Text))
            {
                e.Handled= true;
            }
        }

        private bool IsInvalidInput(string input)
        {
            char[] invalidPathChars = Path.GetInvalidPathChars();
            return !Path.IsPathRooted(Text + input) || input.Any(c => invalidPathChars.Contains(c));
        }
    }
}