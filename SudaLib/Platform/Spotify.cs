using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using AIGS.Common;
using AIGS.Helper;
using static SudaLib.Common;
using EmbedIO;
using EmbedIO.Actions;

namespace SudaLib
{
    public class Spotify
    {
        public class LoginKey
        {
            public string UserID { get; set; }
            public string UserName { get; set; }
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }

            // Stub implementation
            public object API(object api = null) 
            {
                return null; 
            }
        }

        #region Login

        private static string CLIENT = "ddcfe87f7ded4cec843769b882905d89";
        private static string BASE_URL = "http://localhost:5000/callback";
        private static WebServer _server;
        private static TaskCompletionSource<string> _authCodeTaskSource;

        public static async Task WorkBeforeLogin(Action<object> action)
        {
            try
            {
                // Start the local server for OAuth callback
                await StartCallbackServer();
                action?.Invoke((true, "Server started successfully"));
            }
            catch (Exception ex)
            {
                action?.Invoke((false, $"Failed to start server: {ex.Message}"));
            }
        }

        private static async Task StartCallbackServer()
        {
            if (_server != null)
            {
                try
                {
                    _server.Dispose();
                }
                catch { }
            }

            _server = new WebServer(o => o.WithUrlPrefix("http://localhost:5000/"))
                .WithLocalSessionManager()
                .WithAction("/callback", HttpVerbs.Get, HandleCallback);

            _ = _server.RunAsync();
            await Task.Delay(500); // Give server time to start
        }

        private static async Task HandleCallback(IHttpContext context)
        {
            var query = HttpUtility.ParseQueryString(context.Request.Url.Query);
            var code = query["code"];
            var error = query["error"];

            string responseHtml;
            if (!string.IsNullOrEmpty(error))
            {
                responseHtml = "<html><body><h1>Spotify Authentication Failed</h1><p>Error: " + error + "</p><p>You can close this window.</p></body></html>";
                _authCodeTaskSource?.SetException(new Exception($"Spotify authentication failed: {error}"));
            }
            else if (!string.IsNullOrEmpty(code))
            {
                responseHtml = "<html><body><h1>Spotify Authentication Successful!</h1><p>You can close this window and return to Suda.</p></body></html>";
                _authCodeTaskSource?.SetResult(code);
            }
            else
            {
                responseHtml = "<html><body><h1>Spotify Authentication Error</h1><p>No authorization code received.</p><p>You can close this window.</p></body></html>";
                _authCodeTaskSource?.SetException(new Exception("No authorization code received"));
            }

            context.Response.ContentType = "text/html";
            using (var writer = context.OpenResponseText())
            {
                await writer.WriteAsync(responseHtml);
            }
        }

        public static string GetLoginUrl()
        {
            var scopes = "user-read-private user-read-email playlist-read-private playlist-read-collaborative playlist-modify-public playlist-modify-private";
            var encodedScopes = HttpUtility.UrlEncode(scopes);
            var encodedRedirectUri = HttpUtility.UrlEncode(BASE_URL);
            
            return $"https://accounts.spotify.com/authorize?client_id={CLIENT}&response_type=code&redirect_uri={encodedRedirectUri}&scope={encodedScopes}&show_dialog=true";
        }

        public static async Task<(string,LoginKey)> GetLoginKey(string sAccessToken = null, string sRefreshToken = null, bool tryRefresh = true)
        {
            try
            {
                // If we already have tokens, try to use them
                if (!string.IsNullOrEmpty(sAccessToken))
                {
                    var loginKey = new LoginKey
                    {
                        AccessToken = sAccessToken,
                        RefreshToken = sRefreshToken
                    };
                    return ("", loginKey);
                }

                // Wait for the OAuth callback
                _authCodeTaskSource = new TaskCompletionSource<string>();
                
                // Open the login URL in the default browser
                var loginUrl = GetLoginUrl();
                NetHelper.OpenWeb(loginUrl);

                // Wait for the callback with a timeout
                var timeoutTask = Task.Delay(TimeSpan.FromMinutes(5));
                var completedTask = await Task.WhenAny(_authCodeTaskSource.Task, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    _authCodeTaskSource?.SetException(new TimeoutException("Authentication timed out"));
                    return ("Authentication timed out. Please try again.", null);
                }

                var authCode = await _authCodeTaskSource.Task;

                // Here you would normally exchange the auth code for tokens
                // For now, return a placeholder
                var key = new LoginKey
                {
                    AccessToken = "placeholder_access_token",
                    RefreshToken = "placeholder_refresh_token",
                    UserID = "user_id",
                    UserName = "Spotify User"
                };

                return ("", key);
            }
            catch (Exception ex)
            {
                return ($"Spotify authentication failed: {ex.Message}", null);
            }
            finally
            {
                // Clean up server
                try
                {
                    _server?.Dispose();
                    _server = null;
                }
                catch { }
            }
        }
        #endregion

        #region user info

        public static async Task<(string, UserInfo)> GetUserInfo(LoginKey oKey)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Spotify API not available in stub version", null);
        }

        public static async Task<(string, ObservableCollection<Playlist>)> GetUserPlaylist(LoginKey oKey)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Spotify API not available in stub version", new ObservableCollection<Playlist>());
        }

        #endregion

        #region favorite
        public static string MY_FAVORITE_PLAYLIST_ID = "@!#&^%156139FDSA#*()";
        public static string MY_FAVORITE_PLAYLIST_IMGURL = "https://i.loli.net/2020/07/28/cJvtXmIjDEfRbsq.jpg";

        public static async Task<(string, Playlist)> GetFavorite(LoginKey oKey)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Spotify API not available in stub version", null);
        }

        public static async Task<(string, bool)> AddFavoriteTracks(LoginKey oKey, string[] sID)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Spotify API not available in stub version", false);
        }

        public static async Task<(string, bool)> DelFavoriteTracks(LoginKey oKey, string[] sID)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Spotify API not available in stub version", false);
        }

        #endregion

        #region playlist

        public static async Task<(string, Playlist)> GetPlaylist(LoginKey oKey, string sID = "7516711891")
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Spotify API not available in stub version", null);
        }

        public static async Task<(string, Playlist)> CreatPlaylist(LoginKey oKey, string sPlaylistName, string sPlaylistDesc)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Spotify API not available in stub version", null);
        }

        public static async Task<(string, bool)> DeletePlaylist(LoginKey oKey, string sPlaylistDirID)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Spotify API not available in stub version", false);
        }

        public static async Task<(string, bool)> AddSongToPlaylist(LoginKey oKey, string[] sSongidArray, string sPlaylistID)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Spotify API not available in stub version", false);
        }

        public static async Task<(string, bool)> DelSongFromPlaylist(LoginKey oKey, string[] sSongidArray, string sPlaylistID)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Spotify API not available in stub version", false);
        }

        #endregion

        public static async Task<ObservableCollection<Common.Track>> Search(LoginKey oKey, string sSearchText)
        {
            // Stub implementation
            await Task.Delay(100);
            return new ObservableCollection<Common.Track>();
        }
    }
}