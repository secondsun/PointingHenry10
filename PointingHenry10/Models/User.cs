using Template10.Mvvm;

namespace PointingHenry10.Models
{
    public class User : ViewModelBase
    {
        private string _vote;
        public string Name { get; set; }

        public string Vote
        {
            get { return _vote; }
            set { Set(ref _vote, value); }
        }
    }
}