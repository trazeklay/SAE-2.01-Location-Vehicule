using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IHM_Sujet2_v1
{
    public class ApplicationData
    {
        #region Créer les listes
        public static ObservableCollection<Employe> LesEmployes
        {
            get;
            set;
        }
        public static ObservableCollection<Vehicule> LesVehicules
        {
            get;
            set;
        }
        public static ObservableCollection<Resa> LesResas
        {
            get;
            set;
        }
        public static ObservableCollection<TypeVehicule> LesTypes
        {
            get;
            set;
        }
        #endregion

        public static void loadApplicationData()
        {
            //chargement des tables
            Employe unEmploye = new Employe();
            Vehicule unVehicule = new Vehicule();
            Resa uneReservation = new Resa();
            TypeVehicule unTypeVehicule = new TypeVehicule();

            LesEmployes = unEmploye.FindAll();
            LesTypes = unTypeVehicule.FindAll();
            LesVehicules = unVehicule.FindAll();
            LesResas = uneReservation.FindAll();
            
        }
    }
}
