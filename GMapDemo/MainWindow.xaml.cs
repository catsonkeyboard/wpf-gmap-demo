using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace GMapDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Rect rc = SystemParameters.WorkArea; //获取工作区大小
            //this.Left = 0; //设置位置
            //this.Top = 0;
            //this.Width = rc.Width;
            //this.Height = rc.Height;
            this.Map_Loaded();//加载地图

            var reader = new StreamReader(File.OpenRead(@".\data.csv"));
            List<PointLatLng> points = new List<PointLatLng>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                if (int.Parse(values[9]) == 0)
                {
                    double lon = double.Parse(values[2]);
                    double lat = double.Parse(values[3]);
                    points.Add(new PointLatLng(lat,lon));
                }
            }
            GMapRoute gmRoute = new GMapRoute(points);
            MainMap.Markers.Add(gmRoute);

            //RoutingProvider routingProvider = MainMap.MapProvider as RoutingProvider ?? GMapProviders.OpenStreetMap;
            //MapRoute route = routingProvider.GetRoute(
            //    new PointLatLng(31.839526, 117.13528), //起始点
            //    new PointLatLng(31.838868, 117.134456), //结束点
            //    false, //是否高速
            //    false, //步行模式
            //    (int)MainMap.Zoom);



            //points.Add(new PointLatLng(31.839526, 117.13528));
            //points.Add(new PointLatLng(31.838868, 117.134456));
            //points.Add(new PointLatLng(31.839862, 117.134133));
            //points.Add(new PointLatLng(31.840937, 117.133963));
            //GMapRoute gmRoute = new GMapRoute(points);
            //MainMap.Markers.Add(gmRoute);
        }

        private void Map_Loaded()
        {
            try
            {
                System.Net.IPHostEntry e = System.Net.Dns.GetHostEntry("ditu.google.cn");
            }
            catch
            {
                MainMap.Manager.Mode = AccessMode.CacheOnly;
                System.Windows.MessageBox.Show("没有可用的internet连接，正在进入缓存模式!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            MainMap.CacheLocation = Environment.CurrentDirectory + "\\GMapCache\\"; //缓存位置
            MainMap.MapProvider = AMapProvider.Instance; //加载高德地图
            MainMap.MinZoom = 2;  //最小缩放
            MainMap.MaxZoom = 17; //最大缩放
            MainMap.Zoom = 12;     //当前缩放
            MainMap.ShowCenter = false; //不显示中心十字点
            MainMap.DragButton = MouseButton.Left; //右键拖拽地图
            MainMap.Position = new PointLatLng(31.837328, 117.141981); //地图中心位置：飞友科技地址
            //MainMap.MouseLeftButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown);
        }
    }
}
