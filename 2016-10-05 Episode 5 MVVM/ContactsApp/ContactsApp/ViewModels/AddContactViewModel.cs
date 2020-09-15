using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    public class AddContactViewModel : INotifyPropertyChanged
    {

        public AddContactViewModel()
        {
            SaveContactCommand = new Command(async () => await SaveContact(),
                                            () => !IsBusy);
        }

        string name = "James Montemagno";

        string contact = "No contact currently saved";
        bool bestFriend;
        bool isBusy = false;
        bool goodName = false;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool BestFriend
        {
            get { return bestFriend; }
            set
            {
                bestFriend = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(DisplayMessage));
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
             
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayMessage));
                
            }
        }

        public string Contact
        {
            get { return contact; }
            set
            {
                contact = value;

                if (contact == "Brendan" || contact == "Dave")
                    goodName = true;
                else
                    goodName = false;

                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayContact));
                OnPropertyChanged(nameof(DisplayHiddenMessage));
            }
        }

        public string DisplayMessage
        {
            get { return $"Your new friend is named {Name} and " +
                         $"{(bestFriend ? "is" : "is not")} your best friend."; }
        }

        public string DisplayContact
        {
            get { return Name; }
        }

        public string DisplayHiddenMessage
        {
            get
            {
                if (goodName)
                    return "Wow! Your currently saved contact is an extraordinary person!";

                return "";
            }
        }


        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;

                OnPropertyChanged();
                SaveContactCommand.ChangeCanExecute();
            }
        }

        public Command SaveContactCommand { get; }

        async Task SaveContact()
        {
            IsBusy = true;
            await Task.Delay(2000);

            IsBusy = false;

            Contact = Name;

            await Application.Current.MainPage.DisplayAlert("Save", "Contact has been saved", "OK");
        }

    }
}
