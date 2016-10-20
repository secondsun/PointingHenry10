using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FHSDK;
using FHSDK.Config;
using Newtonsoft.Json;
using PointingHenry10.Models;
using Quobject.SocketIoClientDotNet.Client;

namespace PointingHenry10.Services.PokerServices
{
    public class PokerService
    {
        public static PokerService Instance { get; } = new PokerService();
        private readonly Socket _socket;

        public event EventHandler<SessionEventArgs> SessionEvent;
        public event EventHandler<UserEventArgs> SessionUpdatedEvent;
        public event EventHandler<UserEventArgs> SessionVoteEvent;
        public event EventHandler<string> SessionFinishEvent;

        private PokerService()
        {
            _socket = IO.Socket(FHConfig.GetInstance().GetHost());
            _socket.On("sessions", data =>
            {
                var session = JsonConvert.DeserializeObject<Session>((string) data);
                SessionEvent?.Invoke(this, new SessionEventArgs {Session = session});
            });

            _socket.On("sessionUpdated", data =>
            {
                var user = JsonConvert.DeserializeObject<User>((string) data);
                SessionUpdatedEvent?.Invoke(this, new UserEventArgs {User = user});
            });

            _socket.On("voteUpdated", data =>
            {
                var user = JsonConvert.DeserializeObject<User>((string)data);
                SessionVoteEvent?.Invoke(this, new UserEventArgs { User = user });
            });

            _socket.On("finish", data =>
            {
                SessionFinishEvent?.Invoke(this, (string) data);
            });
        }

        public async Task<List<Session>> GetSessions()
        {
            var response = await FH.Cloud("poker", "GET", null, null);
            if (response.Error != null)
                throw response.Error;

            return JsonConvert.DeserializeObject<List<Session>>(response.RawResponse);
        }

        public async Task<Session> JoinSession(string sessionName, string userName)
        {
            var response = await FH.Cloud("poker/join", "POST", null, new Dictionary<string, string>() { { "session", sessionName }, { "user", userName } });
            if (response.Error != null)
                throw response.Error;

            _socket.Emit("room-join", () => { }, sessionName);

            return JsonConvert.DeserializeObject<Session>(response.RawResponse);
        }

        public async Task<Session> CreateSession(Session session)
        {
            await FH.Cloud("poker", "POST", null, session);
            return await JoinSession(session.Name, session.CreatedBy.Name);
        }

        public async void CastVote(string sessionName, User user)
        {
            await
                FH.Cloud("poker/vote", "POST", null,
                    new Dictionary<string, object> {{"session", sessionName}, {"user", user}});
        }

        public void ShowVotes(string sessionName)
        {
            _socket.Emit("showVotes", () => { }, sessionName);
        }
    }
}
