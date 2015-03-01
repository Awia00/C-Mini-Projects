using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Sudoko.Annotations;

namespace Sudoko.Viewmodels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The game array is created in the following form
        /// [0,0 0,1 0,2] [1,0 1,1 1,2] [2,0 2,1 2,2] 
        /// [0,3 0,4 0,5] [1,3 1,4 1,5] [2,3 2,4 2,5] 
        /// [0,6 0,7 0,8] [1,6 1,7 1,8] [2,6 2,7 2,8] 
        /// 
        /// [3,0 3,1 3,2] [4,0 4,1 4,2] [5,0 5,1 5,2] 
        /// [3,3 3,4 3,5] [4,3 4,4 4,5] [5,3 5,4 5,5] 
        /// [3,6 3,7 3,8] [4,6 4,7 4,8] [5,6 5,7 5,8] 
        /// 
        /// [6,0 6,1 6,2] [7,0 7,1 7,2] [8,0 8,1 8,2] 
        /// [6,3 6,4 6,5] [7,3 7,4 7,5] [8,3 8,4 8,5] 
        /// [6,6 6,7 6,8] [7,6 7,7 7,8] [8,6 8,7 8,8] 
        /// </summary>
        public ObservableCollection<ObservableCollection<int>> GameList { get; set; }
        public GameViewModel()
        {
            GameList = new ObservableCollection<ObservableCollection<int>>();
            for (int i = 0; i < 9; i++)
            {
                GameList.Add(new ObservableCollection<int>());
                for (int j = 0; j < 9; j++)
                {
                    GameList[i].Add(i);
                }
            }
        }
        #region Actions
        /// <summary>
        /// This method gets called by the solve button.
        /// </summary>
        public async void Solve()
        {
            // Remove this and put the solving algorithm in here.
            GameList.Clear();
            for (int i = 0; i < 9; i++)
            {
                GameList.Add(new ObservableCollection<int>());
                for (int j = 0; j < 9; j++)
                {
                    GameList[i].Add(j);
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// A method for notifying the view if any properties have been changed.
        /// <param name="info">The name of the property which has changed</param>
        /// </summary>
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}
