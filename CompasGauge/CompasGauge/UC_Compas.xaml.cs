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

namespace CompasGauge
{
    /// <summary>
    /// Interaction logic for UC_Compas.xaml
    /// </summary>
    public partial class UC_Compas : UserControl
    {
        public UC_Compas()
        {
            InitializeComponent();

            BigDegreelLine();
            SmalDegreelLine();
        }
        int ZSizeOfLabel = 30;
        int ZNumberOfLargeDegreeLines = 8;
        int ZNumberOfSmallDegreeLinesh = 180;
        string[] Direction = new[] { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };
        #region ZBackgroundCompas
        public Color ZBackgroundCompas
        {
            get { return (Color)GetValue(ZBackgroundCompasProperty); }
            set
            {
                SetValue(ZBackgroundCompasProperty, value);
            }
        }
        public static readonly DependencyProperty ZBackgroundCompasProperty = DependencyProperty.Register("ZBackgroundCompas", typeof(Color), typeof(UC_Compas), new PropertyMetadata(Colors.Yellow, OnChangeBackgroundCompas));
        private static void OnChangeBackgroundCompas(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ct = d as UC_Compas;

            ct.UpdateBackgroundCompas((Color)e.NewValue);
        }
        public void UpdateBackgroundCompas(Color _ZBackgroundCompas)
        {
            StartMainPlateColor.Color = _ZBackgroundCompas;
            StopMainPlateColor.Color = _ZBackgroundCompas;

        }
        #endregion
        #region ZPin
        public int ZPin
        {
            get { return (int)GetValue(ZPinProperty); }
            set
            {
                SetValue(ZPinProperty, value);
            }
        }
        public static DependencyProperty ZPinProperty = DependencyProperty.Register("ZPin", typeof(int), typeof(UC_Compas), new PropertyMetadata(25, OnChangeZPin));
        private static void OnChangeZPin(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ct = d as UC_Compas;

            ct.UpdateZPin((int)e.NewValue);
        }
        public void UpdateZPin(int _ZPin)
        {
            Pin.Width = _ZPin;
            Pin.Height = _ZPin;
        }
        #endregion
        #region Zvalue
        public double Zvalue
        {
            get { return (int)GetValue(ZvalueProperty); }
            set
            {
                SetValue(ZvalueProperty, value);
            }
        }
        public static DependencyProperty ZvalueProperty = DependencyProperty.Register("Zvalue", typeof(double), typeof(UC_Compas), new PropertyMetadata(0d, OnChangeZvalue));
        private static void OnChangeZvalue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ct = d as UC_Compas;

            ct.UpdateZvalue((double)e.NewValue);
        }
        public void UpdateZvalue(double _Zvalue)
        {
            NeedleAngle.Angle = _Zvalue;
            PinAngle.Angle = _Zvalue;
        }
        #endregion
        #region ZSize
        public int ZSize
        {
            get { return (int)GetValue(widthProperty); }
            set
            {
                SetValue(widthProperty, value);
            }
        }
        public static DependencyProperty widthProperty = DependencyProperty.Register("ZSize", typeof(int), typeof(UC_Compas), new PropertyMetadata(230, OnChangeZwidth));
        private static void OnChangeZwidth(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ct = d as UC_Compas;

            ct.UpdateZSize((int)e.NewValue);
        }
        public void UpdateZSize(int _size)
        {
            BackPlate.Width = _size;
            MainPlate.Width = _size - ZBorder;
            MainCanvas.Width = _size - ZBorder;



            BackPlate.Height = _size;
            MainPlate.Height = _size - ZBorder;
            MainCanvas.Height = _size - ZBorder;



            MainCanvas.Children.Clear();
            BigDegreelLine();
            SmalDegreelLine();
        }
        #endregion Zwidth
        #region ZBorder
        public int ZBorder
        {
            get { return (int)GetValue(ZBorderProperty); }
            set
            {
                SetValue(ZBorderProperty, value);
            }
        }
        public static DependencyProperty ZBorderProperty = DependencyProperty.Register("ZBorder", typeof(int), typeof(UC_Compas), new PropertyMetadata(10, OnChangeZBorder));
        private static void OnChangeZBorder(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ct = d as UC_Compas;

            ct.UpdateZBorder((int)e.NewValue);
        }
        public void UpdateZBorder(int _zBorder)
        {
            ZBorder = _zBorder;
            UpdateZSize(ZSize);
        }
        #endregion
        #region ZNeedle
        public int ZNeedle
        {
            get { return (int)GetValue(ZNeedleProperty); }
            set
            {
                SetValue(ZNeedleProperty, value);
            }
        }
        public static DependencyProperty ZNeedleProperty = DependencyProperty.Register("ZNeedle", typeof(int), typeof(UC_Compas), new PropertyMetadata(15, OnChangeZNeedle));
        private static void OnChangeZNeedle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ct = d as UC_Compas;

