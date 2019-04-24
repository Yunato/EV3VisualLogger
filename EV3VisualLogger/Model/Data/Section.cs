using System;
using EV3VisualLogger.UI.MyForm;

namespace EV3VisualLogger.Model.Data {
    public class Section {

        private Column[] _columns;
        public Column[] Columns {
            get { return _columns; }
        }

        private string _memo;
        private string _changedMemo;
        public string ChangedMemo {
            get { return _changedMemo; }
            set { _changedMemo = value; }
        }

        // fileNameは時刻だけで良い
        private string _fileName;
        public string FileName {
            get { return _fileName; }
        }
        private string _filePath;
        public string FilePath {
            get { return _filePath; }
        }
        private bool _isAllowedToSaveData;
        public bool IsAllowdToSaveData {
            get { return _isAllowedToSaveData; }
        }
        private bool _isUpdate = false;
        public bool IsUpdate {
            get { return _isUpdate; }
            set { _isUpdate = value; }
        }
        private bool _isAllowdToSaveMemo;
        public bool ISAllowdToSaveMemo {
            get { return _isAllowdToSaveMemo; }
        }

        private Section(string filePath) {
            _filePath = filePath;
            _fileName = filePath.Remove(0, filePath.LastIndexOf('\\') + 1);
        }

        public static Section CreateReceiveSection() {
            Section section = new Section(MainForm.RootPath + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
            section._isAllowedToSaveData = true;
            section._isAllowdToSaveMemo = false;
            section.CreateMemo();
            return section;
        }

        public static Section CreateReadSection(string filePath) {
            filePath = filePath.Remove(filePath.LastIndexOf('.'));
            Section section = new Section(filePath);
            section._isAllowedToSaveData = false;
            section._isAllowdToSaveMemo = false;
            return section;
        }

        public void AvoidOverWriteFile() {
            _fileName += "OR";
            _filePath += "OR";
        }

        /// <summary>
        /// カラム配列を生成する．
        /// </summary>
        /// <param name="data">カラム名配列</param>
        public void CreateColumns(int columnSize, string[] data) {
            _columns = new Column[columnSize];
            for(int index = 0; index < columnSize; ++index) {
                _columns[index] = new Column(data[index]);
            }
        }

        private void CreateMemo() {
            _memo = _changedMemo = "";
        }

        public void ReadMemo(string memo) {
            _memo = _changedMemo = memo;
        }

        public void ChangeMemo(string memo) {
            _changedMemo = memo;

            if (_changedMemo.Equals(_memo) || string.IsNullOrWhiteSpace(_changedMemo)) {
                _isAllowdToSaveMemo = false;
            }else {
                _isAllowdToSaveMemo = true;
            }
        }
    }
}
