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
using GameOfLifeClans.Simulation;


namespace GameOfLifeClans
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SimulationHandler _simulation = new SimulationHandler();
        private BitmapRenderer _renderer = new BitmapRenderer();


        public MainWindow()
        {
            InitializeComponent();

            _renderer.SetRenderOutput(imgRenderOutput);
        }

        private void RunSimulation_Click(object sender, RoutedEventArgs e)
        {
            if (_simulation.IsSimulationRunning)
            {
                _simulation.CalculateStep(20);
                _renderer.Render();
            }
        }

        private void GenerateMap_Click(object sender, RoutedEventArgs e)
        {
            _simulation.GenerateMap(100, 100);
            _renderer.LinkMapContainer(_simulation.Map);
        }
    }
}
