using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2_hash_algorithms.ViewModels
{
    public class HashCodeViewModel:INotifyPropertyChanged
    {
        private string hashCodeHex;
        private string hashCodeBin;
        private string originalValue;

        public string HashCodeHex
        {
            get { return hashCodeHex; }
            private set
            {
                if (value != this.hashCodeHex)
                {
                    this.hashCodeHex = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                              new PropertyChangedEventArgs("HashCodeHex"));
                    }
                }
            }
        }
        public string HashCodeBin
        {
            get { return hashCodeBin; }
            private set
            {
                if (value != this.hashCodeBin)
                {
                    this.hashCodeBin = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                              new PropertyChangedEventArgs("HashCodeBin"));
                    }
                }

            }
        }

        public string OriginalValue
        {
            get { return originalValue; }
            private set
            {
                if (value != this.originalValue)
                {
                    this.originalValue = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this,
                              new PropertyChangedEventArgs("OriginalValue"));
                    }
                }

            }
        }

        public HashCodeViewModel()
        {
            HashCodeHex = "init hex";
            HashCodeBin = "init bin";
            originalValue = string.Empty;
        }

        public HashCodeViewModel(byte[] hashCode, string originalValue)
        {
            SetModel(hashCode, originalValue);
        }
        public HashCodeViewModel SetModel(byte[] hashCode, string originalValue)
        {
            HashCodeHex = ConvertToHex(hashCode);
            HashCodeBin = ConvertToBin(hashCode);
            OriginalValue = originalValue;
            return this;
        }
        private string ConvertToHex(byte[] bytes)
        {
            var hex = BitConverter.ToString(bytes);

            return hex;
        }

        private string ConvertToBin(byte[] bytes)
        {
            var bins = new StringBuilder();
            var bin = string.Empty;

            foreach (var b in bytes)
            {
                bin = Convert.ToString(b, 2);
                bins.Append(bin);
                bins.Append(" ");
            }

            return bins.ToString().TrimEnd();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
