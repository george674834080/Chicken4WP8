﻿using System.Threading.Tasks;
using Chicken4WP8.Controllers.Implementation.Base;
using Chicken4WP8.Controllers.Interface;
using Chicken4WP8.Models.Setting;
using CoreTweet;

namespace Chicken4WP8.Controllers.Implementation.Custom
{
    public class CustomOAuthController : IOAuthController
    {
        private OAuth.OAuthSession session;

        public CustomOAuthController()
        { }

        public async Task<OAuthSessionModel> AuthorizeAsync(string consumerKey, string consumerSecret)
        {
            session = await OAuth.AuthorizeAsync(consumerKey, consumerSecret);
            return new OAuthSessionModel(session.AuthorizeUri)
            {
                ConsumerKey = session.ConsumerKey,
                ConsumerSecret = session.ConsumerSecret,
                RequestToken = session.RequestToken,
                RequestTokenSecret = session.RequestTokenSecret
            };
        }

        public async Task<OAuthSetting> GetTokensAsync(string pinCode)
        {
            var tokens = await OAuth.GetTokensAsync(session, pinCode);
            return new CustomOAuthSetting
            {
                ConsumerKey = session.ConsumerKey,
                ConsumerSecret = session.ConsumerSecret,
                AccessToken = tokens.AccessToken,
                AccessTokenSecret = tokens.AccessTokenSecret
            };
        }

        public async Task<IUserModel> VerifyCredentialsAsync(OAuthSetting setting)
        {
            var oauth = setting as CustomOAuthSetting;
            var tokens = Tokens.Create(oauth.ConsumerKey, oauth.ConsumerSecret, oauth.AccessToken, oauth.AccessTokenSecret);
            var user = await tokens.Account.VerifyCredentialsAsync();
            return new UserModel(user);
        }
    }
}
