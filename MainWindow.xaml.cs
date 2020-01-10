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

using GameOfLifeClans.Render;


namespace GameOfLifeClans
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RunSimulation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GenerateMap_Click(object sender, RoutedEventArgs e)
        {
            BitmapRenderer Renderer = new BitmapRenderer();
            Renderer.SetRenderOutput(renderOutput);
            Renderer.TestCreateCanvas();
            Renderer.Render();

        }
    }
}
