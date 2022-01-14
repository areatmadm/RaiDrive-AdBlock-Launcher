using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaiDrive_Launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void timer__Tick(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("RaiDrive").Length < 1)
            {
                //광고 제거를 위해 사용되었던 크로미움 브라우저 원복
                try
                {
                    File.Move(@"C:\Program Files\OpenBoxLab\RaiDrive\CefSharp.BrowserSubprocess.exe.temp", @"C:\Program Files\OpenBoxLab\RaiDrive\CefSharp.BrowserSubprocess.exe");
                }
                catch { }
                Delay(1000);
                timer_d.Enabled = false;
                this.Close();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();
            if (Process.GetProcessesByName("RaiDrive").Length > 0) //RaiDrive가 실행 중일 경우
            {
                MessageBox.Show("이미 RaiDrive가 실행 중입니다. RaiDrive 종료 후 다시 실행해 주세요.");
                this.Close(); //앱종료
                return;
            }
            else if (Process.GetProcessesByName("RaiDrive").Length > 1) //RaiDrive AdBlock Launcher 실행 중일 경우
            {
                MessageBox.Show("이미 RaiDrive AdBlock Launcher가 실행 중입니다. 재실행을 원하실 경우, RaiDrive와 RaiDrive AdBlock Launcher 종료 후 다시 실행해 주세요.");
                this.Close(); //앱종료
                return;
            }

            //이전에 강종 했는지 확인
            if (File.Exists(@"C:\Program Files\OpenBoxLab\RaiDrive\CefSharp.BrowserSubprocess.exe.temp"))
            {
                File.Move(@"C:\Program Files\OpenBoxLab\RaiDrive\CefSharp.BrowserSubprocess.exe.temp", @"C:\Program Files\OpenBoxLab\RaiDrive\CefSharp.BrowserSubprocess.exe");
                Delay(200);
            }


            Process raiDrive = new Process(); //새로운 변수 생성
            raiDrive.StartInfo.FileName = @"C:\Program Files\OpenBoxLab\RaiDrive\RaiDrive.exe"; //레이드라이브 경로 탐색

            raiDrive.Start(); //Process 시작
            Delay(5000); //10초 후 액션 실행

            //광고 제거를 위해 광고에 적용되는 크로미움 브라우저 실행 방지
            File.Move(@"C:\Program Files\OpenBoxLab\RaiDrive\CefSharp.BrowserSubprocess.exe", @"C:\Program Files\OpenBoxLab\RaiDrive\CefSharp.BrowserSubprocess.exe.temp");


            timer_d.Enabled = true;
            //timer_d.Start();
        }
    }
}
