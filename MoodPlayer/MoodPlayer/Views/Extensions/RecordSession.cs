using APIManager.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoodPlayer.Extensions
{
    public static class RecordSession
    {
        private static string SessionId = "";
        public static void SetSessionId()
        {
            bool result = UInt64.TryParse(SessionId, out ulong sessionIdInt);

            if (result)
            {
                sessionIdInt++;
                DataCollectionManager.MasterDataManager.DataRecordingManager.SessionId = sessionIdInt.ToString();
            }
            else
            {
                DataCollectionManager.MasterDataManager.DataRecordingManager.SessionId = "0";
            }
        }
        public static void GetSessionId()
        {
            var result = AccountManager.TokenValidation().Result;
            var sessionId = (result.Data[0]).Data.SessionId;
        }
    }
}
