﻿using Laba2_hash_algorithms.Commands;
using Laba2_hash_algorithms.Cryptography.HashAlgorithms;
using Laba2_hash_algorithms.Models;
using Laba2_hash_algorithms.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Laba2_hash_algorithms.ViewModels
{
    public class CryptographyViewModel : BaseViewModel
    {
        private ICommand _commandOpenFile;
        private ICommand _commandSwitchViewMode;
        private ICommand _commandCloseWindow;

        private OpenFileDialog _openFileDialog;
        private bool _fileLoaded;
        private CryptographyModel _cryptoModel;

        private readonly int countOfBytesPerRow = 16;
        private const string ViewModeText_Bytes = "Switch to bytes";
        private const string ViewModeText_Text = "Switch to text";
        private string _hashCodeHex;
        private string _hashCodeBin;
        private string _originalValue;
        private bool _isViewModeBytes;
        private bool _isText;
        private ObservableCollection<ObservableCollection<string>> _originValueBytes;
        private ObservableCollection<ObservableCollection<string>> _encryptedValueBytes;
        private ObservableCollection<HashCodeRoundInfo> _hashCodeSeries;

        public CryptographyViewModel()
        {
            HashCodeHex = "init hex";
            HashCodeBin = "init bin";
            IsViewModeBytes = true;
            _originalValue = string.Empty;
            _originValueBytes = new ObservableCollection<ObservableCollection<string>>();
            _encryptedValueBytes = new ObservableCollection<ObservableCollection<string>>();

            _hashCodeSeries = new ObservableCollection<HashCodeRoundInfo>();

            _openFileDialog = new OpenFileDialog();
            _cryptoModel = new CryptographyModel(string.Empty);
            _cryptoModel.RoundChanged += _cryptoModel_RoundChanged;

            InitializeCommands();

        }

        #region Commands
        private void InitializeCommands()
        {
            _commandOpenFile = new RelayCommand((o) => OpenFileDialog(o));
            _commandSwitchViewMode = new RelayCommand((o) => IsViewModeBytes = !IsViewModeBytes);
            _commandCloseWindow = new RelayCommand((o) => CloseWindow(o));

        }

        public ICommand CommandOpenFile
        {
            get { return _commandOpenFile; }
        }

        public ICommand CommandSwitchViewMode
        {
            get { return _commandSwitchViewMode; }
        }
        public ICommand CommandCloseWindow
        {
            get { return _commandCloseWindow; }
        }

        private void OpenFileDialog(object o)
        {
            this._fileLoaded = false;
            if (_openFileDialog.ShowDialog() ?? false)
            {
                _cryptoModel.FilePath = _openFileDialog.FileName;
                _cryptoModel.CalculateFileHashCode();
                _cryptoModel.Encrypt();

                this.SetModel(_cryptoModel);
            }
            this._fileLoaded = true;
        }
        private void CloseWindow(object o)
        {
            var window = o as Window;
            if (window != null)
            {
                window.Close();
            }
        }

        #endregion

        public CryptographyViewModel SetModel(CryptographyModel model)
        {
            HashCodeHex = model.HashCode.ConvertToHexString();
            HashCodeBin = model.HashCode.ConvertToBinString();
            IsText = model.IsTxt;
            OriginalValue = IsText ? model.Text : model.FilePath;

            SetCollection(model.FileBytes, OriginValueBytes);

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
                collectionItem.CollectionChanged += OriginValueBytes_CollectionChanged;
                collection.Add(collectionItem);
            }
        }

        private void OriginValueBytes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!this._fileLoaded) return;

            var bytes = CollectionToArray(OriginValueBytes);
            var newHash = _cryptoModel.CalculateHash(bytes);
            HashCodeHex = newHash.ConvertToHexString();
            HashCodeBin = newHash.ConvertToBinString();
            if (IsText)
            {
                OriginalValue = Encoding.Default.GetString(bytes);
            }
        }

        private byte[] CollectionToArray(ObservableCollection<ObservableCollection<string>> collection)
        {
            var listBytes = new List<byte>();

            foreach (var subCollection in collection)
            {
                listBytes.AddRange(subCollection.Select(x => Convert.ToByte(x, 16)));
            }

            return listBytes.ToArray();
        }
        #region Properties
        public ObservableCollection<ObservableCollection<string>> OriginValueBytes
        {
            get { return _originValueBytes; }
            set
            {
                _originValueBytes = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ObservableCollection<string>> EncryptedValueBytes
        {
            get { return _encryptedValueBytes; }
            set
            {
                _encryptedValueBytes = value;
                RaisePropertyChanged();
            }
        }

        public bool IsText
        {
            get { return _isText; }
            set
            {
                _isText = value;
                RaisePropertyChanged();
            }
        }

        public bool IsViewModeBytes
        {
            get { return _isViewModeBytes; }
            set
            {
                _isViewModeBytes = value;
                RaisePropertyChanged(() => ViewModeText);
                RaisePropertyChanged();
            }
        }
        public string ViewModeText
        {
            get { return !_isViewModeBytes ? ViewModeText_Bytes : ViewModeText_Text; }
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
                    if (IsText)
                    {
                        var bytes = Encoding.Default.GetBytes(value);
                        var newHash = _cryptoModel.CalculateHash(bytes);
                        HashCodeHex = newHash.ConvertToHexString();
                        HashCodeBin = newHash.ConvertToBinString();
                        SetCollection(bytes, OriginValueBytes);
                    }
                    this._originalValue = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<HashCodeRoundInfo> HashCodeSeries
        {
            get { return _hashCodeSeries; }
            set
            {
                _hashCodeSeries = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        private void _cryptoModel_RoundChanged(RoundEventArgs e)
        {
            if (e.LastRound)
            {
                UpdateHashCodeSeries(_newHashCodeSeries);
                _newHashCodeSeries.Clear();
                return;
            }

            _newHashCodeSeries.Add(new HashCodeRoundInfo { Round = e.Round, BitsChanged = e.BitsChangedCount });

        }
        List<HashCodeRoundInfo> _newHashCodeSeries = new List<HashCodeRoundInfo>();
        private void UpdateHashCodeSeries(List<HashCodeRoundInfo> collection)
        {
            HashCodeSeries.Clear();
            HashCodeSeries = new ObservableCollection<HashCodeRoundInfo>(collection);

        }
    }
}
