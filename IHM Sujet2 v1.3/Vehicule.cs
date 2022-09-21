using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace IHM_Sujet2_v1
{
	public class Vehicule : Crud<Vehicule>
	{
        private int idVehicule;
		private string immatriculation;
		private string nom;
		private TypeVehicule type;
        private bool isChecked;

        public Vehicule()
        {
            this.Type = new TypeVehicule();
        }

        public Vehicule(string immatriculation, string nom, TypeVehicule type)
        {
            this.Immatriculation = immatriculation;
            this.Nom = nom;
            this.Type = type;
        }

        public string Immatriculation
        {
            get
            {
                return this.immatriculation;
            }

            set
            {
                this.immatriculation = value;
            }
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public TypeVehicule Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
            }
        }

        public int IdVehicule
        {
            get
            {
                return this.idVehicule;
            }

            set
            {
                this.idVehicule = value;
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
                    if(access.setData("INSERT INTO dbo.Vehicule VALUES ('" + this.Immatriculation + "', '" + this.Type.IdCategorie + "', '" + this.Nom + "');")) { }
                    access.closeConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message");
            }
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
                    if(access.setData("Update Vehicule set immutriculation ='" + this.Immatriculation + "' where idVehicule='" + this.IdVehicule + "';")) { }
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
            SqlDataReader reader;
            try
            {
                if (access.openConnection())
                {
                    reader = access.getData("DELETE FROM dbo.Vehicule WHERE = '" + this.IdVehicule + "';");
                    reader.Read();
                    reader.Close();
                    access.closeConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message");
            }
        }

        public ObservableCollection<Vehicule> FindAll()
        {
            ObservableCollection<Vehicule> listVehicule = new ObservableCollection<Vehicule>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.openConnection())
                {
                    reader = access.getData("select * from dbo.Vehicule v join dbo.TypeVehicule tp on v.idCategorie=tp.idCategorie;");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Vehicule unVehicule = new Vehicule();
                            unVehicule.IdVehicule = reader.GetInt32(0);
                            unVehicule.Immatriculation = reader.GetString(1);
                            unVehicule.Nom = reader.GetString(3);
                            unVehicule.Type = new TypeVehicule(reader.GetString(5));
                            listVehicule.Add(unVehicule);
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
            return listVehicule;
        }

        public ObservableCollection<Vehicule> FindBySelection(string criteres)
        {
            ObservableCollection<Vehicule> listVehciule = new ObservableCollection<Vehicule>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.openConnection())
                {
                    reader = access.getData("select * from dbo.Employe WHERE idVehicule = '" + criteres + "';");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Vehicule unVehicule = new Vehicule();
                            unVehicule.idVehicule = reader.GetInt32(0);
                            unVehicule.Immatriculation = reader.GetString(1);
                            unVehicule.Nom = reader.GetString(2);
                            unVehicule.Type.LibelleCategorie = reader.GetString(5);
                            listVehciule.Add(unVehicule);
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
            return listVehciule;
        }
    }
}
