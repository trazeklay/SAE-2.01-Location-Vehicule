using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace IHM_Sujet2_v1
{
	public class Employe : Crud<Employe>
	{
		private int idEmploye;
		private string nom;
		private string prenom;
		private string telephone;
		private string email;
        private bool isChecked;

        public bool IsChecked
        {
            get { return this.isChecked; }
            set { this.isChecked = value; }
        }

        public int IdEmploye
        {
            get
            {
                return this.idEmploye;
            }

            set
            {
                this.idEmploye = value;
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

        public string Prenom
        {
            get
            {
                return this.prenom;
            }

            set
            {
                this.prenom = value;
            }
        }

        public string Telephone
        {
            get
            {
                return this.telephone;
            }

            set
            {
                this.telephone = value;
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                this.email = value;
            }
        }

        public Employe() {}

        public Employe(int idEmploye, string nom, string prenom, string telephone, string email)
		{
            this.IdEmploye = idEmploye;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Telephone = telephone;
            this.Email = email;
        }

        public void Create()
        {
            DataAccess access = new DataAccess();
            try
            {
                if (access.openConnection())
                {
                    if (access.setData("INSERT INTO dbo.Employe VALUES ('" + this.Nom + "', '" + this.Prenom + "', '" + this.Telephone + "', '" + this.Email + "');")) { }
                }
                access.closeConnection();
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
                    if(access.setData("Update Employe set emailEmploye ='" + this.Email + "' where idEmploye ='" + this.IdEmploye + "';")) { }
                }
                access.closeConnection();
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
                    if (access.setData("DELETE FROM dbo.Employe WHERE emailEmploye = '" + this.Email + "';")) { }
                    access.closeConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message");
            }
        }

        public ObservableCollection<Employe> FindAll()
        {
            ObservableCollection<Employe> listEmploye = new ObservableCollection<Employe>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.openConnection())
                {
                    reader = access.getData("select * from dbo.Employe;");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Employe unEmploye = new Employe();
                            unEmploye.IdEmploye = reader.GetInt32(0);
                            unEmploye.Nom = reader.GetString(1);
                            unEmploye.Prenom = reader.GetString(2);
                            unEmploye.Telephone = reader.GetString(3);
                            unEmploye.Email = reader.GetString(4);
                            listEmploye.Add(unEmploye);
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
            return listEmploye;
        }

        public ObservableCollection<Employe> FindBySelection(string criteres)
        {
            ObservableCollection<Employe> listEmploye = new ObservableCollection<Employe>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.openConnection())
                {
                    reader = access.getData("select * from dbo.Employe WHERE idEmploye = '" + criteres + "';");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Employe unEmploye = new Employe();
                            unEmploye.IdEmploye = reader.GetInt32(0);
                            unEmploye.Nom = reader.GetString(1);
                            unEmploye.Prenom = reader.GetString(2);
                            unEmploye.Telephone = reader.GetString(3);
                            unEmploye.Email = reader.GetString(4);
                            listEmploye.Add(unEmploye);
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
            return listEmploye;
        }
    }
}
