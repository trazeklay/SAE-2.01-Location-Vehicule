using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IHM_Sujet2_v1
{
    public interface Crud<T>
    {
        void Create();
        void Read();
        void Update();
        void Delete(); 
        ObservableCollection<T> FindAll();
        ObservableCollection<T> FindBySelection(string criteres);
    }
}
