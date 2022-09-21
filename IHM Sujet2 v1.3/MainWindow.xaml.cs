using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections;
using static IHM_Sujet2_v1.ApplicationData;

namespace IHM_Sujet2_v1
{
    public partial class MainWindow : Window
    {
        private bool IsMaximize = false;
        public double rowH = 33.29;

        public MainWindow()
        {
            #region Load data
            loadApplicationData();
            #endregion
            #region Initialiser les composants
            InitializeComponent();
            this.DataContext = this;

            dgdResas.ItemsSource = LesResas;
            dgdEmps.ItemsSource = LesEmployes;
            dgdVehi.ItemsSource = LesVehicules;
            dgdTypeVehi.ItemsSource = LesTypes;

            dgdAddEmps.ItemsSource = LesEmployes;
            dgdAddVehi.ItemsSource = LesVehicules;
            dgdModifyEmps.ItemsSource = LesEmployes;
            dgdModifyVehi.ItemsSource = LesVehicules;
            this.rbnResa.IsChecked = true; //Définir le menu de base sur les résas

            DisplayCounts();
            #endregion

        }

        public void DisplayCounts()
        {
            this.tbxNbResas.Text = $"{LesResas.Count} réservations";
            this.tbxNbEmps.Text = $"{LesEmployes.Count} employés";
            this.tbxNbVehi.Text = $"{LesVehicules.Count} véhicules";
            this.tbxNbTypeVehi.Text = $"{LesTypes.Count} types de véhicule";
        }

        //Choper le parent d'un composant ex => la grid ds laquelle est une textbox
        private TargetType GetParent<TargetType>(DependencyObject o) where TargetType : DependencyObject
        {
            if (o is null || o is TargetType)
                return (TargetType)o;
            return GetParent<TargetType>(VisualTreeHelper.GetParent(o));
        }

