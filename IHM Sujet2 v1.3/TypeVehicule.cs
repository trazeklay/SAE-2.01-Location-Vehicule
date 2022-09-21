using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;

namespace IHM_Sujet2_v1
{
    public class TypeVehicule : Crud<TypeVehicule>
    {
        private int idCategorie;
        private string libelleCategorie;
        private bool isChecked;

        public TypeVehicule() { }
        public TypeVehicule(string libelleCategorie)
        {
            this.LibelleCategorie = libelleCategorie;
        }

        public int IdCategorie
        {
            get
            {
                return this.idCategorie;
            }

            set
            {
                this.idCategorie = value;
            }
        }

        public string LibelleCategorie
        {
            get
            {
                return this.libelleCategorie;
            }

            set
            {
                this.libelleCategorie = value;
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
                    if(access.setData("INSERT INTO dbo.TypeVehicule VALUES ('"+ this.LibelleCategorie + "');")){ }
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
                    if(access.setData("DELETE FROM dbo.TypeVehicule WHERE = '" + this.IdCategorie + "';")) { }
                    access.closeConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message");
            }
        }

        public ObservableCollection<TypeVehicule> FindAll()
        {
            ObservableCollection<TypeVehicule> listTypeVehicule= new ObservableCollection<TypeVehicule>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.openConnection())
                {
                    reader = access.getData("select * from dbo.TypeVehicule;");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TypeVehicule unTypeVehicule = new TypeVehicule();
                            unTypeVehicule.IdCategorie = reader.GetInt32(0);
                            unTypeVehicule.LibelleCategorie= reader.GetString(1);
                            listTypeVehicule.Add(unTypeVehicule);
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
            return listTypeVehicule;
        }

        public ObservableCollection<TypeVehicule> FindBySelection(string criteres)
        {
            ObservableCollection<TypeVehicule> listTypeVehicule = new ObservableCollection<TypeVehicule>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.openConnection())
                {
                    reader = access.getData("select * from dbo.TypeVehicule WHERE idCategorie = '" + criteres + "';");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TypeVehicule unTypeVehicule = new TypeVehicule();
                            unTypeVehicule.IdCategorie = reader.GetInt32(0);
                            unTypeVehicule.LibelleCategorie = reader.GetString(1);
                            listTypeVehicule.Add(unTypeVehicule);
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
            return listTypeVehicule;
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
                    if(access.setData("Update TypeVehicule set libelleCategorie ='" + this.LibelleCategorie+ "' where idCategorie ='" + this.IdCategorie + "';")) { }
                    access.closeConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message");
            }
        }
    }
}
