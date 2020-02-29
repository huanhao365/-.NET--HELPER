using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SpeechLib;

namespace Speach
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        public static Speach sp = Speach.instance();
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SpeechRead());
        }

        #region 语音朗读类
        public class Speach
        {
            private static Speach _Instance = null;
            private SpeechLib.SpVoiceClass voice = null;
            private Speach()
            {
                BuildSpeach();
            }
            public static Speach instance()
            {
                if (_Instance == null)
                    _Instance = new Speach();
                return _Instance;
            }

            #region 设置朗读语言为中文
            private void SetChinaVoice()
            {
                voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
            }
            #endregion

            #region 设置朗读语言为英文
            private void SetEnglishVoice()
            {
                voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(1);
            }
            #endregion

            #region 中文朗读函数
            private void SpeakChina(string strSpeak)
            {
                SetChinaVoice();
                Speak(strSpeak);
            }
            #endregion

            #region 英文朗读函数
            private void SpeakEnglishi(string strSpeak)
            {
                SetEnglishVoice();
                Speak(strSpeak);
            }
            #endregion

            #region 分析语言函数
            public void AnalyseSpeak(string strSpeak)
            {
                int iCbeg = 0;
                int iEbeg = 0;
                bool IsChina = true;
                for (int i = 0; i < strSpeak.Length; i++)
                {
                    char chr = char.Parse(strSpeak.Substring(i, 1));
                    if (IsChina)
                    {
                        if (chr <= 122 && chr >= 65)
                        {
                            int iLen = i - iCbeg;
                            string strValue = strSpeak.Substring(iCbeg, iLen);
                            SpeakChina(strValue);
                            iEbeg = i;
                            IsChina = false;
                        }
                    }
                    else
                    {
                        if (chr > 122 || chr < 65)
                        {

                            int iLen = i - iEbeg;
                            string strValue = strSpeak.Substring(iEbeg, iLen);
                            this.SpeakEnglishi(strValue);
                            iCbeg = i;
                            IsChina = true;
                        }
                    }

                }
                if (IsChina)
                {
                    int iLen = strSpeak.Length - iCbeg;
                    string strValue = strSpeak.Substring(iCbeg, iLen);
                    SpeakChina(strValue);
                }
                else
                {
                    int iLen = strSpeak.Length - iEbeg;
                    string strValue = strSpeak.Substring(iEbeg, iLen);
                    SpeakEnglishi(strValue);
                }

            }
            #endregion

            #region 建立语音类
            private void BuildSpeach()
            {
                if (voice == null)
                    voice = new SpVoiceClass();
            }
            #endregion

            #region 设置音量函数
            public int Volume
            {
                get
                {
                    return voice.Volume;
                }
                set
                {
                    voice.SetVolume((ushort)(value));
                }
            }
            #endregion

            #region 设置语速函数
            public int Rate
            {
                get
                {
                    return voice.Rate;
                }
                set
                {
                    voice.SetRate(value);
                }
            }
            #endregion

            #region 朗读函数
            private void Speak(string strSpeack)
            {
                try
                {
                    voice.Speak(strSpeack, SpeechVoiceSpeakFlags.SVSFlagsAsync);
                }
                catch (Exception err)
                {
                    throw (new Exception("发生一个错误：" + err.Message));
                }
            }
            #endregion

            #region 停止函数
            public void Stop()
            {
                voice.Speak(string.Empty, SpeechLib.SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
            }
            #endregion

            #region 暂停函数
            public void Pause()
            {
                voice.Pause();
            }
            #endregion

            #region 继续函数
            public void Continue()
            {
                voice.Resume();
            }
            #endregion
        }
        #endregion

    }
}