        //Récupérer une liste de lignes dans un datagrid
        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            IEnumerable itemsSource = grid.ItemsSource;
            if (itemsSource is null) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null) yield return row;
            }
        }

        /*Opérations sur la fenêtre*/
        #region déplacer et agrandir la fenêtre quand on clique dessus
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximize = true;
                }
            }
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        #endregion
        #region Fermer et Réduire les fenêtres
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion
        /**/


        private void btnAddResa_Click(object sender, RoutedEventArgs e)
        {
            this.rbnResa.IsChecked = false;
            this.grdAddResa.Visibility = Visibility.Visible;
        }
        private void Rbn_Checked(object sender, RoutedEventArgs e)
        {
            tbxSaisieDate.Text = "";
            tbxSaisieMotif.Text = "";
            LesEmployes.ToList().ForEach(x => x.IsChecked = false);
            LesVehicules.ToList().ForEach(x => x.IsChecked = false);
            dgdAddEmps.Items.Refresh();
            dgdAddVehi.Items.Refresh();
            grdAddResa.Visibility = Visibility.Hidden;
            grdModifyResa.Visibility = Visibility.Hidden;
        }

        public DateTime FromTbx(TextBox tbx)
        {
            string[] dateParsed = tbx.Text.Split("/");
            int
                d = Int32.Parse(dateParsed[0]),
                m = Int32.Parse(dateParsed[1]),
                y = Int32.Parse(dateParsed[2]);
            return new DateTime(y, m, d);
        }

        /*Ajouter une résa*/
        private void btnAddResaToData_Click(object sender, RoutedEventArgs e)
        {

            DateTime date = FromTbx(this.tbxSaisieDate);
            string motif = this.tbxSaisieMotif.Text;
            Employe employe = LesEmployes.First(emp => emp.IsChecked == true);
            Vehicule vehicule = LesVehicules.First(v => v.IsChecked == true);

            LesResas.Add(new Resa(date, employe, vehicule, motif));
            rbnResa.IsChecked = true;
        }

        //Voir si on peut pas compacter tout ça pcq le code est dégueu
        #region Sélectionner / Déselectionner toutes les CheckBox
        /*DataGrid Résas*/
        private void cbxSelectAllResas_Checked(object sender, RoutedEventArgs e)
        {
            LesResas.ToList().ForEach(x => x.IsChecked = true);
            dgdResas.Items.Refresh();
        }
        private void cbxSelectAllResas_Unchecked(object sender, RoutedEventArgs e)
        {
            LesResas.ToList().ForEach(x => x.IsChecked = false);
            dgdResas.Items.Refresh();
        }
        /**/

        /*DataGrid Employés*/
        private void cbxSelectAllEmployes_Checked(object sender, RoutedEventArgs e)
        {
            LesEmployes.ToList().ForEach(x => x.IsChecked = true);
            dgdEmps.Items.Refresh();
        }
        private void cbxSelectAllEmployes_Unchecked(object sender, RoutedEventArgs e)
        {
            LesEmployes.ToList().ForEach(x => x.IsChecked = false);
            dgdEmps.Items.Refresh();
        }
        /**/

        /*DataGrid Vehicule*/
        private void cbxSelectAllVehicules_Checked(object sender, RoutedEventArgs e)
        {
            LesVehicules.ToList().ForEach(x => x.IsChecked = true);
            this.dgdVehi.Items.Refresh();
        }
        private void cbxSelectAllVehicules_Unchecked(object sender, RoutedEventArgs e)
        {
            LesVehicules.ToList().ForEach(x => x.IsChecked = false);
            this.dgdVehi.Items.Refresh();
        }
        /**/

        /*DataGrid TypeVehicule*/
        private void cbxSelectAllTypes_Checked(object sender, RoutedEventArgs e)
        {
            LesTypes.ToList().ForEach(x => x.IsChecked = true);
            this.dgdTypeVehi.Items.Refresh();
        }
        private void cbxSelectAllTypes_Unchecked(object sender, RoutedEventArgs e)
        {
            LesTypes.ToList().ForEach(x => x.IsChecked = false);
            this.dgdTypeVehi.Items.Refresh();
        }
        /**/

        /*Datagrid ajouter des employés*/
        private void cbxSelectAllEmp_Checked(object sender, RoutedEventArgs e)
        {
            LesEmployes.ToList().ForEach(x => x.IsChecked = true);
            dgdAddEmps.Items.Refresh();
        }
        private void cbxSelectAllEmp_Unchecked(object sender, RoutedEventArgs e)
        {
            LesEmployes.ToList().ForEach(x => x.IsChecked = false);
            dgdAddEmps.Items.Refresh();
        }
        /**/

        /*DataGrid ajouter des véhicules*/
        private void cbxSelectAllVehi_Checked(object sender, RoutedEventArgs e)
        {
            LesVehicules.ToList().ForEach(x => x.IsChecked = true);
            this.dgdAddVehi.Items.Refresh();
        }
        private void cbxSelectAllVehi_Unchecked(object sender, RoutedEventArgs e)
        {
            LesVehicules.ToList().ForEach(x => x.IsChecked = false);
            this.dgdAddVehi.Items.Refresh();
        }
        /**/

        /*Datagrid modifier employés*/
        private void cbxSelectAllEmpMod_Checked(object sender, RoutedEventArgs e)
        {
            LesEmployes.ToList().ForEach(x => x.IsChecked = true);
            dgdModifyEmps.Items.Refresh();
        }
        private void cbxSelectAllEmpMod_Unchecked(object sender, RoutedEventArgs e)
        {
            LesEmployes.ToList().ForEach(x => x.IsChecked = false);
            dgdModifyEmps.Items.Refresh();
        }
        /**/

        /*Datagrid modifier véhicules.*/
        private void cbxSelectAllVehiMod_Checked(object sender, RoutedEventArgs e)
        {
            LesVehicules.ToList().ForEach(x => x.IsChecked = true);
            this.dgdModifyVehi.Items.Refresh();
        }
        private void cbxSelectAllVehiMod_Unchecked(object sender, RoutedEventArgs e)
        {
            LesVehicules.ToList().ForEach(x => x.IsChecked = false);
            this.dgdModifyVehi.Items.Refresh();
        }
        /**/
        #endregion

        /*Setup pour modifier une résa*/
        private void gridRemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Resa r in LesResas.ToList())
                if (r.IsChecked)
                    r.Delete();
        }

        //Quand on clique sur le bouton modifier
        private void btnGridEdit_Clicked(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DataGridRow row = GetParent<DataGridRow>(btn);
            int index = dgdResas.Items.IndexOf(row.Item);
            dgdResas.SelectedIndex = index;

            rbnResa.IsChecked = false;
            grdModifyResa.Visibility = Visibility.Visible;

            Resa r = new Resa();
            r.FindBySelection($"idResa = {index+2}");
            //Resa r = LesResas.First(x => x.IdResa == index + 1);

            tbxModifyDate.Text = $"{r.DateResa.Day}/{r.DateResa.Month}/{r.DateResa.Year}";
            tbxModifyMotif.Text = r.Mission;

            foreach (Employe emp in LesEmployes)
                if (emp.IdEmploye == r.Employe.IdEmploye)
                    emp.IsChecked = true;

            foreach (Vehicule v in LesVehicules)
                if (v.IdVehicule == r.Vehicule.IdVehicule)
                    v.IsChecked = true;

            tbxResaNum.Text = $"{r.IdResa}";

            dgdModifyEmps.Items.Refresh();
            dgdModifyVehi.Items.Refresh();
        }

        //Modifier la Réservation
        private void btnModifyResa_Click(object sender, RoutedEventArgs e)
        {
            bool test = Int32.TryParse(this.tbxResaNum.Text, out int index);


            rbnResa.IsChecked = true;
            tbxModifyDate.Text = "";
            tbxModifyMotif.Text = "";

            LesEmployes.ToList().ForEach(x => x.IsChecked = false);
            LesVehicules.ToList().ForEach(x => x.IsChecked = false);

            dgdModifyEmps.Items.Refresh();
            dgdModifyVehi.Items.Refresh();
            dgdResas.Items.Refresh();
        }
        /**/

        /*Moteurs de recherche*/
        private void tbxFilterEmp_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (DataGridRow row in GetDataGridRows(this.dgdEmps))
            {
                Employe emp = (Employe)row.Item;
                string nomPrenom = $"{emp.Nom} {emp.Prenom}";
                row.Height = nomPrenom.ToLower().Contains(this.tbxFilterEmp.Text.ToLower()) ? this.rowH : 0;
            }
        }
        private void tbxFilterResa_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (DataGridRow row in GetDataGridRows(this.dgdResas))
            {
                Resa r = (Resa)row.Item;
                row.Height = r.Mission.ToLower().Contains(this.tbxFilterResa.Text.ToLower()) ? rowH : 0;
            }
        }
        private void tbxFilterVehi_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (DataGridRow row in GetDataGridRows(this.dgdVehi))
            {
                Vehicule v = (Vehicule)row.Item;
                row.Height = v.Nom.ToLower().Contains(this.tbxFilterVehi.Text.ToLower()) ? rowH : 0;
            }
        }
        private void tbxFilterTypeVehi_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (DataGridRow row in GetDataGridRows(this.dgdTypeVehi))
            {
                TypeVehicule tv = (TypeVehicule)row.Item;
                row.Height = tv.LibelleCategorie.ToLower().Contains(this.tbxFilterTypeVehi.Text.ToLower()) ? rowH : 0;
            }
        }
        /**/
    }
}
