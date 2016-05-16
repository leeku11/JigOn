using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel; // CancelEventArgs

// _lkh
using Xceed.Wpf.Toolkit;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KeyboardDef
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        //const int MAX_COLUMN = 25;
        const int MAX_COLUMN = 20;
        const int MAX_ROW = 6;
        const int DEFAULT_KEY_SIZE = 32;//40;
        const int DEFAULT_KEY_SPAN = 1;//2;
        const int DEFAULT_KEYMAP_MARGIN_LEFT = 82;
        //const int MAX_LAYER = 8;
        const int MAX_LAYER = 4;
        const int MAX_KEYS = 256;
        const int MAX_RGB_COLUMN = 10;
        const int MAX_RGB_ROW = 2;
        const int MAX_RGB_COUNT = (MAX_RGB_COLUMN * MAX_RGB_ROW);
        const int DEFAULT_RGB_COLUMN = 10;
        const int DEFAULT_LEFT_MARGIN_LAYER = 100;
        const int DEFAULT_TOP_MARGIN_LAYER = 5;
        const int DEFAULT_LEFT_MARGIN_PICKER = 0;
        const int DEFAULT_TOP_MARGIN_PICKER = 0;
        const int MAX_PROCESS_WAIT_TIME = 5000;   // 5 seconds in msec

        public enum LAYOUT_DATA_MODE {
            LAYOUT_MODE,
            MAPPING_MODE
        }
        public enum KeyCode  {
                NONE,               // 0
                ErrorRollOver,
                POSTFail,
                ErrorUndefined,
                A,                  // 4
                B,                  // 5
                C,
                D,
                E,
                F,
                G,                  // 10
                H,
                I,
                J,
                K,
                L,                  // 15
                M,
                N,
                O,
                P,
                Q,                  // 20
                R,
                S,
                T,
                U,
                V,                  // 25
                W,
                X,
                Y,
                Z,
                _1,                 // 30
                _2,
                _3,
                _4,
                _5,
                _6,                 // 35
                _7,
                _8,
                _9,
                _0,
                ENTER,              // 40
                ESC,
                BKSP,               
                TAB,
                SPACE,
                MINUS,              // 45
                EQUAL,
                LBR,
                RBR,
                BKSLASH,
                Europe1,            // 50
                COLON,
                QUOTE,
                HASH,
                COMMA,
                DOT,                // 55
                SLASH,
                CAPS,
                F1,
                F2,
                F3,                 // 60
                F4,
                F5,
                F6,
                F7,
                F8,                 // 65
                F9,
                F10,
                F11,
                F12,
                PRNSCR,             // 70
                SCRLCK,
                PAUSE,
                INSERT,
                HOME,
                PGUP,               // 75
                DEL,
                END,
                PGDN,
                RIGHT,
                LEFT,               // 80
                DOWN,
                UP,
                NUMLOCK,
                KP_SLASH,
                KP_AST,             // 85
                KP_MINUS,
                KP_PLUS,
                KP_ENTER,
                KP_1,
                KP_2,               // 90
                KP_3,
                KP_4,
                KP_5,
                KP_6,
                KP_7,               // 95
                KP_8,
                KP_9,
                KP_0,
                KP_DOT,
                Europe2,            // 100
                APPS,
                POWER_HID,
                KP_EQUAL,
                LED0,
                LED1,               // 105
                LED2,
                LED3,
                LFX, 
                LPAD,
                LFULL,              // 110
                LASD,
                LARR,
                LVESEL,   
                MRESET, 
                RESET,              // 115
                FN,
                HELP,
                MENU,
                SEL,
                STOP_HID,           // 120
                AGAIN,
                UNDO,
                CUT,
                COPY,
                PASTE,              // 125
                FIND,
                MUTE_HID,
                VOLUP,
                VOLDN,
                KL_CAP,             // 130
                KL_NUM,
                KL_SCL,
                KP_COMMA,
                EQUAL_SIGN,
                L0,                 // 135
                L1,
                L2,
                L3,
                L4,
                L5,                 // 140
                L6,
                INTL8,
                INTL9,
                HANJA,
                HANGLE,             // 145
                KATA,
                HIRA,
                System,
                POWER,
                SLEEP,              // 150
                WAKE,
                KEYLOCK,
                WINKEYLOCK,
                SYSREQ,
                CANCEL,             // 155
                CLEAR,
                PRIOR,
                RETURN,
                SPERATOR,
                OUT,                // 160
                OPER,
                CLR_AGIN,
                CRSEL,
                EXCEL,
                /* These are NOT standard USB HID - handled specially in decoding, 
                so they will be mapped to the modifier byte in the USB report */
                Modifiers,          // 165
                LCTRL,
                LSHIFT,
                LALT,
                LGUI,
                RCTRL,              // 170
                RSHIFT,
                RALT,
                RGUI,
                Modifiers_end,
                NEXT_TRK,           // 175        
                PREV_TRK,
                STOP,
                PLAY,
                MUTE,
                BASS_BST,           // 180
                LOUDNESS,
                VOL_UP,
                VOL_DOWN,
                BASS_UP,
                BASS_DN,            // 185
                TRE_UP,
                TRE_DN,
                MEDIA_SEL,
                MAIL,
                CALC,               // 190
                MYCOM,
                WWW_SEARCH,
                WWW_HOME,
                WWW_BACK,
                WWW_FORWARD,        // 195
                WWW_STOP,
                WWW_REFRESH,
                WWW_FAVORITE,
                EJECT,
                SCREENSAVE,         // 200
                REC,
                REWIND,
                MINIMIZE,
                M01,
                M02,                // 205
                M03,
                M04,
                M05,
                M06,
                M07,                // 210
                M08,
                M09,
                M10,
                M11,
                M12,                // 215
                M13,
                M14,
                M15,
                M16,
                M17,                // 220
                M18,
                M19,
                M20,        
                M21,        
                M22,                // 225        
                M23,       
                M24,   
                M25,
                M26,
                M27,                // 230
                M28,
                M29,
                M30,
                M31,
                M32,                // 235
                M33,
                M34,
                M35,
                M36,
                M37,                // 240
                M38,
                M39,
                M40,
                M41,
                M42,                // 245
                M43,
                M44,
                M45,
                M46,
                M47,                // 250
                M48,
                M49,
                M50,
                M51,
                M52                 // 255
        };

        public Dictionary<Key, KeyCode> KeyCodeMap = new Dictionary<Key, KeyCode>
        {
            {Key.A, KeyCode.A},
            {Key.B, KeyCode.B},
            {Key.Add, KeyCode.KP_PLUS},
            {Key.Apps, KeyCode.APPS},
            {Key.Back, KeyCode.BKSP},
            {Key.BrowserBack, KeyCode.WWW_BACK},
            {Key.BrowserFavorites, KeyCode.WWW_FAVORITE},
            {Key.BrowserForward, KeyCode.WWW_FORWARD},
            {Key.BrowserHome, KeyCode.WWW_HOME},
            {Key.BrowserRefresh, KeyCode.WWW_REFRESH},
            {Key.BrowserSearch, KeyCode.WWW_SEARCH},
            {Key.BrowserStop, KeyCode.WWW_STOP},
            {Key.C, KeyCode.C},
            {Key.Cancel, KeyCode.CANCEL},
            //{Key.Capital, KeyCode.KL_CAP},
            {Key.CapsLock, KeyCode.CAPS},
            {Key.Clear, KeyCode.CLEAR},
            {Key.CrSel, KeyCode.CRSEL},
            {Key.D, KeyCode.D},
            {Key.D0, KeyCode._0},
            {Key.D1, KeyCode._1},
            {Key.D2, KeyCode._2},
            {Key.D3, KeyCode._3},
            {Key.D4, KeyCode._4},
            {Key.D5, KeyCode._5},
            {Key.D6, KeyCode._6},
            {Key.D7, KeyCode._7},
            {Key.D8, KeyCode._8},
            {Key.D9, KeyCode._9},
            {Key.Delete,KeyCode.DEL},
            {Key.Divide,KeyCode.KP_SLASH},
            {Key.Down, KeyCode.DOWN},
            {Key.E, KeyCode.E},
            {Key.End, KeyCode.END},
            {Key.Enter, KeyCode.ENTER},
            {Key.Escape, KeyCode.ESC}
           
        };

        public enum KeyPressState {
            KEY_PRS_ST_START = 0,
            KEY_PRS_ST_ROW,
            KEY_PRS_ST_DELIMITER,
            KEY_PRS_ST_COLUMN,
            KEY_PRS_ST_TERMINATION,
            KEY_PRS_ST_MAX
        };

        const string KEYMAP = "KEYMAP";
        const string LAYER = "LAYER";
        const string ROW = "ROW";
        const string COLUMN = "COLUMN";
        const string LAYOUT = "LAYOUT";
        const string MAPPING = "MAPPING";
        
        private Style myStyle;
        private Style myStyleSelected;
        private Style myStyleAssigned;

        private KeyCode[,] keyArray = new KeyCode[MAX_LAYER, MAX_KEYS];
        private Button[] keyboardArray = new Button[MAX_LAYER * MAX_KEYS];
        private int gLayer = MAX_LAYER;
        private int gRow = MAX_ROW;
        private int gColumn = MAX_COLUMN;
        private int KeyboardCount = MAX_ROW * MAX_COLUMN;
        private int selectedIndex = 0;
        private int mCurrentLayer = 0;
        private int mRgbColumn = DEFAULT_RGB_COLUMN;
        private int mKeyLayerChildOffset = 0; // WAR for strange symptom

        private ComboBox mRgbComboBox;
        private ComboBox mEffectComboBox;
        private ComboBox mLimitComboBox;
        private ComboBox mLedPresetSelectComboBox;
        private ComboBox[] mLedPresetKeyComboBox = new ComboBox[3];
        private ComboBox[] mLedPresetArrowComboBox = new ComboBox[3];
        private ComboBox mLayerComboBox;
        private ComboBox mSleepComboBox;
        private CheckBox[] mSyncCheckBox = new CheckBox[MAX_RGB_ROW];

        private KeyPressState mKeyPressState;
        private Key[] mKeyPressed = new Key [5];

        private Color[,] gRgbLedArray = new Color[MAX_RGB_ROW, MAX_RGB_COLUMN]
            {
                { Color.FromRgb(250, 0, 250), Color.FromRgb(250, 0, 250), Color.FromRgb(250, 0, 0), Color.FromRgb(250, 0, 100), Color.FromRgb(250, 250, 0), Color.FromRgb(0, 250, 0), Color.FromRgb(0, 0, 250), Color.FromRgb(50, 0, 250), Color.FromRgb(250, 0, 250), Color.FromRgb(250, 0, 100) },
                { Color.FromRgb(250, 0, 0), Color.FromRgb(250, 0, 0), Color.FromRgb(250, 100, 0), Color.FromRgb(250, 250, 0), Color.FromRgb(0, 250, 0), Color.FromRgb(0, 0, 250), Color.FromRgb(50, 0, 250), Color.FromRgb(250, 0, 250), Color.FromRgb(250, 0, 100), Color.FromRgb(250, 0, 100) }
            };
        private byte[, ,] keymapArray = new byte[MAX_LAYER, MAX_ROW, MAX_COLUMN];

        public double[,] KeyboardLayout = new double[MAX_ROW, MAX_COLUMN] 
            {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
/*
                { 1.3, -0.5, 1, 1, 1, 1, -0.5, 1, 1, 1, 1, -0.5, 1, 1, 1, 1, -0.5, 1, 1, 1, 0, 0, 0, 0, 0},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, -0.5, 1, 1, 1, -0.5, 1, 1, 1, 1, 0, 0 },
                { 1.5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1.5, -0.5, 1, 1, 1, -0.5, 1, 1, 1, 0, 0, 0},
                { 1.75, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2.25, -4.25, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0},
                { 2.0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3.0, -1.75, 1, -1.5, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0},
                { 1, 1.25, 1, 1, 5, 1, 1, 1, 1, 1, 1, -0.5, 1, 1, 1, -0.5, 2, 1, 0, 0, 0, 0, 0, 0, 0},
*/
            };

        public Dictionary<int, int> KeyMapping = new Dictionary<int,int>();  // 저장순서 - Key 위치 

        private int keymAddress = 0x6000;

        private byte[] gSleepTime = new byte[9]
            {
                1, 3, 5, 7, 10, 20, 30, 60, 120
            };

        private ushort[] gRbgLimit = new ushort[5]
            {
                100, 200, 300, 400, 500
            };

        // _lkh
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct rgb_type
        {
            public byte g;
            public byte r;
            public byte b;
        };

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct rgb_effect_param_type
        {
            public byte index;
            public byte high_hold;
            public byte low_hold;
            public byte accel_mode;
        };

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct kbd_configuration_t
        {
            public byte ps2usb_mode;                                                           // 0: PS2, 1: USB
            public byte keymapLayerIndex;                                                      // KEYMAP layer index;
            public byte swapCtrlCaps;                                                          // 1: Swap Capslock <-> Left Ctrl
            public byte swapAltGui;                                                            // 1: Swap Alt <-> GUI(WIN) 
            public byte led_preset_index;                                                      // LED effect  preset index
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            public byte[] led_preset;                                                          // Block configuration for LED effect  preset 3 x 5
            public byte rgb_effect_index;                                                      // RGB effect preset
            public byte rgb_chain;                                                             // RGB5050 numbers (H/W dependent)
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public rgb_type[] rgb_preset;                                                         // Chain color 20 x 3
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public rgb_effect_param_type[] rgb_effect_param;                                   // RGB effect parameter 6
            public ushort rgb_limit;
            public ushort rgb_speed;
            public byte matrix_debounce;
            public byte sleep_timer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] pad;
        };

        private kbd_configuration_t kbd_conf;

        public MainWindow()
        {
            InitializeComponent();
            // _lkh
            InitKeyboardConfig();

            mKeyPressState = KeyPressState.KEY_PRS_ST_START;
            //this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            this.KeyUp += new KeyEventHandler(OnButtonKeyUp);
        }

        private void LoadKeyboardData()
        {
            //if (LoadKeyboardLayout(System.IO.Path.GetTempPath() + "keylayout.txt"))
            if (!LoadKeyboardLayout(".\\keylayout.txt"))
            {
                setKeyboardLayout();
            }

            /*
            //if (!LoadKeyboardData(System.IO.Path.GetTempPath() + "keydata.bin"))
            if (!LoadKeyboardData(".\\keydata.bin"))
            {
                setDefalutKeyData();
            }
            */

            // _lkh
            if(!LoadKeymapData(".\\keymap.bin"))
            {
                setDefaultKeymapData();
            }

            if (!LoadKbdCfgData(".\\kbdcfg.cfg"))
            {
                setDefaultKbdCfgData();
            }
        }

        private void setKeyboardLayout()
        {
            int i;
            KeyMapping.Clear();
            for (i = 0; i < (MAX_ROW * MAX_COLUMN); i++)
            {
                KeyMapping.Add(i, i);
            }
        }

        private void setDefalutKeyData()
        {
            int i, j;

            for (i = 0; i < MAX_LAYER; i++)
            {
                for (j = 0; j < MAX_KEYS; j++)
                {
                    keyArray[i, j] = KeyCode.NONE;
                }
            }
        }

        // _lkh
        private void setDefaultKeymapData()
        {
            int i, j, k;
            for (i = 0; i < MAX_LAYER; i++)
            {
                for (j = 0; j < MAX_ROW; j++)
                {
                    for (k = 0; k < MAX_COLUMN; k++)
                    {
                        keymapArray[i, j, k] = (byte)KeyCode.NONE;
                    }
                }
            }
        }

        private void setDefaultKbdCfgData()
        {
            InitKeyboardConfig();
        }

        private void savekeyData()
        {
            //SaveKeyboardLayout(System.IO.Path.GetTempPath() + "keylayout.txt");
            //SaveKeyboardData(System.IO.Path.GetTempPath() + "keydata.bin");
            SaveKeyboardLayout(".\\keylayout.txt");
            //SaveKeyboardData(".\\keydata.bin");
            SaveKeymapData(".\\keymap.bin");
            SaveKeyboardConfigData(".\\kbdcfg.cfg");
        }


        private void Window_Initialized(object sender, EventArgs e)
        {
            //UsbInitialize();
            LoadKeyboardData();

            makeKeyboardLayout();
            makeRgbLayout();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            savekeyData();
        }

        private void makeKeymapLayout()
        {
            int i;

            keyboardGrid.Children.Clear();

            myStyle = (Style)Application.Current.FindResource("buttonRoundCorner");
            myStyleSelected = (Style)Application.Current.FindResource("buttonRoundSelected");
            myStyleAssigned = (Style)Application.Current.FindResource("buttonRoundAssgined");

            KeyboardCount = 0;

            Label newLabel = new Label();
            newLabel.Margin = new Thickness(-4, 0, 0, 0);
            newLabel.Content = "Layer";
            keyboardGrid.Children.Add(newLabel);

            for (i = 0; i < gLayer; i++)
            {
                RadioButton newRadio = new RadioButton();
                if (i == 3)
                {
                    newRadio.Content = "Layer FN";
                    newRadio.Name = "LayerFN";
                }
                else
                {
                    newRadio.Content = "Layer " + i.ToString();
                    newRadio.Name = "Layer" + i.ToString();
                }
                //newRadio.Margin = new Thickness(DEFAULT_LEFT_MARGIN_LAYER + i * 100, DEFAULT_TOP_MARGIN_LAYER, 0, 0);
                newRadio.Margin = new Thickness(0, 30 + i * 20, 0, 0);
                newRadio.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                newRadio.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                newRadio.Tag = i;
                newRadio.Click += new RoutedEventHandler(Layer_Checked);
                if (i == 0)
                    newRadio.IsChecked = true;

                keyboardGrid.Children.Add(newRadio);
            }

            mKeyLayerChildOffset = keyboardGrid.Children.Count;

            int leftPosition;
            double keySize;

            for (int j = 0; j < MAX_ROW; j++)
            {
                leftPosition = DEFAULT_KEYMAP_MARGIN_LEFT;
                for (i = 0; i < MAX_COLUMN; i++)
                {
                    keySize = KeyboardLayout[j, i] * DEFAULT_KEY_SIZE;
                    if (keySize > 0)
                    {
                        System.Windows.Controls.Button newBtn = new Button();

                        //newBtn.Content = j.ToString() + " x " + i.ToString();
                        newBtn.Name = "Button" + j.ToString() + "x" + i.ToString();
                        newBtn.Width = (int)keySize;
                        newBtn.Height = DEFAULT_KEY_SIZE;
                        newBtn.Margin = new Thickness(leftPosition, j * (DEFAULT_KEY_SIZE + DEFAULT_KEY_SPAN), 0, 0);
                        newBtn.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        newBtn.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        newBtn.Style = myStyle;
                        //Grid.SetColumn(newBtn, 1);
                        //Grid.SetRow(newBtn, 2);
                        newBtn.Tag = KeyboardCount;
                        newBtn.Click += new RoutedEventHandler(keyButton_Click);
                        //newBtn.KeyUp += new KeyEventHandler(Button_KeyUp);

                        if (keyArray[mCurrentLayer, KeyboardCount] == KeyCode.NONE)
                        {
                            newBtn.Style = myStyle;
                        }
                        else
                        {
                            newBtn.Style = myStyleAssigned;
                        }
                        newBtn.Content = keyArray[mCurrentLayer, KeyboardCount].ToString();

                        keyboardArray[KeyboardCount] = newBtn;

                        keyboardGrid.Children.Add(newBtn);

                        leftPosition += ((int)keySize + DEFAULT_KEY_SPAN);

                        KeyboardCount++;
                    }
                    else if (keySize < 0)
                    {
                        leftPosition += ((int)(-keySize) + DEFAULT_KEY_SPAN);
                    }
                }
            }

            selectKey(selectedIndex);
        }

        /*
        private void makeLayerLayout()
        {
            int i;

            Label newLabel = new Label();
            newLabel.Margin = new Thickness(-4, 0, 0, 0);
            newLabel.Content = "Keymap/Layer";
            keyboardGrid.Children.Add(newLabel);

            for (i = 0; i < gLayer; i++)
            {
                RadioButton newRadio = new RadioButton();
                if (i == 3)
                {
                    newRadio.Content = "Layer FN";
                    newRadio.Name = "LayerFN";
                }
                else
                {
                    newRadio.Content = "Layer " + i.ToString();
                    newRadio.Name = "Layer" + i.ToString();
                }
                //newRadio.Margin = new Thickness(DEFAULT_LEFT_MARGIN_LAYER + i * 100, DEFAULT_TOP_MARGIN_LAYER, 0, 0);
                newRadio.Margin = new Thickness(0, 30 + i * 20, 0, 0);
                newRadio.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                newRadio.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                newRadio.Tag = i;
                newRadio.Click += new RoutedEventHandler(Layer_Checked);
                if (i == 0)
                    newRadio.IsChecked = true;

                keyboardGrid.Children.Add(newRadio);
            } 
        }
        */

        private int GetSleepTimerIndexFromValue(int nTime)
        {
            int nIndex = 0;

            for (nIndex = 0; nIndex < 9; nIndex++)
            {
                if (gSleepTime[nIndex] == nTime)
                {
                    break;
                }
            }

            if (nIndex >= 9)
            {
                nIndex = 0;
            }

            return nIndex;
        }

        private byte GetSleepTimerValueFromIndex(int nIndex)
        {
            byte nTime = 1;

            if (nIndex < 9)
            {
                nTime = gSleepTime[nIndex];
            }

            return nTime;
        }

        private int GetRgbLimitIndexFromValue(int nLimit)
        {
            int nIndex = 0;

            for (nIndex = 0; nIndex < 5; nIndex++)
            {
                if (gRbgLimit[nIndex] == nLimit)
                {
                    break;
                }
            }

            if (nIndex >= 5)
            {
                nIndex = 0;
            }

            return nIndex;
        }

        private ushort GetRgbLimitValueFromIndex(int nIndex)
        {
            ushort nLimit = gRbgLimit[4];

            if (nIndex < 5)
            {
                nLimit = gRbgLimit[nIndex];
            }

            return nLimit;
        }

        private void makeLedEffectLayout()
        {
            int i = 0;

            LedEffectGrid.Children.Clear();

            Label newLabel = new Label();
            newLabel.Margin = new Thickness(-4, 25, 0, 0);
            newLabel.Content = "Default Layer";
            LedEffectGrid.Children.Add(newLabel);

            ComboBox newComboBox = new ComboBox();
            newComboBox.Margin = new Thickness(80, 25, 0, 0);
            newComboBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            newComboBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            newComboBox.Width = 80;
            newComboBox.Height = 25;
            List<string> data = new List<string>();
            data.Add("Layer 0");
            data.Add("Layer 1");
            data.Add("Layer 2");
            newComboBox.ItemsSource = data;
            newComboBox.SelectedIndex = kbd_conf.keymapLayerIndex;
            newComboBox.SelectionChanged += new SelectionChangedEventHandler(LayerComboBox_SelectionChanged);
            mLayerComboBox = newComboBox;
            LedEffectGrid.Children.Add(mLayerComboBox);

            newLabel = new Label();
            newLabel.Margin = new Thickness(-4, 50, 0, 0);
            newLabel.Content = "LED Effect";
            LedEffectGrid.Children.Add(newLabel);

            newComboBox = new ComboBox();
            newComboBox.Margin = new Thickness(80, 51 , 0, 0);
            newComboBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            newComboBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            newComboBox.Width = 80;
            newComboBox.Height = 25;
            data = new List<string>();
            data.Add("Preset 0");
            data.Add("Preset 1");
            data.Add("Preset 2");
            newComboBox.ItemsSource = data;
            newComboBox.SelectedIndex = kbd_conf.led_preset_index;
            newComboBox.SelectionChanged += new SelectionChangedEventHandler(LedPresetComboBox_SelectionChanged);
            mLedPresetSelectComboBox = newComboBox;
            LedEffectGrid.Children.Add(mLedPresetSelectComboBox);

            newLabel = new Label();
            newLabel.Margin = new Thickness(-4, 75, 0, 0);
            newLabel.Content = "Sleep Time";
            LedEffectGrid.Children.Add(newLabel);

            newComboBox = new ComboBox();
            newComboBox.Margin = new Thickness(80, 77, 0, 0);
            newComboBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            newComboBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            newComboBox.Width = 80;
            newComboBox.Height = 25;
            data = new List<string>();
            data.Add("1 min");
            data.Add("3 min");
            data.Add("5 min");
            data.Add("7 min");
            data.Add("10 min");
            data.Add("20 min");
            data.Add("30 min");
            data.Add("60 min");
            data.Add("120 min");
            newComboBox.ItemsSource = data;
            newComboBox.SelectedIndex = GetSleepTimerIndexFromValue(kbd_conf.sleep_timer);
            newComboBox.SelectionChanged += new SelectionChangedEventHandler(SleepComboBox_SelectionChanged);
            mSleepComboBox = newComboBox;
            LedEffectGrid.Children.Add(mSleepComboBox);

            newLabel = new Label();
            newLabel.Margin = new Thickness(285, 0, 0, 0);
            newLabel.Content = "Key";
            LedEffectGrid.Children.Add(newLabel);

            newLabel = new Label();
            newLabel.Margin = new Thickness(375, 0, 0, 0);
            newLabel.Content = "Arrow";
            LedEffectGrid.Children.Add(newLabel);

            for (i = 0; i < 3; i++)
            {
                newLabel = new Label();
                newLabel.Margin = new Thickness(180, 24 + i * 26, 0, 0);
                newLabel.Content = "Preset " + i.ToString();
                LedEffectGrid.Children.Add(newLabel);

                ComboBox newComboBox2 = new ComboBox();
                newComboBox2.Margin = new Thickness(250, 25 + i * 26, 0, 0);
                newComboBox2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                newComboBox2.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                newComboBox2.Width = 100;
                newComboBox2.Height = 25;
                newComboBox2.Tag = i * 5 + 3;
                data = new List<string>();
                data.Add("Fading");
                data.Add("Fading+Push On");
                data.Add("Push Level");
                data.Add("Push On");
                data.Add("Push Off");
                data.Add("Always");
                data.Add("Caps Lock");
                data.Add("Off");
                newComboBox2.ItemsSource = data;
                newComboBox2.SelectedIndex = kbd_conf.led_preset[i*5 + 3];
                newComboBox2.SelectionChanged += new SelectionChangedEventHandler(LedEffectKeyComboBox_SelectionChanged);
                mLedPresetKeyComboBox[i] = newComboBox2;
                LedEffectGrid.Children.Add(mLedPresetKeyComboBox[i]);

                ComboBox newComboBox3 = new ComboBox();
                newComboBox3.Margin = new Thickness(351, 25 + i * 26, 0, 0);
                newComboBox3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                newComboBox3.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                newComboBox3.Width = 100;
                newComboBox3.Height = 25;
                newComboBox3.Tag = i * 5 + 4;
                data = new List<string>();
                data.Add("Fading");
                data.Add("Fading+Push On");
                data.Add("Push Level");
                data.Add("Push On");
                data.Add("Push Off");
                data.Add("Always");
                data.Add("Caps Lock");
                data.Add("Off");
                newComboBox3.ItemsSource = data;
                newComboBox3.SelectedIndex = kbd_conf.led_preset[i * 5 + 4]; ;
                newComboBox3.SelectionChanged += new SelectionChangedEventHandler(LedEffectArrowComboBox_SelectionChanged);
                mLedPresetArrowComboBox[i] = newComboBox3;
                LedEffectGrid.Children.Add(mLedPresetArrowComboBox[i]);
            }
 
        }

        private void makeRgbEffectLayout()
        {
            RgbEffectGrid.Children.Clear();

            Label newLabel = new Label();
            newLabel.Margin = new Thickness(-4, 0, 0, 0);
            newLabel.Content = "RGB Effect";
            RgbEffectGrid.Children.Add(newLabel);

            ComboBox newComboBox = new ComboBox();
            newComboBox.Margin = new Thickness(80, 0, 0, 0);
            newComboBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            newComboBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            newComboBox.Width = 80;
            newComboBox.Height = 25;

            List<string>  data = new List<string>();
            data.Add("14 RGB");
            data.Add("20 RGB");
            newComboBox.ItemsSource = data;
            if (mRgbColumn == DEFAULT_RGB_COLUMN)
            {
                newComboBox.SelectedIndex = 1;
            }
            else
            {
                newComboBox.SelectedIndex = 0;
            }
            newComboBox.SelectionChanged += new SelectionChangedEventHandler(RgbComboBox_SelectionChanged);
            mRgbComboBox = newComboBox;
            RgbEffectGrid.Children.Add(mRgbComboBox);

            newComboBox = new ComboBox();
            newComboBox.Margin = new Thickness(180, 0, 0, 0);
            newComboBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            newComboBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            newComboBox.Width = 100;
            newComboBox.Height = 25;

            data = new List<string>();
            data.Add("RGB Effect 1");
            data.Add("RGB Effect 2");
            data.Add("RGB Effect 3");
            data.Add("RGB Effect 4");
            data.Add("RGB Effect 5");
            data.Add("RGB Effect 6");
            newComboBox.ItemsSource = data;
            newComboBox.SelectedIndex = kbd_conf.rgb_effect_index;
            newComboBox.SelectionChanged += new SelectionChangedEventHandler(EffectComboBox_SelectionChanged);
            mEffectComboBox = newComboBox;
            RgbEffectGrid.Children.Add(mEffectComboBox);

            newComboBox = new ComboBox();
            newComboBox.Margin = new Thickness(300, 0, 0, 0);
            newComboBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            newComboBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            newComboBox.Width = 150;
            newComboBox.Height = 25;

            data = new List<string>();
            data.Add("Max Brightness 100");
            data.Add("Max Brightness 200");
            data.Add("Max Brightness 300");
            data.Add("Max Brightness 400");
            data.Add("Max Brightness 500");
            newComboBox.ItemsSource = data;
            newComboBox.SelectedIndex = GetRgbLimitIndexFromValue(kbd_conf.rgb_limit);
            newComboBox.SelectionChanged += new SelectionChangedEventHandler(LimitComboBox_SelectionChanged);
            mLimitComboBox = newComboBox;
            RgbEffectGrid.Children.Add(mLimitComboBox);

        }

        private void makeKeyboardLayout()
        {
            makeKeymapLayout();

            //makeLayerLayout();

            makeLedEffectLayout();

            makeRgbEffectLayout();
        }

        private void makeRgbLayout()
        {
            int i, j;

            ColorPickerGrid.Children.Clear();
            for (j = 0; j < MAX_RGB_ROW; j++)
            {
                for (i = 0; i < mRgbColumn; i++)
                {
                    ColorPicker newColorPicker = new ColorPicker();
                    newColorPicker.Width = 65;
                    newColorPicker.Height = 25;
                    newColorPicker.Margin = new Thickness(DEFAULT_LEFT_MARGIN_PICKER + i * (newColorPicker.Width + 1), DEFAULT_TOP_MARGIN_PICKER + (MAX_RGB_ROW - j - 1) * (newColorPicker.Height + 1), 0, 0);
                    newColorPicker.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    newColorPicker.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    newColorPicker.Tag = j * mRgbColumn + i;
                    newColorPicker.SelectedColor = gRgbLedArray[j, i];
                    newColorPicker.SelectedColorChanged += new RoutedPropertyChangedEventHandler<Color>(Color_Changed);
                    ColorPickerGrid.Children.Add(newColorPicker);
                }

                CheckBox newCheckBox = new CheckBox();
                newCheckBox.Width = 80;
                newCheckBox.Height = 25;
                newCheckBox.Content = "Sync";
                newCheckBox.Margin = new Thickness(20 + DEFAULT_LEFT_MARGIN_PICKER + mRgbColumn * (65 + 1), 5 + j * 25, 0, 0);
                newCheckBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                newCheckBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                newCheckBox.Tag = j;
                mSyncCheckBox[j] = newCheckBox;
                ColorPickerGrid.Children.Add(mSyncCheckBox[j]);
            }
            // test
        }

        private void selectKey(int index)
        {
            if (keyArray[mCurrentLayer, selectedIndex] == KeyCode.NONE)
                keyboardArray[selectedIndex].Style = myStyle;
            else
                keyboardArray[selectedIndex].Style = myStyleAssigned;

            selectedIndex = index;

            keyboardArray[selectedIndex].Style = myStyleSelected;
        }

        private void assignKey(int keycode)
        {
            keyArray[mCurrentLayer, selectedIndex] = (KeyCode)keycode;
            keyboardArray[selectedIndex].Content = keyArray[mCurrentLayer, selectedIndex].ToString();
            // _lkh
            int row, col;
            row = selectedIndex / MAX_COLUMN;
            col = selectedIndex % MAX_COLUMN;
            keymapArray[mCurrentLayer, row, col] = (byte)keycode;
        }

        private void ChangeKeyboardLayer()
        {
            for (int i = 0; i < KeyboardCount; i++)
            {
                keyboardArray[i].Content = keyArray[mCurrentLayer, i].ToString();
                if (keyArray[mCurrentLayer, i] == KeyCode.NONE)
                    keyboardArray[i].Style = myStyle;
                else
                    keyboardArray[i].Style = myStyleAssigned;
            }
            selectKey(selectedIndex);
        }

        private void keyButton_Click(object sender, EventArgs e)
        {
            //do something or...
            Button clicked = (Button)sender;

            int index = (int)clicked.Tag;

            selectKey(index);

            /*
            KeyInput kiDialag = new KeyInput();
            kiDialag.Owner = this;
            if (kiDialag.ShowDialog() == true)
            {
                clicked.Style = myStyleSelected;
                int index = (int)clicked.Tag;
                keyArray[mCurrentLayer, index] = TranslateKeyCode(kiDialag.selectKey);
                clicked.Content = KeyName[keyArray[mCurrentLayer, index]];
            }
            */
        }

        private void LayoutSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".txt"; // Default file extension
            sfd.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension 
            if (sfd.ShowDialog() == true)
            {
                string filename = sfd.FileName;
                SaveKeyboardLayout(filename);
            }
        }

        private void SaveKeyboardLayout(string filename)
        {
            StreamWriter outfile = new StreamWriter(filename);

            outfile.Write(KEYMAP);
            outfile.Write("\t");
            outfile.Write(String.Format("0x{0:X}", keymAddress));
            outfile.Write("\r\n");

            outfile.Write(LAYER);
            outfile.Write("\t\t");
            outfile.Write(String.Format("{0}", gLayer));
            outfile.Write("\r\n");

            outfile.Write(ROW);
            outfile.Write("\t\t");
            outfile.Write(String.Format("{0}", gRow));
            outfile.Write("\r\n");

            outfile.Write(COLUMN);
            outfile.Write("\t");
            outfile.Write(String.Format("{0}", gColumn));
            outfile.Write("\r\n");

            outfile.Write(LAYOUT);
            outfile.Write("\r\n");

            for (int j = 0; j < gRow; j++)
            {
                for (int i = 0; i < gColumn; i++)
                {
                    outfile.Write(KeyboardLayout[j, i].ToString());
                    outfile.Write(",");
                }
                outfile.Write("\r\n");
            }

            outfile.Write(MAPPING);
            outfile.Write("\r\n");

            for (int j = 0; j < KeyMapping.Count; j++)
            {
                outfile.Write(KeyMapping[j]);
                if (((j + 1) % 16) == 0)
                    outfile.Write("\r\n");
                else
                {
                    if (j != (KeyMapping.Count - 1))
                        outfile.Write(",");
                }
            }

            outfile.Close();
        }

        private bool LoadKeyboardLayout(string txtName)
        {
            bool isError = false;
            try
            {
                StreamReader sr = new StreamReader(txtName);
                int i;
                int j = 0;
                LAYOUT_DATA_MODE mode = LAYOUT_DATA_MODE.LAYOUT_MODE;
                String aline;
                char[] delimiters = new char[] { ',', ' ', '\t' };

                KeyMapping.Clear();
                while ((aline = sr.ReadLine()) != null)
                {
                    string[] tokens = aline.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length == 0)
                        continue;
                    else if (tokens[0].CompareTo(KEYMAP) == 0)
                    {
                        keymAddress = Convert.ToInt32(tokens[1], 16);
                    }
                    else if (tokens[0].CompareTo(LAYER) == 0)
                    {
                        gLayer = Convert.ToInt32(tokens[1]);
                    }
                    else if (tokens[0].CompareTo(ROW) == 0)
                    {
                        gRow = Convert.ToInt32(tokens[1]);
                        if (gRow > MAX_ROW)
                            gRow = MAX_ROW;
                    }
                    else if (tokens[0].CompareTo(COLUMN) == 0)
                    {
                        gColumn = Convert.ToInt32(tokens[1]);
                        if (gColumn > MAX_COLUMN)
                            gColumn = MAX_COLUMN;

                        KeyboardCount = gRow * gColumn;
                    }
                    else if (tokens[0].CompareTo(LAYOUT) == 0)
                    {
                        mode = LAYOUT_DATA_MODE.LAYOUT_MODE;
                        j = 0;
                    }
                    else if (tokens[0].CompareTo(MAPPING) == 0)
                    {
                        mode = LAYOUT_DATA_MODE.MAPPING_MODE;
                        j = 0;
                    }
                    else
                    {
                        if (mode == LAYOUT_DATA_MODE.LAYOUT_MODE)
                        {
                            if (tokens.Length > 0 && tokens.Length <= MAX_COLUMN)
                            {
                                for (i = 0; i < tokens.Length; i++)
                                {
                                    KeyboardLayout[j, i] = Convert.ToDouble(tokens[i]);
                                }
                                for (; i < MAX_COLUMN; i++)
                                    KeyboardLayout[j, i] = 0;

                                j++;
                            }
                            else
                            {
                                isError = true;
                                break;
                            }
                        }
                        else if (mode == LAYOUT_DATA_MODE.MAPPING_MODE)
                        {
                            for (i = 0; i < tokens.Length; i++)
                            {
                                try
                                {
                                    KeyMapping.Add(j, Convert.ToInt32(tokens[i]));
                                }
                                catch (System.ArgumentException)
                                {
                                    isError = true;
                                    break;
                                }
                            }
                            j++;
                        }
                    }
                }

                sr.Close();
            }
            catch (FileNotFoundException)
            {
                isError = true;
            }

            return (!isError);
        }

        private void LayoutLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.DefaultExt = ".txt"; // Default file extension
            sfd.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension 
            if (sfd.ShowDialog() == true)
            {
                if (LoadKeyboardLayout(sfd.FileName))
                {
                    makeKeyboardLayout();
                    makeRgbLayout();
                }
            }
        }

        private void Layer_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Tag != null)
            {
                mCurrentLayer = Convert.ToInt32(((RadioButton)sender).Tag);
                ChangeKeyboardLayer(); 
            }
        }

        // _lkh
        private void Color_Changed(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            ColorPicker cp = (ColorPicker)sender;
            if (cp.Tag != null)
            {
                byte r, g, b;

                int tag = Convert.ToInt32(cp.Tag);
                int column = tag;
                int row = tag / mRgbColumn;
                if(row != 0)
                {
                    column -= mRgbColumn;
                }
                
                gRgbLedArray[row, column] = cp.SelectedColor;
                r = cp.SelectedColor.R;
                g = cp.SelectedColor.G;
                b = cp.SelectedColor.B;

                if (row == 1)
                {
                    kbd_conf.rgb_preset[2 * mRgbColumn - column - 1].r = r;
                    kbd_conf.rgb_preset[2 * mRgbColumn - column - 1].g = g;
                    kbd_conf.rgb_preset[2 * mRgbColumn - column - 1].b = b;
                }
                else
                {
                    kbd_conf.rgb_preset[column].r = r;
                    kbd_conf.rgb_preset[column].g = g;
                    kbd_conf.rgb_preset[column].b = b;
                }
            }

            for(int i = 0; i < MAX_RGB_ROW; i++)
            {
                if (true == mSyncCheckBox[i].IsChecked)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Checked");
                }
            }
        }

      
        private void ButtonAssign_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;

            int keycode = Convert.ToInt32(clicked.Tag);

            assignKey(keycode);  
        }

        private void Button_KeyUp(object sender, KeyEventArgs e)
        {
            //KeyCode kcode;
            try
            {
                //kcode = KeyCodeMap[e.Key];
                //assignKey((int)kcode);
            }
            catch (KeyNotFoundException)
            {

            }
        }

        private void Menu_New_Click(object sender, RoutedEventArgs e)
        {
            setDefalutKeyData();
        }

        private void Menu_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save the Keymap data";
            sfd.DefaultExt = ".bin"; // Default file extension
            sfd.Filter = "Binary files (.bin)|*.bin"; // Filter files by extension 
            if (sfd.ShowDialog() == true)
            {
                string filename = sfd.FileName;
                //SaveKeyboardData(filename);
                SaveKeymapData(filename);
            }
        }

        private void Keyboard_Connect_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo startInfo2 = new ProcessStartInfo("l3cmd.exe");
            startInfo2.Arguments = "cmd matrix";
            startInfo2.CreateNoWindow = true;
            startInfo2.UseShellExecute = false;
            startInfo2.RedirectStandardOutput = false;

            Process UserProcess2 = new Process();
            UserProcess2.StartInfo = startInfo2;
            UserProcess2.Start();
            UserProcess2.WaitForExit(MAX_PROCESS_WAIT_TIME);

            if (UserProcess2.ExitCode == 0)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("l3cmd.exe");
                startInfo.Arguments = "readkey keymap_r.tmp";
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = false;

                Process UserProcess = new Process();
                UserProcess.StartInfo = startInfo;
                UserProcess.Start();
                UserProcess.WaitForExit(MAX_PROCESS_WAIT_TIME);
                UserProcess.Close();

                ProcessStartInfo startInfo1 = new ProcessStartInfo("l3cmd.exe");
                startInfo1.Arguments = "readcfg kbdconf_r.tmp";
                startInfo1.CreateNoWindow = true;
                startInfo1.UseShellExecute = false;
                startInfo1.RedirectStandardOutput = false;

                Process UserProcess1 = new Process();
                UserProcess1.StartInfo = startInfo1;
                UserProcess1.Start();
                UserProcess1.WaitForExit(MAX_PROCESS_WAIT_TIME);
                UserProcess1.Close();

                if (LoadKbdCfgData("kbdconf_r.tmp"))
                {
                    if (LoadKeymapData("keymap_r.tmp"))
                    {
                        makeKeyboardLayout();
                        makeRgbLayout();
                    }
                }

                Xceed.Wpf.Toolkit.MessageBox.Show("Connected");
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Connection fail");
            }

            UserProcess2.Close();
        }

        private void Keyboard_Disconnect_Click(object sender, RoutedEventArgs e)
        {
            Disconnect();
            Xceed.Wpf.Toolkit.MessageBox.Show("Disconnected");
        }

        private int UpdateKeymap()
        {
            int nExitCode;
            // Save current configuration to the file.
            SaveKeymapData(".\\keymap.bin");

            ProcessStartInfo startInfo = new ProcessStartInfo("l3cmd.exe");
            startInfo.Arguments = "writekey keymap.bin";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = false;

            Process UserProcess = new Process();
            UserProcess.StartInfo = startInfo;
            UserProcess.Start();
            UserProcess.WaitForExit(MAX_PROCESS_WAIT_TIME);
            nExitCode = UserProcess.ExitCode;
            UserProcess.Close();

            return nExitCode;
        }

        private int UpdateKbdcfg()
        {
            int nExitCode;
            // Save current configuration to the file.
            SaveKeyboardConfigData(".\\kbdcfg.cfg");
            ProcessStartInfo startInfo = new ProcessStartInfo("l3cmd.exe");
            startInfo.Arguments = "writecfg kbdcfg.cfg";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = false;

            Process UserProcess = new Process();
            UserProcess.StartInfo = startInfo;
            UserProcess.Start();
            UserProcess.WaitForExit(MAX_PROCESS_WAIT_TIME);
            nExitCode = UserProcess.ExitCode;
            UserProcess.Close();

            return nExitCode;
        }

        private void Keyboard_Update_All_Click(object sender, RoutedEventArgs e)
        {
            int nExitCode;

            nExitCode = UpdateKeymap();
            if(nExitCode == 0)
            {
                System.Threading.Thread.Sleep(1000);
                nExitCode = UpdateKbdcfg();
                if(nExitCode == 0)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Completed");
                }
                else
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Update fail");
                }
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Update fail");
            }
       }
        private void Keyboard_Update_Keymap_Click(object sender, RoutedEventArgs e)
        {
            int nExitCode;
            nExitCode = UpdateKeymap();

            if(nExitCode == 0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Completed");
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Update fail");
            }
        }

        private void Keyboard_Update_Kbdcfg_Click(object sender, RoutedEventArgs e)
        {
            int nExitCode;
            nExitCode = UpdateKbdcfg();

            if(nExitCode == 0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Completed");
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Update fail");
            }
        }

        private void Menu_Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.Title = "Load the Keymap Data";
            sfd.DefaultExt = ".bin"; // Default file extension
            sfd.Filter = "Binary files (.bin)|*.bin"; // Filter files by extension 
            if (sfd.ShowDialog() == true)
            {
                //if (LoadKeyboardData(sfd.FileName))
                if (LoadKeymapData(sfd.FileName))
                {
                    makeKeyboardLayout();
                }
            }
        }

        private bool addressExtended = false;

        private void buffer2Hex(StreamWriter fp, int address, int length, int layer)
        {
           byte checksum = 0;
           int j, index = 0;
           int mapIndex;
           byte cnt;   

           while (length > 0)
           {
              if (!addressExtended && (address >= 0x10000))
              {
                fp.Write(":020000021000EC\n");
                addressExtended = true;
              }
	          else if (addressExtended && (address < 0x10000))
              {
                  fp.Write(":020000020000FC\n");
                 addressExtended = false;
              }

              length = length - 0x10;

              cnt = (byte)(length >= 0 ? 0x10 : length + 0x10);

              fp.Write(String.Format(":{0:X2}", cnt));
              checksum += cnt;
              fp.Write(String.Format("{0:X4}", address));
              checksum += (byte)((address >> 8) & 0xFF);
              checksum += (byte)(address & 0xFF);
              fp.Write("00");
              checksum += 0x00;

              for(j = 0; j < cnt; j++)
              {
                  mapIndex = KeyMapping[index];
                  fp.Write(String.Format("{0:X2}", (byte)keyArray[layer, mapIndex]));
                  checksum += (byte)keyArray[layer, index];
                  index++;
              }
              checksum = (byte)(~(checksum & 0xFF) + 1);
              fp.Write(String.Format("{0:X2}\n", checksum & 0xFF));
              address += 0x10;
              checksum = 0;
           }
        }

        private void SaveKeyboardData(string filename)
        {

            StreamWriter outfile = new StreamWriter(filename);
            int address = keymAddress;

            for (int layer = 0; layer < gLayer; layer++)
            {
                buffer2Hex(outfile, address, KeyboardCount, layer);
                address += 0x100;
            }

            outfile.Close();
        }

        private bool LoadKeyboardData(string txtName)
        {
            bool bRet = false;
            try
            {
                StreamReader sr = new StreamReader(txtName);
                int i, mapIndex;
                int count, keyIndex = 0;
                int address; //, previousArress = 0;
                String aline;
                StringBuilder hexString = new StringBuilder();
                // _lkh
                //int layer = -1;
                int layer = 0;
                while ((aline = sr.ReadLine()) != null)
                {
                    if (aline[0] == ':')
                    {
                        count = Convert.ToInt32(aline.Substring(1, 2), 16);
                        address = Convert.ToInt32(aline.Substring(3, 4), 16);
                        // _lkh
                        /*
                        if ((address - previousArress) > 0x100)
                        {
                            previousArress = address; 
                            layer++;
                            keyIndex = 0;
                        }
                        */
                        if (keyIndex == KeyMapping.Count)
                        {
                            layer++;
                            keyIndex = 0;
                        }

                        if (layer >= gLayer)
                            break;

                        for (i = 9 ; i < (9+count*2) ; i += 2)
                        {
                            hexString.Clear();
                            hexString.Append(aline[i]);
                            hexString.Append(aline[i+1]);

                            mapIndex = KeyMapping[keyIndex];
                            keyArray[layer, mapIndex] = (KeyCode)Convert.ToByte(hexString.ToString(), 16);
                            keyIndex++;
                        }

                        bRet = true;
                    }
                }

                sr.Close();
            }
            catch (FileNotFoundException)
            {
            }
            return bRet;
        }

        // _lkh
        private void SaveKeymapData(string filename)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                for (int i = 0; i < MAX_LAYER; i++)
                {
                    for (int j = 0; j < MAX_ROW; j++)
                    {
                        for (int k = 0; k < MAX_COLUMN; k++)
                        {
                            keymapArray[i, j, k] = (byte)keyArray[i, j * MAX_COLUMN + k];
                            writer.Write(keymapArray[i, j, k]);
                        }
                    }
                }
                writer.Close();
            }
        }

        private void SaveKeyboardConfigData(string filename)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                byte[] kbdcfg = StructToByte(kbd_conf);
                writer.Write(kbdcfg);
                writer.Close();
            }
        }

        // _lkh
        private bool LoadKeymapData(string binName)
        {
            bool bRet = false;

            try
            {
                if (File.Exists(binName))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(binName, FileMode.Open)))
                    {
                        for (int i = 0; i < MAX_LAYER; i++)
                        {
                            for(int j = 0; j < MAX_ROW; j++)
                            {
                                for (int k = 0; k < MAX_COLUMN; k++)
                                {
                                    keymapArray[i, j, k] = reader.ReadByte();
                                    keyArray[i, j * MAX_COLUMN + k] = (KeyCode)keymapArray[i, j, k];
                                }
                            }
                        }
                        bRet = true;
                    }
                }
            }
            catch (IOException)
            {
            }

            return bRet;
        }

        private bool LoadKbdCfgData(string binName)
        {
            bool bRet = false;

            try
            {
                if (File.Exists(binName))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(binName, FileMode.Open)))
                    {
                        int length = (int)reader.BaseStream.Length;
                        byte[] kbdConfig = reader.ReadBytes(length);
                        reader.Close();

                        kbd_conf = ByteToStruct<kbd_configuration_t>(kbdConfig);

                        // Default Layer
                        //mLayerComboBox.SelectedIndex = kbd_conf.keymapLayerIndex;

                        // RGB Effect
                        //mEffectComboBox.SelectedIndex = kbd_conf.rgb_effect_index;

                        // LED preset index
                        //mLedPresetComboBox.SelectedIndex = kbd_conf.led_preset_index;

                        //Number of RGB Column
                        mRgbColumn = kbd_conf.rgb_chain / MAX_RGB_ROW;
                        //mRgbComboBox.SelectedIndex = mRgbColumn / MAX_RGB_COLUMN;

                        for (int i = 0; i < mRgbColumn; i++)
                        {
                            gRgbLedArray[0, i] = Color.FromRgb(kbd_conf.rgb_preset[0 * mRgbColumn + i].r, kbd_conf.rgb_preset[0 * mRgbColumn + i].g, kbd_conf.rgb_preset[0 * mRgbColumn + i].b);
                            gRgbLedArray[1, i] = Color.FromRgb(kbd_conf.rgb_preset[2 * mRgbColumn - i - 1].r, kbd_conf.rgb_preset[2 * mRgbColumn - i - 1].g, kbd_conf.rgb_preset[2 * mRgbColumn - i - 1].b);
                        }

                        bRet = true;
                    }
                }
            }
            catch(IOException)
            {

            }

            return bRet;
        }

        private void Disconnect()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("l3cmd.exe");
            startInfo.Arguments = "cmd keycode";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = false;

            Process UserProcess = new Process();
            UserProcess.StartInfo = startInfo;
            UserProcess.Start();
            UserProcess.WaitForExit(MAX_PROCESS_WAIT_TIME);
            UserProcess.Close();
        }

        private void Menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void Window_Closing(object sender, CancelEventArgs e)
        {
            Disconnect();
        }

        // _lkh
        private void UsbInitialize()
        {
            UsbKeyboardDevice usbKeyboardDevice = UsbKeyboardDevice.FindLeeKuKeyboard();	// look for the device on the USB bus
            if (usbKeyboardDevice != null)	// did we find it?
            {
				// Yes! So wire into its events and update the UI
				//m_oBuzzDevice.OnDeviceRemoved += new EventHandler(BuzzDevice_OnDeviceRemoved);
				//m_oBuzzDevice.OnButtonChanged += new BuzzButtonChangedEventHandler(BuzzDevice_OnButtonChanged);
				//UpdateHandsetEnabled();
				//RefreshLights();
			}
;
        }

        // _lkh
        private void InitKeyboardConfig()
        {
            int i;

            kbd_conf.ps2usb_mode = 1; // USB
            kbd_conf.keymapLayerIndex = 2;
            kbd_conf.swapCtrlCaps = 128;
            kbd_conf.swapAltGui = 0;
            kbd_conf.led_preset_index = 1;

            kbd_conf.led_preset = new byte[15] { 0x08, 0x08, 0x08, 0x00, 0x01, 0x08, 0x08, 0x08, 0x02, 0x03, 0x08, 0x08, 0x08, 0x04, 0x06 };

            kbd_conf.rgb_effect_index = 3;
            kbd_conf.rgb_chain = 20;

            kbd_conf.rgb_preset = new rgb_type[20];
            for (i = 0; i < 10; i++)
            {
                kbd_conf.rgb_preset[i].r = gRgbLedArray[0, i].R;
                kbd_conf.rgb_preset[i].g = gRgbLedArray[0, i].G;
                kbd_conf.rgb_preset[i].b = gRgbLedArray[0, i].B;

                kbd_conf.rgb_preset[20 - i - 1].r = gRgbLedArray[1, i].R;
                kbd_conf.rgb_preset[20 - i - 1].g = gRgbLedArray[1, i].G;
                kbd_conf.rgb_preset[20 - i - 1].b = gRgbLedArray[1, i].B;
            }

            kbd_conf.rgb_effect_param = new rgb_effect_param_type[6];
            kbd_conf.rgb_effect_param[0].index = 0;
            kbd_conf.rgb_effect_param[0].high_hold = 0;
            kbd_conf.rgb_effect_param[0].low_hold = 0;
            kbd_conf.rgb_effect_param[0].accel_mode = 0;

            kbd_conf.rgb_effect_param[1].index = 1;
            kbd_conf.rgb_effect_param[1].high_hold = 5;
            kbd_conf.rgb_effect_param[1].low_hold = 0;
            kbd_conf.rgb_effect_param[1].accel_mode = 0;

            kbd_conf.rgb_effect_param[2].index = 2;
            kbd_conf.rgb_effect_param[2].high_hold = 5;
            kbd_conf.rgb_effect_param[2].low_hold = 0;
            kbd_conf.rgb_effect_param[2].accel_mode = 0;

            kbd_conf.rgb_effect_param[3].index = 3;
            kbd_conf.rgb_effect_param[3].high_hold = 1;
            kbd_conf.rgb_effect_param[3].low_hold = 5;
            kbd_conf.rgb_effect_param[3].accel_mode = 1;

            kbd_conf.rgb_effect_param[4].index = 4;
            kbd_conf.rgb_effect_param[4].high_hold = 1;
            kbd_conf.rgb_effect_param[4].low_hold = 5;
            kbd_conf.rgb_effect_param[4].accel_mode = 1;

            kbd_conf.rgb_effect_param[5].index = 5;
            kbd_conf.rgb_effect_param[5].high_hold = 1;
            kbd_conf.rgb_effect_param[5].low_hold = 5;
            kbd_conf.rgb_effect_param[5].accel_mode = 1;

            kbd_conf.rgb_limit = 300;
            kbd_conf.rgb_speed = 500;
            kbd_conf.matrix_debounce = 4;
            kbd_conf.sleep_timer = 30;

            kbd_conf.pad = new byte[16] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        }

        // _lkh
        // Byte Array를 Structure 로 변환하는 함수
        public static T ByteToStruct<T>(byte[] buffer) where T : struct
        {
            int nSize = Marshal.SizeOf(typeof(T));

            if (nSize > buffer.Length)
            {
                throw new Exception();
            }

            IntPtr ptr = Marshal.AllocHGlobal(nSize);
            Marshal.Copy(buffer, 0, ptr, nSize);
            T obj = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);

            return obj;
        }

        // Structure 정보를 Byte Array로 변환하는 함수
        public static byte[] StructToByte(object obj)
        {
            int nSize = Marshal.SizeOf(obj);
            byte[] arr = new byte[nSize];
            IntPtr ptr = Marshal.AllocHGlobal(nSize);

            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, nSize);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        // Structure 일부 정보를 Offset 부터 버퍼에 저장하는 함수
        public static void StructToByte(ref byte[] buf, object obj, int nOffset)
        {
            int nSize = Marshal.SizeOf(obj);
            IntPtr ptr = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, buf, nOffset, nSize);
            Marshal.FreeHGlobal(ptr);
        }

        // Byte의 Offset 부터 Structure로 변환하는 함수
        public static T ByteToStruct<T>(byte[] buffer, int nOffset) where T : struct
        {
            int nSize = Marshal.SizeOf(typeof(T));
            if (nSize > buffer.Length - nOffset)
            {
                throw new Exception();
            }

            IntPtr ptr = Marshal.AllocHGlobal(nSize);
            Marshal.Copy(buffer, nOffset, ptr, nSize);
            T obj = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);
            return obj;
        }

        private void RgbComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> data = new List<string>();
            data.Add("14 RGB");
            data.Add("20 RGB");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        private void RgbComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            int selected = comboBox.SelectedIndex;

            int prevRgbColumn = mRgbColumn;
            if (selected == 0)
            {
                mRgbColumn = 7;
            }
            else
            {
                mRgbColumn = 10;
            }

            if (prevRgbColumn != mRgbColumn)
            {
                kbd_conf.rgb_chain = (byte)(mRgbColumn * 2);
                makeRgbLayout();
            }
        }

        private void LedEffectComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> data = new List<string>();
            data.Add("LED Effect 1");
            data.Add("LED Effect 2");
            data.Add("LED Effect 3");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 2;
        }

        private void RgbEffectComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> data = new List<string>();
            data.Add("RGB Effect 1");
            data.Add("RGB Effect 2");
            data.Add("RGB Effect 3");
            data.Add("RGB Effect 4");
            data.Add("RGB Effect 5");
            data.Add("RGB Effect 6");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 2;
        }

        private void EffectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            int selected = comboBox.SelectedIndex;

            kbd_conf.rgb_effect_index = (byte)selected;
        }

        private void LimitComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            int selected = comboBox.SelectedIndex;

            kbd_conf.rgb_limit = GetRgbLimitValueFromIndex(selected);
        }

        private void LedPresetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            int selected = comboBox.SelectedIndex;

            kbd_conf.led_preset_index = (byte)selected;
        }

        private void LedEffectKeyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            int selected = comboBox.SelectedIndex;
            int index = (int)comboBox.Tag;

            kbd_conf.led_preset[index] = (byte)selected;
        }

        private void LedEffectArrowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            int selected = comboBox.SelectedIndex;
            int index = (int)comboBox.Tag;

            kbd_conf.led_preset[index] = (byte)selected;
        }

        private void LayerComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> data = new List<string>();
            data.Add("Layer 0");
            data.Add("Layer 1");
            data.Add("Layer 2");
            //data.Add("Layer FN");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        private void LayerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            int selected = comboBox.SelectedIndex;

            kbd_conf.keymapLayerIndex = (byte)selected;
        }

        private void SleepComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            int selected = comboBox.SelectedIndex;

            kbd_conf.sleep_timer = GetSleepTimerValueFromIndex(selected);
        }

        private void OnButtonKeyUp(object sender, KeyEventArgs e)
        {
            MakeKeyLayoutPress(e);
        }

        private void MakeKeyLayoutPress(KeyEventArgs e)
        {
            //this.Title = "You pressed " + e.Key.ToString();

            switch(mKeyPressState)
            {
                case KeyPressState.KEY_PRS_ST_START:
                    if (e.Key == Key.OemOpenBrackets)
                    {
                        mKeyPressState = KeyPressState.KEY_PRS_ST_ROW;
                    }
                    break;
                case KeyPressState.KEY_PRS_ST_DELIMITER:
                    if (e.Key == Key.OemMinus)
                    {
                        mKeyPressState = KeyPressState.KEY_PRS_ST_COLUMN;
                    }
                    else
                    {
                        mKeyPressState = KeyPressState.KEY_PRS_ST_START;
                    }
                    break;
                case KeyPressState.KEY_PRS_ST_ROW:
                    if(e.Key >= Key.A && e.Key <= Key.Z)
                    {
                        mKeyPressed[0] = e.Key;
                        mKeyPressState = KeyPressState.KEY_PRS_ST_DELIMITER;
                    }
                    break;
                case KeyPressState.KEY_PRS_ST_COLUMN:
                    if (e.Key >= Key.A && e.Key <= Key.Z)
                    {
                        mKeyPressed[1] = e.Key;
                        mKeyPressState = KeyPressState.KEY_PRS_ST_TERMINATION;
                    }
                    else
                    {
                        mKeyPressState = KeyPressState.KEY_PRS_ST_START;
                    }
                    break;
                case KeyPressState.KEY_PRS_ST_TERMINATION:
                    if (e.Key == Key.Oem6)
                    {
                        this.Title = "JigOn  Selected: Row " + mKeyPressed[0].ToString() + " Column " + mKeyPressed[1].ToString();

                        int layer = 0, row = 0, col = 0, i;

                        layer = mCurrentLayer;
                        row = (mKeyPressed[0] - Key.A);
                        col = (mKeyPressed[1] - Key.A);

                        if((row >=0 && row < MAX_ROW) && (col >= 0 && col < MAX_COLUMN))
                        {
                            int num = keyboardGrid.Children.Count;
                            KeyCode sel = (KeyCode)keymapArray[layer, row, col];
                            int index = row * MAX_COLUMN + col;
                            for (i = 0; i < num; i++)
                            {
                                Button btn = (Button)keyboardGrid.Children[i + mKeyLayerChildOffset];
                                if ((int)btn.Tag == index)
                                {
                                    btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, btn));
                                    break;
                                }
                            }
                        }
                    }
                    mKeyPressState = KeyPressState.KEY_PRS_ST_START;
                    break;
                default:
                    mKeyPressState = KeyPressState.KEY_PRS_ST_START;
                    break;
            }

        }
    }
}
