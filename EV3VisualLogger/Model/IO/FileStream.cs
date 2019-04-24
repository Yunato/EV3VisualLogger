using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using EV3VisualLogger.Model.Data;
using EV3VisualLogger.UI.Dialog;
using EV3VisualLogger.UI.MyForm;
using EV3VisualLogger.UI.View;

namespace EV3VisualLogger.Model.IO {
    class FileStream {

        private Section _section;

        public FileStream(Section section) {
            _section = section;
        }

        public void SaveFiles() {
            if (!Directory.Exists(MainForm.RootPath)){
                Directory.CreateDirectory(MainForm.RootPath);
            }
            SaveDataToCsv();
            SaveMemoToTxt();
        }

        private void SaveDataToCsv(){
            if (_section == null || !IsAbleToSaveData(_section.Columns.Length)) {
                return;
            }
	        Encoding enc = Encoding.GetEncoding("utf-8");
            StreamWriter writeStream = new StreamWriter(AssembleSaveCSVPath() , false, enc);

            foreach (Column column in _section.Columns) {
                writeStream.Write(column.Name + ",");
                for(int index = 0; index < column.ItemSize; ++index) {
                    writeStream.Write(column.GetValue(index) + ",");
                }
                writeStream.Write("\n");
            }
            writeStream.Write("\r\n");
            writeStream.Close();
            _section.IsUpdate = false;
        }

        private void SaveMemoToTxt() {
            string savePath = AssembleSaveTXTPath();
            if (!_section.ISAllowdToSaveMemo) {
                if (!File.Exists(savePath)) {
                    return;
                }

                FileInfo fileInfo = new FileInfo(savePath);
                fileInfo.Delete();
                return;
            }

	        Encoding enc = Encoding.GetEncoding("utf-8");
            StreamWriter writeStream = new StreamWriter(savePath, false, enc);
            writeStream.Write(_section.ChangedMemo);
            writeStream.Close();
        }

        private bool IsAbleToSaveData(int columnSize) {
            if(!_section.IsAllowdToSaveData || !_section.IsUpdate || columnSize <= 0) {
                return false;
            }

            if (!AutoSaveCheckBox.IsChecked) {
                if (!ShowQuestionMessageDialog("ログデータが保存されていません．\n\n自動保存がOFFになっています．\n\n取得したデータを保存しますか？")) {
                    return false;
                }
            }

            if(!File.Exists(AssembleSaveCSVPath())) {
                return true;
            }

            if (!ShowQuestionMessageDialog($"{_section.FileName + ".csv"}\n\n過去のログファイルが存在しています．\n\n上書き保存をしてよろしいですか？\n\n※「いいえ」の場合、別名(語尾にORを付加)で保存されます．")) {
                _section.AvoidOverWriteFile();
            }
            return true;
        }

        private bool ShowQuestionMessageDialog(string message) {
            MessageDialog dialog = new MessageDialog("ファイルの保存", SystemIcons.Question, message);
            if (dialog.ShowDialog() == DialogResult.No) {
                return false;
            }
            return true;
        }

        public string[] AccessFile() {
            try {
                List<string> fileContent = new List<string>();
                string[] dataAry = readData();
                foreach(string data in dataAry) {
                    fileContent.Add(data);
                }
                fileContent.Add(readMemo());
                return fileContent.ToArray();
            }catch (IOException e) {
                Console.WriteLine("エラー文開始");
                Console.WriteLine(e.ToString());
                Console.WriteLine("エラー文終了");
                string messagePath = AssembleSaveCSVPath();
                int position = 0;
                int findCount = 0;
                while((position = messagePath.IndexOf('\\', position)) != -1) {
                    if(++findCount % 6 == 0) {
                        messagePath = messagePath.Insert(position, "\n");
                        ++position;
                    }
                    ++position;
                }
                MessageDialog dialog = new MessageDialog("警告", SystemIcons.Exclamation, messagePath　+ "\n\n指定されたファイルは、現在別のプロセスによってアクセスが禁止されています。");
                dialog.ShowDialog();
                return null;
            }

        }

        private string[] readData() {
            StreamReader rs = new StreamReader(AssembleSaveCSVPath(), Encoding.GetEncoding("utf-8"));
            List<string> columnValues = new List<string>();

            if(rs.Peek() < 0) {
                rs.Close();
                return null;
            }

            while (rs.Peek() >= 0) {
                columnValues.Add(rs.ReadLine());
            }
            return columnValues.ToArray();
        }

        private string readMemo() {
            string filePath = AssembleSaveTXTPath();
            if (!File.Exists(filePath)) {
                return "";
            }else {
                StreamReader rs = new StreamReader(filePath, Encoding.GetEncoding("utf-8"));
                string memo = "";
                while(rs.Peek() >= 0) {
                    memo += rs.ReadLine();
                }
                rs.Close();
                return memo;
            }
        }

        private string AssembleSaveCSVPath() {
            return _section.FilePath + ".csv";
        }

        private string AssembleSaveTXTPath() {
            return _section.FilePath + ".txt";
        }

        /// <summary>
        /// 拡張子が.csvであるか調べる
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>判定結果</returns>
        public static bool CheckFileTypeIsCsv(string fileName) {
            return "csv".Equals(fileName.Substring(fileName.Length - "csv".Length, "csv".Length));
        }
    }
}
