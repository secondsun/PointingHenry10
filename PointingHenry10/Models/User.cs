using Template10.Mvvm;

namespace PointingHenry10.Models
{
    public class User : ViewModelBase
    {
        private bool _showVotes;
        private string _vote;
        public string Name { get; set; }

        public bool ShowVotes
        {
            get { return _showVotes; }
            set { Set(ref _showVotes, value); }
        }

        public string Vote
        {
            get { return _vote; }
            set { Set(ref _vote, value); }
        }
    }
}