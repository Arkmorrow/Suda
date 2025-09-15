using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGS.Common;
using static SudaLib.Common;

namespace SudaLib
{
    public class Tidal
    {
        public class LoginKey
        {
            public string UserID { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }

            private string playlistID { get; set; }
            public string PlaylistID(string ID = null)
            {
                if (ID != null)
                    playlistID = ID;
                return playlistID;
            }

            private string etag { get; set; }
            public string ETag(string ID = null)
            {
                if (ID != null)
                    etag = ID;
                return etag;
            }

            // Stub implementations
            public object Client() { return null; }
            public object Session(object set = null) { return null; }
        }

        #region Login

        public static async Task<(string, LoginKey)> GetLoginKey(string username, string password)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Tidal API not available in stub version", null);
        }

        #endregion

        #region user info

        public static async Task<(string, UserInfo)> GetUserInfo(LoginKey oKey)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Tidal API not available in stub version", null);
        }

        public static async Task<(string, ObservableCollection<Playlist>)> GetUserPlaylist(LoginKey oKey)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Tidal API not available in stub version", new ObservableCollection<Playlist>());
        }

        #endregion

        #region favorite
        public static string MY_FAVORITE_PLAYLIST_ID = "@!#&^%156139FDSA#*()";
        public static string MY_FAVORITE_PLAYLIST_IMGURL = "https://i.loli.net/2020/07/28/cJvtXmIjDEfRbsq.jpg";

        public static async Task<(string, Playlist)> GetFavorite(LoginKey oKey)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Tidal API not available in stub version", null);
        }

        public static async Task<(string, bool)> AddFavoriteTracks(LoginKey oKey, string[] sID)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Tidal API not available in stub version", false);
        }

        public static async Task<(string, bool)> DelFavoriteTracks(LoginKey oKey, string[] sID)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Tidal API not available in stub version", false);
        }

        #endregion

        #region playlist

        public static async Task<(string, Playlist)> GetPlaylist(LoginKey oKey, string sID = "7516711891")
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Tidal API not available in stub version", null);
        }

        public static async Task<(string, Playlist)> CreatPlaylist(LoginKey oKey, string sPlaylistName, string sPlaylistDesc)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Tidal API not available in stub version", null);
        }

        public static async Task<(string, bool)> DeletePlaylist(LoginKey oKey, string sPlaylistDirID)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Tidal API not available in stub version", false);
        }

        public static async Task<(string, bool)> AddSongToPlaylist(LoginKey oKey, string[] sSongidArray, string sPlaylistID)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Tidal API not available in stub version", false);
        }

        public static async Task<(string, bool)> DelSongFromPlaylist(LoginKey oKey, string[] sSongidArray, string sPlaylistID)
        {
            // Stub implementation
            await Task.Delay(100);
            return ("Tidal API not available in stub version", false);
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