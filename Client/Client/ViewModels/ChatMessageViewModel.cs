using System.Collections.ObjectModel;
using Client.Models;

namespace Client.ViewModels
{
    public class ChatMessageViewModel
    {
        public ObservableCollection<ChatMessage> Messages { get; set; } = new ObservableCollection<ChatMessage>();
    }
}
