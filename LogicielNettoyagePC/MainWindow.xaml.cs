using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogicielNettoyagePC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DirectoryInfo winTemp;
        public DirectoryInfo appTemp;


        public MainWindow()
        {
            InitializeComponent();
            winTemp = new DirectoryInfo(@"C:\Windows\Temp");
            appTemp = new DirectoryInfo(System.IO.Path.GetTempPath());
        }

        // Calcul de la taille d'un dossier
        public long DirSize(DirectoryInfo dir)
        {
            return dir.GetFiles().Sum(fi => fi.Length) + dir.GetDirectories().Sum(di => DirSize(di));
        }

        // Vider un dossier
        public void ClearTempData(DirectoryInfo di)
        {
            foreach(FileInfo file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                    Console.WriteLine(file.FullName);
                    // totalRemovedFiles++;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                    Console.WriteLine(dir.FullName);
                    // totalRemovedFiles++;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private void Bouton_NETTOYER_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Bouton_HISTORIQUE_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TODO : Créer une page historique", "Historique", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Bouton_MAJ_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Votre logiciel est à jour!", "Mise à jour", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Bouton_WEB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("https://www.ccleaner.com/fr-fr") { UseShellExecute = true });
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Erreur :  " + ex.Message);
            }
        }

        private void LancerAnalyse_Click(object sender, RoutedEventArgs e)
        {
            AnalyseFolders();
        }

        public void AnalyseFolders()
        {
            Console.WriteLine("Début de l'analyse...");
            long totalSize = 0;

            try
            {
                totalSize += DirSize(winTemp) / 1000000;
                totalSize += DirSize(appTemp) / 1000000;
            } catch (Exception ex)
            {
                Console.WriteLine("Impossible d'analyser les dossiers : " + ex.Message);
            }

            Espace.Content = totalSize + " Mb";
            Date.Content = DateTime.Today;
        }
    }
}