            ct.UpdateZNeedle((int)e.NewValue);
        }
        public void UpdateZNeedle(int _ZNeedle)
        {
            Needle.Width = _ZNeedle;
        }
        #endregion
        #region BigDegreelLine
        private void BigDegreelLine()
        {
            double radius = MainPlate.Width / 2;

            double min = 0; double max = ZNumberOfLargeDegreeLines;
            double step = 360.0 / (max - min);


            for (int i = 0; i < max - min; i++)
            {
                radius = MainPlate.Width / 2;
                Line lineScale = new Line
                {
                    X1 = ((radius - 20) * Math.Cos(i * step * Math.PI / 180)) + radius,
                    Y1 = ((radius - 20) * Math.Sin(i * step * Math.PI / 180)) + radius,
                    X2 = (radius * Math.Cos(i * step * Math.PI / 180)) + radius,
                    Y2 = (radius * Math.Sin(i * step * Math.PI / 180)) + radius,
                    Stroke = Brushes.DarkGray,
                    StrokeThickness = 4
                };
                #region Draw Number
                radius = MainPlate.Width / 2;
                //radius -= 30;
                double X1 = ((radius - 20 - ZSizeOfLabel) * Math.Sin(i * step * Math.PI / 180)) + radius;
                double Y1 = ((radius - 20 - ZSizeOfLabel) * -Math.Cos(i * step * Math.PI / 180)) + radius;
                double X2 = ((radius - ZSizeOfLabel) * Math.Sin(i * step * Math.PI / 180)) + radius;
                double Y2 = ((radius - ZSizeOfLabel) * -Math.Cos(i * step * Math.PI / 180)) + radius;
                Brush TEMP;
                if (Direction[i] == "N")
                {
                    TEMP = Brushes.Red;
                }
                else
                {
                    TEMP = Brushes.Gray;

                }
                Label lbl = new Label
                {

                    Content = Direction[i].ToString(),
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = ZSizeOfLabel,
                    Height = ZSizeOfLabel,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(X2 - ZSizeOfLabel / 2, Y2 - ZSizeOfLabel / 2, 0, 0),
                    Foreground = TEMP
                };
                #endregion
                MainCanvas.Children.Add(lineScale);
                MainCanvas.Children.Add(lbl);

            }

        }
        #endregion BigDegreelLine
        #region SmalDegreelLine
        private void SmalDegreelLine()
        {
            double radius = MainPlate.Width / 2;

            double min = 0; double max = ZNumberOfSmallDegreeLinesh;
            double step = 360.0 / (max - min);

            for (int i = 0; i < max - min; i++)
            {
                Line lineScale = new Line
                {
                    X1 = ((radius - 10) * Math.Cos(i * step * Math.PI / 180)) + radius,
                    Y1 = ((radius - 10) * Math.Sin(i * step * Math.PI / 180)) + radius,
                    X2 = (radius * Math.Cos(i * step * Math.PI / 180)) + radius,
                    Y2 = (radius * Math.Sin(i * step * Math.PI / 180)) + radius,
                    Stroke = Brushes.DarkGray,
                    StrokeThickness = 2
                };

                MainCanvas.Children.Add(lineScale);
            }

        }
        #endregion
    }
}
