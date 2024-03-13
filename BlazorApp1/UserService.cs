namespace BlazorApp1
{
    public class UserService
    {
        private readonly Dictionary<string, string> _userConnections = new Dictionary<string, string>();

        public void Add(string connectionId, string username)
        {
            _userConnections[username] = connectionId;
        }

        public void RemoveByName(string username)
        {
            if (_userConnections.ContainsKey(username))
            {
                _userConnections.Remove(username);
            }
        }

        public string GetConnectioIdByName(string username)
        {
            return _userConnections.ContainsKey(username) ? _userConnections[username] : null;
        }

        public IEnumerable<(string ConnectionId, string Username)> GetAll()
        {
            foreach (var pair in _userConnections)
            {
                yield return (pair.Value, pair.Key);
            }
        }
    }
}
