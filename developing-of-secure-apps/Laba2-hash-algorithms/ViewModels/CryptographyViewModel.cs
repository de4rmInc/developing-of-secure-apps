using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2_hash_algorithms.ViewModels
{
    public class CryptographyViewModel : BaseViewModel
    {
        private readonly int countOfBytesPerRow = 16;
        private const string ViewModeText_Bytes = "Switch to bytes";
        private const string ViewModeText_Text = "Switch to text";
        private string _hashCodeHex;
        private string _hashCodeBin;
        private string _originalValue;
        private string _encryptedValue;
        private string _originalValueHex;
        private bool _isViewModeBytes;

        private ObservableCollection<ObservableCollection<string>> _originValueBytes;

        public ObservableCollection<ObservableCollection<string>> OriginValueBytes
        {
            get { return _originValueBytes; }
            set
            {
                _originValueBytes = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<ObservableCollection<string>> _encryptedValueBytes;

        public ObservableCollection<ObservableCollection<string>> EncryptedValueBytes
        {
            get { return _encryptedValueBytes; }
            set
            {
                _encryptedValueBytes = value;
                RaisePropertyChanged();
            }
        }

        public CryptographyViewModel()
        {
            HashCodeHex = "init hex";
            HashCodeBin = "init bin";
            _originalValue = string.Empty;
            OriginValueBytes = new ObservableCollection<ObservableCollection<string>>();
            EncryptedValueBytes = new ObservableCollection<ObservableCollection<string>>();
        }

        public CryptographyViewModel SetModel(Models.CryptographyModel model)
        {
            return SetModelInternal(
                model.HashCode,
                model.FileBytes,
                model.IsTxt ? model.Text : model.FilePath,
                model.EncryptedFileBytes);
        }

        private CryptographyViewModel SetModelInternal(byte[] hashCode, byte[] originalValueBytes, string originalValue, byte[] encryptedValueBytes)
        {
            HashCodeHex = ConvertToHex(hashCode);
            HashCodeBin = ConvertToBin(hashCode);
            OriginalValueHex = ConvertToHex(originalValueBytes);
            OriginalValue = originalValue;
            EncryptedValue = ConvertToHex(encryptedValueBytes);

            SetCollection(originalValueBytes, OriginValueBytes);
            SetCollection(encryptedValueBytes, EncryptedValueBytes);

            RaisePropertyChanged(() => OriginalValueView);

            return this;
        }

        private void SetCollection(byte[] values, ObservableCollection<ObservableCollection<string>> collection)
        {
            collection.Clear();

            for (int i = 0, j = countOfBytesPerRow; i < values.Length; j = countOfBytesPerRow)
            {
                var collectionItem = new ObservableCollection<string>();

                while (j-- > 0 && i < values.Length)
                {
                    collectionItem.Add(Convert.ToString(values[i++], 16));
                }

                collection.Add(collectionItem);
            }
        }

        public string OriginalValueHex
        {
            get { return _originalValueHex; }
            set
            {
                _originalValueHex = value;
                RaisePropertyChanged();
            }
        }

        public bool IsViewModeBytes
        {
            get { return _isViewModeBytes; }
            set
            {
                _isViewModeBytes = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => ViewModeText);
                RaisePropertyChanged(() => OriginalValueView);
            }
        }

        public string ViewModeText
        {
            get { return !_isViewModeBytes ? ViewModeText_Bytes : ViewModeText_Text; }
        }
        public string OriginalValueView
        {
            get { return _isViewModeBytes ? OriginalValueHex : OriginalValue; }
            set
            {
                if (_isViewModeBytes)
                {
                    OriginalValueHex = value;
                }
                else
                {
                    OriginalValue = value;
                }
                RaisePropertyChanged();
            }
        }

        public string HashCodeHex
        {
            get { return _hashCodeHex; }
            private set
            {
                if (value != this._hashCodeHex)
                {
                    this._hashCodeHex = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string HashCodeBin
        {
            get { return _hashCodeBin; }
            private set
            {
                if (value != this._hashCodeBin)
                {
                    this._hashCodeBin = value;
                    RaisePropertyChanged();
                }

            }
        }

        public string OriginalValue
        {
            get { return _originalValue; }
            private set
            {
                if (value != this._originalValue)
                {
                    this._originalValue = value;
                    RaisePropertyChanged();
                }

            }
        }
        public string EncryptedValue
        {
            get { return _encryptedValue; }
            private set
            {
                if (value != this._encryptedValue)
                {
                    this._encryptedValue = value;
                    RaisePropertyChanged();
                }

            }
        }

        private string ConvertToHex(byte[] bytes)
        {
            if (bytes == null)
            {
                return string.Empty;
            }
            var hex = BitConverter.ToString(bytes);

            return hex;
        }

        private string ConvertToBin(byte[] bytes)
        {
            if (bytes == null)
            {
                return string.Empty;
            }

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
    }
}
