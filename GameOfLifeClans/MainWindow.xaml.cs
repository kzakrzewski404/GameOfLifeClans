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
using System.Windows.Threading;

using GameOfLifeClans.Render;
using GameOfLifeClans.Simulation;


namespace GameOfLifeClans
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private SimulationHandler _simulation = new SimulationHandler();
        private BitmapRenderer _renderer = new BitmapRenderer();
        private DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            _timer.Tick += On_SimulationTimerTick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 16); // 0.016s
            _renderer.SetRenderOutput(imgRenderOutput);
        }

        private void RunSimulation_Click(object sender, RoutedEventArgs e)
        {
            if (_simulation.IsSimulationRunning)
            {
                if (_timer.IsEnabled)
                {
                    _timer.Stop();
                    btnRunSimulation.Content = "Run Simulation";
                    btnGenerateMap.IsEnabled = true;
                }
                else
                {
                    _timer.Start();
                    btnRunSimulation.Content = "Pause";
                    btnGenerateMap.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Map not generated!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void On_SimulationTimerTick(object sender, EventArgs e)
        {
            if (_simulation.IsSimulationRunning)
            {
                _simulation.CalculateStep(1);
                _renderer.Render();
            }
        }

        private void GenerateMap_Click(object sender, RoutedEventArgs e)
        {
            if (!_simulation.IsSimulationRunning || !_timer.IsEnabled)
            {
                _simulation.GenerateMap(100, 100);
                _renderer.LinkMapContainer(_simulation.Map);
                _renderer.Render();
            }
        }
    }
}
