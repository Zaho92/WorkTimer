using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimer.View.ViewModel
{
    internal class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public ViewModelBase(string displayName)
        {
            DisplayName = displayName;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string DisplayName { get; internal set; }
        public bool ThrowOnInvalidPropertyName => true;

        public void Dispose()
        {
            
        }

        public void OnPropertyChanged(object sender, string propertyName)
        {
            this.VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }
    }
}