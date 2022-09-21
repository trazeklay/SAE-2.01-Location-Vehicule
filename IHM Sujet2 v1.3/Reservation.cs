using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using static IHM_Sujet2_v1.MainWindow;

namespace IHM_Sujet2_v1
{
    public class Resa : Crud<Resa>
    {
        private int idResa;
        private DateTime dateResa;
        private Employe employe;
        private Vehicule vehicule;
        private string mission;
        private bool isChecked;

        public Resa()
        {
            this.Employe = new Employe();
            this.Vehicule = new Vehicule();
        }

        public Resa(DateTime dateDeResa, Employe employe, Vehicule vehicule, string mission)
        {
            this.DateResa = dateDeResa;
            this.Mission = mission;
            this.Employe = employe;
            this.Vehicule = vehicule;
        }

        public DateTime DateResa
        {
            get
            {
                return this.dateResa;
            }

            set
            {
                this.dateResa = value;
            }
        }

        public string Mission
        {
            get
            {
                return this.mission;
            }

            set
            {
                this.mission = value;
            }
        }

        public Employe Employe
        {
            get
            {
                return this.employe;
            }

            set
            {
                this.employe = value;
            }
        }

        public Vehicule Vehicule
        {
            get
            {
                return this.vehicule;
            }

            set
            {
                this.vehicule = value;
            }
        }

        public int IdResa
        {
            get
            {
                return this.idResa;
            }

            set
            {
                this.idResa = value;
            }
        }

        public bool IsChecked
        {
            get
            {
                return this.isChecked;
            }

            set
            {
                this.isChecked = value;
            }
        }

        public void Create()
        {
            DataAccess access = new DataAccess();
            try
            {
                if (access.openConnection())
                {
                    if (access.setData("INSERT INTO dbo.Resa VALUES ('" + Employe.IdEmploye + "," + Vehicule.IdVehicule + "," + this.DateResa + "," + this.Mission + "');")) { }
                    access.closeConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message");
            }
        }

        public void Delete()
        {
            DataAccess access = new DataAccess();
            try
            {
                if (access.openConnection())
                {
                    if (access.setData("DELETE FROM dbo.Resa WHERE = '" + this.IdResa + "';")) { }
                    access.closeConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message");
            }
        }

        public ObservableCollection<Resa> FindAll()
        {
            ObservableCollection<Resa> listResa = new ObservableCollection<Resa>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.openConnection())
                {
                    reader = access.getData("select * from dbo.Resa r " +
                        "join dbo.Employe e on r.idEmploye = e.idEmploye " +
                        "join VEHICULE v on v.IDVEHICULE = r.IDVEHICULE " +
                        "join dbo.TypeVehicule tp on v.idCategorie = tp.idCategorie;");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            MainWindow app = (MainWindow)(Application.Current.Windows[0]);
                            Resa uneResa = new Resa();
                            uneResa.IdResa = reader.GetInt32(0);
                            uneResa.DateResa = reader.GetDateTime(3);
                            uneResa.Mission = reader.GetString(4);

                            Employe unEmploye = new Employe(reader.GetInt32(2), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                            uneResa.Employe = unEmploye;
                            Vehicule unVehicule = new Vehicule(reader.GetString(11), reader.GetString(13), new TypeVehicule(reader.GetString(15)));
                            uneResa.Vehicule = unVehicule;

                            listResa.Add(uneResa);
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("No rows found.", "Important Message");
                    }
                    reader.Close();
                    access.closeConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message");
            }
            return listResa;
        }

        public ObservableCollection<Resa> FindBySelection(string criteres)
        {
            ObservableCollection<Resa> listResa = new ObservableCollection<Resa>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.openConnection())
                {
                    reader = access.getData("select * from dbo.Resa r " +
                        "join dbo.Employe e on r.idEmploye = e.idEmploye " +
                        "join VEHICULE v on v.IDVEHICULE = r.IDVEHICULE " +
                        "join dbo.TypeVehicule tp on v.idCategorie = tp.idCategorie WHERE "+ criteres + " ;");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            MainWindow app = (MainWindow)(Application.Current.Windows[0]);
                            Resa uneResa = new Resa();
                            uneResa.IdResa = reader.GetInt32(0);
                            uneResa.DateResa = reader.GetDateTime(3);
                            uneResa.Mission = reader.GetString(4);

                            Employe unEmploye = new Employe(reader.GetInt32(2), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                            uneResa.Employe = unEmploye;
                            Vehicule unVehicule = new Vehicule(reader.GetString(11), reader.GetString(13), new TypeVehicule(reader.GetString(15)));
                            uneResa.Vehicule = unVehicule;

                            listResa.Add(uneResa);
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("No rows found.", "Important Message");
                    }
                    reader.Close();
                    access.closeConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message");
            }
            return listResa;
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            DataAccess access = new DataAccess();
            try
            {
                if (access.openConnection())
                {
                    if (access.setData("Update dbo.Resa set dataResa ='" + this.DateResa + "', mission='" + this.Mission + "', idEmploye='" + this.Employe.IdEmploye + "', idVehicule='" + this.Vehicule.IdVehicule + "'where idResa ='" + this.IdResa + "';")) { }
                }
                access.closeConnection();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message");
            }
        }

    }
}


